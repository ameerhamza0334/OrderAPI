namespace Orders.Service.ViewModels
{
	public class GenericResponseModel

	{
		public object Data { get; set; }
		public bool isError { get; set; }
		public string Message { get; set; }
	}
}
