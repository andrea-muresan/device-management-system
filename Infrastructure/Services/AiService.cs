using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public interface IAiService
{
    Task<string> GetDeviceDescriptionAsync(string specs);
}

public class AiService(HttpClient httpClient, IConfiguration configuration) : IAiService
{
    public async Task<string> GetDeviceDescriptionAsync(string specsDev)
    {
        string? apiKey = configuration["GoogleAI:ApiKey"];
        Console.Write(apiKey);

        string prompt = $@"
            Task: Translate the following technical data into a single, user-friendly sentence for a business catalog.
            Input: Name: {specsDev}
            Goal: Write a concise, professional sentence that focuses on the brand and how the device serves an employee.
            Guidline: 
                - Make it sound helpful and easy to understand.
                - Provide only the answer.
                - Exemple Output: 'A high-performance Apple smartphone running iOS, suitable for daily business use.'
            ";
                

        var requestBody = new
        {
            contents = new[] { 
                new { 
                    parts = new[] {
                         new { text = prompt } 
                    } 
                } 
            }
        };

        var request = new HttpRequestMessage (
            HttpMethod.Post,
            "https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-latest:generateContent"
        );

        request.Headers.Add("X-goog-api-key", apiKey);

        request.Content = new StringContent (
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            using var responseContent = await response.Content.ReadAsStreamAsync();
            using var jsonDoc = await JsonDocument.ParseAsync(responseContent);

            var text = jsonDoc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return text ?? "No response from Gemini";
        } else
        {
            throw new HttpRequestException($"Error: {response.StatusCode}, {await response.Content.ReadAsByteArrayAsync()}");
        }
    }
}
