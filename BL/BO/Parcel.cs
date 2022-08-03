using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

    public class Parcel
    {
        public int Id { get; set; }
        public CustomerInDelivery Sender { get; set; }
        public CustomerInDelivery Receives { get; set; }
        public WeightCategory Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneInParcel Drone { get; set; }
        public DateTime? TimeOfCreateDelivery { get; set; }//נוצרה
        public DateTime? AssignmentTime { get; set; }//שויכה
        public DateTime? CollectingTime { get; set; }//נאספה
        public DateTime? SupplyTime { get; set; }//סופקה

        public override string ToString()
        {
            return $"***Parcel***\n" +
                $" Id: {Id}\n" +
                $" SenderId: {Sender}\n" +
                $" ReceivesId: {Receives}\n" +
                $" Weight: {Weight}\n" +
                $" Priority: {Priority}\n" +
                $" TimeOfCreateDelivery {TimeOfCreateDelivery}\n" +
                $" AssignmentTime: {AssignmentTime}\n" +
                $" CollectingTime: {CollectingTime}\n" +
                $" supplyTime: {SupplyTime}\n";
        }

    }
}
