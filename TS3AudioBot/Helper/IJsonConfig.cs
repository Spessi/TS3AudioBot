// TS3AudioBot - An advanced Musicbot for Teamspeak 3
// Copyright (C) 2017  TS3AudioBot contributors
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the Open Software License v. 3.0
//
// You should have received a copy of the Open Software License along with this
// program. If not, see <https://opensource.org/licenses/OSL-3.0>.

namespace TS3AudioBot.Helper
{
	using Newtonsoft.Json;
	using System;
	using System.IO;
	using System.Text;

	public interface IJsonSerializable
	{
		bool ExpectsString { get; }
		void ToJson(JsonWriter writer);
		E<string> FromJson(JsonReader reader);
	}

	public static class JsonSerializableExtensions
	{
		public static E<string> FromJson(this IJsonSerializable jsonConfig, string json)
		{
			if (jsonConfig.ExpectsString)
				json = JsonConvert.SerializeObject(json);

			var sr = new StringReader(json);
			using (var reader = new JsonTextReader(sr))
			{
				return jsonConfig.FromJson(reader);
			}
		}

		public static string ToJson(this IJsonSerializable jsonConfig)
		{
			var sb = new StringBuilder();
			var sw = new StringWriter(sb);
			using (var writer = new JsonTextWriter(sw))
			{
				writer.Formatting = Formatting.Indented;
				jsonConfig.ToJson(writer);
			}
			return sb.ToString();
		}
	}
}
