namespace Kritikos.ServicesTests.Arrange.Database
{
	using Kritikos.Services.Abstractions;

	public class CatDto : ViewModel<long>
	{
		public string Name { get; set; }
	}
}
