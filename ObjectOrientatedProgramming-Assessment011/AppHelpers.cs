using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace ObjectOrientatedProgramming_Assessment011
{
    public class AppHelpers
    {
        public static Random rngNo = new Random(); //random number generator for the whole program to use.

        public static int BottomTemp = -2; //lowest ambient room temp
        public static int TopTemp = 34; //highest ambient room temp

        public static List<string> resources = new List<string>() { "Toy", "Food", "Medicine" };

        public static List<string> pets = new List<string>() { "ObjectOrientatedProgramming_Assessment011.Bird", "ObjectOrientatedProgramming_Assessment011.Cat", "ObjectOrientatedProgramming_Assessment011.Dog", "ObjectOrientatedProgramming_Assessment011.Fish" };

        public static List<string> petNames = new List<string>() { "Kylee", "Arline", "Felicia", "Walter", "Adam", "Julia", "Tamara", "Wanda", "Bart", "Irwin" };

        public static void WriteAt(string s, int x, int y) //writing method
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        public static Dictionary<string, string> resourcesTypes = new Dictionary<string, string>()
        {
            { "Toy", "Toys increase your pets mood!" },
            { "Food", "Feed your pet to reduce their hunger!" },
            { "Medicine", "Give your pet medicine to heal them!" }
        };

        public static App LoadExisting()
        {
            if (File.Exists(@"save.json"))
            {
                string exisitingData;
                using (StreamReader reader = new StreamReader(@"save.json", Encoding.Default))
                {
                    exisitingData = reader.ReadToEnd();
                }
                //this bit makes it check the subclass to create the objects
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                App app = JsonConvert.DeserializeObject<App>(exisitingData, settings);

                return app;
            }

            else
            {
                App app = new App(true);
                return app;
            }

        }

        public static void ExitAndSave(App app)
        {
            if (File.Exists(@"save.json"))
            {
                File.Delete(@"save.json");
            }

            using (StreamWriter file = File.CreateText(@"save.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.TypeNameHandling = TypeNameHandling.All; //Added this bit to it save the subclass type for later reading
                serializer.Serialize(file, app);
            }
        }
    }
}
