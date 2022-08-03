using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DlApi
{
    public static class DalFactory
    {
        /// <summary>
        /// Return the instance of the IDal
        /// </summary>
        /// <returns></returns>
        public static IDal GetDAL()
        {
            string dalType = DalConfig.DalName;
            string dalNamespace = DalConfig.DalPackages[dalType].Item1;
            string dalClass = DalConfig.DalPackages[dalType].Item2;
            string dalAssembly = DalConfig.DalPackages[dalType].Item3;
            if (dalNamespace == null || dalClass == null || dalAssembly == null)
            {
                throw new DalConfigException("dal package not fount");
            }

            try
            {
                Assembly.LoadFrom(dalAssembly);
            }
            catch (Exception)
            {
                throw;
            }
            Type type1 = Type.GetType($"{dalNamespace}.{dalClass}, {dalClass}");
            if (type1 == null)
            {
                throw new DalConfigException("");
            }

            IDal dal = (IDal)type1.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null);
            return dal == null ? throw new DalConfigException("") : dal;
        }
    }
}
