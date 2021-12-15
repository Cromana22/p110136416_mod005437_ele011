using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Newtonsoft.Json;

namespace ObjectOrientatedProgramming_Assessment011
{
    public class App : IUserInterface
    {
        public int coins = 0;
        public string currentView = "Room";
        public int currentRoom = 0;
        public bool main = true;
        public string selectedShopResource;
        public int selectedInvResource;

        public List<Resource> inventory = new List<Resource>();
        public List<Resource> shop = new List<Resource>();
        public List<Resource> selectedShopResourceList = new List<Resource>();
        public List<Room> rooms = new List<Room>();

        [JsonIgnore]
        public bool running = false;

        public App() //constructor
        {

        }

        public App(bool newApp) //constructor
        {
            if (newApp == true)
            {
                rooms.Add(new Room()); //create a single room on app start, and load it (index 0)

                #region - create shop
                shop.Add(new Toy("Ball", 5, 10, 0, 20));
                shop.Add(new Toy("Stick", 1, 5, 0, 10));
                shop.Add(new Toy("Twine", 2, 5, 0, 10));
                shop.Add(new Toy("Hoop", 5, 10, 0, 20)); 
                shop.Add(new Food("Meat", 10, 2, 0, -20));
                shop.Add(new Food("Fruit", 5, 1, 0, -10));
                shop.Add(new Food("Plant", 2, 1, 0, -5));
                shop.Add(new Food("Fish", 2, 2, 0, -3));
                shop.Add(new Medicine("Sml", 20, 1, 0, -10, 50));
                shop.Add(new Medicine("Med", 30, 1, 0, -10, 100));
                shop.Add(new Medicine("Lrg", 40, 1, 0, -10, 200));
                shop.Add(new Medicine("Full", 50, 1, 0, -10, 500));
                #endregion

                //create inventory from shop (all with 0 quantity)
                inventory = shop;
            }
        }

        public void Run()
        {
            running = true;

            //start threads
            Thread one = new Thread(oneSecUpdate);
            Thread five = new Thread(fiveSecUpdate);

            one.Start();
            five.Start();

            while (running == true)
            {

                if (Console.KeyAvailable)  //if user presses a key, do stuff below
                {
                    ConsoleKeyInfo keypressed = Console.ReadKey(true);
                    SwitchMenu(keypressed);
                }

                else
                {
                    Display();
                }
            }
        }

        public void Display()
        {
            DisplayHeader();

            if (rooms[currentRoom].APet.Health == 0)
            {
                currentView = "Room";
                main = true;
            }

            if (currentView == "Room") { DisplayRoom(main); }
            else if (currentView == "Inventory") { DisplayInventory(); }
            else if (currentView == "Shop") { DisplayShop(main); }
            else { DisplayRoom(main); }
        }

        #region - display methods
        void DisplayHeader()
        {
            //set values to be used in UI
            string roomNo = String.Format("{0,2}", currentRoom + 1);
            string petName = String.Format("{0,-8}", rooms[currentRoom].APet.Name);
            string coinCount = String.Format("{0,4}", coins);

            string tempStatus = "";
            if (rooms[currentRoom].CurrentTemp < rooms[currentRoom].APet.ComfortTempFrom) { tempStatus = "Cold"; }
            else if (rooms[currentRoom].CurrentTemp > rooms[currentRoom].APet.ComfortTempTo) { tempStatus = "Hot"; }
            tempStatus = String.Format("{0,4}", tempStatus);

            //stat bars
            string healthA, healthB, hungerA, hungerB, moodA, moodB;
            healthA = StatChunks(rooms[currentRoom].APet.Health, rooms[currentRoom].APet.MaxHealth());
            hungerA = StatChunks(rooms[currentRoom].APet.Hunger, rooms[currentRoom].APet.MaxHunger());
            moodA = StatChunks(rooms[currentRoom].APet.Mood, rooms[currentRoom].APet.MaxMood());
            healthB = StatEmpty(rooms[currentRoom].APet.Health, rooms[currentRoom].APet.MaxHealth());
            hungerB = StatEmpty(rooms[currentRoom].APet.Hunger, rooms[currentRoom].APet.MaxHunger());
            moodB = StatEmpty(rooms[currentRoom].APet.Mood, rooms[currentRoom].APet.MaxMood());

            //write out the UI
            AppHelpers.WriteAt(@" _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ ", 0, 0);
            AppHelpers.WriteAt(@"|                                             |", 0, 1);
            AppHelpers.WriteAt($@"|  Room: {roomNo}    Pet:  {petName}    Coins: {coinCount}  |", 0, 2);
            AppHelpers.WriteAt(@"|                                  _ _ _ _    |", 0, 3);

            AppHelpers.WriteAt(@"|  Health: ", 0, 4);
            StatBar(healthA, "Health");
            Console.Write(healthB);
            AppHelpers.WriteAt(@"   |       |   |", 31, 4);

            AppHelpers.WriteAt(@"|  Mood  : ", 0, 5);
            StatBar(moodA, "Mood");
            Console.Write(moodB);
            AppHelpers.WriteAt($@"   | {tempStatus}  |   |", 31, 5);

            AppHelpers.WriteAt(@"|  Hunger: ", 0, 6);
            StatBar(hungerA, "Hunger");
            Console.Write(hungerB);
            AppHelpers.WriteAt(@"   |_ _ _ _|   |", 31, 6);

            AppHelpers.WriteAt(@"|_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _|", 0, 7);
        }

