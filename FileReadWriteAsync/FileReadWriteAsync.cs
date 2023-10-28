using System;
using System.IO;
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
        public async Task TransferSymbolsAsync(Stream source, Stream destination, int buferLenth)
        {
            byte[] buffer = new byte[buferLenth];
            try
            {
                while (true)
                {
                    var task = source.ReadAsync(buffer, 0, buffer.Length);
                    await task;
                    if (task.Result == 0)
                        break;

                    int cntSymbolInStream = 0;
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        if (buffer[i] >= 0x61 && buffer[i] <= 0x7a)
                        {
                            buffer[cntSymbolInStream] = buffer[i];
                            cntSymbolInStream++;
                        }
                    }

                    if (cntSymbolInStream > 0)
                    {
                        await destination.WriteAsync(buffer, 0, cntSymbolInStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}