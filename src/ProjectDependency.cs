namespace Lost.Rust.Cargo
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using HyperTomlProcessor;

	public abstract class ProjectDependency
	{
		internal ProjectDependency() {}

		string name;
		Version version;

		public string Name
		{
			get { return this.name; }
			set
			{
				this.ValidateName(value);
				this.name = value;
			}
		}

		public Version Version
		{
			get { return this.version; }
			set
			{
				this.ValidateVersion(value);
				this.version = value;
			}
		}

		protected abstract void ValidateVersion(Version version);

		protected virtual void ValidateName(string name)
		{
			Rust.ValidateIdentifier(name);
		}

		internal static ProjectDependency Parse(string name, object info)
		{
			if (name == null)
				throw new ArgumentNullException("name");
			if (info == null)
				throw new ArgumentNullException("info");

			var versionString = info as string;
			if (versionString != null)
				return new CrateDependency {Name = name, Version = new Version(versionString)};

			var obj = info as IEnumerable<KeyValue>;
			if (obj == null)
				throw new ArgumentException("Can't recognize type " + info.GetType(), "info");

			if (obj.Any(kv => kv.Key == "path")) {
				var localPath = obj.SingleKey<string>("path");
				return new LocalDependency {Name = name, RelativePath = localPath};
			}

			return new GitDependency{Name = name};
		}
	}
}
