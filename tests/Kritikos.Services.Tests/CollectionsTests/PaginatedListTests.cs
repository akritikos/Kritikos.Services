namespace Kritikos.ServicesTests.CollectionsTests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Kritikos.Services.Collections;
	using Xunit;

	public class PaginatedListTests
	{
		private readonly IEnumerable<string> _data = new List<string>
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
			var pagedQueryable = new PaginatedList<string>(_data.AsQueryable(), 2, 1);
			var pagedList = new PaginatedList<string>(_data.Take(2), 2, 1, _data.Count());
			Assert.True(pagedList.SequenceEqual(pagedQueryable));
		}

		[Fact]
		public void InvalidPageNumberTest()
		{
			var exception = Record.Exception(() => new PaginatedList<string>(_data, 2, 0, _data.Count()));
			Assert.NotNull(exception);
			Assert.IsType<ArgumentException>(exception);
			Assert.Equal("Page number should be greater than 1!", exception.Message);
		}

		[Fact]
		public void InvalidPageSizeTest()
		{
			var exception = Record.Exception(() => new PaginatedList<string>(_data.AsQueryable(), 1, 1));
			Assert.NotNull(exception);
			Assert.IsType<ArgumentException>(exception);
			Assert.Equal("Page size should allow for more than one element!", exception.Message);
		}

		[Fact]
		public void TooManyElementsTest()
		{
			var exception = Record.Exception(() => new PaginatedList<string>(_data, 2, 1, _data.Count()));
			Assert.NotNull(exception);
			Assert.IsType<ArgumentException>(exception);
			Assert.Equal("Source contains more elements than those allowed in a single page!", exception.Message);
		}

		[Fact]
		public void NoTotalElementsTest()
		{
			var exception = Record.Exception(() => new PaginatedList<string>(_data, 5, 1, -1));
			Assert.NotNull(exception);
			Assert.IsType<ArgumentException>(exception);
			Assert.Equal("Total elements should be a positive integer!", exception.Message);
		}

		[Fact]
		public void NonExistingPageNumberTest()
		{
			var exception = Record.Exception(() => new PaginatedList<string>(_data, 5, 2, 5));
			Assert.NotNull(exception);
			Assert.IsType<ArgumentException>(exception);
			Assert.Equal("Page number outside range specified by total elements and page size!", exception.Message);
		}
	}
}
