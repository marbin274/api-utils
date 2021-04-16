using System;
using System.Collections.Generic;
using Models;
using Network.Utilidades;
using Newtonsoft.Json.Linq;

namespace ShowCase
{
    public class ExecuteClient
    {
        readonly IClient _client;
        readonly string _token = "cf5942067fa15da2d978489db2b01c092371898198676b0df53b5609cd23bae5";
        readonly string _baseURL = "https://gorest.co.in/public-api/";
        public ExecuteClient()
        {
            ClientOptions ClientOptions = new ClientOptions() { Authorization = _token };
            _client = new Network.Client.HttpClient(_baseURL, ClientOptions);
        }
        public void Init()
        {
            ExecuteGetList();
            ExecuteGet();
            ExecutePost();
            ExecutePatch();
            ExecuteDelete();
        }


        bool CheckResponse(Response<object, UsersMeta> response, string codeSuccess)
        {
            if(response.code == codeSuccess){
                return true;
            }
            else if (response.code == "401")
            {
                var obj = ((JObject)response.data).ToObject<ErroAuth>();
                Console.WriteLine($"Auth error: {obj.Message}");
                return false;
            }
            else if (response.code == "404")
            {
                var obj = ((JObject)response.data).ToObject<ErroAuth>();
                Console.WriteLine($"404: {obj.Message}");
                return false;
            }
            else if (response.code == "422")
            {
                var errors = ((JArray)response.data).ToObject<List<ErrorMessage>>();
                errors.ForEach(it =>
                {
                    Console.WriteLine($"[{it.Field}]: {it.Message}");
                });
                return false;
            }
            else
            {
                Console.WriteLine("Error no implement");
                return false;
            }
        }

        void ExecuteGetList()
        {
            Response<List<User>, UsersMeta> response = _client.GetAsync<Response<List<User>, UsersMeta>>("users").Result;
            Console.WriteLine($"Cantidad de registros: {response.data.Count}");
        }

        void ExecuteGet()
        {
            Response<User, UsersMeta> response = _client.GetAsync<Response<User, UsersMeta>>("users/1").Result;
            Console.WriteLine($"Email del usuario: {response.data.Email}");
        }

        int ExecutePost()
        {
            Network.Client.HttpClient localClient = new Network.Client.HttpClient(_baseURL);
            string token = "cf5942067fa15da2d978489db2b01c092371898198676b0df53b5609cd23bae5";
            User newUser = new User()
            {
                Name = "testingMA",
                Gender = "Male",
                Email = "testing.MA@testing.MA6546546",
                Status = "Active"
            };

            Response<object, UsersMeta> response = localClient.Authorization(token).PostAsync<User, Response<object, UsersMeta>>("users", newUser).Result;
            if (CheckResponse(response, "201"))
            {
                var obj = ((JObject)response.data).ToObject<User>();
                Console.WriteLine($"Id created: {obj.Id}");
                return obj.Id;
            }
            return 0;
        }
        void ExecutePatch()
        {
            User newUser = new User()
            {
                Id = 1483,
                Name = "testingChangeMA",
                Gender = "Male",
                Email = "testing.MA@change.MA6546546",
                Status = "Active"
            };
            Response<object, UsersMeta> response = _client.PatchAsync<User, Response<object, UsersMeta>>($"users/{newUser.Id}", newUser).Result;

            if (CheckResponse(response, "201"))
            {
                var obj = ((JObject)response.data).ToObject<User>();
                Console.WriteLine($"Name updated: {obj.Name}");
            }
        }

        void ExecuteDelete()
        {
            User newUser = new User()
            {
                Id = ExecutePost(),
            };
            Response<object, UsersMeta> response = _client.DeleteAsync<Response<object, UsersMeta>>($"users/{newUser.Id}").Result;

            if (CheckResponse(response, "204"))
            {
                Console.WriteLine($"Deleted: {newUser.Id}");
            }
        }
    }
}