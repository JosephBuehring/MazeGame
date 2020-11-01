using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExplorableWorld_Maze
{
    class Level_Parser
    {
        //This method reads a text file into a series of strings that it then parses to an array where it has a 
        //pliable set of rows and columns to match whatever the user inputs into the text file 
        public static string[,] ParseFileToArray(string filepath) 
        {

            string[] lines = File.ReadAllLines(filepath);
            string firstLine = lines[0];
            int rows = lines.Length;
            int cols = firstLine.Length;
            string[,] grid = new string[rows, cols];
            for (int y = 0; y < rows; y++) 
            {
                string line = lines[y];
                for (int x = 0; x < cols; x++) 
                {

                    char currentChar = line[x];
                    grid[y, x] = currentChar.ToString(); 
                }
            
            
            
            }

            return grid; 
        }

    }
}
