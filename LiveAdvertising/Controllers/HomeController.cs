using LiveAdvertising.Models;
using LiveAdvertising.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveAdvertising.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext context;

        public HomeController(ApplicationContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Stream> streams = await context.Streams
                .Where(x => x.isActive == true)
                .Include(x => x.Shop)
                .ToListAsync();

            return View(streams);
        }
    }
}
