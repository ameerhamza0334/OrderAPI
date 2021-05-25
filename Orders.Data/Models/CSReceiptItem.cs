using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Orders.Data.Models
{
	public class CSReceiptItem : GenericModel
	{

		[ForeignKey("OrderID")]
		public virtual CSReceipt Orders { get; set; }
		[Display(Name = "Order")]
		public virtual Guid OrderID { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }

	}
}