        void DisplayRoom(bool main)
        {
            if (main == true)
            {
                if (rooms[currentRoom].APet.Health > 0)
                {
                    AppHelpers.WriteAt(@"|                                             |", 0, 21);
                    AppHelpers.WriteAt(@"|  What would you like to do?:                |", 0, 22);
                    AppHelpers.WriteAt(@"|     1. Change Heating                       |", 0, 23);
                    AppHelpers.WriteAt(@"|     2. View Items                           |", 0, 24);
                    AppHelpers.WriteAt(@"|     3. View Shop                            |", 0, 25);
                    AppHelpers.WriteAt(@"|                                             |", 0, 26);
                    AppHelpers.WriteAt(@"|     (Press Esc to save and exit)            |", 0, 27);
                    AppHelpers.WriteAt(@"|                                             |", 0, 28);
                    AppHelpers.WriteAt(@" ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ", 0, 29);
                }
                else
                {
                    AppHelpers.WriteAt(@"|                                             |", 0, 21);
                    AppHelpers.WriteAt(@"|     Your pet has died...                    |", 0, 22);
                    AppHelpers.WriteAt(@"|                                             |", 0, 23);
                    AppHelpers.WriteAt(@"|     Press 1 to generate a new pet.          |", 0, 24);
                    AppHelpers.WriteAt(@"|                                             |", 0, 25);
                    AppHelpers.WriteAt(@"|                                             |", 0, 26);
                    AppHelpers.WriteAt(@"|     (Press Esc to save and exit)            |", 0, 27);
                    AppHelpers.WriteAt(@"|                                             |", 0, 28);
                    AppHelpers.WriteAt(@" ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ", 0, 29);
                }
            }
            else
            {
                AppHelpers.WriteAt(@"|                                             |", 0, 21);
                AppHelpers.WriteAt(@"|  What would you like to do?:                |", 0, 22);
                AppHelpers.WriteAt(@"|     1. Turn on heating                      |", 0, 23);
                AppHelpers.WriteAt(@"|     2. Turn on air conditioning             |", 0, 24);
                AppHelpers.WriteAt(@"|     3. Turn off vents                       |", 0, 25);
                AppHelpers.WriteAt(@"|                                             |", 0, 26);
                AppHelpers.WriteAt(@"|     (Press Esc to go back)                  |", 0, 27);
                AppHelpers.WriteAt(@"|                                             |", 0, 28);
                AppHelpers.WriteAt(@" ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ", 0, 29);
            }
        
            rooms[currentRoom].Display();
        }

