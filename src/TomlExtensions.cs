namespace Lost.Rust.Cargo
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using HyperTomlProcessor;

	internal static class TomlExtensions {
		static readonly Dictionary<Type, TomlItemType> tomlTypes = new Dictionary<Type, TomlItemType> {
			{typeof(string), TomlItemType.BasicString},
			{typeof(int), TomlItemType.Integer},
			{typeof(bool), TomlItemType.Boolean},
			{typeof(float), TomlItemType.Float},
		};

		public static T SingleKey<T>(this TableTree tree, string name)
		{
			return tree.Nodes.OfType<KeyValue>().SingleKey<T>(name);
		}

		public static T SingleKey<T>(this IEnumerable<KeyValue> nodes, string name)
		{
			var keyNode = nodes.SingleOrDefault(kv => kv.Key == name);

			if (keyNode == null)
				throw new FormatException("Expected single key with name: " + name);

			if (keyNode.Value.Value is T)
				return (T)keyNode.Value.Value;
			else
				throw new FormatException(name + " must be of type " + typeof(T).FullName + 
										", but the actual type is "
										+ (keyNode.Value.Value == null? "null": keyNode.Value.Value.GetType().FullName));
		}

		public static void SetSingleKey<T>(this TableTree tree, string name, T value)
		{
			var keyNode = tree.Nodes.OfType<KeyValue>().SingleOrDefault(kv => kv.Key == name);
			if (keyNode != null)
				tree.Nodes.Remove(keyNode);

			var tomlValue = new TomlValue(GetType<T>(), value);
			keyNode = new KeyValue(name.ToCharArray(), tomlValue, keyNode == null? null: keyNode.Comment);
			tree.Nodes.Add(keyNode);
		}

		static TomlItemType GetType<T>()
		{
			return tomlTypes[typeof(T)];
		}
	}
}