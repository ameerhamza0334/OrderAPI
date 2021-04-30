using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Orders.Data.Models
{
	public class Order : GenericModel
	{
		public decimal Discount { get; set; }
		public decimal Tax { get; set; }
		public int OrderNo { get; set; }
		public string CompanyName { get; set; }
		public string Address { get; set; }
		public string MerchantID { get; set; }
		public DateTime CreatedDate { get; set; }
		public string IpAddress { get; set; }
		public string PhoneNumber { get; set; }
	}
}
