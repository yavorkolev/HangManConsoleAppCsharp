using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Hangman
    {
        static void Main(string[] args)
        {
            // Guessed letters number
            int guessedNum = 0;
            // win or lose
            bool winner = false;
            // 5 lives(-1=>head, -2=>neck, -3=>hands, -4=>body, -5=>legs)=>hanged
            int lives = 5;
            // ASCII "image" of the hanged man
            char[][] hangedMan =
            {
                new char[] { '_', '_', '_', '_', '_', ' ' },
                new char[] { '|', ' ', ' ', ' ', '|', ' ' },
                new char[] { '|', ' ', ' ', ' ', ' ', ' ' },
                new char[] { '|', ' ', ' ', ' ', ' ', ' ' },
                new char[] { '|', ' ', ' ', ' ', ' ', ' ' },
                new char[] { '|', ' ', ' ', ' ', ' ', ' ' },
                new char[] { '|', ' ', ' ', ' ', ' ', ' ' },
                new char[] { '|', ' ', ' ', ' ', ' ', ' ' },
                new char[] { '|', ' ', ' ', ' ', ' ', ' ' },

            };
            // Word bank for the game
            string[] words = { "Awkward", "Bagpipes", "Banjo", "Bungler", "Croquet", "Crypt", "Dwarves", "Fervid",
                               "Fishhook", "Fjord", "Gazebo", "Gypsy", "Haiku", "Haphazard", "Hyphen", "Ivory",
                               "Jazzy", "Jiffy", "Jinx", "Jukebox", "Kayak", "Kiosk", "Klutz", "Memento",
                               "Mystify", "Numbskull", "Ostracize", "Oxygen", "Pajama", "Phlegm", "Pixel", "Polka",
                               "Quad", "Quip", "Rhythmic", "Rogue", "Sphinx", "Squawk", "Swivel", "Toady",
                              "Twelfth", "Unzip", "Waxy", "Wildebeest", "Yacht", "Zealous", "Zigzag", "Zippy", "Zombie" };
            // Information text
            Console.WriteLine("-----You play the game \"Hangman\", it's not \"case sensitivity\"-----");
            // Loop for "are you want to play again" y/n||Y/N continue or break
            while (true)
            {
                // The answer of:  "are you want to play again" y/n||Y/N continue or break, it's the reset 
                char playAgain;
                // Random object to pull out random word from word bank "words" 
                Random rand = new Random();
                // The random number, to be smaller than the number of word in "words"
                int index = rand.Next(words.Length);
                // The last letter in certain word from "words"
                int lastChar = words[index].Length-1;
                // String builder object to mask the middle letters of the certain word with  "_"
                StringBuilder wordContent = new StringBuilder();
                // The count of the middle letters of the certain word
                int contentSize = lastChar - 1;
                // Loop to mask the middle letters of the certain word with "_"
                while (contentSize > 0)
                {
                    wordContent.Append("_");
                    contentSize--;
                }
                // Shows the randomly selected word "masked" 
                Console.WriteLine($"The randomly selected word, which have to guess is: {words[index][0]}{wordContent}{words[index][lastChar]}");
                // Loop to the end of the game exit with lose/won
                while (lives > 0 && winner == false)
                { 
                    Console.WriteLine("Еnter a letter: ");
                    
                    char x;
                    // Validate to enter only letters!
                    do
                    {
                        x = Console.ReadKey(true).KeyChar;
                    } while ((x < 'a' || x > 'z') && (x < 'A' || x > 'Z'));
                    Console.WriteLine($"The entered letter is  {x}");
                    // Puts the word in lowercase, because the game is not case sensitive
                    string wordLow = words[index].ToLower();
                    // Puts and the letter in lowercase, to be same
                    string letterLow = x.ToString().ToLower();
                    // The first and last letters, they should not be recognize
                    // if the first or last letter of the word occurs in another position, they must be recognized
                    string lettersToGess = string.Concat(wordLow.Skip(1).Take(wordLow.Length - 2));
                    // There is a match of the entered letter with some letter of the word without first/last
                    if (lettersToGess.Contains(letterLow))
                    { 
                        int idx = lettersToGess.IndexOf(letterLow);
                        // Iterate over the content of the word it's (the word without first and last)
                        for (int i = 0; i < lettersToGess.Length; i++)
                            if (lettersToGess[i].ToString() == letterLow)
                            {
                                wordContent[i] = x;
                                guessedNum++;
                            }
                                
                        Console.WriteLine("You guessed a letter! ");
                        // Replace in the content "_" with the letter which is found
                        wordContent[idx] = x;
                        // Recognized only a letter
                        if ((guessedNum + 2) < words[index].Length)
                        {
                            // Show the word with new founded letters 
                            Console.WriteLine($"The randomly selected word, which have to guess now is: {words[index][0]}{wordContent}{words[index][lastChar]}");
                        }
                        // Recognized the whole word
                        if (lettersToGess.ToLower() == wordContent.ToString().ToLower())
                        {
                            Console.WriteLine($"You guessed the word --- {words[index]} --- and you won the game!");
                            winner = true;
                        }
                    }
                    // There is NO match of the entered letter with some letter of the word
                    else
                    {
                        //Draws ASCII "image" of the hanged man depending  of the number in "lives" 
                        if (lives == 5) // head
                        {
                            hangedMan[2][4] = 'O';
                            lives--;
                        }
                        else if (lives == 4)// neck
                        {
                            hangedMan[3][4] = '!';
                            lives--;
                        }
                        else if (lives == 3)// hands
                        {
                            hangedMan[4][3] = '/';
                            hangedMan[4][5] = '\\';
                            lives--;
                        }
                        else if (lives == 2)// body
                        {
                            hangedMan[3][4] = '#';
                            hangedMan[4][4] = '#';
                            hangedMan[5][4] = '#';
                            lives--;
                        }
                        else if (lives == 1)// legs=>hanged
                        {
                            hangedMan[6][3] = '/';
                            hangedMan[6][5] = '\\';
                            lives--;
                            winner = false;
                            Console.WriteLine("Lost the game and you are hanged!");
                        }
                        // Loops to draws ASCII "image"
                        for (int i = 0; i < hangedMan.Length; i++)
                        {
                            for (int j = 0; j < hangedMan[i].Length; j++)
                            {
                                Console.Write(hangedMan[i][j]);
                            }
                            Console.WriteLine();
                        }
                    }
                }
                // Suggestion to play again 
                Console.WriteLine("Would you like to play again ? (y / n)");
                // Validate to enter only 'y'||'Y'||'n'||'N'!
                do
                {
                    playAgain = Console.ReadKey(true).KeyChar;
                } while (playAgain != 'y' && playAgain != 'Y' && playAgain != 'n' && playAgain != 'N');
                // "n"||'N' exit from the loop
                if (playAgain == 'n' || playAgain == 'N')
                {
                    break;
                }
                // "y"||'Y' continue the loop and the player play again
                else if (playAgain == 'y' || playAgain == 'Y')
                {
                    //Prepares the necessary values to play again
                    winner = false;
                    lives = 5;
                    guessedNum = 0;
                    hangedMan[2][4] = ' ';
                    hangedMan[3][4] = ' ';
                    hangedMan[4][3] = ' ';
                    hangedMan[4][5] = ' ';
                    hangedMan[3][4] = ' ';
                    hangedMan[4][4] = ' ';
                    hangedMan[5][4] = ' ';
                    hangedMan[6][3] = ' ';
                    hangedMan[6][5] = ' ';
                    continue;
                }
            }
        }
    }
}
