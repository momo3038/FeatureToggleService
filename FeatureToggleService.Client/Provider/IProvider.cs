using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace FeatureToggleService.Client.Provider
{
    public interface IProvider
    {
        FeatureToggleDto Get(IFeatureToggle featureToggle);
    }

    public interface IInitProvider
    {
        IList<FeatureToggleDto> GetAll();
    }

    public class WebApiProviderInitialisation : IInitProvider
    {
        private readonly IProviderConfiguration _configuration;
        private IList<FeatureToggleDto> _features;
        private TimeSpan _pollingDelay;

        public WebApiProviderInitialisation(TimeSpan pollingDelay, IProviderConfiguration configuration)
        {
            _pollingDelay = pollingDelay;
            _configuration = configuration;
            _features = new List<FeatureToggleDto>();
        }

        public IList<FeatureToggleDto> GetAll()
        {
            if (!_configuration.IsInitialized())
                throw new Exception("Toggle are not yet retrieved.");
            return _features;
        }

        public async Task Start()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            //var listener = Task.Factory.StartNew(async () =>
            //{
            //while (true)
            //{
                //if (token.IsCancellationRequested)
                //    break;

                _features = await GetFeatureToggles();
                //await Task.Delay(_pollingDelay);
            //}
            //}, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private async Task<IList<FeatureToggleDto>> GetFeatureToggles()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = _configuration.WebApiUrl;
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        var sss = await content.ReadAsStringAsync();
                        var result = await content.ReadAsStreamAsync();

                        result.Position = 0;
                        StreamReader sr = new StreamReader(result);
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<FeatureToggleDto>));
                        List<FeatureToggleDto> features = (List<FeatureToggleDto>)ser.ReadObject(result);
                        return features;
                    }
                }
            }
        }
    }
}
