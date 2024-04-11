using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace delegate_main
{
    public class main
    {
        public static void Main()
        {
            string s = @"Data Source=DESKTOP-IPDB6V8\SQLEXPRESS2022;Initial Catalog=QuanLyLop;Integrated Security=True;TrustServerCertificate=True";
            dbhelper dbHelper = new dbhelper(s);
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Manager Student");
                Console.WriteLine("--------------------");
                Console.WriteLine("1. Select all records");
                Console.WriteLine("2. Select by Name LSH and Name Student");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Sort by DTB");
                Console.WriteLine("6. Exit");
                Console.WriteLine("--------------------");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        {
                            DataTable dtbr = dbHelper.getRecords("Select * from SV");
                            foreach (DataRow i in dtbr.Rows)
                            {
                                Console.WriteLine("MSSV: {0}, Name: {1}, DTB: {2}, Gender: {3}, NS: {4}", i["MSSV"].ToString(),
                                i["NameSV"].ToString(), i["DTB"].ToString(), i["Gender"], i["NS"]);
                            }
                            break;
                        }
                    case "2":
                        String nameLSH;
                        String nameSV;
                        Console.WriteLine("Enter Name LSH and Name Student");
                        Console.WriteLine("Enter Name LSH: ");
                        nameLSH = Console.ReadLine().Trim();
                        Console.WriteLine("Enter Name Student: ");
                        nameSV = Console.ReadLine().Trim();
                        String query = "select * from SV as s join LSH as lsh on s.ID_Lop = LSH.ID_Lop where LSH.NameLop = @TenLop and NameSV = @Name";
                        SqlParameter[] parameters = new SqlParameter[]{
                            new SqlParameter("@TenLop",nameLSH),
                            new SqlParameter("@Name",nameSV)
                        };
                        DataTable dtSearch = dbHelper.getRecords(query, parameters);
                        foreach (DataRow i in dtSearch.Rows)
                        {
                            Console.WriteLine("MSSV: {0}, Name: {1}, DTB: {2}, Gender: {3}, NS: {4}", i["MSSV"].ToString(),
                            i["NameSV"].ToString(), i["DTB"].ToString(), i["Gender"], i["NS"]);
                        }
                        break;
                    case "3":
                        Console.WriteLine("Enter MSV you want Delete");
                        String MSV = Console.ReadLine();
                        String queryDelete = "delete from SV where MSSV =@MSV";
                        SqlParameter[] paramer1 = new SqlParameter[]{
                            new SqlParameter("@MSV",MSV)
                        };
                        dbHelper.ExucteDB(queryDelete, paramer1);
                        break;
                    case "4":
                        Console.WriteLine("Enter MSV you want Update");
                        String MSVU = Console.ReadLine();
                        String querySelectMSVU = "select * from SV where MSSV = @MSVU";
                        SqlParameter[] paramer2 = new SqlParameter[]{
                            new SqlParameter("@MSVU",MSVU)
                        };
                        DataTable SVU = dbHelper.getRecords(querySelectMSVU, paramer2);
                        String chooseUpdate;
                        bool flagUp = false;
                        String NSVUP;
                        String DTBUP;
                        String GenderUP;
                        String NSUP;
                        DataRow StudentUpdate = SVU.Rows[0];
                        Console.WriteLine("MSSV: {0}, Name: {1}, DTB: {2}, Gender: {3}, NS: {4}", StudentUpdate["MSSV"].ToString(),
                            StudentUpdate["NameSV"].ToString(), StudentUpdate["DTB"].ToString(), StudentUpdate["Gender"], StudentUpdate["NS"]);
                        String queryUPSV = "update SV set ";
                        int flagC = 0;
                        List<SqlParameter> listPU = new List<SqlParameter>();
                        while (!flagUp)
                        {
                            Console.WriteLine("Enter choosen update");
                            Console.WriteLine("1.NameSV");
                            Console.WriteLine("2.DTB");
                            Console.WriteLine("3.Gender");
                            Console.WriteLine("4.NS");
                            Console.WriteLine("5.Update");
                            chooseUpdate = Console.ReadLine();
                            switch (chooseUpdate)
                            {
                                case "1":
                                    Console.WriteLine("Enter Name SV update: ");
                                    NSVUP = Console.ReadLine();
                                    queryUPSV += (flagC <= 0) ? " NameSV = @NSSVUP" : ",NameSV = @NSSVUP";
                                    flagC = 1;
                                    listPU.Add(new SqlParameter("@NSSVUP", NSVUP));
                                    break;
                                case "2":
                                    Console.WriteLine("Enter DTB SV update: ");
                                    DTBUP = Console.ReadLine();
                                    queryUPSV += (flagC <= 0) ? " DTB = @DTBSVUP" : ",DTB = @DTBSVUP";
                                    flagC = 1;
                                    listPU.Add(new SqlParameter("@DTBSVUP", DTBUP));
                                    break;
                                case "3":
                                    Console.WriteLine("Enter Gender SV update: ");
                                    GenderUP = Console.ReadLine();
                                    queryUPSV += (flagC <= 0) ? " Gender = @GenderUP" : ",Gender = @GenderUP";
                                    listPU.Add(new SqlParameter("@GenderUP", GenderUP));
                                    flagC = 1;
                                    break;
                                case "4":
                                    Console.WriteLine("Enter NS SV update: (year-month-day)");
                                    NSUP = Console.ReadLine();
                                    queryUPSV += (flagC <= 0) ? " NS = @NSPUP" : ",NS = @NSPUP";
                                    listPU.Add(new SqlParameter("@NSPUP", NSUP));
                                    flagC = 1;
                                    break;
                                case "5":
                                    flagUp = true;
                                    break;
                            }
                        }
                        SqlParameter[] paraU = new SqlParameter[listPU.Count + 1];
                        for (int i = 0; i < listPU.Count; i++)
                        {
                            paraU[i] = listPU[i];
                        }
                        queryUPSV += " where MSSV = @MSSVU";
                        paraU[listPU.Count] = new SqlParameter("@MSSVU", MSVU);
                        dbHelper.ExucteDB(queryUPSV, paraU);
                        Console.WriteLine("Update thành công !");
                        break;
                    case "5":
                        DataTable dtbrSort = dbHelper.getRecords("Select * from SV");
                        for (int i = 0; i < dtbrSort.Rows.Count - 1; i++)
                        {
                            for (int j = i + 1; j < dtbrSort.Rows.Count; j++)
                            {
                                double firstStudent = Convert.ToDouble(dtbrSort.Rows[i]["DTB"]);
                                double secondStudent = Convert.ToDouble(dtbrSort.Rows[j]["DTB"]);
                                if (firstStudent > secondStudent)
                                {
                                    DataRow tempRow = dtbrSort.NewRow();
                                    tempRow.ItemArray = dtbrSort.Rows[i].ItemArray;
                                    dtbrSort.Rows[i].ItemArray = dtbrSort.Rows[j].ItemArray;
                                    dtbrSort.Rows[j].ItemArray = tempRow.ItemArray;
                                }
                            }
                        }
                        foreach (DataRow z in dtbrSort.Rows)
                        {
                            Console.WriteLine("MSSV: {0}, Name: {1}, DTB: {2}, Gender: {3}, NS: {4}", z["MSSV"].ToString(),
                          z["NameSV"].ToString(), z["DTB"].ToString(), z["Gender"], z["NS"]);
                        }
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Error Number");
                        break;
                }

                Console.WriteLine(); // Thêm một dòng trống để tạo khoảng cách giữa các lần hiển thị menu
            }

            Console.WriteLine("Goodbye!");
        }
    }
}