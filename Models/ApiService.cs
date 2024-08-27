using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using WEB_APP_Panaderia.Services;

namespace WEB_APP_Panaderia.Models
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public List<Email> GetEmails()
        {
            using (var client = new WebClient())
            {
                var json = client.DownloadString(_baseUrl + "api/email");
                return JsonConvert.DeserializeObject<List<Email>>(json);
            }
        }
        public void DeleteEmail(int id)
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.UploadString($"{_baseUrl}api/email/{id}", "DELETE", "");
            }
        }

        public void SendEmail(Email email)
        {
            using (var client = new WebClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(email);
                    Console.WriteLine($"Sending email: {json}");
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var response = client.UploadString(_baseUrl + "api/email", "POST", json);
                    Console.WriteLine($"Response: {response}");
                }
                catch (WebException ex)
                {
                    using (var stream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                    {
                        string error = reader.ReadToEnd();
                        Console.WriteLine($"Error sending email: {error}");
                        throw new Exception($"Error sending email: {error}", ex);
                    }
                }
            }
        }

        public List<Cliente> GetClientes()
        {
            using (var client = new WebClient())
            {
                var json = client.DownloadString(_baseUrl + "api/email/clientes");
                return JsonConvert.DeserializeObject<List<Cliente>>(json);
            }
        }
    }
}
