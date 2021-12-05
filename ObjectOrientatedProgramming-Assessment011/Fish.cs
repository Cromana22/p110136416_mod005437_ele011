using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ObjectOrientatedProgramming_Assessment011
{
    class Fish: Pet
    {
        public Fish() //constructor
        {
            Name = AppHelpers.petNames[AppHelpers.rngNo.Next(0, AppHelpers.petNames.Count - 1)];
            Health = maxHealth;
            Hunger = minHunger;
            Mood = maxMood;
            ComfortTempFrom = 10;
            ComfortTempTo = 24;
        }

        public override void Display()
        {
            if (Health > minHealth)
            {
                AppHelpers.WriteAt(@"      /\      /‾‾‾‾)", 14, 13);
                AppHelpers.WriteAt(@" /‾‾‾‾  ‾‾\  /    / ", 14, 14);
                AppHelpers.WriteAt(@"/ [_] }}}  \/    /  ", 14, 15);
                AppHelpers.WriteAt(@"\     }}}        \_ ", 14, 16);
                AppHelpers.WriteAt(@" \_____  __/\     \ ", 14, 17);
                AppHelpers.WriteAt(@"       \/    \_____)", 14, 18);
                Console.SetCursorPosition(31, 22);

                Thread.Sleep(300);

                AppHelpers.WriteAt(@"      /\      /‾‾‾‾)", 14, 13);
                AppHelpers.WriteAt(@" /‾‾‾‾  ‾‾\  /    / ", 14, 14);
                AppHelpers.WriteAt(@"/ [.] }}}  \/    /  ", 14, 15);
                AppHelpers.WriteAt(@"\     }}}        \_ ", 14, 16);
                AppHelpers.WriteAt(@" \_____  __/\     \ ", 14, 17);
                AppHelpers.WriteAt(@"       \/    \_____)", 14, 18);
                Console.SetCursorPosition(31, 22);

                Thread.Sleep(700);
            }

            else
            {
                AppHelpers.WriteAt(@"      /\      /‾‾‾‾)", 14, 13);
                AppHelpers.WriteAt(@" /‾‾‾‾  ‾‾\  /    / ", 14, 14);
                AppHelpers.WriteAt(@"/ [x] }}}  \/    /  ", 14, 15);
                AppHelpers.WriteAt(@"\     }}}        \_ ", 14, 16);
                AppHelpers.WriteAt(@" \_____  __/\     \ ", 14, 17);
                AppHelpers.WriteAt(@"       \/    \_____)", 14, 18);
                Console.SetCursorPosition(31, 22);
                Console.SetCursorPosition(31, 22);

                Thread.Sleep(1000);
            }
        }
    }
}
