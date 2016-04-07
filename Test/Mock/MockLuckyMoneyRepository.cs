using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Mock
{
    class MockLuckyMoneyRepository
    {
    }
}



//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Data;
//using System.Data.SqlClient;
//using App;
//using Business;

//namespace Infrastructure
//{
//    public class SqlLuckyMoneyRepository : Checks, ILuckyMoneyNumbersRepository
//    {
//        private readonly List<LuckyMoneyNumbers> _LuckyMoneySqlData;
//        private string _connString { get; set; }

//        public SqlLuckyMoneyRepository(string connString)
//        {
//            _connString = connString;
//            _LuckyMoneySqlData = new List<LuckyMoneyNumbers>();
            
//        }

//        public List<LuckyMoneyNumbers> GetLuckyMoneyNumbers()
//        {
//            GetLuckyMoneyNumbersFromSql();
//            var LMN = _LuckyMoneySqlData;
//            return LMN;
//        }

//        public void InsertLuckyMoneyNumbers(int Operation, int Num1, int Num2, int Num3, int Num4, int LB, DateTime WinDate)
//        {
//            CUDHistory(Operation, Num1, Num2, Num3, Num4, LB, WinDate);
//        }

//        public void UpdateLuckyMoneyNumbers(int Operation, int Num1, int Num2, int Num3, int Num4, int LB, DateTime WinDate)
//        {
//            //CUDHistory(Operation, Num1, Num2, Num3, Num4, LB, WinDate);
//        }

//        public void DeleteLuckyMoneyNumbers(int Operation, int Num1, int Num2, int Num3, int Num4, int LB, DateTime WinDate)
//        {
//            CUDHistory(Operation, Num1, Num2, Num3, Num4, LB, WinDate);
//        }

//        public void GetLuckyMoneyNumbersFromSql()
//        {
//            using (SqlConnection conn = new SqlConnection(_connString))
//            {
//                using (SqlCommand comm = new SqlCommand())
//                {
//                    comm.Connection = conn;
//                    comm.CommandText = "spCRUDHistory";
//                    comm.CommandType = CommandType.StoredProcedure;

//                    comm.Parameters.AddWithValue("@Operation", 2);
//                    comm.Parameters.AddWithValue("@Num1", 0);
//                    comm.Parameters.AddWithValue("@Num2", 0);
//                    comm.Parameters.AddWithValue("@Num3", 0);
//                    comm.Parameters.AddWithValue("@Num4", 0);
//                    comm.Parameters.AddWithValue("@LB", 0);
//                    comm.Parameters.AddWithValue("@WinDate", DateTime.Now.ToShortDateString());

//                    conn.Open();

//                    SqlDataReader sdr = comm.ExecuteReader();

//                    while (sdr.Read())
//                    {
//                        LuckyMoneyNumbers lmn = new LuckyMoneyNumbers();
//                        lmn.Num1 = CheckIntReturnZero(sdr["Num1"].ToString());
//                        lmn.Num2 = CheckIntReturnZero(sdr["Num2"].ToString());
//                        lmn.Num3 = CheckIntReturnZero(sdr["Num3"].ToString());
//                        lmn.Num4 = CheckIntReturnZero(sdr["Num4"].ToString());
//                        lmn.LB = CheckIntReturnZero(sdr["LB"].ToString());
//                        lmn.WinDate = CheckDateReturnMin(sdr["WinDate"].ToString());

//                        _LuckyMoneySqlData.Add(lmn);
//                    }

//                }

//            }
//        }
//        public void CUDHistory(int Operation, int Num1, int Num2, int Num3, int Num4, int LB, DateTime WinDate)
//        {

//            using (SqlConnection conn = new SqlConnection(_connString))
//            {
//                using (SqlCommand comm = new SqlCommand())
//                {
//                    comm.Connection = conn;
//                    comm.CommandText = "spCRUDHistory";
//                    comm.CommandType = CommandType.StoredProcedure;

//                    comm.Parameters.AddWithValue("@Operation", Operation);
//                    comm.Parameters.AddWithValue("@Num1", Num1);
//                    comm.Parameters.AddWithValue("@Num2", Num2);
//                    comm.Parameters.AddWithValue("@Num3", Num3);
//                    comm.Parameters.AddWithValue("@Num4", Num4);
//                    comm.Parameters.AddWithValue("@LB", LB);
//                    comm.Parameters.AddWithValue("@WinDate", WinDate);

//                    conn.Open();
//                    comm.ExecuteNonQuery();

//                    conn.Close();
//                    conn.Dispose();
//                    comm.Dispose();
//                }
//            }
//        }

//    }
//}





        //public List<LMNumbers> GenNums()
        //{
        //    List<LMNumbers> llmp = new List<LMNumbers>(); 

        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        SqlCommand comm = new SqlCommand();
        //        comm.Connection = conn;
        //        comm.CommandText = "spGenNums";
        //        comm.CommandType = CommandType.StoredProcedure;


        //        conn.Open();

        //        SqlDataReader sdr = comm.ExecuteReader();
        //        string tResult = string.Empty;

        //        while (sdr.Read())
        //        {
        //            LMNumbers lmp = new LMNumbers();
        //            lmp.Num1 = CheckIntReturnZero(sdr["Num1"].ToString());
        //            lmp.Num2 = CheckIntReturnZero(sdr["Num2"].ToString());
        //            lmp.Num3 = CheckIntReturnZero(sdr["Num3"].ToString());
        //            lmp.Num4 = CheckIntReturnZero(sdr["Num4"].ToString());
        //            lmp.LB = CheckIntReturnZero(sdr["LB"].ToString());

        //            llmp.Add(lmp);
        //        }
        //    }

        //    return llmp;
        //}

        //public List<LMNumbers> RHistory()
        //{

        //    List<LMNumbers> llmn = new List<LMNumbers>();
            
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        using (SqlCommand comm = new SqlCommand())
        //        {
        //            comm.Connection = conn;
        //            comm.CommandText = "spCRUDHistory";
        //            comm.CommandType = CommandType.StoredProcedure;

        //            comm.Parameters.AddWithValue("@Operation", 2);
        //            comm.Parameters.AddWithValue("@Num1", 0);
        //            comm.Parameters.AddWithValue("@Num2", 0);
        //            comm.Parameters.AddWithValue("@Num3", 0);
        //            comm.Parameters.AddWithValue("@Num4", 0);
        //            comm.Parameters.AddWithValue("@LB", 0);
        //            comm.Parameters.AddWithValue("@WinDate", DateTime.Now.ToShortDateString());


        //                conn.Open();

        //                SqlDataReader sdr = comm.ExecuteReader();

        //                while (sdr.Read())
        //                {
        //                    LMNumbers lmp = new LMNumbers();
        //                    lmp.Num1 = CheckIntReturnZero(sdr["Num1"].ToString());
        //                    lmp.Num2 = CheckIntReturnZero(sdr["Num2"].ToString());
        //                    lmp.Num3 = CheckIntReturnZero(sdr["Num3"].ToString());
        //                    lmp.Num4 = CheckIntReturnZero(sdr["Num4"].ToString());
        //                    lmp.LB = CheckIntReturnZero(sdr["LB"].ToString());
        //                    lmp.WinDate = DateTime.Parse(sdr["WinDate"].ToString());

        //                    llmn.Add(lmp);
        //                }

        //            }

        //        }

        //    return llmn;
        //}



