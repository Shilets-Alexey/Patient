using System;
using System.Net.Http;
using System.Net.Http.Json;
using Faker;
using Generators;

namespace PatientsGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var patients = new PatiensGenerator().Generate(100);
            HttpClient httpClient = new HttpClient();
            JsonContent content = JsonContent.Create(patients);
            using var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7162/patients/range");
            request.Content = content;
            // отправляем запрос
            using var response = httpClient.Send(request);
        }
    }
}
