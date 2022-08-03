using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInParcel
    {
        public int Id { get; set; }
        public DroneStatuses Status { get; set; }
        public Location Location { get; set; }

        public override string ToString()
        {
            return $"***Drone In Parcel***\n" +
                $" Id: {Id}\n" +
                $" Status {Status}\n" +
                $" Location: {Location}\n";
        }

    }
}
//●	רחפן בחבילה
//o	מספר מזהה ייחודי
//o	מצב סוללה
//o	מיקום (נוכחי)

