using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DepartmentContracts.BusinessLogicsContracts;
using DepartmentContracts.Configs;
using DepartmentContracts.Dtos.OneC;
using Microsoft.Extensions.Options;

namespace DepartmentBusinessLogic.Services.OneC
{
    public class OneCApiService : IOneCApiService
    {
        private readonly HttpClient _httpClient;
        private readonly OneCConnectionConfig _config;
        private readonly JsonSerializerOptions _jsonOptions;

        public OneCApiService(HttpClient httpClient, IOptions<OneCConnectionConfig> config)
        {
            _httpClient = httpClient;
            _config = config.Value;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (!string.IsNullOrWhiteSpace(_config.BaseUrl))
            {
                _httpClient.BaseAddress = new Uri(_config.BaseUrl);
            }

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrWhiteSpace(_config.Login) || !string.IsNullOrWhiteSpace(_config.Password))
            {
                var authValue = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes($"{_config.Login}:{_config.Password}"));

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", authValue);
            }
        }

        public async Task<List<AcademicPlanOneCDto>> GetAcademicPlansAsync()
        {
            return await GetAsync<List<AcademicPlanOneCDto>>(_config.AcademicPlansEndpoint) ?? new List<AcademicPlanOneCDto>();
        }

        public async Task<List<AcademicPlanRecordOneCDto>> GetAcademicPlanRecordsAsync()
        {
            return await GetAsync<List<AcademicPlanRecordOneCDto>>(_config.AcademicPlanRecordsEndpoint) ?? new List<AcademicPlanRecordOneCDto>();
        }

        public async Task<List<StudentGroupOneCDto>> GetStudentGroupsAsync()
        {
            return await GetAsync<List<StudentGroupOneCDto>>(_config.StudentGroupsEndpoint) ?? new List<StudentGroupOneCDto>();
        }

        public async Task<List<StudentOneCDto>> GetStudentsAsync()
        {
            return await GetAsync<List<StudentOneCDto>>(_config.StudentsEndpoint) ?? new List<StudentOneCDto>();
        }

        public async Task<List<DisciplineStudentRecordOneCDto>> GetDisciplineStudentRecordsAsync()
        {
            return await GetAsync<List<DisciplineStudentRecordOneCDto>>(_config.DisciplineStudentRecordsEndpoint)
                   ?? new List<DisciplineStudentRecordOneCDto>();
        }

        public async Task<List<StudentOrderOneCDto>> GetStudentOrdersAsync()
        {
            return await GetAsync<List<StudentOrderOneCDto>>(_config.StudentOrdersEndpoint)
                   ?? new List<StudentOrderOneCDto>();
        }

        private async Task<T?> GetAsync<T>(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new InvalidOperationException("1C endpoint is not configured.");
            }

            using var response = await _httpClient.GetAsync(endpoint);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"1C request failed. StatusCode: {(int)response.StatusCode}. Response: {content}");
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(content, _jsonOptions);
        }
    }
}