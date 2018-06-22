using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Parsers
{
    public class Acq_TH_Parser
    {
		private List<DateTime> dates;

        public Acq_TH_Parser()
        {
        }

        public void Parse( string dataDir, string startDate, string endDate ){

			DateTime.Parse(startDate);
			DateTime.Parse(endDate);

            var exportDir = $@"{dataDir}/Unzipped";
            var logger = Logger.Instance;

            if(Directory.Exists(exportDir)){
                var backupDirName = $@"{exportDir}_Backup{DateTime.Now.ToFileTime()}";
                Directory.Move(exportDir, backupDirName);
            }

            foreach (var file in Directory.GetFiles(dataDir, "*.zip"))
            {
                logger.Log($"Extracting file {file}");
                ZipFile.ExtractToDirectory(file, exportDir);
            }

            var devices = new List<Device>();

            foreach (var directoryPath in Directory.GetDirectories(exportDir))
            {
                var directoryName = directoryPath.Substring(directoryPath.LastIndexOf('/')+1);
                var serialNumber = directoryName.Substring(0, directoryName.IndexOf('_'));
                logger.Log($"Create new device: \n" +
                           $"SerialNumber: {serialNumber}");
                devices.Add(new Device
                {
                    SystemSerialNumber = serialNumber,
                    QCResults = GetQCResults(directoryPath)
                });
            }

            devices.ForEach(d => d.Build());

            var output = ReportGenerator.GenerateReport(devices);
            var outputFileName = $"{dataDir}/output.csv";
            if(File.Exists(outputFileName)){
                File.Move( outputFileName, $"{outputFileName}_Backup{DateTime.Now.ToFileTime()}");
            }
            File.WriteAllText( outputFileName, output);
        }

        private static IEnumerable<QcResult> GetAcqFiles( List<DateTime> dates, string dir ){
            var acqFiles = Directory.GetFiles(dir, "acqhealth*", SearchOption.AllDirectories).Where( file =>
			{
				return dates.Exists(date => file.Contains(date.ToString("yyyy-MM-DD")));
			});
			
			return acqFiles.Select((fileName) => new AcqFile(fileName)).ToList();
        }
    }
}
