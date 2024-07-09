// Create display message
connection.on("SendMessageToUser", function (result) {
  let message = result;
  console.log(message);
  let display_message = document.getElementById("display-message");
  if (thisUserId === message.userId) {
    display_message.innerHTML += renderMyMessage(
      message.content,
      message.createdDate
    );
  } else {
    display_message.innerHTML += renderYourMessage(
      message.content,
      message.createdDate
    );
  }

  scrollToEndOfPage();
});

// Create display group
connection.on("DisplayGroup", function (groupId, groupName) {
  renderNewGroup(groupId, groupName);
});

// Load message for group
connection.on("LoadMessage", function (listMessage) {
    console.log(listMessage);
  // Load message to UI
  loadMessage(listMessage);
});

const loadMessage = (list) => {
  // Load message to display
  let display_message = document.getElementById("display-message");
  display_message.innerHTML = "";

  try {
    Array.from(list).forEach((element) => {
      if (thisUserId === element.userId) {
        display_message.innerHTML += renderMyMessage(
          element.content,
          element.createdDate
        );
      } else {
        display_message.innerHTML += renderYourMessage(
          element.content,
          element.createdDate
        );
      }
    });

    scrollToEndOfPage();
  } catch (e) {
    console.error(e.toString());
  }
};

// Handle send message to group
document.getElementById("groupmsg").addEventListener("click", async (event) => {
  let groupName = document.getElementById("group-name");
  let groupMsg = document.getElementById("group-message-text");

  try {
    await connection.invoke("ChatAsysnc", thisUserId, groupMsg.value);
    groupMsg.value = "";
  } catch (e) {
    console.error(e.toString());
  }
  event.preventDefault();
});