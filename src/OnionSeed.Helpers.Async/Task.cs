#if NET452
using System;
using System.Diagnostics.CodeAnalysis;

namespace OnionSeed.Helpers.Async
{
	/// <summary>
	/// Contains static helpers for <see cref="System.Threading.Tasks.Task"/>.
	/// </summary>
	[SuppressMessage("AsyncUsage.CSharp.Naming", "UseAsyncSuffix:Use Async suffix", Justification = "Test methods don't need to end in 'Async'.")]
	public static class Task
	{
		/// <summary>
		/// Gets a task that has already completed successfully.
		/// </summary>
		/// <returns>The successfully completed task.</returns>
		public static System.Threading.Tasks.Task CompletedTask => System.Threading.Tasks.Task.FromResult(0);

		/// <summary>Creates a <see cref="System.Threading.Tasks.Task"/> that has completed with a specified exception.</summary>
		/// <param name="exception">The exception with which to complete the task.</param>
		/// <returns>The faulted task.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="exception"/> is <c>null</c>.</exception>
		public static System.Threading.Tasks.Task FromException(Exception exception)
		{
			return FromException<int>(exception);
		}

		/// <summary>Creates a <see cref="System.Threading.Tasks.Task{TResult}"/> that's completed with a specified exception.</summary>
		/// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
		/// <param name="exception">The exception with which to complete the task.</param>
		/// <returns>The faulted task.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="exception"/> is <c>null</c>.</exception>
		public static System.Threading.Tasks.Task<TResult> FromException<TResult>(Exception exception)
		{
			var source = new System.Threading.Tasks.TaskCompletionSource<TResult>();
			source.SetException(exception);
			return source.Task;
		}
	}
}
#endif
