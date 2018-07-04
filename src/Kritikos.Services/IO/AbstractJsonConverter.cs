// Based on the work of Tom DuPont, retrieved from
// http://www.tomdupont.net/2014/04/deserialize-abstract-classes-with.html
namespace Kritikos.Services.IO
{
	using System;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	/// <inheritdoc />
	/// <summary>
	/// Abstract method to convert classes inheriting from abstract parents
	/// </summary>
	/// <typeparam name="T">Type of the abstract class</typeparam>
	public abstract class AbstractJsonConverter<T> : JsonConverter
	{
		public override bool CanConvert(Type objecType) => typeof(T).IsAssignableFrom(objecType);

		public override object ReadJson(
			JsonReader reader,
			Type objectType,
			object existingValue,
			JsonSerializer serializer)
		{
			var obj = JObject.Load(reader);

			var target = Create(objectType, obj);
			serializer.Populate(obj.CreateReader(), target);

			return target;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			=> throw new NotImplementedException();

		protected static bool FieldExists(JObject obj, string name, JTokenType type)
			=> obj.TryGetValue(name, out var token) && token.Type == type;

		protected abstract T Create(Type objectType, JObject jobject);
	}
}
