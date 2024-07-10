$(() => {
    LoadMessageData();
    //Creates and starts a connection
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    connection.start();
    console.log(connection);


    //Handle the text send
    connection.on("LoadMessage", function () {
        LoadMessageData();
    });

    function LoadMessageData() {
        var messageList = '';
        var groupNameValue = $("#room").val();
        var currentUser = $("#currentUser").val();
        console.log(currentUser);
        $.ajax({
            url: '/Admin/Chat/ChatPage?handler=Message',
            method: 'GET',
            data: {
                "groupName": groupNameValue
            },
            dataType: "json",
            contentType: 'application/x-www-form-urlencoded',
            success: function (result) {
                result.forEach(function (v) {
                    if (v.senderId == currentUser) {
                        messageList += `<div class="d-flex flex-row justify-content-end">
                                        <div>
                                            <p class="small p-2 me-3 mb-1 text-white rounded-3 bg-primary">
                                                ${v.messageContent}
                                            </p>
                                            <div class="d-flex">
                                                <p class="small me-3 mb-3 rounded-3 text-muted flex-grow-1">${v.sendDate}</p>
                                                <p class="small me-3 mb-3 rounded-3 text-muted ms-auto">receiver</p>
                                            </div>
                                        </div>
                                        <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava6-bg.webp"
                                             alt="avatar 1" style="width: 45px; height: 100%;">
                                    </div>`;
                    }
                    else {
                        messageList += `<div class="d-flex flex-row justify-content-start">
                                        <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava2-bg.webp"
                                             alt="avatar 1" style="width: 45px; height: 100%; ">
                                        <div>
                                            <p class="small p-2 ms-3 mb-1 rounded-3 bg-body-tertiary" style="background-color:#f0f0f0">
                                                ${v.messageContent}
                                            </p>

                                            <div class="d-flex">
                                                <p class="small ms-3 mb-3 rounded-3 text-muted flex-grow-1">sender</p>
                                                <p class="small ms-3 mb-3 rounded-3 text-muted ms-auto">${v.sendDate}</p>
                                            </div>

                                        </div>
                                    </div>`;
                    }
                    /*tr += `<tr>
                            <td>${v.ToyId}</td>
                            <td>${v.ToyName}</td>
                            <td>${v.Description}</td>
                            <td>${v.Age}</td>
                            <td>${v.Price}</td>
                            <td>${v.Category.CategoryName}</td>
                            <td>
                                <a href='../Toys/Delete?id=${v.ToyId}'>
                                    <button class="btn btn-danger">
                                        Delete
                                    </button>
                                </a>
                            </td>
                           </tr>`;*/
                });
                $('#messagesList').html(messageList);
                console.log(result)
            },

            error: function (error) {
                console.log(error);
            }
        });
    }
});