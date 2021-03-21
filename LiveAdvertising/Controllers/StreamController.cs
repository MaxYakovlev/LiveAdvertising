using LiveAdvertising.Models;
using LiveAdvertising.Models.Dto;
using Entities = LiveAdvertising.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LiveAdvertising.Controllers
{
    [Route("[controller]")]
    public class StreamController : Controller
    {
        private readonly ApplicationContext context;

        public StreamController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StreamDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProductsFile.ContentType != "text/xml")
                    ModelState.AddModelError("", "Файл должен быть формата xml");
                else if (!model.Source.Contains("https://www.youtube.com/watch?v="))
                    ModelState.AddModelError("", "Некорректная ссылка на ютуб трансляцию");
                else
                {
                    Entities.Shop shop = await context.Shops.Where(x => x.Name == User.Identity.Name).FirstOrDefaultAsync();

                    using StreamReader streamReader = new StreamReader(model.ProductsFile.OpenReadStream());

                    string products = await streamReader.ReadToEndAsync();

                    Entities.Stream stream = new Entities.Stream();

                    stream.Shop = shop;
                    stream.ShopId = shop.Id;
                    stream.isActive = true;
                    stream.Products = products;
                    stream.Title = model.Title;
                    stream.Source = model.Source;

                    await context.Streams.AddAsync(stream);

                    await context.SaveChangesAsync();

                    return Redirect($"/stream/watch/{stream.Id}");
                }
            }

            return View(model);
        }

        [Authorize]
        [Route("mystreams")]
        [HttpGet]
        public async Task<IActionResult> MyStreams()
        {
            List<Entities.Stream> streams = await context.Streams
                .Where(x => x.Shop.Name == User.Identity.Name)
                .Include(x => x.Shop)
                .ToListAsync();

            return View(streams);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Watch()
        {
           int id = int.Parse(HttpContext.Request.Path.ToString().Split("/")[3]);

            Entities.Stream stream = await context.Streams
                .Where(x => x.Id == id)
                .Include(x => x.Messages)
                .Include(x => x.Shop)
                .FirstOrDefaultAsync();

            if(stream == null || (stream != null && !stream.isActive))
                return Redirect("/");

            StreamInfoDto streamInfo = new StreamInfoDto();
            streamInfo.Source = stream.Source;
            streamInfo.Products = stream.Products;
            streamInfo.ShopName = stream.Shop.Name;
            streamInfo.Messages = stream.Messages;
            streamInfo.StreamTitle = stream.Title;

            return View(streamInfo);
        }
    }
}
