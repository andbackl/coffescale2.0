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
        private int _postCounter = 0;

        public WebPoster()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(CloudConfigurationManager.GetSetting("WebBaseAddress")) };
        }

        public async void Tick(State state)
        {
            _postCounter++;

            if (_postCounter == 10)
            {
                var value = new { StateName = state.GetType().Name, state.ElapsedTime, state.Weight };
                var requestContent = new StringContent(await JsonConvert.SerializeObjectAsync(value));
                requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                _httpClient.PutAsync("", requestContent);

                _postCounter = 0;
            }
        }
    }
}