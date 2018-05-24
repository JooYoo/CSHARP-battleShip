using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BattleShip.DataContracts;
using WMPLib;

namespace BattleShip.Implementations
{
    public class EndGameManager
    {
        public static void WhoWin(Player player, Player computer, WindowsMediaPlayer bgm, IShootManager shootManager)
        {
            Console.Clear();

            //Player win
            if (true) // test
            //if (shootManager.IsAllShipsSunken(computer.Ships))
            {
                // On Winner Sound
                SoundEffects.WinnerSoundPlayer(bgm);
                // Animation
                //GraphicManager.WelcomeScreen();

                //TypeMaschine Sound
                WindowsMediaPlayer typeSound = new WindowsMediaPlayer();
                SoundEffects.TypeSoundPlayer(typeSound);

                // Developer Names
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("                                               ");
                TypeMaschine("Programmer: Yu Zhu");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("                                               ");
                TypeMaschine("Ausbilder: Maximilian Köpf");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("                                               ");
                TypeMaschine("Ausbilder: Markus Binder");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("                                               ");
                TypeMaschine("@ Artiso Solutions");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("                                               ");
                TypeMaschine("2017.3......");

                typeSound.close();


            }
            else if (shootManager.IsAllShipsSunken(player.Ships)) // Computer Win
            {
                // On Loser Sound
                SoundEffects.LoserSoundPlayer();
                // Loser View
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("                                               ");
                Console.Write(" YOU DIE...");
                Thread.Sleep(900);
                Console.Write(" ...");
                Thread.Sleep(900);
                Console.Write(" ...");
                Thread.Sleep(900);
                Console.Write(" ...");
                Thread.Sleep(900);
                Console.Write(" ...");
                Thread.Sleep(900);
                Console.Write(" ...");
                Thread.Sleep(900);
                Console.Write(" ...");
                Thread.Sleep(900);
                Console.Write(" ...");
                Thread.Sleep(1500);
                Console.ReadKey();
                Console.BackgroundColor = ConsoleColor.Black;
            }

           


        }

        private static void TypeMaschine(string text)
        {
            Random r = new Random();
            for (int i = 0; i < text.Length; i++)
            {
                int timeSpan = r.Next(100, 200);
                Console.Write(text[i]);
                Thread.Sleep(timeSpan);
            }
        }


        public static void RestartGame(WindowsMediaPlayer bgm)
        {
            string result;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("                                               Play again ?");
                Console.Write("                                             [y]es or [n]o >");
                result = (Console.ReadLine()).ToLower();
                if (result == "y")
                {
                    Console.WriteLine();
                    Console.Write("                                        Press Enter to Play Again >");
                    Console.ReadKey();
                    // Off BGM
                    bgm.close();
                    // Call Main() to restart the Console App
                    Program.Main();
                }
                else if (result == "n")
                {
                    Console.WriteLine();
                    Console.WriteLine("                                           Thanks for Playing :)");
                    Console.ReadKey();
                }

            } while (result != "y" && result != "n");
        }
    }
}
