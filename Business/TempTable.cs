//AUTHOR:       Randy Harris
//Date:         4/8/2016
//Class:        TempTable
//Description:  This struct is a data structure used in the Generate Numbers class. 

#region Assemblies
using System;
#endregion

namespace Business
{
    public class TempTable
    {
        #region Properties
        public int NumberValue { get; set; }
        public int TotalOccurrences { get; set; }
        public int nDraws { get; set; }
        public double AverageOccurrencesPernDraws { get; set; }
        public int DrawsSinceLastOccurrence { get; set; }
        public DateTime LastOccurrence { get; set; }
        public int OccurrencesInLastnDraws { get; set; }
        public int RemianingInnDraws { get; set; }
        public int Ranking { get; set; }
        #endregion
    }
}
