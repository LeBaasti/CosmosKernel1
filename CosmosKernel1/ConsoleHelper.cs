using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    public static class ConsoleHelper
    {
        public static void WriteColored(string input)
        {
            var colorCodes = new Dictionary<string, ConsoleColor>
            {
                { "<red>", ConsoleColor.Red },
                { "<green>", ConsoleColor.Green },
                { "<gray>", ConsoleColor.Gray },
                { "<magenta>", ConsoleColor.Magenta },
                { "<white>", ConsoleColor.White },
                { "<blue>", ConsoleColor.Blue },
                { "<cyan>", ConsoleColor.Cyan },
                { "<yellow>", ConsoleColor.Yellow }
                // Add more colors here
            };

            int index = 0;
            while (index < input.Length)
            {
                // Check if we find a color code starting with < and not escaped by \
                if (input[index] == '<' && (index == 0 || input[index - 1] != '\\'))
                {
                    // Search for the closing >
                    int endIndex = input.IndexOf('>', index);
                    if (endIndex > index)
                    {
                        string colorCode = input.Substring(index, endIndex - index + 1);

                        // Check if the color code exists in our dictionary
                        if (colorCodes.TryGetValue(colorCode, out ConsoleColor color))
                        {
                            Console.ForegroundColor = color;
                            index = endIndex + 1;
                            continue;
                        }
                    }
                }

                // Handle escaped color codes (e.g., \<green>)
                if (input[index] == '\\' && index + 1 < input.Length && input[index + 1] == '<')
                {
                    Console.Write('<');
                    index += 2;
                    continue;
                }

                // Print current character and move to the next
                Console.Write(input[index]);
                index++;
            }

            // Reset color after finished output
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
