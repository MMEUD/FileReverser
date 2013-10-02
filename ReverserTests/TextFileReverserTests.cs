using System;
using FileManager;
using Reverser;
using FileReverser;
using Moq;
using NUnit.Framework;


namespace FileReverserTests
{
    [TestFixture]
    public class TextFileReverserTests
    {
        private Mock<IFile> _mockTextFile;
        private Mock<IReverser> _mockReverser;

        [Test]        
        public void TextFileReverserNullDependenciesThrowsInvalidArgumentException()
        {
            Assert.Throws(Is.TypeOf<ArgumentNullException>(), 
                          () => new TextFileReverser(null, null));
        }

        [Test]
        [Category("Unit Test")]
        public void ReverseTextFileContentsChecksIfAnyFileExists()
        {            
            var textFileReverser = CreateTextFileReverser();
            _mockTextFile.Setup(s => s.FileExists(It.IsAny<string>())).Returns(true);
            textFileReverser.ReverseTextFileContents(It.IsAny<string>());
            _mockTextFile.VerifyAll();
        }

        [Test]
        [Category("Unit Test")]
        public void ReverseTextFileContentsChecksIfNamedFileExists()
        {
            var textFileReverser = CreateTextFileReverser();
            textFileReverser.ReverseTextFileContents("text.file");
            _mockTextFile.Verify(v => v.FileExists("text.file"), Times.Exactly(1));
        }

        [Test]
        [Category("Unit Test")]
        public void ReverseTextFileContentsExistingFileCallsGetFileContents()
        {
            var textFileReverser = CreateTextFileReverser();
            _mockTextFile.Setup(s => s.FileExists(It.IsAny<string>())).Returns(true);
            _mockTextFile.Setup(s => s.GetFileContents(It.IsAny<string>())).Returns(It.IsAny<string>());
            textFileReverser.ReverseTextFileContents(It.IsAny<string>());
            _mockTextFile.VerifyAll();
        }

        [Test]
        [Category("Unit Test")]
        public void ReverseTextFileContentsMissingFileDoesNotCallGetFileContents()
        {
            var textFileReverser = CreateTextFileReverser();
            _mockTextFile.Setup(s => s.FileExists("text.file")).Returns(false);
            textFileReverser.ReverseTextFileContents("text.file");
            _mockTextFile.Verify(v => v.FileExists("text.file"), Times.Exactly(1));
            _mockTextFile.Verify(v => v.GetFileContents(It.IsAny<string>()), Times.Never());
        }

        private ITextFileReverser CreateTextFileReverser()
        {
            _mockTextFile = new Mock<IFile>();
            _mockReverser = new Mock<IReverser>();
            ITextFileReverser textFileReverser = new TextFileReverser(_mockTextFile.Object, _mockReverser.Object);
            return textFileReverser;
        }
    }
}
