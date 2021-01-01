using DecryptPluralSightVideos.Option;
using System;
using System.IO;
using static DecryptPluralSightVideos.Option.Utils;

namespace DecryptPluralSightVideos
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Decryptor decryptor;
            DecryptorOptions decryptorOptions = new DecryptorOptions();

            try
            {
                decryptorOptions = ParseCommandLineArgs(args);
                decryptor = new Decryptor(decryptorOptions);

                if (decryptorOptions.UsageCommand)
                {
                    HelpCommand();
                    goto End;
                }

                if (!string.IsNullOrWhiteSpace(decryptorOptions.InputPath))
                {
                    decryptor.DecryptAllFolders(decryptorOptions.InputPath, decryptorOptions.OutputPath);

                    if (decryptorOptions.RemoveFolderAfterDecryption)
                    {
                        WriteToConsole("Removing course in database after decryption." + Environment.NewLine,
                            ConsoleColor.Yellow);
                        foreach (string coursePath in Directory.GetDirectories(decryptorOptions.InputPath, "*",
                            SearchOption.TopDirectoryOnly))
                        {
                            decryptor.RemoveCourseInDb(coursePath);
                            WriteToConsole("Course " + decryptor.GetFolderName(coursePath) + " has been deleted in database." + Environment.NewLine,
                                ConsoleColor.Yellow);
                        }
                    }
                }
                else
                {
                    WriteToConsole("\t/F\t flag is mandatory. Please you the /HELP flag to more information.");
                }
            }
            catch (Exception exception)
            {
                WriteToConsole(
                    "Error occured: " + exception.Message + "\n" + exception.StackTrace + Environment.NewLine,
                    ConsoleColor.Red);
                WriteToConsole(
                    "Please use\t/HELP\tflag to know more about other commands or contact with the publisher.");
            }

        End:
            WriteToConsole(Environment.NewLine + "Press any key to exit the program...");
            Console.ReadKey();
        }
    }
}