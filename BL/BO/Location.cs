namespace BO
{

    public class Location
    {
        double longitude;
        public double Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                if (value > 90)
                    throw new TheValueOutOfRange("The longitude value out of range");
                longitude = value;
            }
        }//קן אורך

        double latitude;
        public double Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                if (value < -90 || value > 90)
                    throw new TheValueOutOfRange("The latitude value out of range");
                latitude = value;
            }
        }//קו רוחב

        public override string ToString()
        {
            return $" Longitude: {Longitude}\n" +
                $" Latitude: {Latitude}\n";
        }

    }
}
