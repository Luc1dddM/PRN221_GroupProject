"use strict";

//Creates and starts a connection
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

//Handle the text send
connection.on("ReceiveMessage", function (user, message, connectionId) {
    const messageList = document.getElementById("messagesList");
    const currentTime = new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    let messageHtml = '';

    const isSender = connection.connectionId === connectionId;

    if (isSender) {
        messageHtml = `<div class="d-flex justify-content-between">
                            <p class="small mb-1 text-muted">${currentTime}</p>
                            <p class="small mb-1">${user}</p>
                        </div>
                        <div class="d-flex flex-row justify-content-end mb-4 pt-1">
                            <div>
                                <p class="small p-2 me-3 mb-3 text-white rounded-3 bg-primary">
                                    ${message}
                                </p>
                            </div>
                            <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava6-bg.webp"
                                 alt="avatar 1" style="width: 45px; height: 100%;">
                        </div>`;
    } else {
        messageHtml = `<div class="d-flex justify-content-between">
                            <p class="small mb-1">${user}</p>
                            <p class="small mb-1 text-muted">${currentTime}</p>
                        </div>
                        <div class="d-flex flex-row justify-content-start">
                            <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava5-bg.webp"
                                 alt="avatar 1" style="width: 45px; height: 100%;">
                            <div>
                                <p class="small p-2 ms-3 mb-3 rounded-3" style="background-color: #f0f0f0">
                                    ${message}
                                </p>
                            </div>
                        </div>`;
    }

    //put the html code for sender and receiver inside the div block with id="messagesList"
    messageList.innerHTML += messageHtml;
    messageList.scrollTop = messageList.scrollHeight; // Scroll to the bottom
});

//If connection was established, send button will enable
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//Invoke the SendMessage() in ChatHub.cs
document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});