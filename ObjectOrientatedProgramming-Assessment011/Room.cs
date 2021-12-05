using System;
using System.Threading;

namespace ObjectOrientatedProgramming_Assessment011
{
    public class Room: IUserInterface
    {
        public int AmbientTemp { get; set; }
        public int CurrentTemp { get; set; }
        public int Vents { get; set; }
        public string VentSign { get; set; }

        public Pet APet;

        public Room() //constructor
        {
            AmbientTemp = AppHelpers.rngNo.Next(AppHelpers.BottomTemp, AppHelpers.TopTemp);
            CurrentTemp = AmbientTemp;

            //select a random pet type...
            string petType = AppHelpers.pets[AppHelpers.rngNo.Next(0, AppHelpers.pets.Count-1)];

            try
            {
                //...convert that to a class and create instance of it
                Type type = Type.GetType(petType, true);
                APet = (Pet)(Activator.CreateInstance(type));
            }

            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e);
            }
        }

        public void Heat()
        {
            Vents = 2;
            VentSign = "+";
        }

        public void Cool()
        {
            Vents = -2;
            VentSign = "-";
        }

        public void Stop()
        {
            Vents = 0;
            VentSign = " ";
        }

        public void UpdateTemp()
        {
            int ambientMod = 0;

            if (CurrentTemp > AmbientTemp)
            {
                ambientMod = -1;
            }
            else if (CurrentTemp < AmbientTemp)
            {
                ambientMod = 1;
            }

            if (CurrentTemp > -20 & CurrentTemp < 40)
            {
                CurrentTemp = CurrentTemp + Vents + ambientMod;
            }
        }

        public void PetIncreaseHunger()
        {
            APet.IncreaseHunger();
        }

        public void PetUpdateHealth()
        {
            APet.UpdateHealth(CurrentTemp);
        }

        public void PetDecreaseMood()
        {
            APet.DecreaseMood();
        }

        public void PetUseItem(Resource resource)
        {
                APet.UseItem(resource);
        }

        public void Display()
        {
            string temp = String.Format("{0,3}", CurrentTemp);

            AppHelpers.WriteAt(@"|    |   ____ _                  __ __   |    |", 0, 8);
            AppHelpers.WriteAt($@"|    |  |{temp}c|{VentSign}|                |__|__|  |    |", 0, 9);
            AppHelpers.WriteAt(@"|    |   ‾‾‾‾ ‾                 |  |  |  |    |", 0, 10);
            AppHelpers.WriteAt(@"|    |                           ‾‾ ‾‾   |    |", 0, 11);
            AppHelpers.WriteAt(@"|    |                                   |    |", 0, 12);
            AppHelpers.WriteAt(@"|    |                                   |    |", 0, 13);
            AppHelpers.WriteAt(@"|    |                                   |    |", 0, 14);
            AppHelpers.WriteAt(@"|    |                                   |    |", 0, 15);
            AppHelpers.WriteAt(@"|    |_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _|    |", 0, 16);
            AppHelpers.WriteAt(@"|   /                                     \   |", 0, 17);
            AppHelpers.WriteAt(@"|  /                                       \  |", 0, 18);
            AppHelpers.WriteAt(@"| /                                         \ |", 0, 19);
            AppHelpers.WriteAt(@"|/_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _\|", 0, 20);

            APet.Display();
        }
    }
}
