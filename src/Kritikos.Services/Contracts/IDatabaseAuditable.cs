namespace Kritikos.Services.Contracts
{
	using System;

	public interface IDatabaseAuditable
	{
		DateTime Updated { get; }
	}
}
