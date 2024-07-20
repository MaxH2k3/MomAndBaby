using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MomAndBaby.BusinessObject.Entity
{
	public partial class Review
	{
		public int Id { get; set; }

		public Guid? UserId { get; set; }

		public Guid? ProductId { get; set; }

		[Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
		public int Rating { get; set; }

		[Required(ErrorMessage = "Comment is required.")]
		[StringLength(1500, ErrorMessage = "Comment cannot be longer than 1500 characters.")]
		public string? Comment { get; set; }

		public DateTime? CreatedAt { get; set; }

		public bool? Status { get; set; }

		public virtual Product? Product { get; set; }

		public virtual User? User { get; set; }
	}
}
