namespace Lost.Rust.Cargo
{
	using System;
	using System.Text.RegularExpressions;

	public class MailAddress
	{
		public MailAddress(string address, string displayName = null)
		{
			var match = GetAddressMatch(address);
			if (match.Index != 0 || match.Length != address.Length)
				throw new FormatException(address + " is not a valid email address");

			this.Address = address;
			this.DisplayName = displayName;
		}
		public string Address { get; private set; }
		public string DisplayName { get; private set; }

		public override string ToString()
		{
			return this.DisplayName != null
				? this.DisplayName + " <" + this.Address + ">"
				: this.Address;
		}

		static readonly Regex mailRegex = new Regex(
			@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
			RegexOptions.IgnoreCase);
		public static MailAddress Parse(string address)
		{
			var addressMatch = GetAddressMatch(address);
			
			if (addressMatch.Index == 0)
				return new MailAddress(address);

			var tail = address.Substring(addressMatch.Index + addressMatch.Length).Trim();
			if (tail != "" && tail != ">")
				throw new FormatException(address + " is not a valid email address");

			int nameEnd = address.LastIndexOf('<', addressMatch.Index, addressMatch.Index - 1);
			if (nameEnd < 0)
				throw new FormatException( address + " is not a valid email address");
			if (nameEnd == 0)
				return new MailAddress(addressMatch.Value);

			var name = address.Substring(0, nameEnd - 1).Trim();
			return new MailAddress(addressMatch.Value, name == "" ? null: name);
		}

		static Match GetAddressMatch(string address)
		{
			if (address == null)
				throw new ArgumentNullException("address");

			if (address.Length > 2048)
				throw new ArgumentException("Value is too long", "address");

			var addressMatch = mailRegex.Match(address);
			if (!addressMatch.Success)
				throw new FormatException(address + " does not contain a valid email address");

			return addressMatch;
		}

		public override bool Equals(object obj)
		{
			var otherMail = obj as MailAddress;
			if (otherMail == null)
				return false;
			return this.Address == otherMail.Address;
		}

		public override int GetHashCode()
		{
			return this.Address.GetHashCode() ^ 0x563a;
		}
	}
}
