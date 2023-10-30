using StreamReadWriteAsync;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace FileReadWriteAsyncTests
{

    public class TestData : IEnumerable<object[]>
    {
        //buferSize,fileName,ContentBytes,expectedBytes
        public IEnumerator<object[]> GetEnumerator()
        {         
            yield return new object[] 
            {
             224,
             "c:\\test.raw",
             ASCIIEncoding.ASCII.GetBytes("AGHGG988685 12/.,a87748b7349c=d"),
             ASCIIEncoding.ASCII.GetBytes("abcd")
            };
            yield return new object[]
           {
             8192,
             "c:\\testLargFile.raw",
             ASCIIEncoding.ASCII.GetBytes(string.Concat(System.Linq.Enumerable.Repeat("-=(0)83^%#4@!",1_000))+"AGHGG988685 12/.,a87748b7349c=d"),
             ASCIIEncoding.ASCII.GetBytes("abcd")
           };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class FileReadWriteAsyncTests
    {
        static string FilePathOut => "c:\\testOut.raw";
        [Theory]
        [ClassData(typeof(TestData))]
        public async Task Test_TransferSymbolsAsync(int buferLenth,string filePath, byte[] contentFile,byte[] expectedBytes)
        {
            //Arrange
            MockFileSystem fileSystem = new ();
            fileSystem.AddFile(filePath,new MockFileData(contentFile));

            Stream streamIn = fileSystem.FileStream.New(filePath, FileMode.Open);
            Stream streamOut = fileSystem.FileStream.New(FilePathOut, FileMode.Create);

            //Act
            var task = new FileReadWriteAsync().TransferSymbolsAsync(streamIn, streamOut, buferLenth);
            await task;
            streamIn.Dispose();
            streamOut.Dispose();
            var fileOutCreate = fileSystem.GetFile(FilePathOut);

            //Assert
            Assert.True(task.IsCompletedSuccessfully);
            Assert.Equal(expectedBytes, fileOutCreate.Contents);
        }

        [Theory]
        [ClassData(typeof(TestData))]
        public async Task TransferSymbolsAsyncWithArrayPool(int buferLenth, string filePath, byte[] contentFile, byte[] expectedBytes)
        {
            //Arrange
            MockFileSystem fileSystem = new();
            fileSystem.AddFile(filePath, new MockFileData(contentFile));

            Stream streamIn = fileSystem.FileStream.New(filePath, FileMode.Open);
            Stream streamOut = fileSystem.FileStream.New(FilePathOut, FileMode.Create);

            //Act
            var task = new FileReadWriteAsync().TransferSymbolsAsyncWithArrayPool(streamIn, streamOut, buferLenth);
            await task;
            streamIn.Dispose();
            streamOut.Dispose();
            var fileOutCreate = fileSystem.GetFile(FilePathOut);

            //Assert
            Assert.True(task.IsCompletedSuccessfully);
            Assert.Equal(expectedBytes, fileOutCreate.Contents);
        }
    }
}