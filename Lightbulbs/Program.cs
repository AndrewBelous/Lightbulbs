using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lightbulbs.Hosting;
using System.Diagnostics;
using System.IO;

namespace Lightbulbs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Should I run as a console app (C) or as a Web API service (S)?");
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.S)
            {
                RunServerMode();
            }
            else 
            {
                if (key.Key != ConsoleKey.C)
                    Console.WriteLine("Unrecognized key detected. Running in console mode.");

                RunConsoleMode();
            }
        }

        private static void RunServerMode()
        {
            Console.WriteLine("Starting server...");
            WebApiConfig.Register();
            Console.WriteLine("Server started.");

            string htmlPathRoot = Environment.CurrentDirectory;
            string indexHtmlPath = Path.Combine(htmlPathRoot, @"Views\index.html");
            
            if (File.Exists(indexHtmlPath))
            {
                Console.WriteLine("Launching browser...");
                Process.Start(indexHtmlPath);
                Console.WriteLine("Browser launched.");
            }
            else
            {
                Console.WriteLine("index.html file not found in expected location. " + 
                    "Use 'http://localhost:8080' URL to interact with service.");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);
        }

        private static void RunConsoleMode()
        {
            while (true)
            {
                Console.WriteLine("How many lights should I create?");
                var tmpLights = Console.ReadLine();
                int numOfLights = 100;
                if (!int.TryParse(tmpLights, out numOfLights))
                    Console.WriteLine("Didn't understand your input. Creating 100 lights.");

                Console.WriteLine("How many people should I create?");
                var tmpPeople = Console.ReadLine();
                int numOfPeople = 100;
                if (!int.TryParse(tmpPeople, out numOfPeople))
                    Console.WriteLine("Didn't understand your input. Creating 100 people.");

                Console.WriteLine(numOfPeople + " people are toggling " + numOfLights + " lights. Please be patient.");

                var response = LightBulbManager.Self.ToggleLights(
                    new LightRequest() { NumberOfLights = numOfLights, NumberOfPeople = numOfPeople });
                var lights = response.Results;

                var onCount = lights.Where(l => l == true).Count();

                Console.WriteLine("Everyone left. " + onCount.ToString() + " lights are on.");

                RenderLightResponse(lights);

                Console.WriteLine("Would you like to see the sessions? (Y/N)");
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    var session = LightBulbManager.Self.GetSession(response.SessionId);

                    for (int i = 0; i < session.Steps.Count; i++)
                    {
                        var lightsOn = session.Steps[i].Where(l => l == true).Count();
                        Console.WriteLine("Session " + (i + 1) + ". " + lightsOn + " lights on.");
                        RenderLightResponse(session.Steps[i]);
                    }
                }

                Console.WriteLine("Press Q to quit and any other key to play again.");
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Q) break;
            }   //while

            Console.WriteLine("Thanks for playing!");
        }

        private static void RenderLightResponse(bool[] lights)
        {
            foreach (var l in lights)
            {
                Console.Write("[" + (l ? "X" : " ") + "]");
            }
            Console.WriteLine();
        }
    }   //c
}
