using DlApi;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Dal
{
    public sealed partial class DalXml : IDal
    {
        private XMLTools XML;
        private const string DRONEPATH = "Drones.xml";
        private const string CUSTOMERPATH = "Customers.xml";
        private const string PARCELPATH = "Parcels.xml";
        private const string BASESTATIONPATH = "BaseStations.xml";
        private const string DRONECHARGESPATH = "DroneCharges.xml";
        private const string USERSPATH = "Users.xml";
        private const string CONFIGPATH = "config.xml";
        //singleton
        static readonly IDal instance = new DalXml();
        //static c'tor
        private DalXml()
        {
            XElement root;
            try
            {
                root = XMLTools.LoadListFromXmlElement(DRONECHARGESPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            root.Elements().Remove();
            try
            {
                XMLTools.SaveListToXmlElement(root, DRONECHARGESPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
        }
        //prop that return the instance of DalObject
        public static IDal Instance => instance;


        public double[] GetData()
        {
            double[] arr = new double[5];
            XElement dataConfig;
            try
            {
                dataConfig = XMLTools.LoadListFromXmlElement(CONFIGPATH);
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            arr[0] = double.Parse(dataConfig.Element("data").Element("PowerConsumptionByDroneAvailable").Value);
            arr[1] = double.Parse(dataConfig.Element("data").Element("PowerConsumptionByDroneCarryEasyWeight").Value);
            arr[2] = double.Parse(dataConfig.Element("data").Element("PowerConsumptionByDroneCarryMediumWeight").Value);
            arr[3] = double.Parse(dataConfig.Element("data").Element("PowerConsumptionByDroneCarryheavyWeight").Value);
            arr[4] = double.Parse(dataConfig.Element("data").Element("DroneLoadingRate").Value);
            return arr;
        }
    }
}