        void DisplayInventory()
        {
            #region - set quantity variables
            Resource result = inventory.Find(x => x.Name == "Ball");
            string ballCount = String.Format("{0,2}", Convert.ToString(result.Quantity));
            result = inventory.Find(x => x.Name == "Stick");
            string stickCount = String.Format("{0,2}", Convert.ToString(result.Quantity));
            result = inventory.Find(x => x.Name == "Twine");
            string twineCount = String.Format("{0,2}", Convert.ToString(result.Quantity));
            result = inventory.Find(x => x.Name == "Hoop");
            string hoopCount = String.Format("{0,2}", Convert.ToString(result.Quantity));

            result = inventory.Find(x => x.Name == "Meat");
            string meatCount = String.Format("{0,2}", Convert.ToString(result.Quantity));
            result = inventory.Find(x => x.Name == "Fruit");
            string fruitCount = String.Format("{0,2}", Convert.ToString(result.Quantity));
            result = inventory.Find(x => x.Name == "Plant");
            string plantCount = String.Format("{0,2}", Convert.ToString(result.Quantity));
            result = inventory.Find(x => x.Name == "Fish");
            string fishCount = String.Format("{0,2}", Convert.ToString(result.Quantity));

            result = inventory.Find(x => x.Name == "Sml");
            string smlCount = String.Format("{0,2}", Convert.ToString(result.Quantity));
            result = inventory.Find(x => x.Name == "Med");
            string medCount = String.Format("{0,2}", Convert.ToString(result.Quantity));
            result = inventory.Find(x => x.Name == "Lrg");
            string lrgCount = String.Format("{0,2}", Convert.ToString(result.Quantity));
            result = inventory.Find(x => x.Name == "Full");
            string fullCount = String.Format("{0,2}", Convert.ToString(result.Quantity));
            #endregion

            if (main == true)
            {
                AppHelpers.WriteAt(@"|INVENTORY                                    |", 0, 8);
                AppHelpers.WriteAt(@"|‾ ‾ ‾ ‾ ‾ ‾ ‾ |‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾|‾ ‾ ‾ ‾ ‾ ‾ ‾ |", 0, 9);
                AppHelpers.WriteAt(@"|   (1) Toy    |   (2) Food    | (3) Medicine |", 0, 10);
                AppHelpers.WriteAt(@"|_ _ _ _ _ _ _ |_ _ _ _ _ _ _ _|_ _ _ _ _ _ _ |", 0, 11);
                AppHelpers.WriteAt(@"|              |               |              |", 0, 12);
                AppHelpers.WriteAt($@"|    Ball  x{ballCount} |    Meat   x{meatCount} |     Sml  x{smlCount} |", 0, 13);
                AppHelpers.WriteAt(@"|              |               |              |", 0, 14);
                AppHelpers.WriteAt($@"|    Stick x{stickCount} |    Fruit  x{fruitCount} |     Med  x{medCount} |", 0, 15);
                AppHelpers.WriteAt(@"|              |               |              |", 0, 16);
                AppHelpers.WriteAt($@"|    Twine x{twineCount} |    Plant  x{plantCount} |     Lrg  x{lrgCount} |", 0, 17);
                AppHelpers.WriteAt(@"|              |               |              |", 0, 18);
                AppHelpers.WriteAt($@"|    Hoop  x{hoopCount} |    Fish   x{fishCount} |     Full x{fullCount} |", 0, 19);
                AppHelpers.WriteAt(@"|_ _ _ _ _ _ _ |_ _ _ _ _ _ _ _|_ _ _ _ _ _ _ |", 0, 20);
                AppHelpers.WriteAt(@"|                                             |", 0, 21);
                AppHelpers.WriteAt(@"|  What would you like to use?:               |", 0, 22);
                AppHelpers.WriteAt(@"|                                             |", 0, 23);
                AppHelpers.WriteAt(@"|                                             |", 0, 24);
                AppHelpers.WriteAt(@"|                                             |", 0, 25);
                AppHelpers.WriteAt(@"|                                             |", 0, 26);
                AppHelpers.WriteAt(@"|     (Press Esc to go back)                  |", 0, 27);
                AppHelpers.WriteAt(@"|                                             |", 0, 28);
                AppHelpers.WriteAt(@" ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ", 0, 29);

                Console.SetCursorPosition(32, 22);
            }
            else
            {
                AppHelpers.WriteAt(@"|INVENTORY                                    |", 0, 8);
                AppHelpers.WriteAt(@"|‾ ‾ ‾ ‾ ‾ ‾ ‾ |‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾|‾ ‾ ‾ ‾ ‾ ‾ ‾ |", 0, 9);
                AppHelpers.WriteAt(@"|     Toy      |     Food      |   Medicine   |", 0, 10);
                AppHelpers.WriteAt(@"|_ _ _ _ _ _ _ |_ _ _ _ _ _ _ _|_ _ _ _ _ _ _ |", 0, 11);
                AppHelpers.WriteAt(@"|              |               |              |", 0, 12);
                AppHelpers.WriteAt($@"| 1. Ball  x{ballCount} | 1. Meat   x{meatCount} |  1. Sml  x{smlCount} |", 0, 13);
                AppHelpers.WriteAt(@"|              |               |              |", 0, 14);
                AppHelpers.WriteAt($@"| 2. Stick x{stickCount} | 2. Fruit  x{fruitCount} |  2. Med  x{medCount} |", 0, 15);
                AppHelpers.WriteAt(@"|              |               |              |", 0, 16);
                AppHelpers.WriteAt($@"| 3. Twine x{twineCount} | 3. Plant  x{plantCount} |  3. Lrg  x{lrgCount} |", 0, 17);
                AppHelpers.WriteAt(@"|              |               |              |", 0, 18);
                AppHelpers.WriteAt($@"| 4. Hoop  x{hoopCount} | 4. Fish   x{fishCount} |  4. Full x{fullCount} |", 0, 19);
                AppHelpers.WriteAt(@"|_ _ _ _ _ _ _ |_ _ _ _ _ _ _ _|_ _ _ _ _ _ _ |", 0, 20);
                AppHelpers.WriteAt(@"|                                             |", 0, 21);
                AppHelpers.WriteAt(@"|  Which item will you use?:                  |", 0, 22);
                AppHelpers.WriteAt(@"|                                             |", 0, 23);
                AppHelpers.WriteAt(@"|                                             |", 0, 24);
                AppHelpers.WriteAt(@"|                                             |", 0, 25);
                AppHelpers.WriteAt(@"|                                             |", 0, 26);
                AppHelpers.WriteAt(@"|     (Press Esc to go back)                  |", 0, 27);
                AppHelpers.WriteAt(@"|                                             |", 0, 28);
                AppHelpers.WriteAt(@" ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ", 0, 29);

                Console.SetCursorPosition(32, 22);

            }
            
            Thread.Sleep(1000);
        }

