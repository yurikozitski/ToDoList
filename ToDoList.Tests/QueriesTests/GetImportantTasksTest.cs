using ToDoList.Core.Models;
using ToDoList.Core.RepositoryInterfaces;
using ToDoList.Infrastructure.Mediator.Queries;

namespace ToDoList.Tests.QueriesTests
{
    public class GetImportantTasksTest
	{
		[Fact]
		public void GetImportantTasksDoesNotReturnNull()
		{
			//Arrange
			var taskRepositoryMock = new Mock<ITaskRepository>();
			taskRepositoryMock.Setup(repo => repo.GetImportantTasksAsync("mockmail@gmail.com")).Returns(GetImportantUserTasksList);
			var getImportantUserTasksHandler = new GetImportantTasksHandler(taskRepositoryMock.Object);

			//Act
			var result = getImportantUserTasksHandler.Handle;

			//Assert
			Assert.NotNull(result(new GetImportantTasksQuery() { UserEmail = "mockmail@gmail.com" }, default).Result);

		}

		[Fact]
		public void GetOnlyImportantTasks()
		{
			//Arrange
			var taskRepositoryMock = new Mock<ITaskRepository>();
			taskRepositoryMock.Setup(repo => repo.GetImportantTasksAsync("mockmail@gmail.com")).Returns(GetImportantUserTasksList);
			var getImportantUserTasksHandler = new GetImportantTasksHandler(taskRepositoryMock.Object);

			//Act
			var result = getImportantUserTasksHandler.Handle;

			//Assert
			Assert.True(result(new GetImportantTasksQuery() { UserEmail = "mockmail@gmail.com" }, default).Result.All(userTask=>userTask.IsImportant));

		}

		private Task<IEnumerable<UserTask>> GetImportantUserTasksList()
		{
			return Task.FromResult(new List<UserTask>
			{
				new UserTask() { Text = "mock1", IsImportant=true, TaskList = new TaskList() },
				new UserTask() { Text = "mock2", IsImportant=true, TaskList = new TaskList() },
				new UserTask() { Text = "mock3", IsImportant=true, TaskList = new TaskList() }
			}.AsEnumerable());
		}
	}
}
