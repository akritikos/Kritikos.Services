namespace Kritikos.ServicesTests.Arrange.Database
{
	using System;
	using AutoMapper;
	using Kritikos.Services.Extensions;
	using Kritikos.ServicesTests.Arrange.AbstractJson;

	public static class MappingProfile
	{
		public static MapperConfiguration Config => new MapperConfiguration(cfg =>
		{
			cfg.CreateMap<CatDto, Cat>()
				.MapProperty(c => c.Id, d => d.Id)
				.MapProperty(d => DateTime.UtcNow, c => c.Updated)
				.ForMember(c => c.Updated, opt => opt.MapFrom(o => DateTime.UtcNow))
				.Ignore(c => c.Updated)
				.Ignore(c => c.WantsToKillYou)
				.ReverseMap();
		});
	}
}
