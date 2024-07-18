const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

(async () => {
    try {
        await connection.start();
        InvokeNotifications();
    } catch (e) {
        console.log(e);
    }
})();

const InvokeNotifications = async () => {
    try {
        await connection.invoke("SendNotification");
    } catch (error) {
        console.log(error)
    }
}

connection.on("ReceivedNotificaiton", (messages) => {
    $('#notification-list').empty();
    messages.forEach(message => {
        renerNotification(message);
    })
})


const renerNotification = (message) => {
    let tag = `
        <li class="list-group-item list-group-item-action dropdown-notifications-item ${message.isRead ? 'marked-as-read' : ''}">
                                <input type="hidden" value="${message.id}" />
                                <div class="d-flex">
                                    <div class="flex-shrink-0 me-3">
                                        <div class="avatar">
                                            <img src="/ok/assets/img/avatars/9.png" alt class="w-px-40 h-auto rounded-circle">
                                        </div>
                                    </div>
                                    <div class="flex-grow-1">
                                        <h6 class="mb-1">${message.title}</h6>
                                        <p class="mb-0">${message.message}</p>
                                        <small class="text-muted">${message.createdAt}</small>
                                    </div>
                                    <div class="flex-shrink-0 dropdown-notifications-actions">
                                        <a href="javascript:void(0)" class="dropdown-notifications-read"><span class="badge badge-dot"></span></a>
                                        <a href="javascript:void(0)" class="dropdown-notifications-archive"><span class="bx bx-x"></span></a>
                                    </div>
                                </div>
                            </li>
    `;

    $('#notification-list').append(tag);

}
