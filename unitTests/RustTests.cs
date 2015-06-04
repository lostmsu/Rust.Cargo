namespace Lost.Rust.Cargo.UnitTests
{
	using System;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class RustTests
	{
		[TestMethod]
		public void ValidateIdentifier_NoThrowOnValid()
		{
			Rust.ValidateIdentifier("HelloWorld_42");
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void ValidateIdentifier_ThrowOnFirstDigit()
		{
			Rust.ValidateIdentifier("42_HelloWorld");
		}

		[TestMethod]
		[ExpectedException(typeof(FormatException))]
		public void ValidateIdentifier_ThrowOnUnrecognizedCharacter()
		{
			Rust.ValidateIdentifier("Hello-World!");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ValidateIdentifier_ThrowOnNull()
		{
			Rust.ValidateIdentifier(null);
		}
	}
}
