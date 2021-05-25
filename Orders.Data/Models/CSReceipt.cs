using System;

namespace Orders.Data.Models
{
	public class CSReceipt : GenericModel
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
		public string CustomerEmail { get; set; }
		public string CustomerPhone { get; set; }
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
		public string PaymentCard { get; set; }
	}
}
