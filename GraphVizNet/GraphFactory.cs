using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphVizNet
{
    public class GraphFactory
    {
        public static void Render(List<Tuple<string, string>> data)
        {
            var gviz = new GraphUI();
            var rm = new ResourceManager();

            var dotexePath = rm.ExtractDotExe();
            var templatePath = rm.ExtractTemplateFile();

            Trace.TraceInformation(string.Format("dotexePath: {0}, templatePath: {1}", dotexePath, templatePath));

            PopulateTemplateData(templatePath, data);

            gviz.inputDotFile = templatePath;
            gviz.pathToDotExe = dotexePath;
            gviz.DrawGraph();
        }

        private static void PopulateTemplateData(string templatePath, List<Tuple<string, string>> data)
        {
            var placeholderStr = "{placeholder}";
            var templateStr = File.ReadAllText(templatePath);
            string replacement = string.Empty;

            foreach(var dataPoint in data)
            {
                var entry = string.Format("\"{0}\" -> \"{1}\";", dataPoint.Item1, dataPoint.Item2);
                replacement += entry + Environment.NewLine;
            }

            var dotString = templateStr.Replace(placeholderStr, replacement);
            File.WriteAllText(templatePath, dotString);

            Trace.TraceInformation(string.Format("DOT string {0}", dotString));
        }
    }
}
