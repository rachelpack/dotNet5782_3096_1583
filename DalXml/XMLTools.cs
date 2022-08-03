using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal
{
    class XMLTools
    {
        private static string path = @"../../Data/";

        /// <summary>
        /// Load list from Xml element.
        /// </summary>
        /// <param name="filePath">The path of the file to loading.</param>
        /// <returns></returns>
        public static XElement LoadListFromXmlElement(string filePath)
        {
            if (!File.Exists(path + filePath))
            {
                throw new DirectoryNotFoundException($"fail to load xml file: {filePath}");
            }
            XDocument document = XDocument.Load(path + filePath);
            return document.Root;

        }

        /// <summary>
        /// Save list to Xml element
        /// </summary>
        /// <param name="rootElem">The root to save.</param>
        /// <param name="filePath">The file to loadinding and save.</param>
        public static void SaveListToXmlElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(path + filePath);
            }
            catch (Exception ex)
            {
                throw new DirectoryNotFoundException($"fail to create xml file: {filePath}", ex);
            }
        }

        /// <summary>
        /// Load list from Xml serializer
        /// </summary>
        /// <typeparam name="T">The type of the object to serializer</typeparam>
        /// <param name="filePath">The path of the file to load.</param>
        /// <returns></returns>
        public static List<T> LoadListFromXmlSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(path + filePath))
                {
                    XmlSerializer x = new(typeof(List<T>));
                    using FileStream file = new(path + filePath, FileMode.Open);
                    return (List<T>)x.Deserialize(file);
                }
                else
                    return new List<T>();
            }
            catch (Exception e)
            {
                throw new DO.XMLFileLoadCreateException($"fail to load xml file: {filePath}", e);
            }
        }

        /// <summary>
        /// Save list to Xml serializer
        /// </summary>
        /// <typeparam name="T">The type of the object to serializer. </typeparam>
        /// <param name="list">The list to save.</param>
        /// <param name="filePath">he path of the file to load.</param>
        public static void SaveListToXmlSerializer<T>(IEnumerable<T> list, string filePath)
        {
            try
            {
                FileStream file = new(path + filePath, FileMode.Create);
                XmlSerializer x = new(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new DirectoryNotFoundException($"fail to create xml file: {filePath}", ex);
            }
        }

    }
}
