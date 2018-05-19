using System;
using System.IO;
using System.IO.Compression;

namespace TOF_FieldData
{
    class Program
    {
        private static string dataDir = "/Users/dan.kaste/Code/TOF_FieldData/TestData";
        private static string exportDir = "/Users/dan.kaste/Code/TOF_FieldData/TestData/Unzipped";
        static void Main(string[] args)
        {
            foreach (var file in Directory.GetFiles(dataDir))
            {
                if (file.Contains(".zip"))
                {
                    Console.WriteLine($"Extracting file {file}");

                    ZipFile.ExtractToDirectory(file, exportDir);
                }
            }
        }
    }
}

