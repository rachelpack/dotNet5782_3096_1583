using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Drone
    {
        public int Id { get; set; }
        public string Modal { get; set; }
        public double Battery { get; set; }
        public WeightCategory Weight { get; set; }
        public DroneStatuses Status { get; set; }
        public DeliveryInTransfer DeliveryInTransfer { get; set; }
        public Location Location { get; set; }

        public override string ToString()
        {
            return $"***Drone***\n" +
                $" Id: {Id}\n" +
                $" Model: {Modal}\n" +
                $" Weight: {Weight}\n" +
                $" Status {Status}\n" +
                $" Delivery In Transfer: {DeliveryInTransfer}\n" +
                $" Location:\n{Location}";
        }
    }
}
//●	רחפן
//o	מספר מזהה ייחודי
//o	מודל הרחפן
//o	קטגוריית משקל (קל, ביניים, כבד)
//o מצב סוללה
//o	מצב רחפן
//o	משלוח בהעברה
//o	מיקום (נוכחי)

