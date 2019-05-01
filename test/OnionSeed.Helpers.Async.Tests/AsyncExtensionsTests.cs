using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace OnionSeed.Helpers.Async
{
	public class AsyncExtensionsTests
	{
		[Fact]
		public void RunSynchronously_ShouldExecuteAsyncMethod()
		{
			// Arrange
			var called = false;

			Func<System.Threading.Tasks.Task> subject = () =>
			{
				called = true;
				return System.Threading.Tasks.Task.Delay(30);
			};

			// Act
			subject.RunSynchronously();

			// Assert
			called.Should().BeTrue();
		}

		[Fact]
		public void RunSynchronously_ShouldExecuteAsyncMethod_AndReturnResult()
		{
			// Arrange
			const int expected = 21;

			Func<Task<int>> subject = () =>
			{
				return System.Threading.Tasks.Task.Delay(30)
					.ContinueWith(t => expected, TaskScheduler.Current);
			};

			// Act
			var result = subject.RunSynchronously();

			// Assert
			result.Should().Be(expected);
		}
	}
}
