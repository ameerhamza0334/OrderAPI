using System;
using System.ComponentModel.DataAnnotations;

namespace Orders.Data.Models
{
	public class GenericModel
	{
		[Key]
		public Guid ID { get; set; }

	}
}
