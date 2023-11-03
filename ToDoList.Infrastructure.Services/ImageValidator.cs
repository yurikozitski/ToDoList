using Microsoft.AspNetCore.Http;
using ToDoList.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Infrastructure.Services
{
	public class ImageValidator : IImageValidator
	{
		public bool IsImageValid(IFormFile picture)
		{
			FileInfo fileInfo = new FileInfo(picture.FileName);
			string fileExtension = fileInfo.Extension;
			bool IsFileExtensionValid = fileExtension == ".jpg" || fileExtension == ".jpeg"||fileExtension==".png";
			if (picture.Length > 2097152 || !IsFileExtensionValid)
				return false;
			return true;
		}
	}
}
