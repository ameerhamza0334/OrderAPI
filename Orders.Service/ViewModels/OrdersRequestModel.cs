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
		public string URL { get; set; }
		public string Transaction { get; set; }
		public string Type_Of_Sale { get; set; }
		public int PaymentMethod { get; set; }
		public string Reference { get; set; }
		public string Auth_ID { get; set; }
		public string MID { get; set; }
		public string AID { get; set; }
		public string Order { get; set; }
		public string Payment { get; set; }
		public string CardName { get; set; }
		public List<OrdersDetailRequestModel> ordersDetails { get; set; }
	}
}
