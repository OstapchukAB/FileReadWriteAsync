using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StreamReadWriteAsync
{
    public class FileReadWriteAsync
    {
        public static void Main()
        {

        }
        //Читаем и пишем асинхронно из файла и в файл
        //условие записи: символы схраняют порядок и принадлежат ascii от a..z
        public async Task TransferSymbolsAsync(Stream source, Stream destination)
        {
            byte[] buffer = new byte[4];
            try
            {
                while (true)
                {
                    var task = source.ReadAsync(buffer, 0, buffer.Length);
                    await task;
                    if (task.Result == 0)
                        break;
                    byte[] bufferOut = new byte[task.Result];
                    int cntSymbolInStream = 0;
                    for (int i = 0; i < bufferOut.Length; i++)
                    {
                        if (buffer[i] >= 0x61 && buffer[i] <= 0x7a)
                        {
                            bufferOut[cntSymbolInStream] = buffer[i];
                            cntSymbolInStream++;
                        }
                    }

                    if (cntSymbolInStream > 0)
                    {
                        await destination.WriteAsync(bufferOut, 0, cntSymbolInStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public Task TransferSymbols(Stream source, Stream destination)
        {
            return Task.Run(async () =>
            {
                byte[] buffer = new byte[4];
                try
                {
                    while (true)
                    {
                        var task = source.ReadAsync(buffer, 0, buffer.Length);
                        await task;
                        if (task.Result == 0)
                            break;
                        byte[] bufferOut = new byte[task.Result];
                        int cntSymbolInStream = 0;
                        for (int i = 0; i < bufferOut.Length; i++)
                        {
                            if (buffer[i] >= 0x61 && buffer[i] <= 0x7a)
                            {
                                bufferOut[cntSymbolInStream] =buffer[i];
                                cntSymbolInStream++;
                            }
                        }
                        
                        if (cntSymbolInStream > 0)
                        {
                            await destination.WriteAsync(bufferOut,0, cntSymbolInStream);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            });
        }
    }
}