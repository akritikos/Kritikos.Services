using Kritikos.Services.Helpers.Collections;

namespace Kritikos.Services.Tests.Kritikos.Services.Collections
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Xunit;

	public class PaginatedListTests
	{
		private readonly IEnumerable<string> data = new List<string>
		{
			"Nessos",
			"Information",
			"Technologies",
			"Alexandros",
			"Kritikos"
		};

		[Fact]
		public void PaginatedListConstructorTests()
		{
			var pagedQueryable = new PaginatedList<string>(data.AsQueryable(), 2, 1);
			var pagedList = new PaginatedList<string>(data.Take(2), 2, 1, data.Count());
			Assert.True(pagedList.SequenceEqual(pagedQueryable));
		}
	}
}
