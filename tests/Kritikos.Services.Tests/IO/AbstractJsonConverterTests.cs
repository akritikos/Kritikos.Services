namespace Kritikos.ServicesTests.IO
{
	using System;
	using Kritikos.ServicesTests.Arrange.AbstractJson;
	using Newtonsoft.Json;
	using Xunit;

	public class AbstractJsonConverterTests
	{
		private readonly PetConverter _converter = new PetConverter();

		[Fact]
		public void PetConverter()
		{
			var original = new Pet[]
			{
				new Cat { Name = "Pepe" },
				new Cat { Name = "Bling" },
				new Dog { Name = "Taboo", FavoriteToy = "Sql" }
			};

			var json = JsonConvert.SerializeObject(original);

			var deserialized = JsonConvert.DeserializeObject<Pet[]>(json, _converter);

			Assert.Equal(original.Length, deserialized.Length);

			for (var i = 0; i < original.Length; i++)
			{
				Assert.Equal(original[i].GetType(), deserialized[i].GetType());
				Assert.Equal(original[i].Name, deserialized[i].Name);
			}
		}

		[Fact]
		public void UnlistedPetConversion()
		{
			Pet exceptionPet = new Aligator { Name = "Jack the Ripper" };
			var serialize = JsonConvert.SerializeObject(exceptionPet);
			var exception =
				Record.Exception(() => exceptionPet = JsonConvert.DeserializeObject<Pet>(serialize, _converter));
			Assert.IsType<InvalidOperationException>(exception);
		}
	}
}
