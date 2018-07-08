namespace Kritikos.Services.Contracts
{
	using System.ComponentModel.DataAnnotations;

	public interface IEntity<TKey> : IDatabaseRecord
	{
		[Key]
		TKey Id { get; set; }
	}
}
