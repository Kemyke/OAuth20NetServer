﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string tokenEndpoint = "http://localhost:49699/oauth/token";
            OAuth2Client client = new OAuth2Client(new Uri(tokenEndpoint), "42ff5dad3c274c97a3a7c3d44b67bb42", "client123456");
            TokenResponse tokenResponse = client.RequestResourceOwnerPasswordAsync("Tugberk", "user123456").Result;

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:49699");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
                HttpResponseMessage response = httpClient.GetAsync("api/users/me").Result;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken.Replace('a','b'));
                HttpResponseMessage response2 = httpClient.GetAsync("api/users/me").Result;
            }
        }
    }
}
