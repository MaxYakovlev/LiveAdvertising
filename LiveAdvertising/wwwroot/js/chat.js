let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
let id = location.pathname.split("/")[3];
let userSendButton = document.getElementById("user-send-button");
let shopSendButton = document.getElementById("shop-send-button");
let chatMessages = document.getElementById("chat-messages");
let closeButton = document.getElementById("close");
let copyLinkButton = document.getElementById("copy-link");

chatMessages.scrollTop = chatMessages.scrollHeight;

copyLinkButton.addEventListener("click", () => {
    navigator.clipboard.writeText(location.href);
});

if (closeButton !== null) {
    closeButton.addEventListener("click", () => {
        connection.invoke("CloseStream", id);
        location.href = "/";
    });
}

connection.on("CloseStream", () => {
    location.href = "/";
});

if (shopSendButton !== null) {
    shopSendButton.addEventListener("click", () => {
        let message = document.getElementById("message").value.trim();

        if (message !== "" || message !== undefined || message !== null) {
            connection.invoke("ShopSendMessage", id, message);

            let now = new Date();

            let chatMessage = document.createElement("div");
            chatMessage.innerHTML = "<div class=\"chat-message-right mb-4\">" +
                "<div><img src=\"/images/default_avatar.jpg\" class=\"rounded-circle mr-1\" width=\"40\" height=\"40\">" +
                "<div class=\"text-muted small text-nowrap mt-2\">" + now.getHours() + ":" + now.getMinutes() + "</div></div><div class=\"bg-light rounded py-2 px-3 ml-3 chat-message-text-right\" style=\"margin-right: 25px;\">" +
                "<div class=\"font-weight-bold mb-1\" style=\"color: red;\">Магазин</div>" + message +
                "</div></div>";

            chatMessages.appendChild(chatMessage);

            chatMessages.scrollTop = chatMessages.scrollHeight;
        }
    });
}

if (userSendButton !== null) {
    userSendButton.addEventListener("click", () => {
        let message = document.getElementById("message").value.trim();

        if (message !== "" || message !== undefined || message !== null) {
            connection.invoke("UserSendMessage", id, message);

            let now = new Date();
            let hours = now.getHours();
            let minutes = now.getMinutes();

            if (hours >= 0 && hours < 10) hours = "0" + hours;
            if (minutes >= 0 && minutes < 10) minutes = "0" + minutes;

            let chatMessage = document.createElement("div");
            chatMessage.innerHTML = "<div class=\"chat-message-right mb-4\">" +
                "<div><img src=\"/images/default_avatar.jpg\" class=\"rounded-circle mr-1\" width=\"40\" height=\"40\">" +
                "<div class=\"text-muted small text-nowrap mt-2\">" + hours + ":" + minutes + "</div></div><div class=\"bg-light rounded py-2 px-3 ml-3 chat-message-text-right\" style=\"margin-right: 25px;\">" +
                "<div class=\"font-weight-bold mb-1\">Вы</div>" + message +
                "</div></div>";

            chatMessages.appendChild(chatMessage);

            chatMessages.scrollTop = chatMessages.scrollHeight;
        }
    });
}

connection.start().then(() => {
    connection.invoke("AddToGroup", id);
});

connection.on("ShopSendMessage", (message, hourMinutes) => {
    let chatMessage = document.createElement("div");
    chatMessage.innerHTML = "<div class=\"chat-message-left mb-4\">" +
        "<div><img src=\"/images/default_avatar.jpg\" class=\"rounded-circle mr-1\" width=\"40\" height=\"40\">" +
        "<div class=\"text-muted small text-nowrap mt-2\">" + hourMinutes + "</div></div><div class=\"bg-light rounded py-2 px-3 ml-3\">" +
        "<div class=\"font-weight-bold mb-1\" style=\"color: red;\">Магазин</div>" + message +
        "</div></div>";

    chatMessages.appendChild(chatMessage);

    chatMessages.scrollTop = chatMessages.scrollHeight;
});

connection.on("UserSendMessage", (message, hourMinutes) => {
    let chatMessage = document.createElement("div");
    chatMessage.innerHTML = "<div class=\"chat-message-left mb-4\">" +
        "<div><img src=\"/images/default_avatar.jpg\" class=\"rounded-circle mr-1\" width=\"40\" height=\"40\">" +
        "<div class=\"text-muted small text-nowrap mt-2\">" + hourMinutes + "</div></div><div class=\"bg-light rounded py-2 px-3 ml-3\">" +
        "<div class=\"font-weight-bold mb-1\">Зритель</div>" + message +
        "</div></div>";

    chatMessages.appendChild(chatMessage);

    chatMessages.scrollTop = chatMessages.scrollHeight;
});