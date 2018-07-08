namespace Kritikos.ServicesTests.Extensions
{
	using System;
	using Kritikos.Services.Extensions;
	using Xunit;

	/// <summary>
	/// Contains unit tests for Helper extensions
	/// </summary>
	public partial class ExtensionsTests
	{
		private readonly Random _rnd = new Random();
		private readonly byte[][] _buffArray;

		public ExtensionsTests()
		{
			var size = _rnd.Next(5, 30);
			_buffArray = new[]
			{
				new byte[size],
				new byte[size + 1],
				new byte[size + 2],
				new byte[size + 3],
			};
			foreach (var buff in _buffArray)
			{
				_rnd.NextBytes(buff);
			}
		}

		/// <summary>
		/// Tests a precalculated conversion of byte array to Base64 string plus random data byte[]->string->byte[] conversions
		/// </summary>
		[Fact]
		public void ToByteArrayTest()
		{
			var preCalculated = new byte[] { 237, 10, 228, 81, 72 };
			Assert.Equal("7QrkUUg=", preCalculated.ToBase64String());
			foreach (var buff in _buffArray)
			{
				Assert.Equal(buff, buff.ToBase64String().ToByteArray());
			}
		}

		/// <summary>
		/// Tests if byte array conversion to Base64 string results in the expected number of characters
		/// </summary>
		[Fact]
		public void ByteLengthTests()
		{
			foreach (var buff in _buffArray)
			{
				var s = buff.ToBase64String();
				Assert.Equal(buff.GetBase64Length(), s.Length);
			}
		}

		[Fact]
		public void FringeCaseTests()
		{
			var buff = new byte[0];
			Assert.Equal(string.Empty, buff.ToBase64String());
			string str = null;
			Assert.Equal(buff, str.ToByteArray());
		}
	}
}
