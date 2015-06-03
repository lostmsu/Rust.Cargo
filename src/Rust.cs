namespace Lost.Rust.Cargo
{
	using System;
	using System.Text.RegularExpressions;

	internal static class Rust
	{
		static readonly Regex identifierRegex = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$",
			RegexOptions.Singleline | RegexOptions.CultureInvariant);
		public static void ValidateIdentifier(string identifier)
		{
			if (identifier == null)
				throw new ArgumentNullException("identifier");
			if (!identifierRegex.IsMatch(identifier))
				throw new FormatException(identifier + " is not a valid Rust identifier");
		}
	}
}
