using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Model
{
    public class CustomerListModel 
    {
     //   private ListCollectionView customers;
        public ListCollectionView Customers { get; set; }
        //public ListCollectionView<CustomerToList> Customers
        //{
        //    get => customers; 
        //    set { customers = value; }
        //}
        public CustomerListModel()
        {
            Customers = new ListCollectionView(ListsModel.CustomersList);
        }
    }
}
