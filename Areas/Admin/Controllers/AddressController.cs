using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using WebClothes.Irepository;
using WebClothes.Models;
#test change

namespace WebClothes.Areas.Admin.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IOrderUnitOfWork _orderUnitOfWork;
        private readonly HttpClient _client;

        private readonly string _connectionString = "Server=ACER;Database=Shop;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true";

        public AddressController(IOrderUnitOfWork order,HttpClient httpClient)
        {

            _orderUnitOfWork = order;
            _client = httpClient;

        }

        [HttpGet]
        public ActionResult<List<Province>> GetProvinces()
        {
            return _orderUnitOfWork.ProvinceRepo.GetProvinceList();
        }
        [HttpGet("{provinceId}/districts")]
        public ActionResult<IEnumerable<District>> GetDistricts(int provinceId)
        {
            return _orderUnitOfWork.districtRepo.GetDisOfPro(provinceId);
        }
        [HttpGet("{provinceId}/districts/{districtId}/wards")]
        public ActionResult<IEnumerable<Ward>> GetWards(int provinceId, int districtId)
        {
            return _orderUnitOfWork.WardRepo.WardOfDis(districtId);
        }



        public async Task<IActionResult> GetProvincesAPI()
        {
            var apiUrl = "https://provinces.open-api.vn/api/p/";
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonResponse = JArray.Parse(content);
                var provinces = jsonResponse.Select(p => new Province { Id = (int)p["code"], Name = (string)p["name"] }).ToList();
                foreach (var i in provinces)
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();
                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // Enable IDENTITY_INSERT
                                using (var command = new SqlCommand("SET IDENTITY_INSERT Provinces ON", connection, transaction))
                                { await command.ExecuteNonQueryAsync(); }
                                // Insert the district
                                var insertQuery = "INSERT INTO Provinces (Id, Name) VALUES (@Id, @Name)";
                                using (var command = new SqlCommand(insertQuery, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@Id", i.Id);
                                    command.Parameters.AddWithValue("@Name", i.Name);
                                    await command.ExecuteNonQueryAsync();
                                }
                                // Disable IDENTITY_INSERT
                                using (var command = new SqlCommand("SET IDENTITY_INSERT Provinces OFF", connection, transaction))
                                { await command.ExecuteNonQueryAsync(); }
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                return StatusCode(500, $"Internal server error: {ex.Message}");
                            }
                        }
                    }
                }
                return RedirectToAction("Province");
            }
            else
            {
                Console.WriteLine($"Request failed with status code {response.StatusCode}"); return null;
            }

        }
        public async Task<IActionResult> GetDistricts()
        {
            var apiUrl = "https://provinces.open-api.vn/api/d/";
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonResponse = JArray.Parse(content);
                var districts = jsonResponse.Select(p => new District { Id = (int)p["code"], Name = (string)p["name"], ProvinceId = (int)p["province_code"] }).ToList();
                foreach (var i in districts)
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();
                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // Enable IDENTITY_INSERT
                                using (var command = new SqlCommand("SET IDENTITY_INSERT Districts ON", connection, transaction))
                                { await command.ExecuteNonQueryAsync(); }
                                // Insert the district
                                var insertQuery = "INSERT INTO Districts (Id, Name, ProvinceId) VALUES (@Id, @Name, @ProvinceId)";
                                using (var command = new SqlCommand(insertQuery, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@Id", i.Id);
                                    command.Parameters.AddWithValue("@Name", i.Name);
                                    command.Parameters.AddWithValue("@ProvinceId", i.ProvinceId); await command.ExecuteNonQueryAsync();
                                }
                                // Disable IDENTITY_INSERT
                                using (var command = new SqlCommand("SET IDENTITY_INSERT Districts OFF", connection, transaction))
                                { await command.ExecuteNonQueryAsync(); }
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                return StatusCode(500, $"Internal server error: {ex.Message}");
                            }
                        }
                    }
                }
                return RedirectToAction("District");
            }
            else
            {
                Console.WriteLine($"Request failed with status code {response.StatusCode}"); return null;
            }

        }
        public async Task<IActionResult> GetWards()
        {
            var apiUrl = "https://provinces.open-api.vn/api/w/";
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonResponse = JArray.Parse(content);
                var ward = jsonResponse.Select(p => new Ward { Id = (int)p["code"], Name = (string)p["name"], DistrictId = (int)p["district_code"] }).ToList();
                foreach (var i in ward)
                {
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();
                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // Enable IDENTITY_INSERT
                                using (var command = new SqlCommand("SET IDENTITY_INSERT Wards ON", connection, transaction))
                                { await command.ExecuteNonQueryAsync(); }
                                // Insert the district
                                var insertQuery = "INSERT INTO Wards (Id, Name,DistrictId) VALUES (@Id, @Name,@DistrictId)";
                                using (var command = new SqlCommand(insertQuery, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@Id", i.Id);
                                    command.Parameters.AddWithValue("@Name", i.Name);
                                    command.Parameters.AddWithValue("@DistrictId", i.DistrictId);

                                    await command.ExecuteNonQueryAsync();
                                }
                                // Disable IDENTITY_INSERT
                                using (var command = new SqlCommand("SET IDENTITY_INSERT Wards OFF", connection, transaction))
                                { await command.ExecuteNonQueryAsync(); }
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                return StatusCode(500, $"Internal server error: {ex.Message}");
                            }
                        }
                    }
                }
                return RedirectToAction("Ward");
            }
            else
            {
                Console.WriteLine($"Request failed with status code {response.StatusCode}"); return null;
            }

        }

    }
}
