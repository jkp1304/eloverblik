using Newtonsoft.Json.Linq;
using System.Text;
using MySqlConnector;
using System.Globalization;
using Newtonsoft.Json;

class Program
{

    static async Task Main()
    {
        string jwtRefreshToken = "xxxxxxxxxx";
        string tokenEndpoint = "https://api.eloverblik.dk/customerapi/api/token";
        string meteringPointId = "xxxxxx";
        string connectionString = "Server=xxx.xxx.xxx.xxx; Port=xxxx; Database=xxxxxxx; Uid = xxxxxxxx;Pwd = xxxxxxxxxxx";

        // Finder start og slut dato.
        DateTime startDate = DateTime.Today.AddDays(-2);
        DateTime endDate = DateTime.Today.AddDays(-1);


        using (HttpClient client = new HttpClient())
        {
            // Tilføj JWT-refresh token til Authorization header
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtRefreshToken);

            // Udfør anmodning om access token
            HttpResponseMessage response = await client.GetAsync(tokenEndpoint);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                dynamic tokenData = JsonConvert.DeserializeObject(responseContent);
                string accessToken = tokenData.result;

                // Tilføj access token til anmodningens header
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                // Opret anmodningens krop (request body)
                string requestBody = $"{{ \"meteringPoints\": {{ \"meteringPoint\": [ \"{meteringPointId}\" ] }} }}";
                StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                string apiurl = $"https://api.eloverblik.dk/customerapi/api/meterdata/gettimeseries/{startDate:yyyy-MM-dd}/{endDate:yyyy-MM-dd}/Day";


                // Send POST-anmodning til API'en
                HttpResponseMessage response2 = await client.PostAsync($"https://api.eloverblik.dk/customerapi/api/meterdata/gettimeseries/{startDate:yyyy-MM-dd}/{endDate:yyyy-MM-dd}/Day", content);

                // Håndter API-svaret
                if (response2.IsSuccessStatusCode)
                {
                    // Læs API-svaret
                    string apiResponse = await response2.Content.ReadAsStringAsync();

                    // Pars JSON-svaret som et objekt
                    JObject jsonObject = JObject.Parse(apiResponse);

                    // Find værdierne for "period.timeInterval" start og end
                    //JToken startTimeToken = jsonObject["result"]?[0]?["MyEnergyData_MarketDocument"]?["period.timeInterval"]?["start"];
                    JToken endTimeToken = jsonObject["result"]?[0]?["MyEnergyData_MarketDocument"]?["TimeSeries"]?[0]?["Period"]?[0]?["timeInterval"]?["end"];
                    JToken quantityToken = jsonObject["result"]?[0]?["MyEnergyData_MarketDocument"]?["TimeSeries"]?[0]?["Period"]?[0]?["Point"]?[0]?["out_Quantity.quantity"];

                    if (endTimeToken != null && quantityToken != null)
                    {
                        CultureInfo culture = CultureInfo.InvariantCulture;
                        string format = "MM/dd/yyyy HH:mm:ss";
                        DateTime endTime;
                        string endTimeString = endTimeToken.Value<string>();

                        string quantityValueString = quantityToken.Value<string>();

                        if (DateTime.TryParseExact(endTimeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime))
                        {
                            //Console.WriteLine("Aflæsnings dato: " + endTime);

                            if (decimal.TryParse(quantityValueString, NumberStyles.Float, culture, out decimal quantityValue))
                            {
                                //Console.WriteLine("Aflæst forbrug: " + quantityValue);

                                using (MySqlConnection connection = new MySqlConnection(connectionString))
                                {
                                    connection.Open();

                                    // Indsæt data i MySQL-databasen
                                    string insertQuery = "INSERT INTO XXXXXXXX (dato, forbrug) VALUES (@dato, @forbrug)";
                                    MySqlCommand command = new MySqlCommand(insertQuery, connection);
                                    command.Parameters.AddWithValue("@dato", endTime);
                                    command.Parameters.AddWithValue("@forbrug", quantityValue);
                                    command.ExecuteNonQuery();

                                    connection.Close();
                                    Console.WriteLine("Data aflæst og gemt i db:");
                                    Console.WriteLine($"Dato: {endTime}");
                                    Console.WriteLine("Kwh " + quantityValue);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Noget gik galt i konveteringen af data");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Fejl ved hentning af access token: " + response.ReasonPhrase + " Response Content " + response.Content);
                Console.ReadLine();
            }
        }



    }

}