using Generators;
using System.Net.Http.Json;

var patients = new PatiensGenerator().Generate(100);
HttpClient httpClient = new HttpClient();
JsonContent content = JsonContent.Create(patients);
using var request = new HttpRequestMessage(HttpMethod.Post, "http://patientapplication:80/patients/range");
request.Content = content;
// отправляем запрос
using var response = httpClient.Send(request);