using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Data;
using System.Data.SqlClient;

namespace Employee1
{
    class errorException
    {


        public static void errorhandle(Exception ex)
        {
            string filePath = @"D:\errorlog.txt";
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }
    }
    class Employee
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int EmployeeID, Salary, PhoneNumber;
        private String EmployeeName, Designation;
        private DateTime Doj;
        public Employee(int EmployeeID, String EmployeeName, string Designation, int Salary, DateTime Doj, int PhoneNumber)
        {
            this.EmployeeID = EmployeeID;
            this.EmployeeName = EmployeeName;
            this.Designation = Designation;
            this.Salary = Salary;
            this.Doj = Doj;
            this.PhoneNumber = PhoneNumber;
        }
        public Employee()
        {

        }

        public int id
        {
            get { return EmployeeID; }
            set { EmployeeID = value; }
        }
        public string name
        {
            get { return EmployeeName; }
            set { EmployeeName = value; }
        }
        public string Design
        {
            get { return Designation; }
            set { Designation = value; }
        }
        public int salary
        {
            get { return Salary; }
            set { Salary = value; }
        }
        public DateTime date
        {
            get { return Doj; }
            set { Doj = value; }
        }
        public int phone
        {
            get { return PhoneNumber; }
            set { PhoneNumber = value; }
        }
        public class example
        {
            public static void Main()
            {


                //employees.Add(new Employee(1236, "Zeerav", "RAT", 26000, new DateTime(2017, 11, 19), 2001345));
                //employees.Add(new Employee(1235, "Shubham", "PAT", 25000, new DateTime(2017, 11, 18), 2001456));

                //foreach (Employee item in employees)
                //{
                //    Console.WriteLine("---------------------------------");
                //    Console.WriteLine(item);
                //    TextWriter tw = new StreamWriter(@"D:\WriteLine.txt");

                //    foreach (Employee items in employees)
                //        tw.WriteLine(items);

                //    tw.Close();

                //    Console.WriteLine("---------------------------------");
                //}

                Employee em = new Employee(4, "qwee", "qwer", 1234, DateTime.Parse("11/11/1976"), 23456789);
                int x;
                x = int.Parse(Console.ReadLine());

                String ConnectionString = "Data Source=PC252069;Initial Catalog=Employee;" + "Integrated Security=True";
                if (x == 1)
                {
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "UspInsertEmployee";
                            cmd.Parameters.AddWithValue("@id", em.id);
                            cmd.Parameters.AddWithValue("@name", em.name);
                            cmd.Parameters.AddWithValue("@Desig", em.Design);
                            cmd.Parameters.AddWithValue("@sal", em.salary);
                            cmd.Parameters.AddWithValue("@Doj", em.date);
                            cmd.Parameters.AddWithValue("@phone", em.phone);
                            con.Open();
                            cmd.ExecuteNonQuery();

                            con.Close();
                        }
                        catch (Exception e)
                        {
                            errorException.errorhandle(e);
                        }
                    }
                }
                else if (x == 2)
                {
                    using (SqlConnection con1 = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            SqlCommand cmd1 = new SqlCommand();
                            cmd1.Connection = con1;
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "UspSelectEmployee";
                            cmd1.Parameters.AddWithValue("@id", int.Parse(Console.ReadLine()));

                            con1.Open();
                            SqlDataReader reader = cmd1.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine(String.Format("{0} {1} {2} {3} {4} {5}", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));
                            }
                            con1.Close();
                        }
                        catch (Exception e)
                        {
                            errorException.errorhandle(e);
                        }
                    }
                }
                else if (x == 3)
                {
                    using (SqlConnection con1 = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            SqlCommand cmd1 = new SqlCommand();
                            cmd1.Connection = con1;
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.CommandText = "UspDeleteEmployee";
                            cmd1.Parameters.AddWithValue("@id", int.Parse(Console.ReadLine()));

                            con1.Open();
                            SqlDataReader reader = cmd1.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine(String.Format("{0} {1} {2} {3} {4} {5}", reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));
                            }
                            con1.Close();
                        }
                        catch (Exception e)
                        {
                            errorException.errorhandle(e);
                        }
                    }
                }
                else if (x == 5)
                {
                    using (SqlConnection con2 = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            SqlCommand cmd1 = new SqlCommand("Select * from employee", con2);
                            SqlDataAdapter DataAdapter = new SqlDataAdapter();
                            DataAdapter.SelectCommand = cmd1;
                            con2.Open();

                            DataTable dt = new DataTable();

                            DataAdapter.Fill(dt);

                            foreach (DataRow dataRow in dt.Rows)
                            {
                                foreach (var item in dataRow.ItemArray)
                                {
                                    Console.Write(item + " ");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            errorException.errorhandle(e);
                        }
                        con2.Close();
                    }
                }
            }
        }
    }
}
