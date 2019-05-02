using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace OnionSeed.Helpers.Async
{
	/// <summary>
	/// Contains static helpers for <see cref="Task"/>.
	/// </summary>
	[SuppressMessage("AsyncUsage.CSharp.Naming", "UseAsyncSuffix:Use Async suffix", Justification = "Test methods don't need to end in 'Async'.")]
	public static class TaskHelpers
	{
		/// <summary>
		/// Gets a task that has already completed successfully.
		/// </summary>
		public static Task CompletedTask
		{
#if NETSTANDARD1_1
			get => Task.FromResult(0);
#else
			get => Task.CompletedTask;
#endif
		}

		/// <summary>Creates a <see cref="Task"/> that has completed with a specified exception.</summary>
		/// <param name="exception">The exception with which to complete the task.</param>
		/// <returns>The faulted task.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="exception"/> is <c>null</c>.</exception>
		public static Task FromException(Exception exception)
		{
			return FromException<int>(exception);
		}

		/// <summary>Creates a <see cref="Task{TResult}"/> that's completed with a specified exception.</summary>
		/// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
		/// <param name="exception">The exception with which to complete the task.</param>
		/// <returns>The faulted task.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="exception"/> is <c>null</c>.</exception>
		public static Task<TResult> FromException<TResult>(Exception exception)
		{
#if NETSTANDARD1_1
			var source = new TaskCompletionSource<TResult>();
			source.SetException(exception);
			return source.Task;
#else
			return Task.FromException<TResult>(exception);
#endif
		}
	}
}
