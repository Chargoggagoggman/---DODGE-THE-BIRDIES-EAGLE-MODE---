namespace Dodge_the_Birdies_Eagle_mode
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    class Program
    {
        static void Main()
        {
            Console.WriteLine("*** - DODGE THE BIRDIES ~[ EAGLE MODE ]~ - *** ");
            Console.CursorVisible = false;
            int roadWidth = 20;
            int roadHeight = 25;
            int playerX = roadWidth / 2;
            int playerY = roadHeight - 1;
            int score = 0;
            bool gameOver = false;


            Random rand = new Random();
            List<(int x, int y)> obstacles = new List<(int x, int y)>();

            // Draw static road borders once
            for (int y = 1; y < roadHeight; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write("|");
                Console.SetCursorPosition(roadWidth, y);
                Console.Write("|");
            }

            while (!gameOver)
            {
                // 1. Handle Input
                int oldPlayerX = playerX;
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.LeftArrow && playerX > 1) playerX--;
                    if (key == ConsoleKey.RightArrow && playerX < roadWidth - 1) playerX++;
                }

                // Erase old player position & draw new
                Console.SetCursorPosition(oldPlayerX, playerY);
                Console.Write(" ");
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("O");

                // 2. Spawn & Move Obstacles
                if (rand.Next(0, 3) == 0)
                {
                    obstacles.Add((rand.Next(1, roadWidth), 1));
                }

                for (int i = obstacles.Count - 1; i >= 0; i--)
                {
                    var obs = obstacles[i];

                    // Erase old obstacle position
                    Console.SetCursorPosition(obs.x, obs.y);
                    Console.Write(" ");

                    obs.y++; // Move down

                    // Check collision
                    if (obs.x == playerX && obs.y == playerY)
                    {
                        gameOver = true;
                    }

                    // Draw new position or remove if off-screen
                    if (obs.y < roadHeight)
                    {
                        obstacles[i] = obs;
                        Console.SetCursorPosition(obs.x, obs.y);
                        int letter = rand.Next(1, 3);
                        if (letter == 1)
                        {
                            Console.Write("M");
                        }
                        else
                        {
                            Console.Write("V");
                        }
                    }
                    else
                    {
                        obstacles.RemoveAt(i);
                        score++;
                    }
                }

                // 3. Update Score & Control Speed
                Console.SetCursorPosition(0, roadHeight + 1);
                Console.Write($"Score: {score}");
                Thread.Sleep(Math.Max(20, 75 - score)); // Speeds up as score grows
                if (score > 999)
                {
                    gameOver = true;
                }
            }

            Console.SetCursorPosition(0, roadHeight + 2);
            if (score < 500)
            {
                Console.WriteLine("CRASH! Game Over.");
                Console.WriteLine(score + "/1000");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You win!");
                Console.WriteLine("\\o/");
                Console.WriteLine(" | ");
                Console.WriteLine("/ \\");
                Console.WriteLine();
                Console.WriteLine(score + "/1000");

            }

            Thread.Sleep(2000);
        }
    }
}
