namespace Lost.Rust.Cargo.UnitTests
{
	using System;
	using System.Linq;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public sealed class PackageManifestTests
	{
		[TestMethod]
		public void CanLoadRustManifest()
		{
			var package = new PackageManifest();
			package.Load(Samples.CargoPackageManifest);
			Assert.AreEqual("cargo", package.Name);
			Assert.AreEqual(new Version(0, 3, 0), package.Version);
			var expectedAuthors = new[] {
				new MailAddress("wycats@gmail.com", "Yehuda Katz"),
				new MailAddress("me@carllerche.com", "Carl Lerche"),
				new MailAddress("alex@alexcrichton.com", "Alex Crichton"),
			};
			CollectionAssert.AreEqual(expectedAuthors, package.Authors.ToArray());
			Assert.IsNotNull(package.Dependencies);
		}
	}
}
