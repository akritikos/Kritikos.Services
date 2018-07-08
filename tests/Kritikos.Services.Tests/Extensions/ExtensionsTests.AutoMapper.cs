namespace Kritikos.ServicesTests.Extensions
{
	using System.Collections.Generic;
	using AutoMapper;
	using Kritikos.Services.Abstractions;
	using Kritikos.ServicesTests.Arrange.AbstractJson;
	using Kritikos.ServicesTests.Arrange.Database;
	using Xunit;

	public partial class ExtensionsTests
	{
		private readonly Mapper _mapper = new Mapper(MappingProfile.Config);

		private readonly List<Entity<long>> _entities = new List<Entity<long>>
		{
			new Cat { Id = 1, Name = "Pepe", WantsToKillYou = true },
			new Cat { Id = 2, Name = "Willy", WantsToKillYou = false },
			new Cat { Id = 3, Name = "Jack", WantsToKillYou = true }
		};

		private readonly List<ViewModel<long>> _models = new List<ViewModel<long>>();

		[Fact]
		public void MapExplicit()
		{
			foreach (var t in _entities)
			{
				var entity = (Cat)t;
				var model = entity.ToModel<CatDto>(_mapper);
				_models.Add(model);
				Assert.Equal(entity.Id, model.Id);
				Assert.Equal(entity.Name, model.Name);
			}

			for (var i = 0; i < _models.Count; i++)
			{
				var model = (CatDto)_models[i];
				var entity = Cat.FromModel<Cat, CatDto>(model, _mapper);
				Assert.Equal(_entities[i].Id, entity.Id);
				Assert.Equal(((Cat)_entities[i]).Name, entity.Name);
			}

			var exception = Record.Exception(() => ((CatDto)_models[0]).ToEntity<Dog>(_mapper));
			Assert.IsType<AutoMapperConfigurationException>(exception);
		}
	}
}
