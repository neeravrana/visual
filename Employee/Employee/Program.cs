using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeDetails
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
        public override String ToString()
        {

            string s = ("employeeid:" + EmployeeID + "\n" +
            "employeename" + EmployeeName + "\n" +
            "Salary:" + Salary + "\n" +
            "designation:" + Designation + "\n" +
            "Doj:" + Doj.ToString()) + "\n" +
            "PhoneNumber" + PhoneNumber;
            return s;

        }
        public bool AddEmployee(List<Employee> employees)
        {

            try
            {
                int id, sal;
                Console.WriteLine("Add id");
                id = int.Parse(Console.ReadLine());
                foreach (Employee item in employees)
                {
                    if (item.id == id)
                    {
                        Console.WriteLine("Already Exists");
                        return false;
                    }
                }
                EmployeeID = id;
                Console.WriteLine("add name");
                string name = Console.ReadLine();
                EmployeeName = name;
                Console.WriteLine("Add designation");
                string desig = Console.ReadLine();
                Designation = desig;
                Console.WriteLine("Add salary");
                sal = int.Parse(Console.ReadLine());
                Salary = sal;
                Console.WriteLine("Add Doj");
                DateTime date = DateTime.Parse(Console.ReadLine());
                Doj = date;
                Console.WriteLine("Add phonenumber");
                int phone = int.Parse(Console.ReadLine());
                PhoneNumber = phone;
                return true;
            }
            catch (Exception e)
            {
                errorException.errorhandle(e);
                //ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                Logger.Error("Alert");
                return false;
            }
        }

    }
    class Employee_SortBySalaryByAscendingOrder : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            if (x.salary > y.salary) return 1;
            else if (x.salary < y.salary) return -1;
            else return 0;
        }

    }
    class Employee_SortByName : IComparer<Employee>
    {


        public int Compare(Employee x, Employee y)
        {
            return string.Compare(x.name, y.name);
        }


    }
    class Employee_SortByIDByAscendingOrder : IComparer<Employee>
    {


        public int Compare(Employee x, Employee y)
        {
            if (x.id > y.id) return 1;
            else if (x.id < y.id) return -1;
            else return 0;
        }

    }
    class Employee_SortByDesig : IComparer<Employee>
    {


        public int Compare(Employee x, Employee y)
        {
            return string.Compare(x.Design, y.Design);
        }


    }
    class Employee_SortByPhone : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            if (x.phone > y.phone) return 1;
            else if (x.phone < y.phone) return -1;
            else return 0;
        }

    }
    class Employee_SortByDoj : IComparer<Employee>
    {


        public int Compare(Employee x, Employee y)
        {
            int result = DateTime.Compare(x.date, y.date);
            if (result < 0) return -1;
            else if (result == 0) return 0;
            else return 1;
        }


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
            x =int.Parse( Console.ReadLine());

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
            List<Employee> employees = new List<Employee>();
        A: Console.WriteLine("1.Add Employee");
            Console.WriteLine("2.Update Employee");
            Console.WriteLine("3.Delete Employee");
            Console.WriteLine("4.Display");
            Console.WriteLine("5.Searching");
            Console.WriteLine("6.Sorting");
            Console.WriteLine("7.Exit");

            int ch = int.Parse(Console.ReadLine());
            try
            {
                if (ch < 8 && ch > 0)
                {
                    switch (ch)
                    {
                        case 1:
                            {
                                Employee empl = new Employee();
                                if (empl.AddEmployee(employees))
                                {

                                    employees.Add(empl);
                                    foreach (Employee item in employees)
                                    {
                                        Console.WriteLine(item);
                                        TextWriter tw = new StreamWriter(@"D:\WriteLine.txt");

                                        foreach (Employee items in employees)
                                            tw.WriteLine(items);

                                        tw.Close();


                                    }
                                    goto A;
                                }
                                else
                                    goto A;
                            }
                        case 2:
                            {
                                Employee emp2 = new Employee();
                                Console.WriteLine("Give EmployeeId");
                                int id = int.Parse(Console.ReadLine());
                                Console.WriteLine("1.Update name");
                                Console.WriteLine("2.Update designation");
                                Console.WriteLine("3.update salary");
                                Console.WriteLine("4.update phonenumber");
                                int a = int.Parse(Console.ReadLine());
                                switch (a)
                                {
                                    case 1:
                                        {
                                            Console.WriteLine("Change Name");
                                            string str = Console.ReadLine();
                                            foreach (Employee item in employees)
                                            {
                                                if (item.id == id)
                                                {
                                                    item.name = str;
                                                    emp2 = item;
                                                }
                                            }
                                            goto A;
                                        }
                                    case 2:
                                        {
                                            Console.WriteLine("Change designation");
                                            string str1 = Console.ReadLine();
                                            foreach (Employee item in employees)
                                            {
                                                if (item.id == id)
                                                {
                                                    item.Design = str1;
                                                }
                                            }
                                            goto A;
                                        }
                                    case 3:
                                        {
                                            Console.WriteLine("Change Salary");
                                            int int1 = int.Parse(Console.ReadLine());
                                            foreach (Employee item in employees)
                                            {
                                                if (item.id == id)
                                                {
                                                    item.salary = int1;
                                                }
                                            }
                                            goto A;
                                        }
                                    case 4:
                                        {
                                            Console.WriteLine("Change phonenumber");
                                            int int1 = int.Parse(Console.ReadLine());
                                            foreach (Employee item in employees)
                                            {
                                                if (item.id == id)
                                                {
                                                    item.phone = int1;
                                                }
                                            }
                                            goto A;

                                        }
                                }
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Enter the id");
                                int id = int.Parse(Console.ReadLine());
                                employees.RemoveAll(item => item.id == id);
                                foreach (Employee item in employees)
                                {
                                    Console.WriteLine(item);
                                    TextWriter tw = new StreamWriter(@"D:\WriteLine.txt");

                                    foreach (Employee items in employees)
                                        tw.WriteLine(items);

                                    tw.Close();

                                }
                                goto A;

                            }
                        case 4:
                            {
                                string line;
                                foreach (Employee item in employees)
                                {
                                    System.IO.StreamReader file =
                                    new System.IO.StreamReader(@"D:\WriteLine.txt");
                                    while ((line = file.ReadLine()) != null)
                                    {
                                        System.Console.WriteLine(line);

                                    }

                                    file.Close();

                                }
                                goto A;
                            }
                        case 5:
                            {
                                Console.WriteLine("Based on what you want to search");
                                string s = Console.ReadLine();
                                if (s == "id")
                                {
                                    Console.WriteLine("Enter the item you want to search");
                                    var v = int.Parse(Console.ReadLine());
                                    var ad = employees.Find(item => item.id == v);
                                    Console.WriteLine(ad);
                                }
                                else if (s == "name")
                                {
                                    Console.WriteLine("Enter the item you want to search");
                                    var v = (Console.ReadLine());
                                    var ad = employees.Find(item => item.name == v);
                                    Console.WriteLine(ad);
                                }
                                else if (s == "desig")
                                {
                                    Console.WriteLine("Enter the item you want to search");
                                    var v = (Console.ReadLine());
                                    var ad = employees.Find(item => item.Design == v);
                                    Console.WriteLine(ad);
                                }
                                else if (s == "salary")
                                {
                                    Console.WriteLine("Enter the item you want to search");
                                    var v = int.Parse(Console.ReadLine());
                                    var ad = employees.Find(item => item.salary == v);
                                    Console.WriteLine(ad);
                                }
                                else if (s == "phone")
                                {
                                    Console.WriteLine("Enter the item you want to search");
                                    var v = int.Parse(Console.ReadLine());
                                    var ad = employees.Find(item => item.phone == v);
                                    Console.WriteLine(ad);
                                }
                                goto A;

                            }
                        case 6:
                            {
                                Console.WriteLine("Sortby:");
                                string str2 = Console.ReadLine();
                                List<Employee> sortedList = new List<Employee>();
                                if (str2 == "id")
                                {
                                    sortedList = employees.OrderBy(e => e.id).ToList();
                                    //Employee_SortByIDByAscendingOrder eAsc =
                                    //      new Employee_SortByIDByAscendingOrder();
                                    // Sort Employees by salary by ascending order.

                                    sortedList.ToList();

                                }
                                else if (str2 == "name")
                                {
                                    sortedList = employees.OrderBy(e => e.name).ToList();
                                    //Employee_SortByName eName = new Employee_SortByName();
                                    // Sort Employees by their names.                                 
                                    sortedList.ToList();

                                }
                                else if (str2 == "designation")
                                {
                                    sortedList = employees.OrderBy(e => e.Design).ToList();
                                    //Employee_SortByDesig eDesig = new Employee_SortByDesig();
                                    // Sort Employees by their names.                                 
                                    //employees.Sort(eDesig);
                                    sortedList.ToList();
                                }
                                else if (str2 == "salary")
                                {
                                    //Employee_SortBySalaryByAscendingOrder eAsc =
                                    //      new Employee_SortBySalaryByAscendingOrder();
                                    // Sort Employees by salary by ascending order.

                                    //employees.Sort(eAsc);
                                    sortedList = employees.OrderBy(e => e.salary).ToList();
                                    sortedList.ToList();
                                }
                                else if (str2 == "doj")
                                {
                                    sortedList = employees.OrderBy(e => e.date).ToList();
                                    //Employee_SortByDoj eDesig = new Employee_SortByDoj();
                                    // Sort Employees by their names.                                 
                                    //employees.Sort(eDesig);
                                    sortedList.ToList();

                                }
                                else if (str2 == "phone")
                                {
                                    // Employee_SortByPhone ephone = new Employee_SortByPhone();
                                    // Sort Employees by their names.                                 
                                    //employees.Sort(ephone);
                                    sortedList = employees.OrderBy(e => e.phone).ToList();
                                    sortedList.ToList();

                                }
                                foreach (Employee item in sortedList)
                                {
                                    Console.WriteLine(item);

                                }
                                Console.WriteLine("================================");
                                goto A;
                            }
                        case 7:
                            Console.WriteLine("Bye Bye!!");
                            break;



                    }
                }
                else
                {
                    Console.WriteLine("Invalid value");
                    Console.WriteLine("================================");
                    goto A;
                }
            }
            catch (Exception e)
            {
                errorException.errorhandle(e);

            }
        }
    }
}
