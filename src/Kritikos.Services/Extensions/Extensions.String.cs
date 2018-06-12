namespace Kritikos.Services.Extensions
{
	using System;

	public static partial class Extensions
	{
		/// <summary>
		/// Converts a Base64 encoded string to the equivalent byte array
		/// </summary>
		/// <param name="s">String to convert</param>
		/// <returns>Byte array representation</returns>
		public static byte[] ToByteArray(this string s)
			=> s == null ? new byte[0] : Convert.FromBase64String(s);
	}
}
