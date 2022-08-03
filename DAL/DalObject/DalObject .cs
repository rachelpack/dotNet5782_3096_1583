using System.Runtime.CompilerServices;
using DlApi;
using DO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal sealed partial class DalObject : IDal
    {
        //singleton
        static readonly IDal instance = new DalObject();
        //prop that return the instance of DalObject
        public static IDal Instance => instance;

        //private c'tor
        private DalObject()
        {
            DataSource.Initialize();
        }


    }
}

