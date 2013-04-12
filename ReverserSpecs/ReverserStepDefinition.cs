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
        [Given(@"I enter an ""(.*)""")]
        public void GivenIEnterAn(string inputFile)
        {
            ScenarioContext.Current.Add("Input File", inputFile);
            var factory = new FileFactory<IFile>();
            object[] arguments = { inputFile };
            IFile textFile = factory.Create<TextFile>(arguments);
            Assert.That(inputFile, Is.EqualTo(textFile.FileName));            
        }

        [Given(@"the file contains the ""(.*)""")]
        public void GivenTheFileContainsThe(string inputText)
        {            
            ScenarioContext.Current.Add("Input Text", inputText);
            var fileName = ScenarioContext.Current.Get<string>("Input File");
            IFile textFile = CreateTextFile(fileName);
            Assert.That(inputText, Is.EqualTo(textFile.GetFileContents(fileName)));
        }

        [When(@"I ehter an ""(.*)"" and press return")]
        public void WhenIEhterAnAndPressReturn(string outputFile)
        {
            ScenarioContext.Current.Add("Output File", outputFile);
            var inputFile = ScenarioContext.Current.Get<string>("Input File");
            var factory = new FileFactory<IFile>();
            object[] arguments = { inputFile };
            IFile textFile = factory.Create<TextFile>(arguments);
            IReverser reverser = new Reverser.Reverser();
            ITextFileReverser textFileReverser = new TextFileReverser(textFile, reverser);
            var outputText = textFileReverser.ReverseTextFileContents(textFile.FileName);
            var reversed = textFile.WriteFileContents(outputFile, outputText);
            Assert.That(reversed, Is.True);
            ScenarioContext.Current.Add("outoutText", outputText);
        }

        [Then(@"the ""(.*)"" is created")]
        public void ThenTheIsCreated(string outputFile)
        {
            var factory = new FileFactory<IFile>();
            object[] arguments = { outputFile };
            IFile textFile = factory.Create<TextFile>(arguments);
            Assert.That(outputFile, Is.EqualTo(textFile.FileName));   
        }

        [Then(@"the contents of the file contains the reverse of the input as ""(.*)""")]
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
    }
}
