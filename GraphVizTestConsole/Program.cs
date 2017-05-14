using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphVizTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener() { TraceOutputOptions = TraceOptions.DateTime });
            List<Tuple<string, string>> data = new List<Tuple<string, string>>();
        
            data.Add(Tuple.Create("A", "B"));
            data.Add(Tuple.Create("B", "C"));
            data.Add(Tuple.Create("A", "D"));
            data.Add(Tuple.Create("X", "S"));
            data.Add(Tuple.Create("B", "C"));
            data.Add(Tuple.Create("A", "Z"));
            data.Add(Tuple.Create("C", "B"));
            data.Add(Tuple.Create("D", "X"));
            data.Add(Tuple.Create("A", "N"));

            GraphVizNet.GraphFactory.Render(data);
        }
    }
}
