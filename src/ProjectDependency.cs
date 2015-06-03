namespace Lost.Rust.Cargo
{
	using System;

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
				this.ValidateName(name);
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
	}
}
