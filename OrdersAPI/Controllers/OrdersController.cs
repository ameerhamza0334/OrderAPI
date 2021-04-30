using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Orders.Service.IServices;
using Orders.Service.ViewModels;
using System.Net;

namespace OrdersAPI.Controllers
{
	[Route("v1/api/order")]
	[ApiController]
	[EnableCors("_myAllowSpecificOrigins")]
	public class OrdersController : ControllerBase
	{
		private IOrdersService _orderService;
#if DEBUG

		private string BaseURL = "https://localhost:5001/v1/api/order";
#else
				
		private string BaseURL = "https://coorder.azurewebsites.net/v1/api/order";
#endif

		public OrdersController(IOrdersService orderService, IConfiguration configuration)
		{
			_orderService = orderService;
		}

		[HttpPost]
		public ActionResult Post([FromBody] OrdersRequestModel ordersRequest)
		{
			IPAddress remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
			(bool isValid, int OrderID) ordersResponse = _orderService.AddOrders(ordersRequest, remoteIpAddress.ToString());
			if (ordersResponse.isValid)
				return Ok($"{BaseURL}/{ordersResponse.OrderID}");
			else
				return BadRequest();
		}

		[HttpGet("{id:int}", Name = "GetByID")]
 
		public ActionResult Get([FromRoute] int id)
		{
			(bool IsValid, FileResponseModel FileResponse) ReceiptResponse = _orderService.GetReciptByID(id);
			if (ReceiptResponse.IsValid)
				return new PhysicalFileResult(ReceiptResponse.FileResponse.FileName, "application/pdf");
			else
				return BadRequest();
		}
	}
}