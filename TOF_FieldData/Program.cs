using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace TOF_FieldData
{
    class Program
    {
        static void Main(string[] args)
        {
            new TOFParser().Parse(args[0]);
        }
    }
}

