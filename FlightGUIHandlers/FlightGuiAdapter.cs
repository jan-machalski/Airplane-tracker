using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using FlightTrackerGUI;
using Mapsui.Projections;

namespace projekt_Jan_Machalski
{
    public class FlightGuiAdapter:FlightGUI
    {

        private static Database database = Database.Instance;

        public FlightGuiAdapter(Flight flight)
        {
            WorldPosition oldPosition = new WorldPosition(flight.Latitude, flight.Longitude);
            float progress = GetProgress(flight.TakeOffTime,flight.LandingTime);
            WorldPosition position = GetPosition(flight, progress);
            if (position.Latitude == oldPosition.Latitude && position.Longitude == oldPosition.Longitude) // is the plane moving at all?
                MapCoordRotation = GetRotation(new WorldPosition(database.AirportInfo[flight.TargetID].Latitude, database.AirportInfo[flight.TargetID].Longitude), oldPosition);
            else
                MapCoordRotation = GetRotation(position, oldPosition);
            WorldPosition = position;
            ID = flight.ID;
        }

        private static double GetRotation(WorldPosition currentPosition, WorldPosition oldPosition) // calculate rotation based on current and previous position
        {
            if(Math.Sign(currentPosition.Longitude)!=Math.Sign(oldPosition.Longitude) && Math.Abs(currentPosition.Longitude- oldPosition.Longitude)>180) // if the International Date Line has been crossed in the last frame
            {
                currentPosition.Longitude -= 180 * Math.Sign(currentPosition.Longitude);
                oldPosition.Longitude -= 180 * Math.Sign(oldPosition.Longitude);
            }

            var oldPosXY = SphericalMercator.FromLonLat(oldPosition.Longitude, oldPosition.Latitude);
            var curPosXY = SphericalMercator.FromLonLat(currentPosition.Longitude, currentPosition.Latitude);

            double dx = curPosXY.x - oldPosXY.x;
            double dy = curPosXY.y - oldPosXY.y;

            return Math.Atan2(dx, dy);
        }
        private static WorldPosition GetPosition(Flight f,float progress) // calculate current flight position based on takeoff and landing times and the progress
        {
            double originLongitude = database.AirportInfo[f.OriginID].Longitude;
            double targetLongitude = database.AirportInfo[f.TargetID].Longitude;

            if (originLongitude - targetLongitude > 180) // if the shortest way crosses International Date Line - West to east or east to west
                targetLongitude += 360;
            else if (targetLongitude - originLongitude > 180)
                originLongitude += 360;
            double currentLongitude = originLongitude + progress * (targetLongitude - originLongitude);
            if (currentLongitude > 180)
                currentLongitude -= 360;
            return new WorldPosition()
            {
                Latitude = database.AirportInfo[f.OriginID].Latitude + progress * (database.AirportInfo[f.TargetID].Latitude - database.AirportInfo[f.OriginID].Latitude),
                Longitude = currentLongitude,
            };
        }
        private static float GetProgress(string takeOffTime, string landingTime) // calculate progress of the flight
        {
            DateTime currentTime = DateTime.Now; 

            DateTime takeOffDateTime = ParseTime(takeOffTime);
            DateTime landingDateTime = ParseTime(landingTime);

            if(takeOffDateTime > landingDateTime) // if the flight lands on a diffrent day than it starts
            {

                if (currentTime > landingDateTime)
                    landingDateTime = landingDateTime.AddDays(1);
                else
                    takeOffDateTime = takeOffDateTime.AddDays(-1);
            }
            if (currentTime < takeOffDateTime || currentTime > landingDateTime)
            {
                return 0;
            }

            TimeSpan totalFlightDuration = landingDateTime - takeOffDateTime;

            TimeSpan elapsedTime = currentTime - takeOffDateTime;

            float progress = (float)(elapsedTime.TotalSeconds / totalFlightDuration.TotalSeconds);

            return Math.Min(1.0f, Math.Max(0.0f, progress)); // cap
        }

        private static DateTime ParseTime(string time) // todays date with hour and minute taken from the dd:mm format
        {
            string[] timeParts = time.Split(':');
            int hours = int.Parse(timeParts[0]);
            int minutes = int.Parse(timeParts[1]);

            DateTime dateTime = DateTime.Today.AddHours(hours).AddMinutes(minutes);
            return dateTime;
        }
        
    }
}
