using Orders.Data;
using Orders.Data.Models;
using Orders.Service.Helpers;
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
		private OrderDbContext _dbContext = new OrderDbContext();
		public OrdersService()
		{

		}

		public (bool, int) AddOrders(OrdersRequestModel ordersRequestModel, string? IPAddress)
		{
			bool isValidResult = false;
			int orderID = 0;

			using (TransactionScope ts = new TransactionScope())
			{
				try
				{
					if (ordersRequestModel.isValid())
					{

						Order order = ordersRequestModel.toEntity(IPAddress);
						_dbContext.Orders.Add(order);
						_dbContext.SaveChanges();
						_dbContext.OrderItems.AddRange(ordersRequestModel.ordersDetails.Select(x => x.toEntity(IPAddress, order.ID)));
						_dbContext.SaveChanges();
						ts.Complete();
						orderID = order.ID;
						isValidResult = true;
					}
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex.Message);
					isValidResult = false;
					orderID = 0;
				}
				ts.Dispose();
			}
			return (isValidResult, orderID);
		}

		public (bool, FileResponseModel) GetReciptByID(int id)
		{
			try
			{
				OrdersRequestModel orders = (from order in _dbContext.Orders
											 join items in _dbContext.OrderItems on order.ID equals items.OrderID
											 where order.ID.Equals(id)
											 select new OrdersRequestModel()
											 {
												 Address = order.Address,
												 CompanyName = order.CompanyName,
												 MerchantID = order.MerchantID,
												 PhoneNumber = order.PhoneNumber,
												 OrderNo = order.OrderNo,
												 ordersDetails = _dbContext.OrderItems.Where(x => x.OrderID == id).Select(x => new OrdersDetailRequestModel()
												 {
													 Name = x.Name,
													 Price = x.Price,
													 Quantity = x.Quantity,
												 }).ToList()
											 }).FirstOrDefault();

				return (true, new PdfHelper().CreateFileFromModel(orders));
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex.Message);
				return (false, null);
			}
		}
	}
}