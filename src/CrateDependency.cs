namespace Lost.Rust.Cargo
{
	using System;

	public sealed class CrateDependency : ProjectDependency
	{
		protected override void ValidateVersion(Version version)
		{
			if (version == null)
				throw new ArgumentNullException("version");
		}
	}
}
