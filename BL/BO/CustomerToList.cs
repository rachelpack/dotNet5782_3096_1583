using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerToList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public int NumberOfParselSentAndDelivered { get; set; }
        public int NumberOfParcelSentButNotYetDelivered { get; set; }
        public int NumberOfParcelHeReceived { get; set; }
        public int SeveralPacelOnTheWayToTheCustomer { get; set; }

        public override string ToString()
        {
            return $"***Client To List***\n" +
                $" Id: {Id}\n" +
                $" Name: {Name}\n" +
                $" Phone: {Phone}\n";
        }
    }
}
