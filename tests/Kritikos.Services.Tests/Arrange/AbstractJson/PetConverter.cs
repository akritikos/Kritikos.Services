namespace Kritikos.ServicesTests.Arrange.AbstractJson
{
	using System;
	using Kritikos.Services.IO;
	using Newtonsoft.Json.Linq;

	public class PetConverter : AbstractJsonConverter<Pet>
	{
		/// <inheritdoc />
		protected override Pet Create(Type objectType, JObject jobject)
		{
			if (FieldExists(jobject, nameof(Dog.FavoriteToy), JTokenType.String))
			{
				return new Dog();
			}

			if (FieldExists(jobject, nameof(Cat.WantsToKillYou), JTokenType.Boolean))
			{
				return new Cat();
			}

			throw new InvalidOperationException($"{nameof(objectType)} not assignable from Pet!");
		}
	}
}
