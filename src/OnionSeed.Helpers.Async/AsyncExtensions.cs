using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace OnionSeed.Helpers.Async
{
	/// <summary>
	/// Contains extension methods for asynchronous operations.
	/// </summary>
	[SuppressMessage("Reliability", "CA2008:Do not create tasks without passing a TaskScheduler", Justification = "The scheduler is defined statically.")]
	public static class AsyncExtensions
	{
		private static readonly TaskFactory SyncTaskFactory = new TaskFactory(
			CancellationToken.None,
			TaskCreationOptions.None,
			TaskContinuationOptions.None,
			TaskScheduler.Default);

		/// <summary>
		/// Runs the given async method synchronously on the default <see cref="TaskScheduler"/>.
		/// </summary>
		/// <param name="method">The async method to be run.</param>
		public static void RunSynchronously(this Func<System.Threading.Tasks.Task> method) => SyncTaskFactory
			.StartNew(method)
			.Unwrap()
			.GetAwaiter()
			.GetResult();

		/// <summary>
		/// Runs the given async method synchronously on the default <see cref="TaskScheduler"/>
		/// and returns the result.
		/// </summary>
		/// <typeparam name="TResult">The type of the return value of the async method.</typeparam>
		/// <param name="method">The async method to be run.</param>
		/// <returns>The result of executing the given async function.</returns>
		public static TResult RunSynchronously<TResult>(this Func<Task<TResult>> method) => SyncTaskFactory
			.StartNew(method)
			.Unwrap()
			.GetAwaiter()
			.GetResult();
	}
}
