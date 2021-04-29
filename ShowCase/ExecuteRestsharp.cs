using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Models;
using Network.Utilidades;
using Newtonsoft.Json.Linq;

namespace ShowCase
{
    public class ExecuteRestsharp
    {
        readonly IClient _client;
        readonly string _token = "cf5942067fa15da2d978489db2b01c092371898198676b0df53b5609cd23bae5";
        readonly string _baseURL = "https://gorest.co.in/public-api/";
        public ExecuteRestsharp()
        {
            ClientOptions ClientOptions = new ClientOptions() { Authorization = _token };
            _client = new Network.Client.RestSharp(_baseURL, ClientOptions);
        }
        public void Init()
        {
            // ExecuteGetList();
            // ExecuteGet();
            // ExecutePost();
            //  ExecutePatch();
            ExecuteDelete();
        }


        bool CheckResponse(string code, object data, string codeSuccess)
        {
            if(code == codeSuccess){
                return true;
            }
            else if (code == "401")
            {
                var obj = ((Dictionary<string, object>)data).ToObject<ErroAuth>();
                Console.WriteLine($"Auth error: {obj.Message}");
                return false;
            }
            else if (code == "404")
            {
                var obj = ((Dictionary<string, object>)data).ToObject<ErroAuth>();
                Console.WriteLine($"404: {obj.Message}");
                return false;
            }
            else if (code == "422")
            {
                var errors = ((RestSharp.JsonArray)data);
                errors.ForEach(it =>
                {
                    var obj = ((Dictionary<string, object>)it).ToObject<ErrorMessage>();
                    Console.WriteLine($"[{obj.Field}]: {obj.Message}");
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
            Response<User, UsersMeta> response = _client.GetAsync<Response<User, UsersMeta>>("users/10").Result;
            Console.WriteLine($"Email del usuario: {response.data.Email}");
        }

        long ExecutePost(User user = null)
        {
            IClient localClient = new Network.Client.RestSharp(_baseURL);
            string token = "cf5942067fa15da2d978489db2b01c092371898198676b0df53b5609cd23bae5";
            User newUser = user != null ? user : new User()
            {
                Name = "testingMA",
                Gender = "Male",
                Email = "testing.MA@testing.oooo",
                Status = "Active"
            };

            Response<object, UsersMeta> response = localClient.Authorization(token).PostAsync<User, Response<object, UsersMeta>>("users", newUser).Result;
            if (CheckResponse(response.code, response.data, "201"))
            {
                var obj = ((Dictionary<string, object>)response.data);
                var id =  ((long)obj.First(it => it.Key == "id").Value);
                Console.WriteLine($"Id created: {id}");
                return id;
            }
            return 0;
        }
        void ExecutePatch()
        {
            User newUser = new User()
            {
                Id = 10,
                Name = "testingChangeMA",
                Gender = "Male",
                Email = "testing.MA@change.MA6546546",
                Status = "Active"
            };
            Response<object, UsersMeta> response = _client.PatchAsync<User, Response<object, UsersMeta>>($"users/{newUser.Id}", newUser).Result;
           if (CheckResponse(response.code, response.data as object, "200"))
            {
                var obj = ((Dictionary<string, object>)response.data);
                Console.WriteLine($"Name updated: {obj.First(it => it.Key == "name").Value}");
            }
        }

        void ExecuteDelete()
        {
            User newUser = new User()
            {
                Name = "test_delete",
                Gender = "Male",
                Email = "testing.delete@testing.test",
                Status = "Active"
            };
            long id = ExecutePost(newUser);
            if(id == 0){
                Console.WriteLine("user not created");
                return;
            }
            Console.WriteLine($"Created: {id}");
            Response<object, UsersMeta> response = _client.DeleteAsync<Response<object, UsersMeta>>($"users/{id}").Result;

            if (CheckResponse(response.code, response.data, "204"))
            {
                Console.WriteLine($"Deleted: {id}");
            }
        }
    }
}