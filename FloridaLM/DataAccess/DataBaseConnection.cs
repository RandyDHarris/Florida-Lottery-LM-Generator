using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloridaLM.DataAccess
{
    public class DataBaseConnection
    {
        public string connString { get; set; }
        public DataBaseConnection()
        {
            connString = ConfigurationManager.ConnectionStrings["FloridaLM.Properties.Settings.DatabaseFLLMConnectionString"].ConnectionString;
        }
    }
}
