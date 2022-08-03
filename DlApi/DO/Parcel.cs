using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Parcel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public WeightCategory Weight { get; set; }
        public Priorities Priority { get; set; }
        public DateTime? Requested { get; set; }//נוצרה
        public int DroneId { get; set; }
        public DateTime? Scheduled { get; set; }//שויכה
        public DateTime? PickedUp { get; set; }//נאספה
        public DateTime? Delivered { get; set; }//סופקה
        public bool IsAvailable { get; set; }
        public override string ToString()
        {
            return $"***Parcel***\n" +
           $" Id: {Id} \n" +
           $" SenderId: {SenderId} \n" +
           $" TargetId: {TargetId} \n" +
           $" Weight: {Weight} \n" +
           $" Priority: {Priority} \n" +
           $" Requested: {Requested}\n" +
           $" DroneId: {DroneId}\n" +
           $" Scheduled: {Scheduled}\n" +
           $" PickedUp: {PickedUp}\n" +
           $" Delivered: {Delivered}\n";
        }
    }
}
