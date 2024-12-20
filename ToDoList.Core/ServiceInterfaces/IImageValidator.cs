using Microsoft.AspNetCore.Http;

namespace ToDoList.Core.ServiceInterfaces
{
    public interface IImageValidator
	{
		public bool IsImageValid(IFormFile picture);
	}
}
