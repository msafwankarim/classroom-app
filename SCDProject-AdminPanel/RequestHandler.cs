using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using URSBackend.Models;
using Newtonsoft.Json;
using System.Net.Security;
using System.Net;

namespace SCDProject_AdminPanel
{

    //class User
    //{
    //    public string Username { get; set; }
    //    public string Password { get; set; }

    //    public string GetJson()
    //    {
    //        return $"{{ \"username\" : \"{Username}\", \"password\" : \"{Password}\"}}";
    //    }
    //}

    class RequestHandler
    {
        public static string url = "http://192.168.43.164:5000/api/";
        public static HttpClientHandler handler = new HttpClientHandler();
        static HttpClient client;
        static RequestHandler()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
    (sender, cert, chain, sslPolicyErrors) => true;
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            client = new HttpClient(handler);

            client.BaseAddress = new Uri(url);
            

        }

        public static async Task<bool> Login(User user)
        {
            HttpContent content = new StringContent($"{user.GetJson()}", Encoding.UTF8, "application/json");

            //MessageBox.Show($"{user.GetJson()}");
            var response = await client.PostAsync("login",content).ConfigureAwait(continueOnCapturedContext: false);
            //MessageBox.Show(response.StatusCode.ToString());
            return response.IsSuccessStatusCode;
        }

        
        public static async Task<string> SaveRecord(string url, ITransferable student)
        {
            HttpContent content = new StringContent($"{student.GetJson()}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content).ConfigureAwait(continueOnCapturedContext: false);
            return response.Content.ReadAsStringAsync().Result;

        }
        

        public static async Task<string> GetRecord(string url)
        {
            var response = await client.GetAsync(url).ConfigureAwait(continueOnCapturedContext: false);
            return response.Content.ReadAsStringAsync().Result;
            
        }

        

        public static async Task<string> UpdateRecord(string url, ITransferable student)
        {
            try
            {
                HttpContent content = new StringContent($"{student.GetJson()}", Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, content).ConfigureAwait(continueOnCapturedContext: false);
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    MessageBox.Show($"{response.StatusCode} {response.Content}");
                    return "";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
            }
            return "";

        }

       

        public static async void RemoveRecord(string url)
        {
            var response = await client.DeleteAsync(url).ConfigureAwait(continueOnCapturedContext: false);

            if (!response.IsSuccessStatusCode)
                MessageBox.Show(response.StatusCode.ToString());
        }

        

        

    }



}
