using System;
using System.Device.Location;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace CafeMaps
{
    class Cafe
    {
        public static int lastID {get; set;}
        public static List<Cafe> cafes = new List<Cafe>();
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double CordinateX { get; set; }
        public double CordinateY { get; set; }
        public double Rating { get; set; }
        public List<WorkingDaysAndTimes> WorkTime = new List<WorkingDaysAndTimes>();
        public List<Review> Review = new List<Review>();
        public GeoCoordinate CafesCoordinate = new GeoCoordinate();

        public Cafe() { }

        public Cafe(int id, string name, string address, double coordinateX, double coordinateY)
        {
            ID = id;
            Name = name;
            Address = address;
            CordinateX = coordinateX;
            CordinateY = coordinateY;
            CafesCoordinate.Latitude = coordinateX;
            CafesCoordinate.Longitude = coordinateY;
        }


        public override string ToString()
        {
            return ID + " - " +
                "Name: " + Name +
                " \n    Address: " + Address +
                " \n    Latitude: " + CafesCoordinate.Latitude +
                " \n    Longitude:" + CafesCoordinate.Longitude +
                " \n    Rating: " + Rating;
        }

        public static string ToJson()
        {
            string json = "[";
            foreach(Cafe cafe in cafes)
            {
                json += "{\"ID\":\""+cafe.ID+ "\",\"Name\":\"" + cafe.Name + "\",\"Address\":\"" + cafe.Address + "\",\"CordinateX\":\"" + cafe.CordinateX + "\",\"CordinateY\":\"" + cafe.CordinateY + "\",\"WorkTime\":[";
                foreach(WorkingDaysAndTimes day in cafe.WorkTime)
                {
                    json += "{\"Day\":\""+day.Day+ "\",\"From\":\"" + day.From + "\",\"To\":\"" + day.To + "\"},";
                }
                json = json.TrimEnd(',');
                json += "],\"Review\":[";
                foreach (Review rev in cafe.Review)
                {
                    json += "{\"CafeID\":\"" + rev.CafeID + "\",\"UserID\":\"" + rev.UserID + "\",\"Rate\":\"" + rev.Rate + "\",\"Comment\":\"" + rev.Comment + "\"},";
                }
                json = json.TrimEnd(',');
                json += "]},";
            }
            json = json.TrimEnd(',');
            json += "]";
            return json;
        }

        public static void ReadCafeData()
        {
            string path = @"Data\Cafes.json";
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {
                    Cafe c = new Cafe(
                        Convert.ToInt32(item.ID),
                        Convert.ToString(item.Name),
                        Convert.ToString(item.Address),
                        double.Parse(Convert.ToString(item.CordinateX)),
                        double.Parse(Convert.ToString(item.CordinateY))
                        );
                    dynamic days = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(item.WorkTime));
                    foreach (var item1 in days)
                    {
                        c.WorkTime.Add(new WorkingDaysAndTimes(Convert.ToString(item1.Day), Convert.ToString(item1.From), Convert.ToString(item1.To)));
                    }
                    double sumRate = 0;
                    int countRate = 0;
                    dynamic rev = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(item.Review));
                    foreach (var item2 in rev)
                    {
                        countRate++;
                        sumRate += Convert.ToInt32(item2.Rate);
                        c.Review.Add(new Review(Convert.ToInt32(item2.CafeID), Convert.ToString(item2.UserID), Convert.ToInt32(item2.Rate), Convert.ToString(item2.Comment)));
                    }
                    if (countRate != 0)
                        c.Rating = sumRate / countRate;
                    else
                        c.Rating = 0;
                    Cafe.cafes.Add(c);
                    lastID = Convert.ToInt32(item.ID);
                }
            }
        }

        public static void UpdateJsonData()
        {
            var json = Cafe.ToJson();
            string path = @"Data\Cafes.json";
            System.IO.StreamWriter file = new System.IO.StreamWriter(path);
            file.WriteLine(json);
            file.Close();
        }
    }


}




