using MomAndBaby.BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndBaby.BusinessObject.Models.ReviewDTO
{
	public class ReviewDTO
	{
		public int? Id {  get; set; }
		public Guid? UserId { get; set; }
		public string? UserFullName { get; set; }
		public Guid? ProductId { get; set; }
		public string? ProductName { get; set; }
		public int? Rating { get; set; }
		public string? Comment { get; set; }
		public bool? Status { get; set; }
		public DateTime? CreatedAt { get; set; }
	}
}
