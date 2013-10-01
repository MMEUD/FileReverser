using FileManager;
using Reverser;
using FileReverser;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ReverserSpecs
{
    [Binding]
    public class StepDefinitions
    {
        [Given(@"I enter the file name ""(.*)""")]
        public void GivenIEnterTheFileName(string inputFile)
        {
            var inputFileName = GetFileName(inputFile);
            ScenarioContext.Current.Add("Input File", inputFileName);
            IFile textFile = CreateTextFile(inputFileName);
            Assert.That(inputFileName, Is.EqualTo(textFile.FileName));            
        }

        [Given(@"the file contains ""(.*)""")]
        public void GivenTheFileContainsTheInputText(string inputText)
        {            
            ScenarioContext.Current.Add("Input Text", inputText);
            var fileName = ScenarioContext.Current.Get<string>("Input File");
            IFile textFile = CreateTextFile(fileName);
            Assert.That(inputText, Is.EqualTo(textFile.GetFileContents(fileName)));
        }

        [When(@"I enter the file name ""(.*)"" and press return")]
        public void WhenIEnterAndPressReturn(string outputFile)
        {
            var outputFileName = GetFileName(outputFile);
            ScenarioContext.Current.Add("Output File", outputFileName);
            var inputFile = ScenarioContext.Current.Get<string>("Input File");
            IFile textFile = CreateTextFile(inputFile);
            var outputText = CreateTextFileReverser(textFile);
            var reversed = textFile.WriteFileContents(outputFileName, outputText);
            Assert.That(reversed, Is.True);            
        }

        [Then(@"the file ""(.*)"" is created")]
        public void ThenTheFileIsCreated(string outputFile)
        {
            var outputFileName = GetFileName(outputFile);
            IFile textFile = CreateTextFile(outputFileName);
            Assert.That(outputFileName, Is.EqualTo(textFile.FileName));   
        }

        [Then(@"the contents of the file contains ""(.*)""")]
        public void ThenTheContentsOfTheFileContainsTheReverseOfTheInput(string outputText)
        {
            var outputFile = ScenarioContext.Current.Get<string>("Output File");
            IFile textFile = CreateTextFile(outputFile);
            Assert.That(outputText, Is.EqualTo(textFile.GetFileContents(outputFile)));
        }

        private static IFile CreateTextFile(string fileName)
        {
            var fileFactory = new FileFactory<IFile>();
            IFile textFile = fileFactory.Create<TextFile>(fileName);
            return textFile;
        }

        private static string CreateTextFileReverser(IFile textFile)
        {
            IReverser reverser = new Reverser.Reverser();
            ITextFileReverser textFileReverser = new TextFileReverser(textFile, reverser);
            var outputText = textFileReverser.ReverseTextFileContents(textFile.FileName);
            return outputText;
        }

        private static string GetFileName(string fileName)
        {
            return Properties.Specs.Default.TextFileLocation + @"\" + fileName;
        }
    }
}
