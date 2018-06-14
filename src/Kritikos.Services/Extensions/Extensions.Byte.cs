namespace Kritikos.Services.Extensions
{
	using System;

	public static partial class Extensions
	{
		/// <summary>
		/// Calculates the amount of characters that a byte array would require in Base64 encoding
		/// </summary>
		/// <param name="array">Array to calculate for</param>
		/// <returns>Number of chars in a Base64 encoding</returns>
		public static int GetBase64Length(this byte[] array)
		{
			// Base64 length is equal to 4/3 of the original byte size
			// padded to multiples of four
			return 4 * (int)Math.Ceiling(array.Length / 3.0);
		}

		/// <summary>
		/// Encodes a byte array to a  Base64 string
		/// </summary>
		/// <param name="array">The array to encode</param>
		/// <returns>Base64 representation</returns>
		public static string ToBase64String(this byte[] array)
			=> Convert.ToBase64String(array);
	}
}
