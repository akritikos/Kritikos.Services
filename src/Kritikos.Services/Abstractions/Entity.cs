namespace Kritikos.Services.Abstractions
{
	using System;
	using System.ComponentModel;
	using AutoMapper;
	using Kritikos.Services.Contracts;

	public abstract class Entity<TKey> : IEntity<TKey>, IDatabaseAuditable
	{
		protected Entity()
		{
			Created = DateTime.UtcNow;
			Updated = Updated > Created ? Updated : Created;
		}

		public TKey Id { get; set; }

		public DateTime Created { get; set; }

		public DateTime Updated { get; protected set; }

		public static TEntity FromModel<TEntity, TModel>(TModel model, IMapper mapper)
			where TEntity : Entity<TKey>
			where TModel : ViewModel<TKey>
		{
			return mapper.Map<TEntity>(model);
		}

		public TModel ToModel<TModel>(IMapper mapper)
			where TModel : ViewModel<TKey>
		{
			return mapper.Map<TModel>(this);
		}
	}
}
