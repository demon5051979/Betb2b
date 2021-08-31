using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace B2B.Services.Extensions
{
	public static class XmlSerializerExtensions
	{
		public static string SerializeToString(this XmlSerializer serializer, object toSerialize)
		{
			var settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.OmitXmlDeclaration = true;

			var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

			StringBuilder result = new StringBuilder();

			using (XmlWriter writer = XmlWriter.Create(result, settings))
			{
				serializer.Serialize(writer, toSerialize, emptyNamespaces);
				return result.ToString();
			}
		}

		public static object SerializeFromString(this XmlSerializer serializer, string toDeserialize)
		{
			using (TextReader reader = new StringReader(toDeserialize))
			{
				return serializer.Deserialize(reader);
			}
		}
	}
}
