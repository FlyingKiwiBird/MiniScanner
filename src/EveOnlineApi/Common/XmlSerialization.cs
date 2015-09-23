//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="XmlSerialization.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Common
{
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// Contains methods to de-serialize XML data into objects.
    /// </summary>
    public static class XmlSerialization
    {
        /// <summary>
        /// Deserializes a XML string into an object.
        /// </summary>
        /// <typeparam name="T">Type to Create</typeparam>
        /// <param name="xml">XML Data</param>
        /// <returns>New Object</returns>
        public static T DeserializeString<T>(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            object result = null;
            
            using (TextReader reader = new StringReader(xml))
            {
                result = xs.Deserialize(reader);
            }

            return (T)result;
        }

        /// <summary>
        /// Deserializes a XML file into an object.
        /// </summary>
        /// <typeparam name="T">Type to Create</typeparam>
        /// <param name="path">Path to file.</param>
        /// <returns>New Object</returns>
        public static T DeserializeFile<T>(string path)
        {
            using (Stream fileStream = File.OpenRead(path))
            {
                return XmlSerialization.DeserializeStream<T>(fileStream);
            }
        }

        /// <summary>
        /// Deserializes an XML stream into an object.
        /// </summary>
        /// <typeparam name="T">Type to Create</typeparam>
        /// <param name="stream">Input Stream</param>
        /// <returns>New Object</returns>
        public static T DeserializeStream<T>(Stream stream)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));

            return (T)xs.Deserialize(stream);
        }
    }
}
