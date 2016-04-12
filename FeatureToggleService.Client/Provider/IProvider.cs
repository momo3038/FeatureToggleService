using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

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
        private readonly TimeSpan _pollingDelay;
        private CancellationTokenSource _cancellationToken;

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

        public async Task Start(int? iterationTime = null)
        {
            _cancellationToken = new CancellationTokenSource();
            await Task.Factory.StartNew(async () =>
            {
                while (!iterationTime.HasValue || iterationTime > 0)
                {
                    if (_cancellationToken.IsCancellationRequested)
                        break;

                    _features = await GetFeatureToggles();
                    await Task.Delay(_pollingDelay, _cancellationToken.Token);
                }
            }, _cancellationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void Stop()
        {
            _cancellationToken.Cancel();
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
                        var result = await content.ReadAsStreamAsync();

                        result.Position = 0;
                        var ser = new DataContractJsonSerializer(typeof(List<FeatureToggleDto>));
                        var features = (List<FeatureToggleDto>)ser.ReadObject(result);
                        return features;
                    }
                }
            }
        }
    }
}
