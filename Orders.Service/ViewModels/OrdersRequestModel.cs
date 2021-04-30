using OrdersAPI.Models;
using System.Collections.Generic;

namespace OrdersAPI
{
	public class OrdersRequestModel
	{
		public int OrderNo { get; set; }
		public string CompanyName { get; set; }
		public string Address { get; set; }
		public string MerchantID { get; set; }
		public string PhoneNumber { get; set; }
		public decimal OverallDiscount { get; set; }
		public decimal Tax { get; set; }
		public List<OrdersDetailRequestModel> ordersDetails { get; set; }

	}
}
