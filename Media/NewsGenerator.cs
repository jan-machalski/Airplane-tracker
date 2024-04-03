using ExCSS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Collections;

namespace projekt_Jan_Machalski
{
    public class NewsGenerator
    {
        private MediaIterator iterator;
        public NewsGenerator(List<IMedia> medias,List<IReportable> reportables)
        {
            iterator = new MediaIterator(medias,reportables);
        }
        public string GenerateNextNews()
        {
            
            if(iterator.Current() == (null,null))
            {
                return null;
            }


            (IReportable reportable, IMedia media) = iterator.Current();
            iterator.MoveNext();
            return reportable.Report(media);
   

        }
    }
    public class MediaIterator:IEnumerator
    {
        private List<IMedia> medias;
        private List<IReportable> reportables;
        private int mediaIndex;
        private int reportableIndex;
        public MediaIterator(List<IMedia> medias, List<IReportable> reportables)
        {
            mediaIndex = 0;
            reportableIndex = 0;
            this.medias = medias;
            this.reportables = reportables;
            
        }
        object IEnumerator.Current => Current();

        public (IReportable?, IMedia?) Current()
        {
            if((medias.Count == 0 && reportables.Count == 0) || reportableIndex>=reportables.Count)
            {
                return ((null,null));
            }
            return (reportables[reportableIndex], medias[mediaIndex]);
        }
        public bool MoveNext()
        {
            if (reportableIndex >= reportables.Count)
                return false;
            mediaIndex++;
            if (mediaIndex >= medias.Count)
            {
                reportableIndex++;
                mediaIndex = 0;
            }
            return true;
        }
        public (int,int) Key()
        {
            return (mediaIndex,reportableIndex);
        }
        public void Reset()
        {
            mediaIndex = 0;
            reportableIndex = 0;
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
