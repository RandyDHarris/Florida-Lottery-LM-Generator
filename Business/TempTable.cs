using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TempTable
    {
        public int NumberValue { get; set; }
        public int TotalOccurrences { get; set; }
        public int nDraws { get; set; }
        public double AverageOccurrencesPernDraws { get; set; }
        public int DrawsSinceLastOccurrence { get; set; }
        public DateTime LastOccurrence { get; set; }
        public int OccurrencesInLastnDraws { get; set; }
        public int RemianingInnDraws { get; set; }
        public int Ranking { get; set; }

    }
}
