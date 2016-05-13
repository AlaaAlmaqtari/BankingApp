﻿using App1.Models;
using App1.REST;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace App1.Services
{
    public class GooglePlacesService : IGoogleServices
    {
        public GooglePlacesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private HttpClient _httpClient { get; set; }

        //android Api key
        private const string Android_API_KEY = "AIzaSyAI7-OYsPcoPsLbO4thycHiv1E5mFKr47Q";

        private const string GoogleMapsUrl =
            "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius=5000&sensor=true&name=car%wash&key={2}";

        /* public async Task<List<Location>> GetCoordinatesMapAsync(Location latitude, Location longitude)
         {
             var uri = string.Format(GoogleMapsUrl, latitude.Latitude,
                           longitude.Longitude, Android_API_KEY);

             var result = await _httpClient.GetAsync<>(uri);

             return result;
         }*/

        //Change Users not this
        public Task<List<Users>> GetCoordinatesMapAsync(Location location)
        {
            throw new NotImplementedException();
        }
    }
}