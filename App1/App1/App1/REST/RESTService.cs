﻿using App1.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace App1.REST
{
    public class RESTService : IRESTService
    {
        public static string token = "";

        /*  204 (NO CONTENT) – the request has been successfully processed and the response is intentionally blank.
        400 (BAD REQUEST) – the request is not understood by the server.
        404 (NOT FOUND) – the requested resource does not exist on the server.*/

        //This function will be used to GET information for the user that doesn´t require any type of login
        public async Task<bool> GetwithoutToken(string url, int choice)
        {
            string responseFromServer = "";
            var uri = string.Format(url);
            var request2 = (HttpWebRequest)WebRequest.Create(uri);
            request2.ContentType = "application/json";
            request2.Method = "GET";

            try
            {
                using (HttpWebResponse response = await request2.GetResponseAsync() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Debug.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                        return false;
                    }
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string content = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            Debug.WriteLine("Response contained empty body...");
                        }
                        else
                        {
                            switch (choice)
                            {
                                case 1: //bank information used
                                    Debug.WriteLine("Response Body: \r\n {0}", content);
                                    //deserializing string of information received into json type to then be called
                                    var info = JsonConvert.DeserializeObject<RootObject>(content);
                                    break;

                                case 2: //location of the branches and their locations
                                    Debug.WriteLine("Response Body: \r\n {0}", content);
                                    //deserializing string of information received into json type to then be called
                                    var info1 = JsonConvert.DeserializeObject<branchlist>(content);
                                    break;

                                case 3: //location of the atms and their locations
                                    Debug.WriteLine("Response Body: \r\n {0}", content);
                                    //deserializing string of information received into json type to then be called
                                    var info2 = JsonConvert.DeserializeObject<atmlist>(content);
                                    break;

                                case 4: //products of all the banks
                                    Debug.WriteLine("Response Body: \r\n {0}", content);
                                    //deserializing string of information received into json type to then be called
                                    var info3 = JsonConvert.DeserializeObject<productlist>(content);
                                    break;

                                default:
                                    break;
                            }
                        }
                        return true;
                    }
                }
            }
            catch (WebException err)
            {
                Stream stream = err.Response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                responseFromServer = reader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(responseFromServer))
                {
                    Debug.WriteLine("Response contained empty body...");
                }
                return false;
            }
        }

        //The user should be autheticated and this GET request will be using the token from the function Createsession
        public async Task<bool> GetWithToken(string url, int choice)
        {
            string responseFromServer = "";

            var uri = string.Format(url);
            var request2 = (HttpWebRequest)WebRequest.Create(uri);
            request2.ContentType = "application/json";
            request2.Method = "GET";
            var token5 = new Token(); 
            var authToken = string.Format("token=\"{0}\"", token5.token);
            request2.Headers[HttpRequestHeader.Authorization] = "DirectLogin " + authToken;
            Debug.WriteLine("token:: {0}", token5);

            try
            {
                using (HttpWebResponse response = await request2.GetResponseAsync() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Debug.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                        return false;
                    }
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            Debug.WriteLine("Response contained empty body...");
                        }
                        else
                        {
                            switch (choice)
                            {
                                case 1: //the login account used and the info that it contains
                                    Debug.WriteLine("Response Body: \r\n {0}", content);
                                    //deserializing string of information received into json type to then be called
                                    JsonConvert.DeserializeObject<Accounts>(content);
                                    break;

                                case 2: //Account with more detailed information containg things like the account balance, IBAN, etc
                                    Debug.WriteLine("Response Body: \r\n {0}", content);
                                    //deserializing string of information received into json type to then be called
                                    JsonConvert.DeserializeObject<AccountInfo>(content);
                                    break;

                                case 3: //this will contain the users transaction information
                                    Debug.WriteLine("Response Body: \r\n {0}", content);
                                    //deserializing string of information received into json type to then be called
                                    JsonConvert.DeserializeObject<AccountInfo>(content);
                                    break;

                                case 4: //this will contain the users card information
                                    Debug.WriteLine("Response Body: \r\n {0}", content);
                                    //deserializing string of information received into json type to then be called
                                    JsonConvert.DeserializeObject<AccountInfo>(content);
                                    break;

                                default: break;
                            }
                        }
                        return true;
                    }
                }
            }
            catch (WebException err)
            {
                Stream stream = err.Response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                responseFromServer = reader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(responseFromServer))
                {
                    Debug.WriteLine("Response contained empty body...");
                }
                return false;
            }
        }

        //verifcation of the users autheticty depending on the received token
        public bool IsAutheticated
        {
            get
            {
                Debug.WriteLine("Here in authetiacation");
                var some = !string.IsNullOrWhiteSpace(token);
                Debug.WriteLine("vaue of some {0}", some);
                return some;
            }
        }

        //Post of the users attributes of their login information and receiving the token needed for future requests
        public async Task<bool> CreateSession(Users user, Users pass)
        {
            string responseFromServer = "";
            //pedido ao servidor pelo request token
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Constants.OpenBankAPI);
            request.ContentType = "application/json";
            request.Method = "POST";
            var authData = string.Format("username=\"{0}\", password=\"{1}\",consumer_key=\"{2}\"", user.User, pass.Password, Constants.oauth_consumer_key);
            var header = request.Headers[HttpRequestHeader.Authorization] = "DirectLogin " + authData;

            try
            {
                using (Stream requestStream = await request.GetRequestStreamAsync())
                {
                    using (var writer = new StreamWriter(requestStream))
                    {
                        // Send the data to the server
                        Debug.WriteLine(header);
                        writer.Write(header);
                        writer.Flush();
                        writer.Dispose();
                    }
                }
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Debug.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                        return false;
                    }
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        try
                        {
                            StreamReader reader = new StreamReader(dataStream);
                            token = reader.ReadToEnd();
                            if (string.IsNullOrWhiteSpace(token))
                            {
                                Debug.WriteLine("Response contained empty body...");
                            }
                            Debug.WriteLine("Response {0}", token);
                            if (string.IsNullOrWhiteSpace(token))
                            {
                                return false;
                            }
                            else
                            {
                                var userstoken = new Token();
                                //deserializing string of information received into json type to then be called
                                JsonConvert.DeserializeObject<Token>(token);
                                Debug.WriteLine("token {0}", token);
                                Debug.WriteLine("users token:: {0}", userstoken.token);
                                return true;
                            }
                        }
                        catch (WebException err)
                        {
                            Stream stream = err.Response.GetResponseStream();
                            StreamReader reader = new StreamReader(stream);
                            responseFromServer = reader.ReadToEnd();

                            if (string.IsNullOrWhiteSpace(responseFromServer))
                            {
                                Debug.WriteLine("Response contained empty body...");
                            }
                            return false;
                        }
                    }
                }
            }
            catch (WebException err)
            {
                Stream stream = err.Response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                responseFromServer = reader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(responseFromServer))
                {
                    Debug.WriteLine("Response contained empty body...");
                }
            }
            Debug.WriteLine("end of create session {0}", token);
            return false;
        }
    }
}