using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DeliveryInTransfer
    {
        public int Id { get; set; }
        public WeightCategory Weight { get; set; }
        public Priorities Priority { get; set; }
        public bool Status { get; set; }//true = On the way to the destination
        public Location CollectLocation { get; set; }
        public Location DeliveryDestinationLocation { get; set; }
        public double Distance { get; set; }
        public double TransportDistance { get; set; }
        public CustomerInDelivery TheSender { get; set; }
        public CustomerInDelivery TheRecipient { get; set; }
        public override string ToString()
        {
            return $"***Delivery In Transfer***\n" +
                $" Id: {Id}\n" +
                $" Weight: {Weight}\n" +
                $" Priority: {Priority}\n" +
                $" Status {Status}\n" +
                $" distance {Distance}\n" +
                $" Transport Distance {TransportDistance}\n" +
                $" The Sender {TheSender}\n" +
                $" The The Recipient {TheRecipient}\n" +
                $" CollectLocation: {CollectLocation}\n" +
                $" Delivery Destination Location: {DeliveryDestinationLocation}\n" +
                $" TransportDistance: {TransportDistance}\n";
        }
    }
}
//●	משלוח בהעברה
//o	מספר מזהה ייחודי
//o	קטגוריית משקל (קל, ביניים, כבד)
//o עדיפות(רגילה, מהיר, חירום)
//o מצב משלוח (בולאני - ממתין לאיסוף \ בדרך ליעד)
//o מיקום איסוף
//o	מיקום יעד אספקה
//o	מרחק הובלה

