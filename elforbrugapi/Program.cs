using Newtonsoft.Json.Linq;
using System.Text;
using MySqlConnector;
using System.Globalization;
using Newtonsoft.Json;

class Program
{

    static async Task Main()
    {
        string jwtRefreshToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0b2tlblR5cGUiOiJDdXN0b21lckFQSV9SZWZyZXNoIiwidG9rZW5pZCI6ImQyZjAwOTBmLWM0ZjAtNGZiYi04ZWE4LWU0ZmM0M2QyNDYxMSIsIndlYkFwcCI6IkN1c3RvbWVyQXBwIiwidmVyc2lvbiI6IjIiLCJpZGVudGl0eVRva2VuIjoieWF1WFVVKzRLQXh0c3VBN3JScE9UdUlCM0ErVHdNZ2l3TlNoODNWRmJwRWF2c3hBYUVhTGVIeWZMdlo5b2FVUnRXZUx3TGFLalNkc2tnOEZtMVdDUXlPUGhTek1SU21Pd1JXSnptVXdXZnIrRklpTlhkeFFVUVlGZEVZdUNCZnkyYkV6YWxycW1FOWw1UXYrVWpQRUpwVmhzN2hpK2s0VVhYalh3SHlxOGdzQ3NuQXkwYWJMWEpDTVV3YmUzcUNzT0J5ZjN0aDloRS9LZkxoQmpUbm1kQlkvWGxGSUcrNlRHRDRTZ1FacDNiSHhEdzV4eTRUQy9xWnk0ckkvU2t3b3FtNFVtVEdXaU90UkFxc0cwOUFUZ0hpSUJwMFJwaXlBQnR6NWJWa2hJZmg2T0tFeUJSMzc5V2h3Qy9XYkN6R21URGpsZTZSRTh6d2MyTlNpZ3pSbFJscDQ2a3ZEOGJQRFpxbXpmWnRpc2JRSkVyU0lzUklHaklBRlMzUUtSMGtqT2RsVHFUREovaStmM1Z1NkVYdFl1b1IrVFV0NXFuWmgycllmT28vQXhmRDVBZFFubXlTNkI5L2VQL2pTM2o1ZzVnUHVMTUFOZ2tOOUdPdGZzMUhLSUoyaXdaeE4xdlVZZklQcjVyRkprNlp3eHVBTU9jNGhuM2tTOStTMm1WYVQwbGVVSnpadTV0NFR4MmZ0SDBHTVFpbGl5QjVRRnBZRGozTGlaOXNTQUMycDZJTFVrYU5aMGgyNEJzTlNhbWxqbmlxTHUzWldpMG5ObSs3c2VOUy9wR3JmSi9UWVE0WEhJd3gyWVN1M0lORUFiTDhtYmJBTnZ5c1E4ZlJlSFdla21GS294ZjloRnJGdmZDelhFU3lJdU0vSWlUM1U1dDVucUFuYWJsd0RaL3NHUGZJc3NkcVF0RFhrLzBxUHBwM1lOOTZBR3BQT1Rhb1dTZnlPTURPb1JZZGZid3VBOEI5MGhlazdDL01nU0VIV2VKQUtVc01wZjYyYks0WlQ3TXN5ZjQzbHQ0NlR3blpnYjgxaFZaWDk1V1Q3d2ZZREFXemhRTDQ4M21mYWhVbEl0bXpNQklYT24xV3RraG44SmlvTVRvdTZmdUh5Mzl1OEhDNnN5OVBjeEl4NUpTdmlkdm05YTllVUh6dFVxVS8wUkdSRWE0czhUTytqdFlpNzh6VktaWFBML3dWZENWNVF0UytiaE9jazJyeUpidUdmWHQxYnFGSTg0bTNqQ1JVQ0c2QVdLMG94eEFBblJ3aHo1WUV2NVpmYnIxK2ZiRHZBbjlzVWpESzdqUUcrRDh5Z0NzUVZsZHdla2FKb2RYV2ZMZTNDam1MQlU2cytlVkd5WnFJQXlLWGJWZURWcWt0dXZtRHpxNVhVZm1sZFV6dHA2Wnk1ZFBqOFVNWWJtYTcxL0YxRHBIK1I5VE13dHgwblMvTFJHWnUwdEpaZlJ3a3c4N0VWWE1WS2lmL1pDTjFOMmEyeVFYMXRVWjJoTmJoRkt1ZUxCTzlkL1E4NGhLTEZJVlhjUlFQVFBGZVNXcVdqdDB2MUk1RVBmazZ0N21UWmdPY1NJRXBJZWFoZmZDWWxsU0VweTFCL005TlR3WmFnVCs1bDl4VStGeWFaRGo4OSt6L1F0KzJDckVPcndFdzhyd1ZzMHI3elNpajBvVnVKbkZCVE5jZlFyOXB1T3IwTzBXYXdSb2gzdzBuU2UxTitxLzFBZlBIVkxSVVFCRlF5Um16MVJzc3M3MWR2N3FpVFNtOHhFRlI1b2lSSHJtbm1ScUlveHArbDZ4NGRvZlFIYW9rVjlYMTZnMUU4WGVwd0FHbXFrLy92WU0xN1o2Z1NRVE9tRmYyd3lsbkp4NW5YR1BOeFM1VzkxWTNJaWpYVHE4eDhRcFFSUGNjTnJ3N2I0dzFFcEQzMHVVN0xGZkhpQVRiTVAzNlFGOWd4M3pqWXBRWkc4MVVCMEhsOXdJNnZENkY3aE1YTUFZdUpibzR4MEJkdDFzT2w3d1VBNGJUcFRjbmJMNU9SeEdFRHZIZWp0V0xTeEtiWlRJRXN6eFNhcjcvVUNrazZhMnFtVVVXOHZadEZUSFIzdjlMUFhiSzhuSVZqeHZnNzFTc0cxU3ZTNXQ0NFlhRVRzdm1XYks5aHgvNFJIVnhxdzNTWGlBc204VEtibEFTRXVSbWhmS0Q1WEJ6M1lLRzdkdDM3L2dXR0M1UFRTYVNzL1FoNzl3aFRjek9iSWRyb2k1YWtvZXZiNzR4ZW5IQXRuMi9iRHRQeExEc2xwVHM3R293YnBEZVhkeEdCcXoxY3UybHlreWVFc2JYRVdLY0FFejBCYmVUSlR6L2xvK0xHRDU3eVE0L2luUkg4SXhZQ2Exc1ZwbkNNRlAwN3RDL09ZVW1kamM3Q0Zjb3gzVDdjbVFzZW9XN2s0Q0JnUGxRYmRab0ZDZ1YrOXNJMVowcHoxZ3pKVDg1TW8wOC9IVzR0eS8wcmk0VHMyM0xBWlYyM2J1WCtIQ0ZEa2tIbEVhaWpIblBoWEc5WkhIcVloNjFLQ0U0T0pXUVRSQ1ZwIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoiSmVzcGVyIEtqw6ZyIFBlZGVyc2VuIiwibG9naW5UeXBlIjoiS2V5Q2FyZCIsImIzZiI6InkwTjB4OHBvcHM3VGJ3RmlsMWFSZkZYNTZPYitoNHpva0VyRy9TQ203eGs9IiwicGlkIjoiUElEOjkyMDgtMjAwMi0yLTgwNjYzMTA3ODMxOSIsInVzZXJJZCI6Ijc4MzUyNCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiUElEOjkyMDgtMjAwMi0yLTgwNjYzMTA3ODMxOSIsImV4cCI6MTczMDk3Mjc0MCwiaXNzIjoiRW5lcmdpbmV0IiwianRpIjoiZDJmMDA5MGYtYzRmMC00ZmJiLThlYTgtZTRmYzQzZDI0NjExIiwidG9rZW5OYW1lIjoibnkgYXBpIHRva2VuIiwiYXVkIjoiRW5lcmdpbmV0In0.IDelG0YmMKAmFIMqKe1OPos7wkNhbN0lYpSApuyRJYQ";
        string tokenEndpoint = "https://api.eloverblik.dk/customerapi/api/token";
        string meteringPointId = "571313105290375527";
        string connectionString = "Server=192.168.1.182; Port=3306; Database=powerConsumption_DB; Uid = root;Pwd = 2xeM!*N44^yF";

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
                                    string insertQuery = "INSERT INTO forbrug (dato, forbrug) VALUES (@dato, @forbrug)";
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



