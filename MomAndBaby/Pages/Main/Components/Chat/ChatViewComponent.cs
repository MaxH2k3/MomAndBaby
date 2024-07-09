using Microsoft.AspNetCore.Mvc;
using MomAndBaby.Service.Extension;
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
            // if(!User.Identity!.IsAuthenticated)
            // {
            //     return View("Chat");
            // }
            //
            // var userId = Guid.Parse(User.GetUserIdFromToken());
            //
            // var messages = _messageService.GetMessages(userId);
            //
            // Console.WriteLine("Messages:");
            // foreach (var item in messages)
            // {
            //     Console.WriteLine(item);
            // }
            return View("Chat");
        }
    }
}