        void DisplayShop(bool main)
        {
            if (main == true)
            {
                AppHelpers.WriteAt(@"|SHOP:                                        |", 0, 8);
                AppHelpers.WriteAt(@"|‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾|", 0, 9);
                AppHelpers.WriteAt(@"|                                             |", 0, 10);
                AppHelpers.WriteAt(@"|                                             |", 0, 11);
                AppHelpers.WriteAt(@"|                                             |", 0, 12);
                AppHelpers.WriteAt(@"|                                             |", 0, 13);
                AppHelpers.WriteAt(@"|          Please Select An Option.           |", 0, 14);
                AppHelpers.WriteAt(@"|                                             |", 0, 15);
                AppHelpers.WriteAt(@"|                                             |", 0, 16);
                AppHelpers.WriteAt(@"|                                             |", 0, 17);
                AppHelpers.WriteAt(@"|                                             |", 0, 18);
                AppHelpers.WriteAt(@"|                                             |", 0, 19);
                AppHelpers.WriteAt(@"|_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _|", 0, 20);
                AppHelpers.WriteAt(@"|                                             |", 0, 21);
                AppHelpers.WriteAt(@"|  What would you like to buy?:               |", 0, 22);
                AppHelpers.WriteAt(@"|     1. Toy                                  |", 0, 23);
                AppHelpers.WriteAt(@"|     2. Food                                 |", 0, 24);
                AppHelpers.WriteAt(@"|     3. Medicine                             |", 0, 25);
                AppHelpers.WriteAt(@"|                                             |", 0, 26);
                AppHelpers.WriteAt(@"|     (Press Esc to go back)                  |", 0, 27);
                AppHelpers.WriteAt(@"|                                             |", 0, 28);
                AppHelpers.WriteAt(@" ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ", 0, 29);

                Console.SetCursorPosition(32, 22);
            }

            else
            {
                #region - set ui variables
                string resourceType = String.Format("{0, -8}", selectedShopResource);
                string resourceTypeDescription = String.Format("{0,-41}", AppHelpers.resourcesTypes[selectedShopResource]);

                selectedShopResourceList.Clear(); //clear any old selections

                foreach (Resource r in shop)
                {
                    if (Convert.ToString(r.GetType()) == "ObjectOrientatedProgramming_Assessment011."+selectedShopResource)
                    {
                        selectedShopResourceList.Add(r);
                    }
                }

                //set the variables from the list
                string resource1 = String.Format("{0, -5}", selectedShopResourceList[0].Name);
                string resource2 = String.Format("{0, -5}", selectedShopResourceList[1].Name);
                string resource3 = String.Format("{0, -5}", selectedShopResourceList[2].Name);
                string resource4 = String.Format("{0, -5}", selectedShopResourceList[3].Name);

                string use1 = String.Format("{0, 2}", selectedShopResourceList[0].Uses);
                string use2 = String.Format("{0, 2}", selectedShopResourceList[1].Uses);
                string use3 = String.Format("{0, 2}", selectedShopResourceList[2].Uses);
                string use4 = String.Format("{0, 2}", selectedShopResourceList[3].Uses);

                string cost1 = String.Format("{0, 3}", selectedShopResourceList[0].Cost);
                string cost2 = String.Format("{0, 3}", selectedShopResourceList[1].Cost);
                string cost3 = String.Format("{0, 3}", selectedShopResourceList[2].Cost);
                string cost4 = String.Format("{0, 3}", selectedShopResourceList[3].Cost);
                #endregion

                AppHelpers.WriteAt($@"|SHOP: {resourceType}                               |", 0, 8);
                AppHelpers.WriteAt(@"|‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾|", 0, 9);
                AppHelpers.WriteAt($@"|  {resourceTypeDescription}  |", 0, 10);
                AppHelpers.WriteAt(@"|                                             |", 0, 11);
                AppHelpers.WriteAt($@"|  1.  {resource1}  {use1} uses              Cost: {cost1}  |", 0, 12);
                AppHelpers.WriteAt(@"|                                             |", 0, 13);
                AppHelpers.WriteAt($@"|  2.  {resource2}  {use2} uses              Cost: {cost2}  |", 0, 14);
                AppHelpers.WriteAt(@"|                                             |", 0, 15);
                AppHelpers.WriteAt($@"|  3.  {resource3}  {use3} uses              Cost: {cost3}  |", 0, 16);
                AppHelpers.WriteAt(@"|                                             |", 0, 17);
                AppHelpers.WriteAt($@"|  4.  {resource4}  {use4} uses              Cost: {cost4}  |", 0, 18);
                AppHelpers.WriteAt(@"|                                             |", 0, 19);
                AppHelpers.WriteAt(@"|_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _|", 0, 20);
                AppHelpers.WriteAt(@"|                                             |", 0, 21);
                AppHelpers.WriteAt(@"|  What would you like to buy?:               |", 0, 22);
                AppHelpers.WriteAt(@"|                                             |", 0, 23);
                AppHelpers.WriteAt(@"|                                             |", 0, 24);
                AppHelpers.WriteAt(@"|                                             |", 0, 25);
                AppHelpers.WriteAt(@"|                                             |", 0, 26);
                AppHelpers.WriteAt(@"|     (Press Esc to go back)                  |", 0, 27);
                AppHelpers.WriteAt(@"|                                             |", 0, 28);
                AppHelpers.WriteAt(@" ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ", 0, 29);

                Console.SetCursorPosition(32, 22);
            }
            
            Thread.Sleep(1000);
        }
        #endregion

