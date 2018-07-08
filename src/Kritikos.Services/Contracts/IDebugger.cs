namespace Kritikos.Services.Contracts
{
	/// <summary>
	/// Generic Identifier for debug methods, used in Reflection to load debuggers dynamically
	/// </summary>
	public interface IDebugger
	{
		/// <summary>
		/// Gets a value indicating whether Debugger has been run yet or not
		/// </summary>
		bool HasRun { get; }

		/// <summary>
		/// Gets or sets a value indicating whether this Debugger should run
		/// </summary>
		bool ShouldRun { get; set; }

		/// <summary>
		/// Entry point for Debugger
		/// </summary>
		void Run();

		/// <summary>
		/// Resets this debugger to be ready for executing again
		/// </summary>
		void Reset();
	}
}
