using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Models;

namespace ToDoList.Core.AuthInterfaces
{
	public interface IJwtGenerator
	{
		string CreateToken(User user);
	}
}
