﻿window.onload = function () {
	var streamObject = JSON.parse(localStorage.getItem("streamObject"));
	if (streamObject == null) {
		streamObject = {
			productIds: []
		}
	}

	let addToBasketButtons = document.querySelectorAll('.btn-add-to-basket');
	for (let i = 0; i < addToBasketButtons.length; i++) {
		let productId = addToBasketButtons[i].parentElement.parentElement.parentElement.parentElement.getAttribute('id');
		if (streamObject.productIds.includes(productId))
			switchButtonStatus(addToBasketButtons[i]);

		addToBasketButtons[i].addEventListener("click", () => {
			let productId = addToBasketButtons[i].parentElement.parentElement.parentElement.parentElement.getAttribute('id');
			if (addToBasketButtons[i].classList.contains("btn-primary") && !streamObject.productIds.includes(productId))
			{
				streamObject.productIds.push(productId);
				switchButtonStatus(addToBasketButtons[i]);
            }
			else if (productIds.includes(productId))
			{
				streamObject.productIds.splice(streamObject.productIds.indexOf(productId), 1);
				switchButtonStatus(addToBasketButtons[i]);
            }
			localStorage.setItem("streamObject", JSON.stringify(streamObject));
		});
	}

	let buyButton = document.querySelector('.buy-button');
	buyButton.addEventListener("click", () => {
		let idsString = "";
		for (let i = 0; i < streamObject.productIds.length; i++) {
			idsString += streamObject.productIds[i] + ",";
		}
		idsString = idsString.slice(0, -1);
		let url = "https://hackathon.oggettoweb.com/checkout/cart/addmultiple/products/" + idsString + "/flush_cart/1";
		var win = window.open(url, '_blank');
		win.focus();
	});
};

function switchButtonStatus(button) {
	if (button.classList.contains("btn-primary")) {
		button.classList.remove('btn-primary');
		button.classList.add('btn-warning');
		button.innerHTML = "<i class='fa fa-shopping-cart'></i> Удалить из корзины";
	}
	else {
		button.classList.remove('btn-warning');
		button.classList.add('btn-primary');
		button.innerHTML = "<i class='fa fa-shopping-cart'></i> Добавить в корзину";
    }
}