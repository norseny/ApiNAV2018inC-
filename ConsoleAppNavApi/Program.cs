using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleAppNavApi
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            first, easiest way

            string NavWebServiceUrl = "http://pibe1:11048/DynamicsNAV110/api/beta/extItems";

            WebRequest objWebRequest = WebRequest.Create(NavWebServiceUrl);
            // set the credentials to authenticate request, if required by the server  
            objWebRequest.Credentials = new NetworkCredential("pibe", "Wadowicka8W");
            //Get the web service response.  
            HttpWebResponse objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
            //get the contents return by the server in a stream and open the stream using a StreamReader for easy access.  
            StreamReader objStreamReader = new StreamReader(objWebResponse.GetResponseStream());

            // Read the contents.  
            string sResponse = objStreamReader.ReadToEnd();
            Console.WriteLine(sResponse);
            Console.ReadLine();

            // Cleanup the streams and the response.  
            objStreamReader.Close();
            objWebResponse.Close();
            */

            string navStringResp = null;
            navStringResp = GET("http://pibe1:11048/DynamicsNAV110/api/beta/extItems");

            LoadJson(navStringResp);

            Console.WriteLine(navStringResp);
        }


        //second way:
        public static string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            
            //Create a proxy for the service request 
            // objWebRequest.Proxy = new WebProxy();

            // set the credentials to authenticate request, if required by the server  
            request.Credentials = new NetworkCredential("pibe", "Wadowicka8W");
            //objWebRequest.Proxy.Credentials = new NetworkCredential("pibe", "Wadowicka8W");

            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    //log errorText
                }
                throw;
            }
        }
        public static void LoadJson(string jsonString)
        {
            dynamic results = JsonConvert.DeserializeObject<dynamic>(jsonString);
            var value = results.value;

            foreach (var el in results.value)
            {
                var extItemCode = el.extItemCode;
                var extItemDescr = el.extItemDescr;
                //do sth more
            }
            
        }

    }
}
