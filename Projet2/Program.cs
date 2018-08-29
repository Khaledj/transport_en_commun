using Newtonsoft.Json;
using Projet2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using ClassLibrary1;


namespace Examples.System.Net
{
    public class WebRequestGetExample
    {
        public static void Main()
        {
            ClassLibrary1.Class1 test1 = new ClassLibrary1.Class1();
            string lignes = test1.GetApi("http://data.metromobilite.fr/api/linesNear/json?x=5.727770&y=45.185540&dist=600&details=true");
           

            ClassLibrary1.Class1 test2 = new ClassLibrary1.Class1();
            string details = test2.GetApi("https://data.metromobilite.fr/api/routers/default/index/routes");

            // Display the content.
            List <Ligne> Lignes = JsonConvert.DeserializeObject<List<Ligne>>(lignes);
            Dictionary<string, List<string>> detail = new Dictionary<string, List<string>>();
            List<DetailObject> Details = JsonConvert.DeserializeObject<List<DetailObject>>(details);

            foreach (Ligne Ligne in Lignes)
            {
                if (!detail.ContainsKey(Ligne.name))
                {
                    detail.Add(Ligne.name, Ligne.lines);
                }
                //afficher les arrêts sans le dictionnaire//
                //Console.WriteLine("Arrêt1 :" + Ligne.name);
                ///afficher les lignes de transport sans le dictionnaire///
                // Console.WriteLine("Lignes :");
                foreach (string line in Ligne.lines)
                {
                    if (!detail[Ligne.name].Contains(line))
                    {
                        detail[Ligne.name].Add(line);
                    }
                    //ligne de transport
                    //Console.WriteLine(line);  
                }
                detail[Ligne.name] = detail[Ligne.name].Distinct().ToList();

            }
            foreach (KeyValuePair<string, List<string>> kvp in detail)
            {
                Console.WriteLine("arrêt:" + kvp.Key);
                foreach (string ligne in kvp.Value)
                {
                    foreach (DetailObject Detail in Details)
                    {
                        if (Detail.id.Contains(ligne))
                        {
                            Console.WriteLine("lignes:" + ligne);
                            Console.WriteLine(Detail.longName);
                            Console.WriteLine(Detail.mode);
                            Console.WriteLine(Detail.type);
                        }

                    }
                }
            }
        }

    }

}



