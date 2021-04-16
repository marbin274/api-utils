using System;
using System.Collections.Generic;
using Models;
using Network.Utilidades;

namespace ShowCase
{

    class Program
    {
        static void Main(string[] args)
        {
            ClientOptions ClientOptions = new ClientOptions() { Authorization = "" };
            IClient client = new Network.Client.HttpClient("https://gorest.co.in/public-api/", ClientOptions);

            Response<List<User>, UsersMeta> responseUserList = client.GetAsync<Response<List<User>, UsersMeta>>("users").Result;
            Console.WriteLine($"Cantidad de registros: {responseUserList.data.Count}");

            Response<User, UsersMeta> responseUser = client.GetAsync<Response<User, UsersMeta>>("users/1").Result;
            Console.WriteLine($"Email del usuario: {responseUser.data.Email}");
        }
    }
}
