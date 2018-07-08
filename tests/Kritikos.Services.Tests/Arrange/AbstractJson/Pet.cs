namespace Kritikos.ServicesTests.Arrange.AbstractJson
{
	using Kritikos.Services.Abstractions;

	public abstract class Pet : Entity<long>
	{
		public string Name { get; set; }
	}

	public class Dog : Pet
	{
		public string FavoriteToy { get; set; }
	}

	public class Cat : Pet
	{
		public bool WantsToKillYou { get; set; } = true;
	}

	public class Aligator : Pet
	{
	}
}
