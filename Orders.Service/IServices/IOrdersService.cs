using Orders.Service.ViewModels;
using OrdersAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Service.IServices
{
	public interface IOrdersService
	{
		public (bool, int) AddOrders(OrdersRequestModel ordersRequestModel, string? IPAddress);

		public (bool, FileResponseModel) GetReciptByID(int id);
	}
}
