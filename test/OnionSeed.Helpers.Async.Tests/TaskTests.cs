#if NET452
using System;
using FluentAssertions;
using Xunit;

namespace OnionSeed.Helpers.Async
{
	public class TaskTests
	{
		[Fact]
		public void CompletedTask_ShouldReturnCompletedTask()
		{
			// Act
			var result = Task.CompletedTask;

			// Assert
			result.Should().NotBeNull();
			result.IsCanceled.Should().BeFalse();
			result.IsCompleted.Should().BeTrue();
			result.IsFaulted.Should().BeFalse();
		}

		[Fact]
		public void FromException_ShouldThrowException_WhenExceptionIsNull()
		{
			// Act
			Action action = () => Task.FromException(null);

			// Assert
			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void FromException_ShouldReturnFaultedTask_WhenExceptionIsGiven()
		{
			// Arrange
			var expected = new InvalidOperationException("huh");

			// Act
			var result = Task.FromException(expected);

			// Assert
			result.Should().NotBeNull();
			result.IsCanceled.Should().BeFalse();
			result.IsCompleted.Should().BeTrue();
			result.IsFaulted.Should().BeTrue();
			result.Exception.Should().NotBeNull();
			result.Exception.InnerExceptions.Count.Should().Be(1);
			result.Exception.InnerException.Should().BeSameAs(expected);
		}

		[Fact]
		public void FromException_ShouldThrowException_WhenReturnTypeIsGiven_AndExceptionIsNull()
		{
			// Act
			Action action = () => Task.FromException<string>(null);

			// Assert
			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void FromException_ShouldReturnFaultedTask_WhenReturnTypeIsGiven_AndExceptionIsGiven()
		{
			// Arrange
			var expected = new InvalidOperationException("huh");

			// Act
			var result = Task.FromException<string>(expected);

			// Assert
			result.Should().NotBeNull();
			result.IsCanceled.Should().BeFalse();
			result.IsCompleted.Should().BeTrue();
			result.IsFaulted.Should().BeTrue();
			result.Exception.Should().NotBeNull();
			result.Exception.InnerExceptions.Count.Should().Be(1);
			result.Exception.InnerException.Should().BeSameAs(expected);
		}
	}
}
#endif
