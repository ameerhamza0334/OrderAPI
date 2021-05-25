using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Service.IServices;
using Orders.Service.ViewModels;
using System;
using System.Net;

namespace OrdersAPI.Controllers
{
	[Route("v1/api/order")]
	[ApiController]
	[EnableCors("_myAllowSpecificOrigins")]
	public class OrdersController : ControllerBase
	{
		private IOrdersService _orderService;

		public OrdersController(IOrdersService orderService)
		{
			_orderService = orderService;
		}

		[HttpPost]
		public ActionResult<GenericResponseModel> Post([FromBody] OrdersRequestModel ordersRequest)
		{
			IPAddress remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
			return ReturnActionResult(_orderService.AddOrders(ordersRequest, remoteIpAddress.ToString()));
		}

		[HttpGet("{id}", Name = "GetByID")]

		public ActionResult<GenericResponseModel> Get([FromRoute] string id)
		{
			return ReturnActionResult(_orderService.GetReciptByID(ParseGUID(id)));
		}

		private static Guid ParseGUID(string id)
		{
			Guid parsedID;
			Guid.TryParse(id, out parsedID);
			return parsedID;
		}

		[HttpPut("{id}", Name = "UpdateByID")]
		public ActionResult<GenericResponseModel> Put([FromRoute] string id, [FromBody] CustomerUpdateRequestModel customerUpdate)
		{
			return ReturnActionResult(_orderService.UpdateOrder(ParseGUID(id), customerUpdate));
		}
		private ActionResult<GenericResponseModel> ReturnActionResult(GenericResponseModel genericResponseModel)
		{
			if (!genericResponseModel.isError)
				return Ok(genericResponseModel);
			else
				return BadRequest(genericResponseModel);
		}

	}
}