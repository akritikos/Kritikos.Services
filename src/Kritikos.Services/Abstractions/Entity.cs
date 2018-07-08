namespace Kritikos.Services.Abstractions
{
	using System;
	using System.ComponentModel;
	using System.Runtime.CompilerServices;
	using AutoMapper;
	using Kritikos.Services.Annotations;
	using Kritikos.Services.Contracts;

	public abstract class Entity<TKey> : IEntity<TKey>, IDatabaseAuditable, INotifyPropertyChanged
	{
		protected Entity()
		{
			Created = DateTime.UtcNow;
			Updated = Updated > Created ? Updated : Created;
		}

		public event PropertyChangedEventHandler PropertyChanged;

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

		[NotifyPropertyChangedInvocator]
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			Updated = DateTime.UtcNow;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(storage, value))
			{
				return false;
			}

			storage = value;
			OnPropertyChanged(propertyName);
			return true;
		}
	}
}
