using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DlApi
{
    static class DalConfig
    {
        internal static string DalName;
        internal static Dictionary<string, (string, string, string)> DalPackages;
        /// <summary>
        /// load the data of the IDal from the config.
        /// </summary>
        static DalConfig()
        {
            XElement dalConfig = XElement.Load(@"../../Data/config.xml");
            DalName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           select pkg)
                           .ToDictionary(p => "" + p.Name,
                           p => (NamespaceName: p.Attribute("namespace").Value, ClassName: p.Attribute("class").Value, AssemblyName: p.Value));

        }
    }
    
    public class DalConfigException : Exception
    {
        public DalConfigException(string message) : base(message) { }
        public DalConfigException(string message, Exception inner) : base(message, inner) { }

    }
}