        void IncreaseCoins()
        {
            coins++;
        }

        void BuyItems(int i)
        {
            if (coins >= selectedShopResourceList[i - 1].Cost)
            {
                //inventory element with index [find index of one that has the same name as selected], increase Quantity by 1
                inventory[inventory.FindIndex(x => x.Name == selectedShopResourceList[i - 1].Name)].Quantity++;
                //remove the coins
                coins -= selectedShopResourceList[i - 1].Cost;

                AppHelpers.WriteAt(@"|                                             |", 0, 21);
                AppHelpers.WriteAt(@"|                                             |", 0, 22);
                AppHelpers.WriteAt(@"|                                             |", 0, 23);
                AppHelpers.WriteAt(@"|       Purchase Successful! Thank You!       |", 0, 24);
                AppHelpers.WriteAt(@"|                                             |", 0, 25);
                AppHelpers.WriteAt(@"|                                             |", 0, 26);
                AppHelpers.WriteAt(@"|                                             |", 0, 27);
                AppHelpers.WriteAt(@"|                                             |", 0, 28);
                AppHelpers.WriteAt(@" ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ", 0, 29);

                Thread.Sleep(500);
            }
            else
            {
                AppHelpers.WriteAt(@"|                                             |", 0, 21);
                AppHelpers.WriteAt(@"|                                             |", 0, 22);
                AppHelpers.WriteAt(@"|                                             |", 0, 23);
                AppHelpers.WriteAt(@"|      Sorry! Not enough coins for that.      |", 0, 24);
                AppHelpers.WriteAt(@"|                                             |", 0, 25);
                AppHelpers.WriteAt(@"|                                             |", 0, 26);
                AppHelpers.WriteAt(@"|                                             |", 0, 27);
                AppHelpers.WriteAt(@"|                                             |", 0, 28);
                AppHelpers.WriteAt(@" ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ‾ ", 0, 29);

                Thread.Sleep(500);
            }
        }

