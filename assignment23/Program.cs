using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment23
{     // Delegate to represent a spin action
            delegate void SpinAction(ref int energyLevel, ref int winningProbability);


    internal class Program
    {
    // Event to notify the result of the game
    static event Action<string, int, int, string> GameResultEvent;


    static void Main(string[] args)
        {
            

            
                Console.WriteLine("Welcome to the Spin Game!");

                // Get user input for name and lucky number
                Console.Write("Enter Your Name: ");
                string playerName = Console.ReadLine();

                Console.Write("Enter Your Lucky Number from 1 to 10: ");
                int luckyNumber;
                while (!int.TryParse(Console.ReadLine(), out luckyNumber) || luckyNumber < 1 || luckyNumber > 10)
                {
                    Console.WriteLine("Invalid input. Enter a number between 1 and 10.");
                    Console.Write("Enter Your Lucky Number from 1 to 10: ");
                }

                // Initialize game parameters
                int energyLevel = 1;
                int winningProbability = 100;
                int spinCount = 10; // No of spins

                // Define spin actions
                SpinAction[] spins = {
            (ref int el, ref int wp) => { el += 1; wp += 10; },
            (ref int el, ref int wp) => { el += 2; wp += 20; },
            (ref int el, ref int wp) => { el -= 3; wp -= 30; },
            (ref int el, ref int wp) => { el += 4; wp += 40; },
            (ref int el, ref int wp) => { el += 5; wp += 50; },
            (ref int el, ref int wp) => { el -= 1; wp -= 60; },
            (ref int el, ref int wp) => { el += 1; wp += 70; },
            (ref int el, ref int wp) => { el -= 2; wp -= 20; },
            (ref int el, ref int wp) => { el -= 3; wp -= 30; },
            (ref int el, ref int wp) => { el += 10; wp += 100; }
        };

                // Subscribe to the GameResultEvent
                GameResultEvent += DisplayResult;

                // Perform spins
                for (int i = 0; i < spinCount; i++)
                {
                    SpinWheel(playerName, ref energyLevel, ref winningProbability, spins[i], i + 1);
                }

                // Notify the result
                GameResultEvent?.Invoke(playerName, energyLevel, winningProbability, "Game Result");
            }

            static void SpinWheel(string playerName, ref int energyLevel, ref int winningProbability, SpinAction spin, int spinCount)
            {
                Console.WriteLine($"\nSpin {spinCount}:");

                spin(ref energyLevel, ref winningProbability);

                Console.WriteLine($"Energy Level: {energyLevel}");
                Console.WriteLine($"Winning Probability: {winningProbability}");
            }

            static void DisplayResult(string playerName, int energyLevel, int winningProbability, string resultType)
            {
                Console.WriteLine($"\n{resultType}:");

                if (energyLevel >= 4 && winningProbability > 60)
                {
                    Console.WriteLine($"{playerName} is the Winner!");
                }
                else if (energyLevel >= 1 && winningProbability > 50)
                {
                    Console.WriteLine($"{playerName} is the Runner Up!");
                }
                else
                {
                    Console.WriteLine($"{playerName} is the Looser!");
                }
            }
        

    }
}

