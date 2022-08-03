using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class CustomerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public CustomerModel()
        {
            customer = new();
            customer.Location = new();
        }
        private Customer customer;
        public Customer Customer
        { get => customer;
            set { customer = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Customer))); }
        }

        private string name;
        public string Name
        {
            get => name;
            set { name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name))); }
        }
        //private int phone;
        //public int Phone
        //{
        //    get => phone;
        //    set { phone = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Phone))); }
        //}

    }
}
