namespace Lost.Rust.Cargo
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using HyperTomlProcessor;

	public class PackageManifest
	{
		TableTree document;

		public PackageManifest()
		{
			this.document = new TableTree(new string[0], new TableNode[0]);
		}

		private TableTree Project
		{
			get { return this.GetOrCreateSection("project"); }
		}

		private TableTree Deps
		{
			get { return this.GetOrCreateSection("dependencies"); }
		}

		private TableTree GetOrCreateSection(string name)
		{
			var section = this.document.Children[name];
			if (section == null) {
				section = new TableTree(new []{name}, new TableNode[0]);
				this.document.Children.Add(name, section);
			}
			return section;
		}

		string packageName;

		public string Name
		{
			get
			{
				return this.Project.SingleKey<string>("name");
			}
			set
			{
				// TODO: validate
				this.Project.SetSingleKey("name", value);
			}
		}

		public Version Version
		{
			get
			{
				var versionString = this.Project.SingleKey<string>("version");
				return new Version(versionString);
			}
			set
			{
				this.Project.SetSingleKey("version", value == null? (string)null
					: string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}",
						value.Major, value.Minor, value.Build));
			}
		}

		public IEnumerable<MailAddress> Authors
		{
			get
			{
				var authors = this.Project.SingleKey<IEnumerable<ArrayItem>>("authors");

				return authors.Where(item => item.Value != null)
					.Select(item => item.Value.Value)
					.Cast<string>()
					.Select(MailAddress.Parse);
			}
		}

		public IDictionary<string, ProjectDependency> Dependencies
		{
			get
			{
				return this.Deps.Nodes.OfType<KeyValue>()
					.Select(kv => ProjectDependency.Parse(kv.Key, kv.Value.Value))
					.ToDictionary(dep => dep.Name);
			}
		}

		// TODO: validation?
		public string Build { get; set; }

		public void Load(TextReader source)
		{
			this.document = Toml.V04.Deserialize(source);
		}

		public void Load(IEnumerable<char> source)
		{
			this.document = Toml.V04.Deserialize(source);
		}
	}
}
