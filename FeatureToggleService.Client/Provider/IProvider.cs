using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using NLog;

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
        private IList<FeatureToggleDto> _features;
        private readonly TimeSpan _pollingDelay;
        private readonly WebApiUrl _url;
        private CancellationTokenSource _cancellationToken;
        private bool _isInitialized;
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public WebApiProviderInitialisation(TimeSpan pollingDelay, WebApiUrl url)
        {
            _pollingDelay = pollingDelay;
            _url = url;
            _features = new List<FeatureToggleDto>();
        }

        public IList<FeatureToggleDto> GetAll()
        {
            if (!_isInitialized)
                throw new Exception("Toggle are not yet retrieved.");
            return _features;
        }

        public async Task Start(int? iterationTime = null)
        {
            _logger.Debug("Starting polling WebApi with a {0}s polling interval", _pollingDelay.TotalSeconds);
            _cancellationToken = new CancellationTokenSource();
            await Task.Factory.StartNew(async () =>
            {
                while (!iterationTime.HasValue || iterationTime > 0)
                {
                    if (_cancellationToken.IsCancellationRequested)
                        break;

                    _logger.Debug("Trying to fetch feature toggles");
                    var getFeatureTak = GetFeatureToggles();
                    _features = await getFeatureTak;
                    if (!_isInitialized && !getFeatureTak.IsFaulted)
                    {
                        _logger.Debug("First initialization done. ");
                        _isInitialized = true;
                    }

                    if (getFeatureTak.IsFaulted)
                    {
                        _logger.Warn("Error when fetching feature toggles.");
                        _logger.Warn("Exception: {0}", getFeatureTak.Exception?.Flatten().Message);
                    }
                    else _logger.Debug("{0} Features toggles correctly fetched.", _features.Count);

                    await Task.Delay(_pollingDelay, _cancellationToken.Token);
                }
            }, _cancellationToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void Stop()
        {
            _logger.Debug("Cancelling feature toggle polling task");
            _cancellationToken.Cancel();
        }

        private async Task<IList<FeatureToggleDto>> GetFeatureToggles()
        {
            using (var client = new HttpClient())
            {
                var webApiUrl = _url.Get();
                _logger.Debug("Calling {0} route", webApiUrl.AbsolutePath);
                using (var response = await client.GetAsync(webApiUrl))
                {
                    using (var content = response.Content)
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
