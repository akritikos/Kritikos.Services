namespace Kritikos.ServicesTests.IO
{
	using Kritikos.ServicesTests.Arrange.AbstractJson;
	using Newtonsoft.Json;
	using Xunit;

	public class AbstractJsonConverterTests
	{
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

			var deserialized = JsonConvert.DeserializeObject<Pet[]>(json, new PetConverter());

			Assert.Equal(original.Length, deserialized.Length);

			for (var i = 0; i < original.Length; i++)
			{
				Assert.Equal(original[i].GetType(), deserialized[i].GetType());
				Assert.Equal(original[i].Name, deserialized[i].Name);
			}
		}
	}
}
