using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Orders.Data.Models
{
	public class OrderItem : GenericModel
	{

		[ForeignKey("OrderID")]
		public virtual Order Orders { get; set; }
		[Display(Name = "Order")]
		public virtual int OrderID { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }


	}
}
