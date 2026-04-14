using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using DepartmentContracts.ViewModels;

namespace DepartmentUserApp
{
    public static class APIClient
    {
        private static readonly HttpClient _client = new HttpClient();
        // public static UserViewModel? CurrentUser { get; set; }

        public static void Connect(IConfiguration configuration)
        {
            _client.BaseAddress = new Uri(configuration["IPAddress"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static T? GetRequest<T>(string requestUrl)
        {
            var response = _client.GetAsync(requestUrl).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }
            return JsonConvert.DeserializeObject<T>(result);
        }

        public static void PostRequest<T>(string requestUrl, T model)
        {
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = _client.PostAsync(requestUrl, data).Result;

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = response.Content.ReadAsStringAsync().Result;
                throw new Exception(errorContent);
            }
        }

        public static void DeleteRequest(string requestUrl)
        {
            var response = _client.DeleteAsync(requestUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = response.Content.ReadAsStringAsync().Result;
                throw new Exception(errorContent);
            }
        }

        public static void PostRequest(string requestUrl)
        {
            var data = new StringContent("", Encoding.UTF8, "application/json");
            var response = _client.PostAsync(requestUrl, data).Result;

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = response.Content.ReadAsStringAsync().Result;
                throw new Exception(errorContent);
            }
        }

        //public static (T? Result, string? Error) GetRequestWithErrorHandling<T>(string requestUrl)
        //{
        //    try
        //    {
        //        var response = _client.GetAsync(requestUrl).Result;
        //        var result = response.Content.ReadAsStringAsync().Result;

        //        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        //        {
        //            return (default, "Нет пользователя с таким логином");
        //        }

        //        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        //        {
        //            return (default, "Неверный логин или пароль");
        //        }

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            return (default, result);
        //        }

        //        return (JsonConvert.DeserializeObject<T>(result), null);
        //    }
        //    catch (Exception ex)
        //    {
        //        return (default, ex.Message);
        //    }
        //}

    }
}