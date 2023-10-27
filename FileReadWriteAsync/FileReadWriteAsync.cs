namespace StreamReadWriteAsync;
public class FileReadWriteAsync
{
    public static void Main()
    {

    }
    //Читаем и пишем асинхронно из файла и в файл
    //условие записи: символы схраняют порядок и принадлежат ascii от a..z
    public async Task<int> TransferSymbolsAsync(Stream source, Stream destination)
    {
        byte[] buffer = new byte[1];
        try
        {
            var cntOUT = 0;

            while (source.Position < source.Length)
            {
                checked
                {
                    await source.ReadAsync(buffer);
                    {
                        if (buffer[0] >= 0x61 && buffer[0] <= 0x7a)
                        {
                            await destination.WriteAsync(buffer);
                            cntOUT++;
                        }
                    }
                }


            }
            return cntOUT;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        finally
        {
            destination.Dispose();
            source.Dispose();

        }
    }

    public Task TransferSymbols(Stream source, Stream destination)
    {
        return Task.Run(async () =>
        {
            byte[] buffer = new byte[1];
            try
            {
                var cntOUT = 0;

                while (source.Position < source.Length)
                {
                    checked
                    {
                        await source.ReadAsync(buffer);
                        {
                            if (buffer[0] >= 0x61 && buffer[0] <= 0x7a)
                            {
                                await destination.WriteAsync(buffer);
                                cntOUT++;
                            }
                        }
                    }


                }
                return cntOUT;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                destination.Dispose();
                source.Dispose();

            }
        });
    }
}