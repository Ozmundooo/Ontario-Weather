using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using System;

namespace JSONConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            String city = "";
            Console.WriteLine("Enter City: ");
            city = Console.ReadLine();
            var json = new WebClient().DownloadString("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22"+
                city
                +"%2C%20on%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
            JObject data = JObject.Parse(json);
            int temp = tempGet(data);

            String condition = conditionGet(data);
            output(city, temp, condition);
            
           
        }

        public static int tempGet(JObject data)
        {
            JObject item = (JObject)data["query"]["results"]["channel"]["item"]["condition"];
            int temp = Convert.ToInt32(item["temp"]);
            temp = (temp - 32) * 5 / 9;
            return temp;
        }


        private static string conditionGet(JObject data)
        {
            JObject item = (JObject)data["query"]["results"]["channel"]["item"]["condition"];
            String condition = Convert.ToString(item["text"]);
            return condition;
        }

        public static void output(String city ,int temp, String condition)
        {
            Console.WriteLine("The temp in " + city + " is " + temp + " °C");
            Console.WriteLine("The condition in " + city + " is " + condition);
            Console.Read();
        }
    }
}
