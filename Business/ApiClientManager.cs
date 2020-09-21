using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Business
{
    public static class ApiClientManager
    {

        public static async Task<User> Login(User user)
        {
            UserLoginRequest userLoginRequest = new UserLoginRequest();
            userLoginRequest.Username = user.Username;
            userLoginRequest.Password = user.Password;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(userLoginRequest), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:59794/api/user/authenticate", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }

            return user;
        }

        public static async Task<List<Student>> GetAllStudents(User user)
        {
            List<Student> students = new List<Student>();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
                using (var response = await httpClient.GetAsync("http://localhost:59794/api/StudentDirectory/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<Student>>(apiResponse);
                }
            }
            return students;
        }
    }
}
