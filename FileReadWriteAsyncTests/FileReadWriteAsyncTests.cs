using StreamReadWriteAsync;
using System.IO;
using System.Threading.Tasks;
using Xunit;
namespace FileReadWriteAsyncTests
{

    public class FileReadWriteAsyncTests
    {
        [Theory]
        [InlineData("test.raw",224)]
        [InlineData("test2Gb.raw", 1024)]
        [InlineData("test2Gb.raw", 4096)]
        [InlineData("test2Gb.raw", 8192)]
        public async Task Test_TransferSymbolsAsync(string pathFile, int buferLenth)
        {
            //Arrange
            Stream streamIn = new FileStream(pathFile, FileMode.Open);
            Stream streamOut = new FileStream("testOut.raw", FileMode.Create);
            var ob = new FileReadWriteAsync();

            //Act
            var task = ob.TransferSymbolsAsync(streamIn, streamOut, buferLenth);
            await task;
            streamIn.Dispose();
            streamOut.Dispose();

            //Assert
            Assert.True(task.IsCompletedSuccessfully);
        }

        [Theory]
        [InlineData("test2Gb.raw", 1024)]
        [InlineData("test2Gb.raw", 4096)]
        [InlineData("test2Gb.raw", 8192)]
        public async Task TransferSymbolsAsyncWithArrayPool(string pathFile, int buferLenth)
        {
            //Arrange
            Stream streamIn = new FileStream(pathFile, FileMode.Open);
            Stream streamOut = new FileStream("testOut.raw", FileMode.Create);
            var ob = new FileReadWriteAsync();

            //Act
            var task = ob.TransferSymbolsAsyncWithArrayPool(streamIn, streamOut, buferLenth);
            await task;
            streamIn.Dispose();
            streamOut.Dispose();

            //Assert
            Assert.True(task.IsCompletedSuccessfully);
        }


    }
}