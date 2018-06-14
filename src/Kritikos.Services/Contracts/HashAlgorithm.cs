namespace Kritikos.Services.Contracts
{
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// Provides HashAlgorithm names
	/// </summary>
	[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Cryptographic algorithm naming")]
	[SuppressMessage("ReSharper", "StyleCop.SA1602", Justification = "Algorithm names are the description")]
	public enum HashAlgorithm
	{
		MD5 = 0,
		SHA1 = 1,
		SHA256 = 2,
		SHA384 = 3,
		SHA512 = 4,
	}
}
