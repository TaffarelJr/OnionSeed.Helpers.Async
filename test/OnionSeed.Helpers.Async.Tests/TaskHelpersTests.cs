using System;
using FluentAssertions;
using Xunit;

namespace OnionSeed.Helpers.Async
{
	public class TaskHelpersTests
	{
		[Fact]
		public void CompletedTask_ShouldReturnCompletedTask()
		{
			// Act
			var result = TaskHelpers.CompletedTask;

			// Assert
			result.Should().NotBeNull();
			result.IsCanceled.Should().BeFalse();
			result.IsCompleted.Should().BeTrue();
			result.IsFaulted.Should().BeFalse();
		}

		[Fact]
		public void FromException_ShouldThrowException_WhenReturnTypeIsNotGiven_AndExceptionIsNull()
		{
			// Act
			Action action = () => TaskHelpers.FromException(null);

			// Assert
			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void FromException_ShouldReturnFaultedTask_WhenReturnTypeIsNotGiven()
		{
			// Arrange
			var exception = new InvalidOperationException("huh");

			// Act
			var result = TaskHelpers.FromException(exception);

			// Assert
			result.Should().NotBeNull();
			result.IsCanceled.Should().BeFalse();
			result.IsCompleted.Should().BeTrue();
			result.IsFaulted.Should().BeTrue();
			result.Exception.Should().BeOfType<AggregateException>();

			var aggregate = (AggregateException)result.Exception;
			aggregate.InnerExceptions.Count.Should().Be(1);
			aggregate.InnerException.Should().BeSameAs(exception);
		}

		[Fact]
		public void FromException_ShouldThrowException_WhenReturnTypeIsGiven_AndExceptionIsNull()
		{
			// Act
			Action action = () => TaskHelpers.FromException<string>(null);

			// Assert
			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void FromException_ShouldReturnFaultedTask_WhenReturnTypeIsGiven()
		{
			// Arrange
			var exception = new InvalidOperationException("huh");

			// Act
			var result = TaskHelpers.FromException<string>(exception);

			// Assert
			result.Should().NotBeNull();
			result.IsCanceled.Should().BeFalse();
			result.IsCompleted.Should().BeTrue();
			result.IsFaulted.Should().BeTrue();
			result.Exception.Should().BeOfType<AggregateException>();

			var aggregate = (AggregateException)result.Exception;
			aggregate.InnerExceptions.Count.Should().Be(1);
			aggregate.InnerException.Should().BeSameAs(exception);
		}
	}
}
