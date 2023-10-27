using StreamReadWriteAsync;
using Xunit;
namespace FileReadWriteAsyncTests;

public  class FileReadWriteAsyncTests
{
    [Fact]
    public async Task Test_TransferSymbolsAsync()
    {
        //Arrange
        Stream streamIn = new FileStream("test.raw", FileMode.Open);
        //var lenSreamIn=streamIn.Length;
        Stream streamOut = new FileStream("testOut.raw", FileMode.Create);
        var ob = new FileReadWriteAsync();

        //Act
        var task = ob.TransferSymbolsAsync(streamIn, streamOut);
        await task;
        //Assert
        Assert.False(streamIn.CanRead);
        Assert.False(streamOut.CanWrite);
        Assert.True(task.IsCompletedSuccessfully);
        //Assert.Equal(lenSreamIn, task.Result);
    }

    [Fact]
    public async Task Test_TransferSymbols()
    {
        //Arrange
        Stream streamIn = new FileStream("test.raw", FileMode.Open);
        //var lenSreamIn=streamIn.Length;
        Stream streamOut = new FileStream("testOut.raw", FileMode.Create);
        var ob = new FileReadWriteAsync();

        //Act
        var task = ob.TransferSymbolsAsync(streamIn, streamOut);
        await task;
        //Assert
        Assert.False(streamIn.CanRead);
        Assert.False(streamOut.CanWrite);
        Assert.True(task.IsCompletedSuccessfully);
        //Assert.Equal(lenSreamIn, task.Result);
    }
}
