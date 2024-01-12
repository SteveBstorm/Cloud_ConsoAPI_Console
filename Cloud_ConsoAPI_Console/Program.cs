using Cloud_ConsoAPI_Console;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

string url = "https://localhost:7152/api/";

HttpClient client = new HttpClient();
client.BaseAddress = new Uri(url);


#region Get

List<Movie> liste = new List<Movie>();

try
{
    using (HttpResponseMessage message = await client.GetAsync("movie"))
    {
        message.EnsureSuccessStatusCode();

        string json = await message.Content.ReadAsStringAsync();
        Console.WriteLine(json);

        liste = JsonConvert.DeserializeObject<List<Movie>>(json);

    }
}
catch (HttpRequestException ex)
{
    Console.WriteLine(ex.Message);
}

foreach (Movie movie in liste)
{
    Console.WriteLine($"{movie.Id} - {movie.Title} - {movie.ReleaseYear}");
}

#endregion


#region POST

Movie m = new Movie() { Id = 5, Title = "Test Conso API", ReleaseYear = 2024 };

string newJson = JsonConvert.SerializeObject(m);
HttpContent content = new StringContent(newJson, Encoding.UTF8, "application/json");

using (HttpResponseMessage message = await client.PostAsync("movie", content))
{
    if (message.StatusCode == HttpStatusCode.OK)
    {
        Console.WriteLine("ok");
    }
} 
#endregion