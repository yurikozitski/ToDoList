using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Models;
using ToDoList.Core.RepositoryInterfaces;
using ToDoList.Infrastructure.Mediator.Queries;

namespace ToDoList.Tests.QueriesTests
{
	public class GetAllUserTasksTest
	{
		[Fact]
		public void GetAllUserTasksDoesNotReturnNull()
		{
			//Arrange
			var taskRepositoryMock=new Mock<ITaskRepository>();
			taskRepositoryMock.Setup(repo => repo.GetAllUserTasksAsync("mockmail@gmail.com")).Returns(GetAllUserTasksList);
			var getAllUserTasksHandler = new GetAllUserTasksHandler(taskRepositoryMock.Object);

			//Act
			var result = getAllUserTasksHandler.Handle;

			//Assert
			Assert.NotNull(result(new GetAllUserTasksQuery() {UserEmail= "mockmail@gmail.com" },default).Result);

		}

		private Task<IEnumerable<UserTask>> GetAllUserTasksList()
		{
			return Task.FromResult(new List<UserTask>
			{
				new UserTask() { Text = "mock1", TaskList = new TaskList() },
				new UserTask() { Text = "mock2", TaskList = new TaskList() },
				new UserTask() { Text = "mock3", TaskList = new TaskList() }
			}.AsEnumerable());
		}
	}
}
