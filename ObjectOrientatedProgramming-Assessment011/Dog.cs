using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ObjectOrientatedProgramming_Assessment011
{
    class Dog: Pet
    {
        public Dog() //constructor
        {
            Name = AppHelpers.petNames[AppHelpers.rngNo.Next(0, AppHelpers.petNames.Count - 1)];
            Health = maxHealth;
            Hunger = minHunger;
            Mood = maxMood;
            ComfortTempFrom = 10;
            ComfortTempTo = 29;
        }

        public override void Display()
        {
            if (Health > minHealth)
            {
                AppHelpers.WriteAt(@"  /‾‾\     /‾‾\   __", 14, 13);
                AppHelpers.WriteAt(@" /  /  ‾‾‾  \  \ / /", 14, 14);
                AppHelpers.WriteAt(@"|_ / [_] [_] \__|\ \", 14, 15);
                AppHelpers.WriteAt(@" _ \    o    / _ / /", 14, 16);
                AppHelpers.WriteAt(@"   /    ^    \__/ / ", 14, 17);
                AppHelpers.WriteAt(@"  (_ _|_ _|_ _ _ /  ", 14, 18);
                Console.SetCursorPosition(31, 22);

                Thread.Sleep(300);

                AppHelpers.WriteAt(@"  /‾‾\     /‾‾\   __", 14, 13);
                AppHelpers.WriteAt(@" /  /  ‾‾‾  \  \ / /", 14, 14);
                AppHelpers.WriteAt(@"|_ / [.] [.] \__|\ \", 14, 15);
                AppHelpers.WriteAt(@" _ \    o    / _ / /", 14, 16);
                AppHelpers.WriteAt(@"   /    ^    \__/ / ", 14, 17);
                AppHelpers.WriteAt(@"  (_ _|_ _|_ _ _ /  ", 14, 18);
                Console.SetCursorPosition(31, 22);

                Thread.Sleep(700);
            }

            else
            {
                AppHelpers.WriteAt(@"  /‾‾\     /‾‾\   __", 14, 13);
                AppHelpers.WriteAt(@" /  /  ‾‾‾  \  \ / /", 14, 14);
                AppHelpers.WriteAt(@"|_ / [x] [x] \__|\ \", 14, 15);
                AppHelpers.WriteAt(@" _ \    o    / _ / /", 14, 16);
                AppHelpers.WriteAt(@"   /    ^    \__/ / ", 14, 17);
                AppHelpers.WriteAt(@"  (_ _|_ _|_ _ _ /  ", 14, 18);
                Console.SetCursorPosition(31, 22);

                Thread.Sleep(1000);
            }
        }
    }
}
