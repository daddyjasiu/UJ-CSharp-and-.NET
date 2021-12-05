using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace Zadanie_1
{
    class Program
    {
        enum EHeroClass
        {
            Barbarzynca,
            Paladyn,
            Amazonka
        }
        static void ShowMenu()
        {
            Console.WriteLine("Witaj w grze Diablo 3!");
            Console.WriteLine("[1] Zacznij nową grę");
            Console.WriteLine("[x] Zamknij program"); 
        }
        static void PickGameOption(Hero hero)
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();

            switch (keyPressed.KeyChar)
            {
                case '1':
                    Console.Clear();
                    Option1Picked(hero);
                    break;
                case 'x':
                    Console.Clear();
                    Console.WriteLine("Do zobaczenia!");
                    Environment.Exit(0);
                    break;
            }
        }
        static bool IsCharacterNameValid(String characterName)
        {
            if (characterName.Length < 2 ||
                !Regex.IsMatch(characterName, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine("Niepoprawna nazwa!");
                Console.WriteLine("Kliknij RETURN aby powtorzyc...");
                Console.ReadKey();
                Console.Clear();
                return false;
            }

            return true;
        }
        static bool IsCharacterClassValid(ConsoleKeyInfo heroClass)
        {
            if (heroClass.KeyChar != '1' &&
                heroClass.KeyChar != '2' &&
                heroClass.KeyChar != '3')
            {
                Console.WriteLine("\nNiepoprawna klasa!");
                Console.WriteLine("Kliknij RETURN aby powtorzyc...");
                Console.ReadKey();
                Console.Clear();
                return false;
            }

            return true;
        }
        static bool IsNPCDialogOptionValid(ConsoleKeyInfo heroClass)
        {
            if (heroClass.KeyChar != '1' &&
                heroClass.KeyChar != '2' &&
                heroClass.KeyChar != 't' &&
                heroClass.KeyChar != 'x')
            {
                Console.WriteLine("\nNiepoprawna opcja!");
                Console.WriteLine("Kliknij RETURN aby powtorzyc...");
                Console.ReadKey();
                Console.Clear();
                return false;
            }

            return true;
        }
        static bool IsTravelDestinationValid(ConsoleKeyInfo destinationKey, int validLocationsCount)
        {
            List<char> locNumbers = new(){ '1', '2', '3', '4', '5', '6' };
            locNumbers = locNumbers.GetRange(0, validLocationsCount);
            if (!locNumbers.Contains(destinationKey.KeyChar) &&
                    destinationKey.KeyChar != 'x')
            {
                   Console.WriteLine("\nNiepoprawna opcja!");
                   Console.WriteLine("Kliknij RETURN aby powtorzyc...");
                   Console.ReadKey();
                   Console.Clear();
                   return false;
            }
            
            return true;
        }
        static void EnterCharacterName(Hero hero)
        {
            String characterName;
            do
            {
                Console.Write("Podaj nazwę bohatera: ");
                characterName = Console.ReadLine();
            
            } while (!IsCharacterNameValid(characterName));

            hero.name = characterName;
            Console.Clear();
        }
        static void PickCharacterClass(Hero hero)
        {
            ConsoleKeyInfo keyPressed;
            do
            {
                Console.WriteLine("Witaj " + hero.name + "!");
                Console.WriteLine("Wybierz klase bohatera:");
                Console.WriteLine("1. Barbarzynca");
                Console.WriteLine("2. Paladyn");
                Console.WriteLine("3. Amazonka");
                keyPressed = Console.ReadKey();
            
            } while (!IsCharacterClassValid(keyPressed));

            switch (keyPressed.KeyChar)
            {
                case '1':
                    hero.heroClass = EHeroClass.Barbarzynca.ToString();
                    break;
                case '2':
                    hero.heroClass = EHeroClass.Paladyn.ToString();
                    break;
                case '3':
                    hero.heroClass = EHeroClass.Amazonka.ToString();
                    break;
            }
            
            Console.Clear();
        }
        static void CharacterCreationFinished(Hero hero)
        {
            Console.WriteLine($"{hero.heroClass} {hero.name} wyrusza na przygode!");
            Console.WriteLine("\nWcisnij dowolny przycisk aby isc dalej...");
            Console.ReadKey();
            Console.Clear();
        }
        static KeyValuePair<List<NPCDialogPart>, List<NPCDialogPart>> SetNPCAndHeroDialogs()
        {
            //Creating fixed text arrays for dialogs
            String[] npc1Texts =
            {
                "Witaj #HERONAME#, czy mozesz mi pomóc dostac się do innego miasta?",
                "Dziękuje! W nagrode otrzymasz ode mnie 100 sztuk zlota.",
                "Niestety nie mam wiecej. Jestem bardzo biedny...",
                "Dziekuje."
            };
            
            String[] hero1Texts =
            {
                "Tak, chetnie pomoge.",
                "Nie, nie pomoge, zegnaj.",
                "Dam znac jak bede gotowy.",
                "100 sztuk zlota to za malo!",
                "Ok, moze byc 100 sztuk zlota.",
                "W takim razie radz sobie sam.",
            };

            String[] npc2Texts =
            {
                "Witaj #HERONAME#, czy pomozesz mi znalezc mojego smoka?",
                "Dziękuje! W nagrode otrzymasz ode mnie jego jajo, bedziesz mogl wykluc i wychowac wlasnego pupilka!",
                "Niestety, nie moge zaoferowac Ci nic innego...",
                "Dziekuje i czekam na Twoja gotowosc!"
            };

            String[] hero2Texts =
            {
                "Tak, pomoge Ci.",
                "Nie mam czasu, spadaj.",
                "Super! Nie moge sie doczekac.",
                "Po co mi Twoje jajo? Chce cos innego.",
                "Ehh... No dobra, niech bedzie, pomoge Ci.",
                "W takim razie zejdz mi z oczu biedaku.",
            };

            //Feeding DPs with correct texts
            HeroDialogPart hdp1IWillHelp = new HeroDialogPart();
            hdp1IWillHelp.text = hero1Texts[0];
            HeroDialogPart hdp1IWillNotHelpEND = new HeroDialogPart();
            hdp1IWillNotHelpEND.text = hero1Texts[1];
            HeroDialogPart hdp1IWillLetUKnowEND = new HeroDialogPart();
            hdp1IWillLetUKnowEND.text = hero1Texts[2];
            HeroDialogPart hdp1INeedMoreMoney = new HeroDialogPart();
            hdp1INeedMoreMoney.text = hero1Texts[3];
            HeroDialogPart hdp1TheMoneyIsFine = new HeroDialogPart();
            hdp1TheMoneyIsFine.text = hero1Texts[4];
            HeroDialogPart hdp1YoureOnYourOwnEND = new HeroDialogPart();
            hdp1YoureOnYourOwnEND.text = hero1Texts[5];

            NPCDialogPart npcdp1Welcome = new NPCDialogPart();
            npcdp1Welcome.text = npc1Texts[0];
            NPCDialogPart npcdp1ThankYouHereIsGold = new NPCDialogPart();
            npcdp1ThankYouHereIsGold.text = npc1Texts[1];
            NPCDialogPart npcdp1SorryButImPoor = new NPCDialogPart();
            npcdp1SorryButImPoor.text = npc1Texts[2];
            NPCDialogPart npcdp1ThankYouByeEND = new NPCDialogPart();
            npcdp1ThankYouByeEND.text = npc1Texts[3];
            
            npcdp1Welcome.heroDialogParts.Add(hdp1IWillHelp);
            npcdp1Welcome.heroDialogParts.Add(hdp1IWillNotHelpEND);
            npcdp1ThankYouHereIsGold.heroDialogParts.Add(hdp1IWillLetUKnowEND);
            npcdp1ThankYouHereIsGold.heroDialogParts.Add(hdp1INeedMoreMoney);
            npcdp1SorryButImPoor.heroDialogParts.Add(hdp1TheMoneyIsFine);
            npcdp1SorryButImPoor.heroDialogParts.Add(hdp1YoureOnYourOwnEND);

            hdp1IWillHelp.nextNPCDialogPart = npcdp1ThankYouHereIsGold;
            hdp1INeedMoreMoney.nextNPCDialogPart = npcdp1SorryButImPoor;
            hdp1TheMoneyIsFine.nextNPCDialogPart = npcdp1ThankYouByeEND;

            List<NPCDialogPart> npc1DPs = new List<NPCDialogPart>();
            npc1DPs.Add(npcdp1Welcome);
            npc1DPs.Add(npcdp1ThankYouHereIsGold);
            npc1DPs.Add(npcdp1SorryButImPoor);
            npc1DPs.Add(npcdp1ThankYouByeEND);
            
            HeroDialogPart hdp2IWillHelp = new HeroDialogPart();
            hdp2IWillHelp.text = hero2Texts[0];
            HeroDialogPart hdp2IWillNotHelpEND = new HeroDialogPart();
            hdp2IWillNotHelpEND.text = hero2Texts[1];
            HeroDialogPart hdp2IWillLetUKnowEND = new HeroDialogPart();
            hdp2IWillLetUKnowEND.text = hero2Texts[2];
            HeroDialogPart hdp2INeedMoreMoney = new HeroDialogPart();
            hdp2INeedMoreMoney.text = hero2Texts[3];
            HeroDialogPart hdp2TheMoneyIsFine = new HeroDialogPart();
            hdp2TheMoneyIsFine.text = hero2Texts[4];
            HeroDialogPart hdp2YoureOnYourOwnEND = new HeroDialogPart();
            hdp2YoureOnYourOwnEND.text = hero2Texts[5];

            NPCDialogPart npcdp2Welcome = new NPCDialogPart();
            npcdp2Welcome.text = npc2Texts[0];
            NPCDialogPart npcdp2ThankYouHereIsGold = new NPCDialogPart();
            npcdp2ThankYouHereIsGold.text = npc2Texts[1];
            NPCDialogPart npcdp2SorryButImPoor = new NPCDialogPart();
            npcdp2SorryButImPoor.text = npc2Texts[2];
            NPCDialogPart npcdp2ThankYouByeEND = new NPCDialogPart();
            npcdp2ThankYouByeEND.text = npc2Texts[3];
            
            npcdp2Welcome.heroDialogParts.Add(hdp2IWillHelp);
            npcdp2Welcome.heroDialogParts.Add(hdp2IWillNotHelpEND);
            npcdp2ThankYouHereIsGold.heroDialogParts.Add(hdp2IWillLetUKnowEND);
            npcdp2ThankYouHereIsGold.heroDialogParts.Add(hdp2INeedMoreMoney);
            npcdp2SorryButImPoor.heroDialogParts.Add(hdp2TheMoneyIsFine);
            npcdp2SorryButImPoor.heroDialogParts.Add(hdp2YoureOnYourOwnEND);

            hdp2IWillHelp.nextNPCDialogPart = npcdp2ThankYouHereIsGold;
            hdp2INeedMoreMoney.nextNPCDialogPart = npcdp2SorryButImPoor;
            hdp2TheMoneyIsFine.nextNPCDialogPart = npcdp2ThankYouByeEND;

            List<NPCDialogPart> npc2DPs = new List<NPCDialogPart>();
            npc2DPs.Add(npcdp2Welcome);
            npc2DPs.Add(npcdp2ThankYouHereIsGold);
            npc2DPs.Add(npcdp2SorryButImPoor);
            npc2DPs.Add(npcdp2ThankYouByeEND);

            KeyValuePair<List<NPCDialogPart>, List<NPCDialogPart>> dialogsPair = new (npc1DPs, npc2DPs);

            return dialogsPair;
        }
        static void TalkTo(Hero hero, NPC npc, List<NPCDialogPart> npcDPs, List<Location> locations, Location currLocation)
        {
            DialogParser dialogParser = new DialogParser(hero);
            NPCDialogPart npcDialog = npcDPs[0];
            npcDialog.text = dialogParser.ParseDialog(npcDialog);
            //„Witaj, czy możesz mi pomóc dostać się do innego miasta?
            Console.WriteLine($"{npc.name}: {npcDialog.text}");
            //Player options
            for (int i = 0; i < npcDialog.heroDialogParts.Count; i++)
            {
               Console.WriteLine($"[{i+1}] {npcDialog.heroDialogParts[i].text}");
            }
            
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            Console.WriteLine();
            switch(keyPressed.KeyChar)
            {   //Tak, chętnie pomogę.
                case '1':
                {
                    //Dziękuję! W nagrodę otrzymasz ode mnie 100 sztuk złota.
                    npcDialog = npcDialog.heroDialogParts[0].nextNPCDialogPart;
                    Console.WriteLine(npcDialog.text);
                    
                    for (int i = 0; i < npcDialog.heroDialogParts.Count; i++)
                    {
                        Console.WriteLine($"[{i+1}] {npcDialog.heroDialogParts[i].text}");
                    }
                    keyPressed = Console.ReadKey();
                    Console.WriteLine();
                    switch (keyPressed.KeyChar)
                    {
                        //Dam znać jak będę gotowy <END>
                        case '1':
                        {
                            Console.Clear();
                            NPCDialogMenu(hero, locations, currLocation);
                        }
                            break;
                        //100 sztuk złota to za mało!
                        case '2':
                        {
                            //„Niestety nie mam więcej. Jestem bardzo biedny
                            npcDialog = npcDialog.heroDialogParts[1].nextNPCDialogPart;
                            Console.WriteLine(npcDialog.text);
                            
                            for (int i = 0; i < npcDialog.heroDialogParts.Count; i++)
                            {
                                Console.WriteLine($"[{i+1}] {npcDialog.heroDialogParts[i].text}");
                            }
                            keyPressed = Console.ReadKey();
                            Console.WriteLine();
                            switch (keyPressed.KeyChar)
                            {
                                //OK, może być 100 sztuk złota.
                                case '1':
                                {
                                    npcDialog = npcDialog.heroDialogParts[0].nextNPCDialogPart;
                                    Console.WriteLine(npcDialog.text);
                                    Console.WriteLine("\nWcisnij dowolny przycisk aby kontynuowac...");
                                    Console.ReadKey();
                                    Console.Clear();
                                    NPCDialogMenu(hero, locations, currLocation);
                                }
                                    break;
                                //W takim razie radź sobie sam. <END>
                                case '2':
                                {
                                    Console.Clear();
                                    NPCDialogMenu(hero, locations, currLocation);
                                }
                                    break;
                            }
                        }
                            break;
                    }
                }
                    break;
                //Nie, nie pomogę, żegnaj. <END>
                case '2':
                {
                    Console.Clear();
                    NPCDialogMenu(hero, locations, currLocation);
                }
                    break;
            }

            Console.ReadKey();
        }
        static void TravelTo(Hero hero, List<Location> locations, Location currLocation)
        {
            var locationQuery = locations
                .Select(q => q)
                .Where(q => q.name != currLocation.name && q.isUnlocked == true)
                .OrderBy(q => q.name)
                .ToList();
            var validLocationsCount = locationQuery.Count;

            ConsoleKeyInfo keyPressed;
            do
            {
                Console.WriteLine($"Znajdujesz sie w: {currLocation.name}. Gdzie chcesz wyruszyc?");
                int i = 1;
                foreach (var loc in locationQuery)
                {
                    Console.WriteLine("[{0}] {1}", i++, loc.name);
                }
                
                Console.WriteLine("[x] Powrot");
                keyPressed = Console.ReadKey();
            } while (!IsTravelDestinationValid(keyPressed, validLocationsCount));
            
            Console.Clear();
            switch (keyPressed.KeyChar)
            {
                case 'x':
                  NPCDialogMenu(hero, locations, currLocation);
                  break;
                default:
                    currLocation = locationQuery[Convert.ToInt32(keyPressed.KeyChar - '0')-1];
                    NPCDialogMenu(hero, locations, currLocation);
                    break;
            }
            
        }
        static void NPCDialogMenu(Hero hero, List<Location> locations, Location currLocation)
        {
            ConsoleKeyInfo keyPressed;
            do{
                Console.WriteLine($"Znajdujesz sie w: {currLocation.name}. Co chcesz zrobic?");
                Console.WriteLine($"[1] Porozmawiaj z {currLocation.npcsList[0].name}");
                Console.WriteLine($"[2] Porozmawiaj z {currLocation.npcsList[1].name}");
                Console.WriteLine("[t] Podrozuj");
                Console.WriteLine("[x] Zamknij program");
                keyPressed = Console.ReadKey();
            } while (!IsNPCDialogOptionValid(keyPressed));
            
            Console.Clear();
            KeyValuePair<List<NPCDialogPart>, List<NPCDialogPart>> dialogsPair = SetNPCAndHeroDialogs();

            List<NPCDialogPart> npc1DPs = dialogsPair.Key;
            List<NPCDialogPart> npc2DPs = dialogsPair.Value;

            switch (keyPressed.KeyChar)
            {
                case '1':
                    TalkTo(hero, currLocation.npcsList[0], npc1DPs, locations, currLocation);
                    break;
                case '2':
                    TalkTo(hero, currLocation.npcsList[1], npc2DPs, locations, currLocation);
                    break;
                case 't':
                    TravelTo(hero, locations, currLocation);
                    break;
                case 'x':
                    Console.Clear();
                    Console.WriteLine($"Do zobaczenia {hero.name}!");
                    Environment.Exit(0);
                    break;
            }
            
            Console.Clear();
        }
        static void EnterTownMenu(Hero hero)
        {
            Location location1 = new Location("Silver City", true);
            Location location2 = new Location("Rookguard", true);
            Location location3 = new Location("CH1 M1", false);
            Location location4 = new Location("Summoner's Rift", true);
            Location location5 = new Location("Runeterra", false);
            Location location6 = new Location("Bruh City", true);
            Location currLocation = location1;
            var locations = new List<Location>()
            {
                location1,
                location2,
                location3,
                location4,
                location5,
                location6
            };
            NPCDialogMenu(hero, locations, currLocation);
        }
        static void Option1Picked(Hero hero)
        {
            EnterCharacterName(hero);
            PickCharacterClass(hero);
            CharacterCreationFinished(hero);
            EnterTownMenu(hero);
        }
        
        static void Main(string[] args)
        {
            Hero hero = new Hero();
            
            ShowMenu();
            PickGameOption(hero);

            Console.ReadKey();
        }
    }
    
}