namespace Lost.Rust.Cargo.UnitTests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class RustTests
	{
		[TestMethod]
		public void ValidateIdentifier_NoThrowOnValid()
		{
			Lost.Rust.Cargo.Rust.ValidateIdentifier("HelloWorld_42");
		}
	}
}
