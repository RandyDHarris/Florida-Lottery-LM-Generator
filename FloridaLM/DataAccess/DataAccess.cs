using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Configuration;
using FloridaLM.IUtilities;


namespace FloridaLM.DataAccess
{
    public class DataAccess : DataBaseConnection, IDataAccess.ICUDHistory, IDataAccess.IGenNums, IDataAccess.IRHistory, IUtilities.ICheckDate, IUtilities.ICheckInt, IUtilities.ICheckIntReturnZero
    {

        public DataAccess()
        {

        }

        public List<LMNumbers> GenNums()
        {
            List<LMNumbers> llmp = new List<LMNumbers>(); 

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "spGenNums";
                comm.CommandType = CommandType.StoredProcedure;


                conn.Open();

                SqlDataReader sdr = comm.ExecuteReader();
                string tResult = string.Empty;

                while (sdr.Read())
                {
                    LMNumbers lmp = new LMNumbers();
                    lmp.Num1 = CheckIntReturnZero(sdr["Num1"].ToString());
                    lmp.Num2 = CheckIntReturnZero(sdr["Num2"].ToString());
                    lmp.Num3 = CheckIntReturnZero(sdr["Num3"].ToString());
                    lmp.Num4 = CheckIntReturnZero(sdr["Num4"].ToString());
                    lmp.LB = CheckIntReturnZero(sdr["LB"].ToString());

                    llmp.Add(lmp);
                }
            }

            return llmp;
        }

        public List<LMNumbers> RHistory()
        {

            List<LMNumbers> llmn = new List<LMNumbers>();
            
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "spCRUDHistory";
                    comm.CommandType = CommandType.StoredProcedure;

                    comm.Parameters.AddWithValue("@Operation", 2);
                    comm.Parameters.AddWithValue("@Num1", 0);
                    comm.Parameters.AddWithValue("@Num2", 0);
                    comm.Parameters.AddWithValue("@Num3", 0);
                    comm.Parameters.AddWithValue("@Num4", 0);
                    comm.Parameters.AddWithValue("@LB", 0);
                    comm.Parameters.AddWithValue("@WinDate", DateTime.Now.ToShortDateString());


                        conn.Open();

                        SqlDataReader sdr = comm.ExecuteReader();

                        while (sdr.Read())
                        {
                            LMNumbers lmp = new LMNumbers();
                            lmp.Num1 = CheckIntReturnZero(sdr["Num1"].ToString());
                            lmp.Num2 = CheckIntReturnZero(sdr["Num2"].ToString());
                            lmp.Num3 = CheckIntReturnZero(sdr["Num3"].ToString());
                            lmp.Num4 = CheckIntReturnZero(sdr["Num4"].ToString());
                            lmp.LB = CheckIntReturnZero(sdr["LB"].ToString());
                            lmp.WinDate = DateTime.Parse(sdr["WinDate"].ToString());

                            llmn.Add(lmp);
                        }

                    }

                }

            return llmn;
        }

        public void CUDHistory(int Operation, int Num1, int Num2, int Num3, int Num4, int LB, DateTime WinDate)
        {

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "spCRUDHistory";
                    comm.CommandType = CommandType.StoredProcedure;

                    comm.Parameters.AddWithValue("@Operation", Operation);
                    comm.Parameters.AddWithValue("@Num1", Num1);
                    comm.Parameters.AddWithValue("@Num2", Num2);
                    comm.Parameters.AddWithValue("@Num3", Num3);
                    comm.Parameters.AddWithValue("@Num4", Num4);
                    comm.Parameters.AddWithValue("@LB", LB);
                    comm.Parameters.AddWithValue("@WinDate", WinDate);

                    conn.Open();
                    comm.ExecuteNonQuery();

                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                }
            }
        }

    }
}