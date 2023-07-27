using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MySqlConnector;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Collections.Generic;
using Newtonsoft.Json;

class Program
{
    //class AccessTokenResponse
    //{
    //    public string? access_token { get; set; }
        
    //}

    static async Task Main()
    {
        string jwtRefreshToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0b2tlblR5cGUiOiJDdXN0b21lckFQSV9SZWZyZXNoIiwidG9rZW5pZCI6Ijk0OGNhZTBkLWM2OGEtNGNkNi05OGQ3LTk3NWI5Nzc1Mjg5NyIsIndlYkFwcCI6IkN1c3RvbWVyQXBwIiwidmVyc2lvbiI6IjIiLCJpZGVudGl0eVRva2VuIjoiNnIwQi9LeC96M1lBYmlQNThqUCtXR2NKVjJlV1FRVWpWRlRYenJ3NUNkRlRzdmE1TGEwdXNWeldzNG80WHRKdFBTdVc3Tk5hSXBsY3BXb1BWVDlXZ0drV2pvSXdQeXhCMkZ5aHpBZjhmWUhsb3BXZlZYRHRhUE50N25obVBtYm1Ndkh3WHBoY0pxVCs4anhxSDJWZzh5Zk5haU5LUGZWd29RYk5QQTM3bTRhT0RkaEIvNmVXNEdFZGp4eHZpVEp5b2t3WHNuMCtKb1hBM1dZQzR1bWtQNEp0cVVIMXB5SlEydEpRK2pnWkxtNjhZbThCQTJOMlFwZ3l2M1g5VzRUelVIYWV1endmUWVoOVZZWXh2SWlZcWtwSEtlN0NvVVQ5Uks1a1B5ZXFyR04yZlppSXJWakY4bms2TGpvNTBnaEZrK0FHN1c0SGpCbGFoTy9UeEMxVThGbHpNMjFlZW92c2JjbFg5YTJYK1VHK2l5b0VsMzk1YUFFZndRcDNYWk4xdmx3QTU0ckpnRHZRQlVLU1VSbmVoVjc5M2FlU3pBYXBTNGp3ZHVuZGxDakFsTlVtRVM3Tmx3QkphYTBYUlFwbnZramFwSlRkZG00cEJmRDFEZ0grcUtJYVdvWXlKODIwbnVuQjZFRlNIV0o5emdDaGhzKzZmbS9tbCtxVWhCS2luY3BPd2crNkpZYnB5WDNEZXBkNVI0QmlSbDEvWU1Ra0dWcmtDMEtVNWJ6VVBqSVg1UTFDdTJOaHJNZEVIK3RYUmVZSDhrbUFnKzVhdTlob1ZJWTdJUG9NK2IyTmZFWll2QVhCK1I2ZzJUZWpwSStpOEZUU1NpVkVGWkMwMGhyQTlGV1EyV2NvbVJoSVRmek8yMDRDMmNCSk9LdUt4amhWU09rV0VObkxUTUp1L2RNNld2QTZnWVdDc2RucUwzQWc4RU1tc0pEQmRLNmY2elpuRFFNOWhLV21qK1BRNXZ1M1JsOW8zb3hROWdSdVljK1MvZ3JaZXJLcjQ1SkthWE83bkdmKy9IdzJyb2J2RjR0OHI0OHFKY0RiQ3Y4d3JRdS83TGZoTmhIRXBxb0tnTVdnN2UrbXJpMm1lM1lTOTdUVzRCVFROQXhVODZBQ1hzUkdGOHVaUktYdkEwZDdWMDRTanFiZFlhM3lodU1RUStKWE0xUXh5UExyT3gzQnBYM3ZPK1g3UHRjUStDQUkxM2xRWWhlQnZJR2lHa0lObmZoRVRKZXBOVHRMS0lidFU2cFNTV1JYQXlqR25IVUpZbk5iOXdjb01uaDVQZnY4T2M3OUtDUHVWNXA5dDBaMlZRNGtGcmV6T1U3TUQ2U09uWEljdFJqL2QwSmNidmU4dG9SQXV3UjFoSHB2WGd5MzdZWHZMYzc3a3pBTmhBTm5wU1hjaDY3c013VnY2aHBncERscVpwc1gyZ0ZUWE5HQnVBTnplRlRURHhBd21DdnRHd2tyYzdDcUplYVhMR0JOTG42QkZzRU1ldXEyOG9ScUw4bklQWUt3V1ZiRXBCcS9QQ012bHdRV29ob1llcGx5cFcxUWdsK2RCcTBqZXI2NDJMaytKNEJaV2NNSk8zaHZtVlJRYVBMc3hnbjB0R3poYWpDRmQxaEpXdHRHZ3NVM2ZvcmNUdWhJcXhpTlA1cnpKQUg1WkpnYVc4S0hqV0xjRURpR1hHRGJKT0cwbzQ1bmdUaEVZSzkycGtpaHNDYnR4dzg1MmVrV1p0dGhCY0FrRG5aK09kKzZTQzROb0RjUEc2VHZkOUFJYy94SWVIcEV4Zy9yempOS3BIZkd4SzgvZis5bVVJelJxek1kOVR5dk9ScUU2ZEFIajB0ei85d01GTHJyRTZZTjJNajB6Qk9PSDU2OHBGRXVzRFlLd2Z3dEtJelphVTJrNHIrbVRrM1pLeUlJVitpMStIQlcrcFNVV25hN2JWZm55aHdRdEFLOFE1MkowV1hWV2sxMXpnazEwOG9vTWhvTVFkeUVrcnZzbWd1TTc1L3BwQkJmRTZxTWp2Sld5U0VnRG9vUkFNNzdkeHFRalhPeXpOSFBwckh4b0JzVVI5S3Y4bnJCZjZIekxGcExaZXNaU3JuYlIwYzYrT0pRQzVydEJqOUFpU3V2eTBudWNBQ0RzUEVVYWVYR2pwMGpHd1pUVStmQXhUNlJCeHI2OG5PQjNaUlBPVzFLYkxXcXhUT2F2QzdpVHhtZmhsT2NWWFEvMkt3RG1oZUpncC9FNE5aUHloOHd0UTRnN0RtTzBrbVVNb0U3RmsvNlJMMzV2VUdvZmlwZmtWVjZlTE1lVXRLUkl2SHN6akhLcVVJdnpCMHIzdlVzblJzSm5MV2VId242U3FHS1Iyemc2MU9WejB5Q0hQejdVRkJ3d3REOXdlQ2YyT2tscEtUdmJtQkxicUNra1NBbFp6TzU3NFRQU2U3elZJZ3lsbEV6YVY4ZDN3ampZdjVySFJ1QmxOVkZiQTRsQ0xHK0ZvR3draUFKUCttcklQL3A4aTlNdGhmUkgwRWViaVEzdk02MTJDRDFuOUVUTG8rV09JNG1RK2dYIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJQSUQ6OTIwOC0yMDAyLTItODA2NjMxMDc4MzE5IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoiSmVzcGVyIEtqw6ZyIFBlZGVyc2VuIiwibG9naW5UeXBlIjoiS2V5Q2FyZCIsInBpZCI6IjkyMDgtMjAwMi0yLTgwNjYzMTA3ODMxOSIsImIzZiI6ImFsbU9LRTBpOWUwT0ZsaWcvWXVLbmVvd0xhRGdXMVdINHowLzFFSnZUMU09IiwidXNlcklkIjoiNzgzNTI0IiwiZXhwIjoxNzIwNTA2NTU5LCJpc3MiOiJFbmVyZ2luZXQiLCJqdGkiOiI5NDhjYWUwZC1jNjhhLTRjZDYtOThkNy05NzViOTc3NTI4OTciLCJ0b2tlbk5hbWUiOiJFbEZvcmJydWciLCJhdWQiOiJFbmVyZ2luZXQifQ.jYUm7LYuj4sMzRTb6Y6S5sPQdPMqDqMqKuj8gxarZHo";
        string tokenEndpoint = "https://api.eloverblik.dk/customerapi/api/token";
        string meteringPointId = "571313105290375527";
        string connectionString = "Server=192.168.1.182; Port=3306; Database=powerConsumption_DB; Uid = root;Pwd = 2xeM!*N44^yF";

        // Finder datoen for i forgårs som er det nyeste data der kan hentes.
        DateTime yesterday = DateTime.Today.AddDays(-2);
        DateTime today = DateTime.Today.AddDays(-1);


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
                //Console.WriteLine("Access Token: " + accessToken);

                //***********Her kommer den næste funktion
                // Tilføj access token til anmodningens header
                //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                // Opret anmodningens krop (request body)
                string requestBody = $"{{ \"meteringPoints\": {{ \"meteringPoint\": [ \"{meteringPointId}\" ] }} }}";
                StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                string apiurl = $"https://api.eloverblik.dk/customerapi/api/meterdata/gettimeseries/{yesterday:yyyy-MM-dd}/{today:yyyy-MM-dd}/Day";


                // Send POST-anmodning til API'en
                HttpResponseMessage response2 = await client.PostAsync($"https://api.eloverblik.dk/customerapi/api/meterdata/gettimeseries/{yesterday:yyyy-MM-dd}/{today:yyyy-MM-dd}/Day", content);

                // Håndter API-svaret
                if (response2.IsSuccessStatusCode)
                {
                    // Læs API-svaret
                    string apiResponse = await response2.Content.ReadAsStringAsync();

                    // Pars JSON-svaret som et objekt
                    JObject jsonObject = JObject.Parse(apiResponse);

                    // Find værdierne for "period.timeInterval" start og end
                    JToken startTimeToken = jsonObject["result"]?[0]?["MyEnergyData_MarketDocument"]?["period.timeInterval"]?["start"];
                    JToken endTimeToken = jsonObject["result"]?[0]?["MyEnergyData_MarketDocument"]?["TimeSeries"]?[0]?["Period"]?[0]?["timeInterval"]?["end"];
                    JToken quantityToken = jsonObject["result"]?[0]?["MyEnergyData_MarketDocument"]?["TimeSeries"]?[0]?["Period"]?[0]?["Point"]?[0]?["out_Quantity.quantity"];

                    if (startTimeToken != null && quantityToken != null)
                    {
                        string startTimeString = startTimeToken.Value<string>();
                        DateTime startTime = DateTime.Parse(startTimeString);
                        string endTimeString = endTimeToken.Value<string>();
                        DateTime endTime = DateTime.Parse(endTimeString);
                        //string forbrug = quantityToken.Value<string>();

                        string quantityValueString = quantityToken.Value<string>();
                        CultureInfo culture = CultureInfo.InvariantCulture;

                        if (decimal.TryParse(quantityValueString, NumberStyles.Float, culture, out decimal quantityValue))
                        {
                            //HAr udkommenteret linjen da det ikke er nødvendig med en success besked.
                            Console.WriteLine("Aflæst forbrug: " + quantityValue);
                        }
                        else
                        {
                            Console.WriteLine("Noget gik galt i konveteringen til decimal");
                        }


                        // Opret forbindelse til MySQL-databasen
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
                            Console.WriteLine("Data er skrevet til db");
                        }

                        Console.WriteLine($"Dato: {endTime}");
                        Console.WriteLine("Decimal " + quantityValue);
                        Console.WriteLine("Værdierne blev gemt i databasen");

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



