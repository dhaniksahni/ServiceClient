using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int Input = 0;
            Console.WriteLine("For Account : 111");
            Console.WriteLine();
            Console.WriteLine("1 for balance :");
            Console.WriteLine("2 for Withdrawl :");
            Console.WriteLine("3 for Deposit :");
            Console.WriteLine("4 for Exit");
            do
            {                
                Console.WriteLine("Enter your Choice : ");
                Input = Convert.ToInt32( Console.ReadLine());

                if (Input == 1)
                    Balance();
                else if (Input == 2)
                {
                    Console.WriteLine("Please Enter The Amount to WithDraw");                    
                    int Amount = Convert.ToInt32(Console.ReadLine());
                    Withdraw(Amount);
                }
                else if (Input == 3)
                {
                    Console.WriteLine("Please Enter The Amount to Deposit");                    
                    int Amount = Convert.ToInt32(Console.ReadLine());
                    Deposit(Amount);
                }
                else if (Input == 4)                
                    break;                
                else 
                {
                    Console.WriteLine("Please Enter Above Mentioned Input");
                    Console.ReadLine();
                }                
            } while (Input != 4);
        }

        private static void Withdraw(int amount)
        {
            HttpClient client1 = new HttpClient();
            object Account = new
            {
                AccountNumber = "111",
                Amount = amount,
                Currency = "$"
            };
            var jsondata = JsonConvert.SerializeObject(Account);
            var stringContent = new StringContent(jsondata,
                          UnicodeEncoding.UTF8,
                          "application/json");

            HttpResponseMessage wcfResponse = client1.PostAsync("http://localhost:59461/RESTfulTransactionService.svc/withdraw", stringContent).Result;
            HttpContent stream1 = wcfResponse.Content;
            var json1 = stream1.ReadAsStringAsync().Result;
            dynamic data1 = JObject.Parse(json1);            
            Console.WriteLine(data1.WithdrawResult.Message);
            Console.WriteLine("Balance : " + data1.WithdrawResult.Balance);
            Console.ReadLine();
            return;
        }

        private static void Deposit(int amount)
        {
            HttpClient client1 = new HttpClient();
            object Account = new
            {
                AccountNumber = "111",
                Amount = amount,
                Currency = "$"
            };
            var jsondata = JsonConvert.SerializeObject(Account);
            var stringContent = new StringContent(jsondata,
                          UnicodeEncoding.UTF8,
                          "application/json");

            HttpResponseMessage wcfResponse = client1.PostAsync("http://localhost:59461/RESTfulTransactionService.svc/deposit", stringContent).Result;
            HttpContent stream1 = wcfResponse.Content;
            var json1 = stream1.ReadAsStringAsync().Result;
            dynamic data1 = JObject.Parse(json1);
            Console.WriteLine("Deposit Successfull. New Balance : " + data1.DepositResult.Balance);
            Console.ReadLine();
            return;
        }

        private static void Balance()
        {
            string method = "balance";
            string AccountNumber = "111";
            HttpClient client = new HttpClient();
            HttpResponseMessage wcfResponse = client.GetAsync("http://localhost:59461/RESTfulTransactionService.svc/" + method + "/" + AccountNumber).Result;
            HttpContent stream = wcfResponse.Content;
            var json = stream.ReadAsStringAsync().Result;
            dynamic data = JObject.Parse(json);
            Console.WriteLine(data.Balance);
        }
    }
}
