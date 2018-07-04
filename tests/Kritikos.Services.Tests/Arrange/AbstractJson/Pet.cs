namespace Kritikos.ServicesTests.Arrange.AbstractJson
{
	public abstract class Pet
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
}
