using System;
using System.Collections.Generic;
using System.IO;

namespace Parsers
{
    class Program
    {
        static void Main(string[] args)
        {
			switch (args[0])
			{
				case "TOF":
					new TOFParser().Parse(args[1]);
					break;
				case "Acq_TH":
					new Acq_TH_Parser().Parse(args[1], args[2], args[3]);
					break;
			}
            
        }
    }
}

