using StreamReadWriteAsync;
using System.IO;
using System.Threading.Tasks;
using Xunit;
namespace FileReadWriteAsyncTests
{

    public class FileReadWriteAsyncTests
    {
        [Fact]
        public async Task Test_TransferSymbolsAsync()
        {
            //Arrange
            Stream streamIn = new FileStream("test.raw", FileMode.Open);
            Stream streamOut = new FileStream("testOut.raw", FileMode.Create);
            int buferLenth = 224;
            var ob = new FileReadWriteAsync();

            //Act
            var task = ob.TransferSymbolsAsync(streamIn, streamOut, buferLenth);
            await task;
            streamIn.Dispose();
            streamOut.Dispose();

            //Assert
            Assert.True(task.IsCompletedSuccessfully);

        }

        //[Fact]
        //public async Task Test_TransferSymbols()
        //{
        //    //Arrange
        //    Stream streamIn = new FileStream("test.raw", FileMode.Open);
        //    Stream streamOut = new FileStream("testOut.raw", FileMode.Create);
        //    var ob = new FileReadWriteAsync();

        //    //Act
        //    var task = ob.TransferSymbols(streamIn, streamOut);
        //    await task;
        //    streamIn.Dispose();
        //    streamOut.Dispose();

        //    //Assert
        //    Assert.True(task.IsCompletedSuccessfully);
        //}
    }
}