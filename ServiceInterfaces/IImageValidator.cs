using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ToDoList.Core.ServiceInterfaces
{
	public interface IImageValidator
	{
		public bool IsImageValid(IFormFile picture);
	}
}
