namespace Lost.Rust.Cargo
{
	using System;

	public sealed class LocalDependency: ProjectDependency
	{
		// TODO: validate?
		public string RelativePath { get; set; }

		protected override void ValidateVersion(Version version){}
	}
}
