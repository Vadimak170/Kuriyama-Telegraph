using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kuriyama.files
{
    class FileWorker
    {
        public FileWorker()
        {

        }

        public IEnumerable<String> SearchFile(string filename)
        {
            return Directory.GetFiles("C:\\", filename, SearchOption.AllDirectories);
        }

        public string SysDirectory(string sample)
        {
            return Environment.GetEnvironmentVariable(sample);
        }

        public void WriteToFile(string path, string content)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path))
            {
                file.Write(content);
            }
        }

        public void WriteBytes(string fileName, byte[] byteArray)
        {
            using (FileStream
            fileStream = new FileStream(fileName, FileMode.Create))
            {
                for (int i = 0; i < byteArray.Length; i++)
                {
                    fileStream.WriteByte(byteArray[i]);
                }
                fileStream.Seek(0, SeekOrigin.Begin);
                for (int i = 0; i < fileStream.Length; i++)
                {
                    if (byteArray[i] != fileStream.ReadByte())
                    {
                        return;
                    }
                }
            }
        }
    }
}
