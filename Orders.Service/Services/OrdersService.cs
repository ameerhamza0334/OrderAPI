using Orders.Data;
using Orders.Data.Models;
using Orders.Service.IServices;
using Orders.Service.ViewModels;
using OrdersAPI;
using OrdersAPI.Extensions;
using OrdersAPI.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Transactions;

namespace Orders.Service.Services
{
	public class OrdersService : IOrdersService
	{
#if DEBUG

		private string BaseURL = "http://127.0.0.1:5500/ReceiptTemplate.html?id=";
#else
				
		private string BaseURL = "https://coorder.azurewebsites.net/v1/api/order";
#endif

		private OrderDbContext _dbContext = new OrderDbContext();
		public OrdersService()
		{

		}

		public GenericResponseModel AddOrders(OrdersRequestModel ordersRequestModel, string? IPAddress)
		{
			GenericResponseModel genericResponseModel = new GenericResponseModel();
			Guid orderID = Guid.Empty;

			using (TransactionScope ts = new TransactionScope())
			{
				try
				{
					if (ordersRequestModel.isValid())
					{

						CSReceipt order = ordersRequestModel.toEntity(IPAddress);
						_dbContext.Orders.Add(order);
						_dbContext.SaveChanges();
						_dbContext.OrderItems.AddRange(ordersRequestModel.ordersDetails.Select(x => x.toEntity(IPAddress, order.ID)));
						_dbContext.SaveChanges();
						ts.Complete();
						orderID = order.ID;
						genericResponseModel.isError = false;
						genericResponseModel.Data = $"{BaseURL}{orderID}";
					}
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex.Message);
					genericResponseModel.isError = true;
					orderID = Guid.Empty;
					genericResponseModel.Message = ex.Message;

				}
				ts.Dispose();
			}
			return genericResponseModel;
		}

		public GenericResponseModel GetReciptByID(Guid id)
		{
			GenericResponseModel returnModel = new GenericResponseModel();
			try
			{
				OrdersResponseModel orders = (from order in _dbContext.Orders
											  join items in _dbContext.OrderItems on order.ID equals items.OrderID
											  where order.ID.Equals(id)
											  select new OrdersResponseModel()
											  {
												  Address = order.Address,
												  CompanyName = order.CompanyName,
												  MerchantID = order.MerchantID,
												  PhoneNumber = order.PhoneNumber,
												  OrderNo = order.OrderNo,
												  AID = order.AID,
												  Auth_ID = order.Auth_ID,
												  MID = order.MID,
												  Type_Of_Sale = order.Type_Of_Sale,
												  URL = order.URL,
												  Reference = order.Reference,
												  ordersDetails = _dbContext.OrderItems.Where(x => x.OrderID == id).Select(x => new OrdersDetailRequestModel()
												  {
													  Name = x.Name,
													  Price = x.Price,
													  Quantity = x.Quantity,
												  }).ToList(),
												  CreatedDate = order.CreatedDate.ToString(),
												  PaymentMethod = order.PaymentMethod,
												  Transaction = order.Transaction,
												  Order = order.Order,
												  Payment = order.Payment,
												  Tax = order.Tax,
												  CardName = order.PaymentCard
											  }).FirstOrDefault();
				orders.Total = orders.ordersDetails.Select(x => x.Price).Sum();
				returnModel.isError = false;
				returnModel.Data = orders;
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.Message);
				returnModel.Message = ex.Message;
				returnModel.isError = true;
				returnModel.Data = null;
			}
			return returnModel;
		}

		public GenericResponseModel UpdateOrder(Guid id, CustomerUpdateRequestModel updateRequestModel)
		{
			GenericResponseModel returnModel = new GenericResponseModel();
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					CSReceipt order = _dbContext.Orders.FirstOrDefault(x => x.ID.Equals(id));
					order.CustomerEmail = updateRequestModel.CustomerEmail;
					order.CustomerPhone = updateRequestModel.CustomerPhone;
					_dbContext.Update(order);
					_dbContext.SaveChanges();
					scope.Complete();
					scope.Dispose();
					returnModel.isError = false;
					returnModel.Message = "Receipt Updated Successfully";
				}
			}
			catch (Exception ex)
			{
				returnModel.isError = true;
				returnModel.Message = ex.Message;
			}
			return returnModel;
		}
	}
}