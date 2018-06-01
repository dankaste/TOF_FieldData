using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace TOF_FieldData
{
    public class TOFParser
    {
        public TOFParser()
        {
        }

        public void Parse( string dataDir ){
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
                    systemSerialNumber = serialNumber,
                    qcResults = GetQCResults(directoryPath)
                });
            }
        }

        private static IEnumerable<QcResult> GetQCResults( string dir ){
            var qcResultFiles = Directory.GetFiles(dir, "qcResults*.log",SearchOption.AllDirectories);
            return qcResultFiles.Select((fileName) => new QcResult(fileName));
        }
    }
}
