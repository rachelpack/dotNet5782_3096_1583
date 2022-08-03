using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public Location Location { get; set; }
        public IEnumerable<DeliveryInCustomer> ListOfParcelsYouSent { get; set; }
        public IEnumerable<DeliveryInCustomer> ListOfParcelsYouReceived { get; set; }

        public override string ToString()
        {
            return $"***Customer***\n" +
                $" Id: {Id}\n" +
                $" Name: {Name}\n" +
                $" Phone: {Phone}\n" +
                $" Location:\n {Location}\n";
        }
    }
}


