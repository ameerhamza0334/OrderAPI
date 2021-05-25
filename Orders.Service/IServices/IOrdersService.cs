using Orders.Service.ViewModels;
using OrdersAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Service.IServices
{
	public interface IOrdersService
	{
		public GenericResponseModel AddOrders(OrdersRequestModel ordersRequestModel, string IPAddress);

		public GenericResponseModel GetReciptByID(Guid id);

		public GenericResponseModel UpdateOrder(Guid id, CustomerUpdateRequestModel updateRequestModel);
	}
}
