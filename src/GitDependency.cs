namespace Lost.Rust.Cargo
{
	using System;

	public sealed class GitDependency : ProjectDependency
	{
		public Uri GitUri { get; set; }

		protected override void ValidateVersion(Version version) {}
	}
}
