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
        public void GivenIEnterAn(string inputFile)
        {
            var inputFileName = GetFileName(inputFile);
            ScenarioContext.Current.Add("Input File", inputFileName);
            var factory = new FileFactory<IFile>();
            object[] arguments = { inputFileName };
            IFile textFile = factory.Create<TextFile>(arguments);
            Assert.That(inputFileName, Is.EqualTo(textFile.FileName));            
        }

        [Given(@"the file contains ""(.*)""")]
        public void GivenTheFileContainsThe(string inputText)
        {            
            ScenarioContext.Current.Add("Input Text", inputText);
            var fileName = ScenarioContext.Current.Get<string>("Input File");
            IFile textFile = CreateTextFile(fileName);
            Assert.That(inputText, Is.EqualTo(textFile.GetFileContents(fileName)));
        }

        [When(@"I ehter the file name ""(.*)"" and press return")]
        public void WhenIEhterAnAndPressReturn(string outputFile)
        {
            var outputFileName = GetFileName(outputFile);
            ScenarioContext.Current.Add("Output File", outputFileName);
            var inputFile = ScenarioContext.Current.Get<string>("Input File");
            var factory = new FileFactory<IFile>();
            object[] arguments = { inputFile };
            IFile textFile = factory.Create<TextFile>(arguments);
            IReverser reverser = new Reverser.Reverser();
            ITextFileReverser textFileReverser = new TextFileReverser(textFile, reverser);
            var outputText = textFileReverser.ReverseTextFileContents(textFile.FileName);
            var reversed = textFile.WriteFileContents(outputFileName, outputText);
            Assert.That(reversed, Is.True);            
        }

        [Then(@"the file ""(.*)"" is created")]
        public void ThenTheIsCreated(string outputFile)
        {
            var outputFileName = GetFileName(outputFile);
            var factory = new FileFactory<IFile>();
            object[] arguments = { outputFileName };
            IFile textFile = factory.Create<TextFile>(arguments);
            Assert.That(outputFileName, Is.EqualTo(textFile.FileName));   
        }

        [Then(@"the contents of the file contains ""(.*)""")]
        public void ThenTheContentsOfTheFileContainsTheReverseOfTheInputAs(string outputText)
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

        private static string GetFileName(string fileName)
        {
            return Properties.Specs.Default.TextFileLocation + @"\" + fileName;
        }
    }
}
