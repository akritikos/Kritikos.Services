#pragma warning disable SA1200 // Using directives should be placed correctly
using Kritikos.Services.Helpers.Collections;
#pragma warning restore SA1200 // Using directives should be placed correctly

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

		[Fact]
		public void InvalidPageNumberTest()
		{
			var exception = Record.Exception(() => new PaginatedList<string>(data, 2, 0, data.Count()));
			Assert.NotNull(exception);
			Assert.IsType<ArgumentException>(exception);
			Assert.Equal("Page number should be greater than 1!", exception.Message);
		}

		[Fact]
		public void InvalidPageSizeTest()
		{
			var exception = Record.Exception(() => new PaginatedList<string>(data.AsQueryable(), 1, 1));
			Assert.NotNull(exception);
			Assert.IsType<ArgumentException>(exception);
			Assert.Equal("Page size should allow for more than one element!", exception.Message);
		}

		[Fact]
		public void TooManyElementsTest()
		{
			var exception = Record.Exception(() => new PaginatedList<string>(data, 2, 1, data.Count()));
			Assert.NotNull(exception);
			Assert.IsType<ArgumentException>(exception);
			Assert.Equal("Source contains more elements than those allowed in a single page!", exception.Message);
		}

		[Fact]
		public void NoTotalElementsTest()
		{
			var exception = Record.Exception(() => new PaginatedList<string>(data, 5, 1, -1));
			Assert.NotNull(exception);
			Assert.IsType<ArgumentException>(exception);
			Assert.Equal("Total elements should be a positive integer!", exception.Message);
		}

		[Fact]
		public void NonExistingPageNumberTest()
		{
			var exception = Record.Exception(() => new PaginatedList<string>(data, 5, 2, 5));
			Assert.NotNull(exception);
			Assert.IsType<ArgumentException>(exception);
			Assert.Equal("Page number outside range specified by total elements and page size!", exception.Message);
		}
	}
}
