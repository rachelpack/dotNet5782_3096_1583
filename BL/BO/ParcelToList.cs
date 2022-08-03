using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class ParcelToList
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceivesId { get; set; }
        public ParcelMode Status { get; set; }
        public Priorities Priority { get; set; }

        public override string ToString()
        {
            return $"***Parcel To List***\n" +
                $" Id: {Id}\n" +
                $" SenderId: {SenderId}\n" +
                $" ReceivesId: {ReceivesId}\n" +
                $" Status: {Status}\n" +
                $" priority: {Priority}\n";
        }


    }
}
