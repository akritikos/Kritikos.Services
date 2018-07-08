namespace Kritikos.Services.Contracts
{
    public interface IViewModel<TKey>
    {
		TKey Id { get; set; }
    }
}
