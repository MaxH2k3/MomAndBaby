using MomAndBaby.Entity;

namespace MomAndBaby.Models.MessageModel
{
    public class MessageDTO
    {
		public Guid? SenderId { get; set; }
		public Guid? ReceiverId { get; set; }
		public string? Content { get; set; }
		public DateTime? CreatedAt { get; set; }
		public bool IsSystem { get; set; }
	}
}
