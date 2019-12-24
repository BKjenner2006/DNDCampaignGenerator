using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace DNDTest
{
    class Program
    {
        private static List<Character> PlayerCharacters = new List<Character>(); //TODO: Save character data to a file or db

        private static void Main(string[] args)
        {
            bool exit = false;
            Console.WriteLine("##############################################");
            Console.WriteLine("##############################################");
            Console.WriteLine("###Dungeons and Dragons campaign generator.###");
            Console.WriteLine("##############################################");
            Console.WriteLine("##############################################");
            while (!exit)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("1. Create a character");
                Console.WriteLine("2. Start a new campaign");
                Console.WriteLine("3. Test pseudo-rng");
                Console.WriteLine("4. Exit program");
                Console.WriteLine("------------------------");
                Console.WriteLine("Choose an option...:");

                switch (Console.ReadLine())
                {
                    case "1":
                        PlayerCharacters.Add(CreateCharacter()); break;
                    case "2":
                        NewCampaign(); break;
                    case "3":
                        RNGTest(); break;
                    case "4":
                        exit = true; break;
                }

                Console.Clear();
            }
        }

        private static Character CreateCharacter()
        {
            Console.Clear();
            Console.WriteLine("Choose a race...");
            Console.WriteLine("-----------------");

            int raceCount = 1;
            var races = (RaceList[])Enum.GetValues(typeof(RaceList));
            foreach (var race in races)
            {
                Console.WriteLine(raceCount + ". " + race.ToString().Replace('_', '-'));
                raceCount++;
            }

            Console.WriteLine("-----------------");
            int selection;
            while (!Int32.TryParse(Console.ReadLine(), out selection) || selection == 0 || selection > races.Length)
            {
                Console.WriteLine("Invalid Selection, please try again...");
            };

            Console.Clear();
            Console.WriteLine("You have selected " + races[selection - 1].ToString().Replace('_', '-'));
            //Parse the race name from the enum value
            var myRace = (IRace)Activator.CreateInstance(Type.GetType("DNDTest." + races[selection - 1].ToString().Replace("_", "")));

            Console.WriteLine("Choose a class...");
            Console.WriteLine("-----------------");

            int classCount = 1;
            var classes = (ClassList[])Enum.GetValues(typeof(ClassList));
            foreach (var className in classes)
            {
                Console.WriteLine(classCount + ". " + className.ToString());
                classCount++;
            }

            Console.WriteLine("-----------------");

            while (!Int32.TryParse(Console.ReadLine(), out selection) || selection == 0 || selection > classes.Length)
            {
                Console.WriteLine("Invalid Selection, please try again...");
            };

            Console.Clear();
            Console.WriteLine("You have selected " + classes[selection - 1].ToString());
            //Parse the class name from the enum value
            var myClass = (IClass)Activator.CreateInstance(Type.GetType("DNDTest." + classes[selection - 1].ToString()));

            Character result = new Character(myRace, myClass);

            Console.WriteLine("Which would you prefer?");
            Console.WriteLine("----------------------------");
            Console.WriteLine("1. Generate ability scores randomly");
            Console.WriteLine("2. Take standard ability scores");
            Console.WriteLine("----------------------------");

            while(!Int32.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > 2)
            {
                Console.WriteLine("Invalid Selection, please try again...");
            }

            List<int> abilityScores = new List<int>();
            switch (selection)
            {
                case 1:
                    for(int i = 0; i < 6; i++)
                    {
                        List<int> dice = new List<int>();
                        for (int dieCount = 0; dieCount < 4; dieCount++)
                        {
                            dice.Add(RollDie(1, 6));
                        }
                        dice.Remove(dice.Min());
                        abilityScores.Add(dice.Sum());
                    }
                    break;
                case 2:
                    abilityScores.Add(15);
                    abilityScores.Add(14);
                    abilityScores.Add(13);
                    abilityScores.Add(12);
                    abilityScores.Add(10);
                    abilityScores.Add(8);
                    break;
            }

            result = AssignScores(abilityScores, result);

            Console.Clear();
            Console.WriteLine("What is your characters name?");
            Console.Write(">");

            result.Name = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("What is your characters sex?");
            Console.WriteLine("----------------");
            Console.WriteLine("1. Male");
            Console.WriteLine("2. Female");
            Console.WriteLine("----------------");

            var myGender = Console.ReadLine();
            while (myGender.ToUpper().Trim() != "MALE" && myGender.ToUpper().Trim() != "FEMALE" &&
                    (!Int32.TryParse(myGender, out selection) || selection < 1 || selection > 2))
            {
                Console.WriteLine("Invalid selection, please try again...");
                Console.Write(">");
                myGender = Console.ReadLine();
            }
            result.Gender = myGender;

            //TODO: Add recommended height ranges for races
            Console.Clear();
            Console.WriteLine("What is your characters height in inches?");
            Console.Write(">");
            while (!Int32.TryParse(Console.ReadLine(), out selection) || selection < 1)
            {
                Console.WriteLine("Invalid input, please try again...");
                Console.Write(">");
            }
            result.Height = selection;

            Console.Clear();
            Console.WriteLine("What is your characters weight in lbs?");
            Console.Write(">");
            while (!Int32.TryParse(Console.ReadLine(), out selection) || selection < 1)
            {
                Console.WriteLine("Invalid input, please try again...");
                Console.Write(">");
            }
            result.Weight = selection;

            Console.ReadLine();
            return result;
        }

        private static void NewCampaign()
        {
            Console.Clear();
            Console.WriteLine("Would you like to generate a random campaign, or use a seed?");
            Console.WriteLine("--------------------------");
            Console.WriteLine("1.Random campaign");
            Console.WriteLine("2.Seeded campaign");
            Console.WriteLine("--------------------------");

            int selection;
            while (!Int32.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > 2)
            {
                Console.WriteLine("Invalid Selection, please try again...");
            }

            uint seed = (uint)0;
            switch (selection)
            {
                case 1:
                    //Generate random uint seed
                    Random rng = new Random();
                    uint thirtyBits = (uint)rng.Next(1 << 30);
                    uint twoBits = (uint)rng.Next(1 << 2);
                    seed = (thirtyBits << 2) | twoBits;
                    Console.WriteLine("Creating map for seed " + (int)seed + " press any key to continue.");
                    Console.Read();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Please enter a seed number...");
                    Console.Write(">");
                    while (!Int32.TryParse(Console.ReadLine(), out selection) || selection < 1)
                    {
                        Console.WriteLine("Invalid Selection, please try again...");
                    }

                    seed = (uint)selection;
                    break;
            }

            WorldMap campaignMap = new WorldMap(seed);

            PlayCampaign(campaignMap);

            Console.ReadLine();
        }

        private static void PrintMap(WorldMap map)
        {


            int ctr = 1;
            foreach (var location in map.MapGrid)
            {
                Console.Write(location.LocationType + "\t" + (location.LocationType == LocationType.Mountain ? "" : "\t"));

                if (ctr == Math.Sqrt(map.MapGrid.Count))
                {
                    Console.WriteLine("\n");
                    ctr = 0;
                }

                ctr++;
            }
        }

        private static void PlayCampaign(WorldMap map)
        {
            ILocation curLoc = map.MapGrid[0];
            string input = "";

            Console.Clear();
            while (input.Trim().ToUpper() != "EXIT")
            {
                PrintMap(map);

                Console.WriteLine("You are in a " + curLoc.LocationType);
                Console.WriteLine("What do you do?");
                Console.Write(">");

                input = Console.ReadLine();
                Console.Clear();

                if (input.ToUpper().Contains("MOVE") || input.ToUpper().Contains("TRAVEL"))
                {
                    Console.Write("You leave the " + curLoc.LocationType + " and head ");
                    if (input.ToUpper().Contains("NORTH"))
                    {
                        Console.WriteLine("North");
                        curLoc = curLoc.North;
                    }else if (input.ToUpper().Contains("SOUTH"))
                    {
                        Console.WriteLine("South");
                        curLoc = curLoc.South;
                    }else if (input.ToUpper().Contains("EAST"))
                    {
                        Console.WriteLine("East");
                        curLoc = curLoc.East;
                    }else if (input.ToUpper().Contains("WEST"))
                    {
                        Console.WriteLine("West");
                        curLoc = curLoc.West;
                    }

                }
            }
        }

        private static Character AssignScores(List<int> abilityScores, Character result)
        {
            int selection;

            do
            {
                if(abilityScores.Count() == 6)
                    ListScores(abilityScores, "You have generated the following ability scores...");
                else
                    ListScores(abilityScores, "You have the following ability scores remaining...");

                Console.WriteLine();
                switch (abilityScores.Count())
                {
                    case 6: Console.WriteLine("Which score would you like to use on Strength?"); break;
                    case 5: Console.WriteLine("Which score would you like to use on Dexterity?"); break;
                    case 4: Console.WriteLine("Which score would you like to use on Constitution?"); break;
                    case 3: Console.WriteLine("Which score would you like to use on Intelligence?"); break;
                    case 2: Console.WriteLine("Which score would you like to use on Wisdom?"); break;
                    case 1: Console.WriteLine("Which score would you like to use on Charisma?"); break;
                }
                
                while (!Int32.TryParse(Console.ReadLine(), out selection) || !abilityScores.Contains(selection))
                {
                    Console.WriteLine("Invalid Selection, please try again...");
                };
                
                switch (abilityScores.Count())
                {
                    case 6: result.Strength = selection; break;
                    case 5: result.Dexterity = selection; break;
                    case 4: result.Constitution = selection; break;
                    case 3: result.Intelligence = selection; break;
                    case 2: result.Wisdom = selection; break;
                    case 1: result.Charisma = selection; break;
                }
                abilityScores.Remove(selection);
            } while (abilityScores.Count() > 0);

            return result;
        }
        
        private static void ListScores(List<int> scores, string prompt)
        {
            Console.WriteLine(prompt);
            int asCount = 0;
            foreach (var score in scores)
            {
                if (asCount != 0)
                    Console.Write(",");

                Console.Write(score);
                asCount++;
            }
        }

        public static int RollDie(int dieCount, int dieSides)
        {
            Random rng = new Random();
            int result = 0;

            for (int i = 0; i < dieCount; i++)
            {
                result += rng.Next(1, dieSides);
            }

            return result;
        }

        //Test function for Mersenne Twister pseudorandom number generator
        //This will be used to generate the campaigns
        private static void RNGTest()
        {
            Console.Clear();
            Console.WriteLine("Please enter a seed value...");
            Console.Write(">");

            uint seed;
            while(!UInt32.TryParse(Console.ReadLine(), out seed))
            {
                Console.WriteLine("Invalid input, please try again...");
                Console.Write(">");
            }

            PseudoRandom prng = new PseudoRandom(seed);

            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine("" + prng.IRandom(0, 50));
            }

            Console.Read();
        }
    }
}
