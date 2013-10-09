using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Coffee.StateMachine;
using Coffee.StateMachine.States;
using Microsoft.WindowsAzure;
using Newtonsoft.Json;

namespace Coffee.WorkerRole.Workers.StateMachinePushToWeb
{
    public class WebPoster : ICoffeeObserver
    {
        private readonly HttpClient _httpClient;

        public WebPoster()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(CloudConfigurationManager.GetSetting("WebBaseAddress")) };
        }

        public async void StateChanged(State state)
        {
            var value = state.GetValues();
            var requestContent = new StringContent(await JsonConvert.SerializeObjectAsync(value));
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _httpClient.PutAsync("", requestContent);
        }
    }
}