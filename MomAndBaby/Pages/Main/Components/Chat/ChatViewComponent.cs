using Microsoft.AspNetCore.Mvc;
using MomAndBaby.Service.MessageCommunication;

namespace MomAndBaby.Pages.Main.Components.Chat
{
    [ViewComponent(Name = "Chat")]
    public class ChatViewComponent : ViewComponent
    {
        private readonly IMessageService _messageService;

		public ChatViewComponent(IMessageService messageService)
		{
			_messageService = messageService;
		}

		public IViewComponentResult Invoke()
        {
            /*var messages = _messageService.GetMessages(Guid.NewGuid()).Result;

            Console.WriteLine("Messages:");
			foreach (var item in messages)
            {
                Console.WriteLine(item);
            }*/
            return View("Chat");
        }
    }
}
