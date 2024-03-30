using ExCSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class NewsGenerator
    {
        private List<IMedia> medias;
        private List<IReportable> reportables;
        private int mediaIndex;
        private int reportableIndex;
        public NewsGenerator(List<IMedia> medias,List<IReportable> reportables)
        {
            mediaIndex = -1;
            reportableIndex = 0;
            this.medias = medias;
            this.reportables = reportables;
        }
        public string GenerateNextNews()
        {
            mediaIndex++;
            if(mediaIndex >= medias.Count)
            {
                reportableIndex++;
                mediaIndex = 0;
            }
            if (reportableIndex >= reportables.Count)
                return null;
            string news = reportables[reportableIndex].Report(medias[mediaIndex]);
            return news;
        }
    }
    public class NewsListsCreator
    {
        private static List<IMedia> mediaList = new List<IMedia>()
        {
            new Television("Telewizja Abelowa"),
            new Television("Kanał TV-sensor"),
            new Radio("Radio Kwantyfikator"),
            new Radio("Radio Shmem"),
            new Newspaper("Gazeta Kategoryczna"),
            new Newspaper("Dziennik Politechniczny")
        };
        public static List<IMedia> GetMediaList()
        {
            return mediaList; 
        }
        public static List<IReportable> GetReportableList()
        {
            Database database = Database.Instance;
            List<IReportable> reportables = new List<IReportable>();
            foreach(var airportInfo in database.AirportInfo)
            {
                reportables.Add(airportInfo.Value);
            }
            foreach(var passengerPlaneInfo in database.PassengerPlaneInfo)
            {
                reportables.Add(passengerPlaneInfo.Value);
            }
            foreach(var cargoPlaneInfo in database.CargoPlaneInfo)
            {
                reportables.Add(cargoPlaneInfo.Value);
            }
            return reportables;
        }


    }
}
