$(() => {
    LoadMessageNotificationData();
    LoadAdminNotificationData();
    //Creates and starts a connection
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    connection.start();
    console.log(connection);

    //Handle the text send
    connection.on("LoadMessageNotification", function () {
        LoadMessageNotificationData();
    });

    connection.on("LoadForAdminNotification", function () {
        LoadAdminNotificationData();
    });

    function LoadMessageNotificationData() {
        var currentUser = $("#applicationUser").val()
        $.ajax({
            url: '/Index?handler=MessageNotification',
            method: 'GET',
            data: {
                "receiver": currentUser
            },
            dataType: "json",
            contentType: 'application/x-www-form-urlencoded',
            success: function (result) {
                console.log(result)
                document.getElementById("messageCount").innerHTML = result;
            },

            error: function (error) {
                console.log(error);
            }
        });
    }


    function LoadAdminNotificationData() {
        var messageList = '';
        var currentUser = $("#applicationUser").val()
        $.ajax({
            url: '/Admin/Chat/ChatPage?handler=AdminNotification',
            method: 'GET',
            data: {
                "receiver": currentUser
            },
            dataType: "json",
            contentType: 'application/x-www-form-urlencoded',
            success: function (result) {
                result.forEach(function (v) {
                    messageList += `<li class="p-2 border-bottom">
                                                    <a href='../Chat/ChatPage?id=${v.users.id}' class="d-flex justify-content-between text-decoration-none">
                                                        <div class="d-flex flex-row">
                                                            <div>
                                                                <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava2-bg.webp"
                                                                     alt="avatar" class="d-flex align-self-center me-3" width="60">
                                                                <span class="badge bg-warning badge-dot"></span>
                                                            </div>
                                                            <div class="d-flex align-items-center">
                                                                <p class="fs-4 fw-bold m-0">${v.users.name}</p>
                                                            </div>
                                                        </div>
                                                        <div class="d-flex align-items-center">
                                                            <span class="badge bg-danger rounded-pill float-end">${v.notification}</span>
                                                        </div>
                                                    </a>
                                                </li>`;

                });
                $('#listUserChatbox').html(messageList);
            },

            error: function (error) {
                console.log(error);
            }
        });
    }

});