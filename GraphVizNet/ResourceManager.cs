using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphVizNet
{
    class ResourceManager
    {
        private string embeddedZipName = "GraphVizNet.GraphViz_2_38.zip";
        private string embeddedTemplateName = "GraphVizNet.input_template.dot";

        internal string ExtractDotExe()
        {
            var outputDirName = Path.Combine(Path.GetTempPath(), "graphviz_2_38");
            var outputFileName = Path.Combine(outputDirName, "dot.exe");
            var zipFileName = Path.Combine(Path.GetTempPath(), "graphviz_2_38.zip");

            if (!File.Exists(outputFileName))
            {
                Trace.TraceInformation("Extracting dot.exe");

                using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedZipName))
                {
                    using (FileStream fileStream = new FileStream(zipFileName, FileMode.Create))
                    {
                        for (int i = 0; i < stream.Length; i++)
                        {
                            fileStream.WriteByte((byte)stream.ReadByte());
                        }
                        fileStream.Close();
                    }
                }

                // Extract 
                ZipFile.ExtractToDirectory(zipFileName, outputDirName);

                if (!File.Exists(outputFileName))
                {
                    throw new Exception("Unexpected error, extraction failed");
                }

                Trace.TraceInformation("Finished extracting dot.exe");
            }


            return outputFileName;
        }

        internal string ExtractTemplateFile()
        {
            var outputFileName = Path.GetTempFileName();
            Trace.TraceInformation("Extracting template file");
            using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedTemplateName))
            {
                using (FileStream fileStream = new FileStream(outputFileName, FileMode.Create))
                {
                    for (int i = 0; i < stream.Length; i++)
                    {
                        fileStream.WriteByte((byte)stream.ReadByte());
                    }
                    fileStream.Close();
                }
            }

            Trace.TraceInformation("Finished extracting template file");
            return outputFileName;
        }
    }
    
}
