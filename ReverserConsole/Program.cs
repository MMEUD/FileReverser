using System;
using FileManager;
using Reverser;
using FileReverser;

namespace ReverserConsole
{
    public static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 0) return;
                Console.WriteLine("Please enter the location (full path) and filename of file to reverse");
                var inputFile = Console.ReadLine();

                if (args.Length != 0) return;
                Console.WriteLine("Please enter the location (full path) and filename of the desired ouput file to output reversed text");
                var outputFile = Console.ReadLine();

                var textFile = CreateTextFile(inputFile);
                var inputText = textFile.GetFileContents(inputFile);
                var reversed = CreateReversedOutputFile(textFile, outputFile);

                if (string.IsNullOrEmpty(reversed))
                    Console.WriteLine("File not created.  Exiting ...");
                else
                {
                    Console.WriteLine("the input file: " + inputFile);
                    Console.WriteLine("containing the text:" + inputText);
                    Console.WriteLine("has been reversed and output to the file: " + outputFile);
                    Console.WriteLine("with the reversed content: " + reversed);

                    if (args.Length != 0) return;
                    var exitText = Console.ReadLine();
                    Console.WriteLine("Please press any key to exit " + exitText);
                }
            }
            catch (Exception e)
            {
                Console.Write("{0} Exception Caught:", e.Message);
            }
        }

        private static string CreateReversedOutputFile(IFile textFile, string outputFile)
        {
            IReverser reverser = new Reverser.Reverser();
            ITextFileReverser textFileReverser = new TextFileReverser(textFile, reverser);
            var reversed = textFileReverser.ReverseTextFileContents(textFile.FileName);
            return textFile.WriteFileContents(outputFile, reversed) ? reversed : string.Empty;
        }

        private static IFile CreateTextFile(string inputFile)
        {
            var factory = new FileFactory<IFile>();
            object[] arguments = {inputFile};
            var textFile = factory.Create<TextFile>(arguments);
            return textFile;
        }
    }
}
