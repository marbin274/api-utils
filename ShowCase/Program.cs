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
            IHandler client = new Network.Client.HttpClient("https://gorest.co.in/public-api/");
            Response<List<User>, UsersMeta> responseUserList = client.Authorization("").GetAsync<Response<List<User>, UsersMeta>>("users").Result;

            Response<User, UsersMeta> responseUser = client.Authorization("").GetAsync<Response<User, UsersMeta>>("users/1").Result;
            
        }
    }
}
