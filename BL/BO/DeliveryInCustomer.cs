using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DeliveryInCustomer
    {
        public int Id { get; set; }
        public WeightCategory Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelMode Status { get; set; }
        public CustomerInDelivery CustomerOf { get; set; }

        public override string ToString()
        {
            return $"***Delivery In Customer***\n" +
                $" Id: {Id}\n" +
                $" Weight: {Weight}\n" +
                $" Priority: {Priority}\n" +
                $" Status {Status}\n" +
                $" Customer Of: {CustomerOf}\n";
        }
    }
}

