namespace Kritikos.Services.Abstractions
{
	using AutoMapper;
	using Kritikos.Services.Contracts;

	public abstract class ViewModel<TKey> : IViewModel<TKey>
	{
		public TKey Id { get; set; }

		public static TModel FromEntity<TModel, TEntity>(TEntity entity, IMapper mapper)
			where TModel : IViewModel<TKey>
			where TEntity : IEntity<TKey>
		{
			return mapper.Map<TModel>(entity);
		}

		public TEntity ToEntity<TEntity>(IMapper mapper)
			where TEntity : Entity<TKey>
		{
			return mapper.Map<TEntity>(this);
		}
	}
}
