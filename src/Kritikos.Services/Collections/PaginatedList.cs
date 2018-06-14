namespace Kritikos.Services.Collections
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;

	/// <inheritdoc />
	/// <summary>
	/// Represents a strongly typed list of objects that can be accessed by index. Provides methods to search, sort and manipulate lists.
	/// </summary>
	/// <remarks>
	/// Adds pagination data on top of list elements.
	/// </remarks>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	public sealed class PaginatedList<T> : List<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
		/// </summary>
		/// <param name="source">Queryable source, should contain all elements to be paginated.</param>
		/// <param name="pageSize">Number of elements per page.</param>
		/// <param name="pageNumber">Number of page to fetch from source.</param>
		/// <exception cref="ArgumentException"><paramref name="pageNumber"/> is not greater or equal to 1.</exception>
		/// <exception cref="ArgumentException"><paramref name="pageSize"/> does not allow for more than one element.</exception>
		public PaginatedList(IQueryable<T> source, int pageSize, int pageNumber)
			: this(pageSize, pageNumber)
		{
			var count = source.Count();
			var elements = source
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToList();
			AddRange(elements);
			FillPaginationInfo(pageSize, pageNumber, count);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
		/// </summary>
		/// <param name="source">Enumerable source, should contain the elements in the page specified by <paramref name="pageNumber"/>.</param>
		/// <param name="pageSize">Number of elements per page.</param>
		/// <param name="pageNumber">Number of page contained in <see cref="PaginatedList{T}" />.</param>
		/// <param name="totalElements">Total number of elements across all pages.</param>
		/// <exception cref="ArgumentException"><paramref name="source"/> contains more elements than <paramref name="pageSize"/> allows.</exception>
		/// <exception cref="ArgumentException"><paramref name="pageNumber"/> is not greater or equal to 1.</exception>
		/// <exception cref="ArgumentException"><paramref name="pageSize"/> does not allow for more than one element.</exception>
		/// <exception cref="ArgumentException"><paramref name="totalElements"/> is negative.</exception>
		/// <exception cref="ArgumentException">Trying to create a <paramref name="pageNumber"/> that can not exist with the given combination of <paramref name="totalElements"/> and <paramref name="pageSize"/>.</exception>
		public PaginatedList(IEnumerable<T> source, int pageSize, int pageNumber, int totalElements)
			: this(pageSize, pageNumber)
		{
			var elements = source.ToList();
			if (elements.Count > pageSize)
			{
				throw new ArgumentException("Source contains more elements than those allowed in a single page!");
			}

			if (totalElements < 0)
			{
				throw new ArgumentException("Total elements should be a positive integer!");
			}

			if (totalElements / (double)pageSize < pageNumber)
			{
				throw new ArgumentException("Page number outside range specified by total elements and page size!");
			}

			AddRange(elements);
			FillPaginationInfo(pageSize, pageNumber, totalElements);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
		/// </summary>
		/// <param name="pageSize">Number of elements per page.</param>
		/// <param name="pageNumber">Current number of page contained in this instance.</param>
		private PaginatedList(int pageSize, int pageNumber)
		{
			Clear();

			if (pageSize < 2)
			{
				throw new ArgumentException("Page size should allow for more than one element!");
			}

			if (pageNumber < 1)
			{
				throw new ArgumentException("Page number should be greater than 1!");
			}
		}

		/// <summary>
		/// Gets the <see cref="PagingInfo"/> for this instance of <see cref="PaginatedList{T}"/>.
		/// </summary>
		public PagingInfo Pagination { get; private set; }

		private void FillPaginationInfo(int pageSize, int pageNumber, int totalElements) =>
			Pagination = new PagingInfo
			{
				PageSize = pageSize,
				PageNumber = pageNumber,
				TotalElements = totalElements,
				NumberOfPages = totalElements > 0 ? (int)Math.Ceiling(totalElements / (double)pageSize) : 0
			};

		/// <summary>
		/// Simple class that contains paging info for paginated lists.
		/// </summary>
		[SuppressMessage("StyleCop", "SA1623:SA1623PropertySummaryDocumentationMustMatchAccessors", Justification = "Simple POCO Class")]
		public class PagingInfo
		{
			/// <summary>
			/// Number of elements per page.
			/// </summary>
			public int PageSize { get; internal set; }

			/// <summary>
			/// Current Page
			/// </summary>
			public int PageNumber { get; internal set; }

			/// <summary>
			/// Total number of Pages
			/// </summary>
			public int NumberOfPages { get; internal set; }

			/// <summary>
			/// Total elements across all pages
			/// </summary>
			public int TotalElements { get; internal set; }
		}
	}
}
