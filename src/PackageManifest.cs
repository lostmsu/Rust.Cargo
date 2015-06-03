namespace Lost.Rust.Cargo
{
	using System;
	using System.Collections.Generic;

	public class PackageManifest
	{
		public PackageManifest()
		{
			this.Authors = new List<MailAddress>();
		}

		string packageName;

		public string Name
		{
			get { return this.packageName; }
			set
			{
				// TODO: validate
				this.packageName = value;
			}
		}
		public Version Version { get; set; }
		public IList<MailAddress> Authors { get; private set; }
		// TODO: validation?
		public string Build { get; set; }
	}
}
