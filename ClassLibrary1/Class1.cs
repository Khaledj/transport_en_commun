using System;
using System.IO;
using System.Net;


namespace ClassLibrary1
{
    public class Class1 
    {
        public string GetApi(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            StreamReader reader = null;
            HttpWebResponse response = null;
            string responseFromServer = null;

            try
            {
                // Create a request for the URL. 		
                WebRequest request = WebRequest.Create(url);
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

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // Cleanup the streams and the response.
            finally
            {
                // Cleanup the streams and the response.
                reader.Close();
                response.Close();
            }
            return responseFromServer;
        }
    }

  
}

