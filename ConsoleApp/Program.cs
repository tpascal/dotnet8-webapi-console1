using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;
using System.IO;
using ConsoleApp.Models;
using System.Net.Http.Headers;

using HttpClient client = new();

//RunWebRequest();
await RunHttpClient(client);

static async Task RunHttpClient(HttpClient client)
{
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:127.0) Gecko/20100101 Firefox/127.0");

    try
    {
        var stations = Stations.GetStations();
        var sortedStations = from s in stations orderby s.Key ascending select s;

        Console.WriteLine($"----------------------------------------");
        var d = sortedStations.ToDictionary(pair => pair.Key, pair => pair.Value);

        for (int i = 0; i < d.Count(); i++)
        {
            Console.WriteLine($"| {i} | {d.ElementAt(i).Key} ({d.ElementAt(i).Value})");
            Console.WriteLine($"----------------------------------------");
        }

        Console.WriteLine($"Which weather observation (0 - {d.Count() - 1})?");
        var observation = Console.ReadLine();
        //Console.WriteLine($"You typed: {observation}");

        var stationId = 0;
        int.TryParse(observation, out var isInteger);
        if (isInteger > -1 && isInteger >= 0 && isInteger <= sortedStations.Count())
        {
            Console.WriteLine($"Retrieving data for the station at {sortedStations.ElementAt(isInteger).Key}");
            stationId = sortedStations.ElementAt(isInteger).Value;
        }
        else
        {
            // fallback station
            stationId = stations["Adelaide Airport"];
        }

        var result = (await ProcessResponseAsync(client, stationId)).observations;
        if (result != default && result.data != default)
        {
            var count = 0;
            var tempTotal = 0.0;
            foreach (var item in result.data)
            {
                if(item.air_temp == default) continue;

                count++;
                var airTemp = (double)item.air_temp;
                tempTotal += airTemp;
            }

            if(count > 0)
                Console.WriteLine($"Average temperature: {(tempTotal / count).ToString("#.##")} degrees");
            else
                Console.WriteLine($"There was not enough temperature data available to calculate average temperature for the chosen weather observation station");
        }
    }
    catch (System.Exception ex)
    {
        Console.WriteLine($"Unexpected error: {ex.Message}");
    }
}

static async Task<WeatherObservationResponse> ProcessResponseAsync(HttpClient client, int stationId)
{
    var url = $"http://www.bom.gov.au/fwo/IDS60801/IDS60801.{stationId}.json";
    await using Stream stream = await client.GetStreamAsync(url);

    var options = new JsonSerializerOptions 
    { 
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    var result = await JsonSerializer.DeserializeAsync<WeatherObservationResponse>(stream, options);
    return result ?? new();
}

#region  Deprecated
static void RunWebRequest()
{
    var stations = Stations.GetStations();
    var sortedStations = from s in stations orderby s.Key ascending select s;

    Console.WriteLine($"----------------------------------------");
    var d = sortedStations.ToDictionary(pair => pair.Key, pair => pair.Value);

    for (int i = 0; i < d.Count(); i++)
    {
        Console.WriteLine($"| {i} | {d.ElementAt(i).Key} ({d.ElementAt(i).Value})");
        Console.WriteLine($"----------------------------------------");
    }

    Console.WriteLine($"Which observation? (0 - {d.Count() - 1})");
    var observation = Console.ReadLine();

    var stationId = 0;
    int.TryParse(observation, out var isInteger);
    if (isInteger > -1 && isInteger >= 0 && isInteger <= sortedStations.Count())
    {
        Console.WriteLine($"Element at {sortedStations.ElementAt(isInteger).Key}");
        stationId = sortedStations.ElementAt(isInteger).Value;
    }
    else
    {
        stationId = stations["Adelaide Airport"];
    }

    var response = Get(stationId);
    ProcessJson(response);
}

static WebResponse Get(int stationId)
{
    var urlJson = $"http://www.bom.gov.au/fwo/IDS60801/IDS60801.{stationId}.json";

    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlJson);
    request.Method = "GET";
    request.Accept = "application/json";
    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:127.0) Gecko/20100101 Firefox/127.0";
    WebResponse response = request.GetResponse();

    return response;
}

static void ProcessJson(WebResponse response)
{
    var result = JsonSerializer.Deserialize<WeatherObservationResponse>(response.GetResponseStream());

    var count = 0;
    var tempTotal = 0.0;
    foreach (var item in result.observations.data)
    {
        count++;
        var airTemp = (double)item.air_temp;
        tempTotal += airTemp;
    }

    Console.WriteLine($"Average : {tempTotal / count}");
}
#endregion