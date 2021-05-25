using Orders.Data.Models;
using OrdersAPI.Models;
using System;

namespace OrdersAPI.Extensions
{
	public static class ExtentionMethods
	{
		public static bool isValid(this OrdersRequestModel requestModel)
		{
			bool _modelValidated = true;

			if (string.IsNullOrEmpty(requestModel.MerchantID))
			{
				_modelValidated = false;
			}
			if (requestModel.ordersDetails is null || requestModel.ordersDetails.Count.Equals(0))
			{
				_modelValidated = false;
			}
			if (requestModel.OrderNo == 0)
			{
				_modelValidated = false;
			}
			if (string.IsNullOrEmpty(requestModel.CompanyName))
			{
				_modelValidated = false;
			}
			if (string.IsNullOrEmpty(requestModel.Address))
			{
				_modelValidated = false;
			}
			requestModel.ordersDetails.ForEach(item =>
			{
				if (string.IsNullOrEmpty(item.Name))
				{
					_modelValidated = false;
				}
				if (item.Price.Equals(0))
				{
					_modelValidated = false;
				}
				if (item.Quantity.Equals(0))
				{
					_modelValidated = false;
				}
			});
			return _modelValidated;
		}

		public static CSReceipt toEntity(this OrdersRequestModel requestModel, string IPAddress)
		{
			CSReceipt order = new CSReceipt
			{
				Address = requestModel.Address,
				IpAddress = IPAddress,
				PhoneNumber = requestModel.PhoneNumber,
				MerchantID = requestModel.MerchantID,
				OrderNo = requestModel.OrderNo,
				CompanyName = requestModel.CompanyName,
				CreatedDate = DateTime.Now,
				Discount = requestModel.OverallDiscount,
				Tax = requestModel.Tax,
				URL = requestModel.URL,
				AID = requestModel.AID,
				MID = requestModel.MID,
				Type_Of_Sale = requestModel.Type_Of_Sale,
				PaymentMethod = requestModel.PaymentMethod,
				Auth_ID = requestModel.Auth_ID,
				Transaction = requestModel.Transaction,
				PaymentCard = requestModel.CardName,
				Order = requestModel.Order,
				Payment = requestModel.Payment,
				Reference = requestModel.Reference
			};
			return order;
		}
		public static CSReceiptItem toEntity(this OrdersDetailRequestModel ItemModel, string IPAddress, Guid OrderID)
		{

			CSReceiptItem orderItem = new CSReceiptItem
			{
				OrderID = OrderID,
				Quantity = ItemModel.Quantity,
				Name = ItemModel.Name,
				Price = ItemModel.Price,
			};
			return orderItem;
		}
	}
}