        void oneSecUpdate()
        {
            while (running == true)
            {
                Thread.Sleep(1000);

                IncreaseCoins();

                foreach (Room room in rooms.ToList())
                {
                    room.PetIncreaseHunger();
                    room.PetDecreaseMood();
                    room.PetUpdateHealth();
                }
            }
        }

        void fiveSecUpdate()
        {
            while (running == true)
            {
                Thread.Sleep(5000);

                IncreaseCoins();

                foreach (Room room in rooms.ToList())
                {
                    room.UpdateTemp();
                }
            }
        }

        void SwitchMenu(ConsoleKeyInfo keypressed)
        {
            int key = 99;
            try { key = Convert.ToInt32(Convert.ToString(keypressed.KeyChar)); }
            catch { }

            if (currentView == "Room" && main == true) 
            {
                if (keypressed.Key == ConsoleKey.Escape)
                {
                    running = false;
                }

                else if (1 <= key && key <= 3)
                {
                    if (rooms[currentRoom].APet.Health == 0)
                    {
                        if (key == 1)
                        {
                            rooms[currentRoom] = new Room();
                        }
                    }
                    else
                    {
                        if (key == 1) { main = false; }
                        if (key == 2) { currentView = "Inventory"; }
                        if (key == 3) { currentView = "Shop"; }
                    }
                }
            }
            
            else if (currentView == "Room" && main == false) 
            {
                if (keypressed.Key == ConsoleKey.Escape)
                {
                    main = true;
                }

                else if (1 <= key && key <= 3)
                {
                    if (key == 1) { rooms[currentRoom].Heat(); }
                    if (key == 2) { rooms[currentRoom].Cool(); }
                    if (key == 3) { rooms[currentRoom].Stop(); }
                }
            }
                        
            else if (currentView == "Inventory" && main == true)
            {
                if (keypressed.Key == ConsoleKey.Escape)
                {
                    currentView = "Room";
                }

                else if (1 <= key && key <= AppHelpers.resources.Count)
                {
                    selectedInvResource = (key - 1)*4;
                    main = false;
                }
            }

            else if (currentView == "Inventory" && main == false)
            {
                if (keypressed.Key == ConsoleKey.Escape)
                {
                    main = true;
                }

                else if (1 <= key && key <= 4)
                {
                    inventory[selectedInvResource + key - 1] = rooms[currentRoom].PetUseItem(inventory[selectedInvResource + key - 1]);
                }
            }

            else if (currentView == "Shop" && main == true)
            {
                if (keypressed.Key == ConsoleKey.Escape)
                {
                    currentView = "Room";
                }

                else if (1 <= key && key <= AppHelpers.resources.Count)
                {
                    selectedShopResource = AppHelpers.resources[key - 1];
                    main = false;
                }
            }
            
            else if (currentView == "Shop" && main == false)
            {
                if (keypressed.Key == ConsoleKey.Escape)
                {
                    main = true;
                }

                else if (1 <= key && key <= 4)
                {
                    BuyItems(key);
                }
            }
        }

        #region - utility methods
        void StatBar(string bar, string stat) //set the font colour for stat bars
        {
            if (stat == "Health")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(bar);
                Console.ResetColor();
            }
            else if (stat == "Hunger")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(bar);
                Console.ResetColor();
            }
            else if (stat == "Mood")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(bar);
                Console.ResetColor();
            }
        }

        string StatChunks(int current, int max)
        {
            int colour = current * 10 / max;
            string s = "";
            for (int i = 0; i < colour; i++) { s += "██"; }
            return s;
        }

        string StatEmpty(int current, int max)
        {
            int empty = 10 - (current * 10 / max);
            string s = "";
            for (int i = 0; i < empty; i++) { s += "[]"; }
            return s;
        }
        #endregion
    }
}
