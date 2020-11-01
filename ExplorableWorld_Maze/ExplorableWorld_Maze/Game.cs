using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using Pastel;
using Figgle;
using System.Data.SqlTypes;

namespace ExplorableWorld_Maze
{
    class Game
    {
        private World MyWorld;
        private Player CurrentPlayer;
      
        public void Start() 
        {
             
            Title = "Welcome to the Maze";
            CursorVisible = false;

            //This is a method that parses a file to an array
            string[,] grid = Level_Parser.ParseFileToArray("Level1.txt");
            //string[,] grid = {

            //    { "█", "█", "█" , "█", "█", "█", "█"},
            //    { "█", " ", "█" , " ", " ", " ", "X"},
            //    { " ", " ", "█" , " ", "█", " ", "█"},
            //    { " ", " ", "█" , " ", "█", " ", "█"},
            //    { " ", " ", " " , " ", " ", " ", "█"},
            //    { " ", "█", "█" , "█", " ", "█", "█"},
            //    { " ", "█", "█" , "█", " ", "█", "█"},
            //    { " ", " ", " " , "█", " ", "█", "█"},
            //    { "█", "█", " " , "█", " ", "█", "█"},
            //    { "█", "█", " " , "█", " ", "█", "█"},
            //    { "█", " ", " " , " ", " ", " ", "█"},
            //    { " ", " ", "█" , " ", "█", " ", "█"},
            //    { "█", " ", "█" , " ", "█", " ", "█"},
            //    { "█", " ", " " , " ", "█", " ", "█"},
            //    { "█", "█", "█" , "█", "█", "█", "█"},
            //};
            MyWorld = new World(grid);

            CurrentPlayer = new Player(3, 2);
          
            //Game loop method runs the content of the game
            RunGameLoop();
            
        }

        //I used a figgle plug in to make the font of the intro more dynamic 
        private void DisplayIntro() 
        {
            
            ForegroundColor = ConsoleColor.Magenta;
            WriteLine(FiggleFonts.Crawford.Render("Welcome To"));
            WriteLine(FiggleFonts.Crawford.Render("The Amazing Maze"));
            Console.Beep(100, 5000);
            ResetColor();
            WriteLine("\nInstructions:");
            WriteLine("> Use the arrow keys to move");
            Write("> Try to reach the goal, which looks like this: ");
            ForegroundColor = ConsoleColor.Green;
            WriteLine("X");
            ResetColor();
            WriteLine("> Press any key to start");
            ReadKey(true);
        
        }
        //Here is the method for the conclusion of the game 
        private void DisplayOutro() 
        {

            Clear();
            ForegroundColor = ConsoleColor.Magenta;
            Console.Beep();
            WriteLine(FiggleFonts.Crawford.Render("You escaped!"));
            WriteLine(FiggleFonts.Crawford.Render("Thanks for playing"));
            WriteLine("Press any key to exit");
            WriteLine(" ");
            HighScores();
            //Using the enum here 
            AdditionalHighSchores test = AdditionalHighSchores.Player1;
            Console.WriteLine((int)test);
            ReadKey(true);


        }

        private void DrawFrame()
        {

            Clear();
            MyWorld.Draw();
            CurrentPlayer.Draw();
        }

        private void HandlePlayerInput() 
        {
            //Get only the most recent key input.
            ConsoleKey key;
            do
            {
                ConsoleKeyInfo keyInfo = ReadKey(true);
                key = keyInfo.Key;

            } while (Console.KeyAvailable);

            //Switch statement prevents player from walking out of the maze, off screen, and ultimately crashing the program. 
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X, CurrentPlayer.Y - 1)) 
                    {
                        CurrentPlayer.Y -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X, CurrentPlayer.Y + 1))
                    {
                        CurrentPlayer.Y += 1;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X - 1, CurrentPlayer.Y))
                    {
                        CurrentPlayer.X -= 1;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (MyWorld.IsPositionWalkable(CurrentPlayer.X + 1, CurrentPlayer.Y))
                    {
                        CurrentPlayer.X += 1;
                    }
                    break;
                default:
                    break;
            }
        }
        private void RunGameLoop() 
        {
            DisplayIntro();
            while (true) 
            {

                //Draw everything
                DrawFrame();
                //Check for player input from the keyboard and move the player
                HandlePlayerInput();

                //Check if the player has reached the exit and end the game if so 
                string elementAtPlayerPos = MyWorld.GetElementAt(CurrentPlayer.X, CurrentPlayer.Y);
                if (elementAtPlayerPos == "X")
                {

                    break;
                
                }
            

                //Give the console a chance to render.
                System.Threading.Thread.Sleep(20);
            }
            DisplayOutro();

        }
        //This methos displays the high scores on the out-tro screen by using a list 
        static void HighScores() 
        {

            Console.WriteLine("=====================");
            Console.WriteLine("+++++HIGH SCORES+++++");
            Console.WriteLine("=====================");
            Console.WriteLine("                    ");
            List<int> highScores = new List<int>();
            highScores.Add(34567);
            highScores.Add(12345);
            highScores.Add(11567);
            highScores.Add(11467);
            highScores.Add(10567);
            highScores.Add(9967);
            highScores.Add(5567);
            highScores.Add(3457);
            highScores.Add(2345);
            highScores.Add(1234);

            foreach (int i in highScores)
                Console.WriteLine(i); 

        }
        //This method makes use of an enumerator and places an additional high schore in the high score list at the end. 
        public enum AdditionalHighSchores

        {

            Player1 = 13456,
            Player2 = 34532,
            Player3 = 34536


        }
    }
}
