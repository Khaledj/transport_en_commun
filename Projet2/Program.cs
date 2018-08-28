using Newtonsoft.Json;
using Projet2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Examples.System.Net
{
    public class WebRequestGetExample
    {
        public static void Main()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            StreamReader reader = null;
            HttpWebResponse response = null;
            string responseFromServer = null;
            try
            {
                // Create a request for the URL. 		
                WebRequest request = WebRequest.Create("http://data.metromobilite.fr/api/linesNear/json?x=5.727770&y=45.185540&dist=600&details=true");
                // Get the response.
                response = (HttpWebResponse)request.GetResponse();
                // Display the status.
                Console.WriteLine(response.StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Display the content.
               List <DetailLigne> detailsLigne = JsonConvert.DeserializeObject<List<DetailLigne>>(responseFromServer);
               Dictionary<string,List<string>> detail = new Dictionary<string,List<string>>();
                foreach (DetailLigne detailLigne in detailsLigne){
                     if (!detail.ContainsKey(detailLigne.name)){
                        detail.Add(detailLigne.name, detailLigne.lines);
                     }
                    //afficher les arrêts sans le dictionnaire//
                    //Console.WriteLine("Arrêt1 :" + detailLigne.name);
                    ///afficher les lignes de transport sans le dictionnaire///
                   // Console.WriteLine("Lignes :");
                    foreach (string line in detailLigne.lines){
                        if (!detail[detailLigne.name].Contains(line)) {
                            detail[detailLigne.name].Add(line);
                        }
                        //ligne de transport
                        //Console.WriteLine(line);  
                    }
                    detail[detailLigne.name] = detail[detailLigne.name].Distinct().ToList();

                }
                foreach (KeyValuePair<string, List<string>> kvp in detail){
                    Console.WriteLine("arrêt:" + kvp.Key);
                    foreach (string ligne in kvp.Value){
                        Console.WriteLine("lignes:" + ligne);
                    }  
                }
            }
            catch (Exception e){
                Console.WriteLine(e.ToString());
            }
          
            // Cleanup the streams and the response.
            finally {
                reader.Close();
                response.Close();
            }
        }
    }
}