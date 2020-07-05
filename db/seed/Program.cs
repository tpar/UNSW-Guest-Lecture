using System;
using System.Data.SqlClient;
using System.IO;

namespace seed
{
    class Program
    {

        const string ConnectionString = @"";
        const int TargetUsers = 100;

        private static DateTime GenerateRandomDateTime()
        {
            DateTime start = new DateTime(2019, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays((new Random()).Next(range));
        }

        private static void SeedCustomers()
        {
            var firstNames = File.ReadAllLines("../../../names/first_names.txt");
            var lastNames = File.ReadAllLines("../../../names/last_names.txt");
            const string sql = "INSERT INTO dbo.Customer (Auth0_Ref,FirstName,LastName,Email,LastSeen) VALUES (@Auth0_Ref,@FirstName,@LastName,@Email,@LastSeen)";

            var random = new Random();
            using (var connection = new SqlConnection(ConnectionString))
            {
                for (int i = 0; i < TargetUsers; i++)
                {
                    var firstName = PickRandom(firstNames);
                    var lastName = PickRandom(lastNames);
                    var auth0_ref = Guid.NewGuid().ToString("N");
                    var email = $"{firstName}.{lastName}@email.com";
                    var lastSeen = GenerateRandomDateTime();

                    {
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@FirstName", firstName);
                            command.Parameters.AddWithValue("@LastName", lastName);
                            command.Parameters.AddWithValue("@Auth0_ref", auth0_ref);
                            command.Parameters.AddWithValue("@Email", email);
                            command.Parameters.AddWithValue("@LastSeen", lastSeen);

                            connection.Open();
                            int result = command.ExecuteNonQuery();

                            // Check Error
                            if (result < 0)
                                Console.WriteLine("Error inserting data into Database!");
                        }
                    }
                }
            }
        }

        private static string PickRandom(string[] list)
        {
            return list[new Random().Next(0, list.Length - 1)].Trim();
        }

        private static int GenerateRandomNumberString(double digits)
        {
            int min = (int)Math.Pow(10, digits-1);
            int max = (int)(Math.Pow(10, digits) - 1);

            return new Random().Next(min, max);
        }

        private static void SeedAccounts()
        {

            const string sql = "INSERT INTO dbo.Account (CustomerId,AccountName,AccountType,AccountBalance,Bsb,AccountNumber) VALUES (@CustomerId,@AccountName,@AccountType,@AccountBalance,@Bsb,@AccountNumber)";
            var accountNames = File.ReadAllLines("../../../names/account_names.txt");
            var accountTypes = new string[] { "Savings", "Spending", "Everyday", "Cash Management" };


            var random = new Random();
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                for (int i = 1; i < TargetUsers + 1; i++)
                {
                    var accounts = random.Next(7, 20);
                    for (int j =0; j<accounts; j++)
                    {
                        int customerId = i;
                        string accountName = PickRandom(accountNames);
                        string accountType = PickRandom(accountTypes);
                        string bsb = $"0{GenerateRandomNumberString(2)}-{GenerateRandomNumberString(3)}";
                        string accountnumber = $"{GenerateRandomNumberString(3)}-{GenerateRandomNumberString(3)}";
                        decimal accounBalance = Math.Round((decimal)(random.NextDouble() * (50000 - 10) + 10), 2);

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@CustomerId", customerId);
                            command.Parameters.AddWithValue("@AccountName", accountName);
                            command.Parameters.AddWithValue("@AccountType", accountType);
                            command.Parameters.AddWithValue("@AccountBalance", accounBalance);
                            command.Parameters.AddWithValue("@Bsb", bsb);
                            command.Parameters.AddWithValue("@AccountNumber", accountnumber);

                            int result = command.ExecuteNonQuery();

                            // Check Error
                            if (result < 0)
                                Console.WriteLine("Error inserting data into Database!");
                        }
                    }
                    
                }
            }
            
        }


        static void Seed()
        {
            SeedCustomers();
            SeedAccounts();
        }

        static void Main(string[] args)
        {
            Seed();
            Console.WriteLine("Complete");
        }
    }
}
