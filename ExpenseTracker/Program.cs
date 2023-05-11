using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace FirstApp
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server=IN-F0979S3; database=ExpenseTracker; Integrated Security=true");
            con.Open();

            
            while (true)
            {
                Console.WriteLine("** EXPENSE TRACKER APP **");
                Console.WriteLine("1. Add Transaction details");
                Console.WriteLine("2. View Income");
                Console.WriteLine("3. View Expense");
                Console.WriteLine("4. AvailableBalance");
                Console.WriteLine("Enter Choice");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:

                        {
                            //ip from user for adding transaction
                            SqlCommand cmd = new SqlCommand($"insert into Tracker values(@Title, @Descriptions, @Amount, @TDate)", con);
                            Console.WriteLine("Enter Title: ");
                            string title = Console.ReadLine();
                            Console.WriteLine("Enter Description: ");
                            string descript = Console.ReadLine();
                            Console.WriteLine("Enter Amount: ");
                            double amount = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter Date: ");
                            DateTime tdate = Convert.ToDateTime(Console.ReadLine());

                            cmd.Parameters.AddWithValue("@Title", title);
                            cmd.Parameters.AddWithValue("@Descriptions", descript);
                            cmd.Parameters.AddWithValue("@Amount", amount);
                            cmd.Parameters.AddWithValue("@TDate", tdate);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Inserted Successfully");
                            con.Close();
                            break;
                        }
                        case 2:
                        {

                            

                            SqlCommand cmd = new SqlCommand($"select * from Tracker where Amount>0", con);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                for (int i = 0; i < dr.FieldCount; i++)
                                {
                                    Console.WriteLine(dr[i]);
                                }
                            }
                            dr.Close();
                            break;
                        }
                    case 3:
                        {
                            
                            SqlCommand cmd = new SqlCommand($"select * from Tracker where Amount<0", con);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                for (int i = 0; i < dr.FieldCount; i++)
                                {
                                    Console.WriteLine(dr[i]);
                                }
                            }
                            dr.Close();
                            break;
                        }
                    case 4:
                        {

                            SqlCommand cmd = new SqlCommand("select sum(Amount) as AvailableBalance from Tracker", con);
                            int bal = (int)cmd.ExecuteScalar();
                            Console.WriteLine($"Available Balance is {bal}");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid Choice");
                            break;
                        }
                }
            }
            con.Close();

        }
        
    }
}