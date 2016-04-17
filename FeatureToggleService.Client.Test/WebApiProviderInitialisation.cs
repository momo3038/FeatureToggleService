using System;
using System.Threading.Tasks;
using FeatureToggleService.Client.Provider;
using Mock4Net.Core;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggleService.Client.Test
{
    public class WebApiProviderInitialisationTest
    {
        [Test]
        public async Task Should_Call_Configured_Route_to_retreive_toggles()
        {
            var server = FluentMockServer.Start();
            var configuration = Substitute.For<IProviderConfiguration>();
            configuration.WebApiHost.Returns("http://localhost:" + server.Port + "/api/");
            configuration.FeatureType.Returns("Demo");

            server
              .Given(
                Requests.WithUrl("/api/Demo").
                UsingGet()
              )
              .RespondWith(
                Responses
                  .WithStatusCode(200)
                  .WithBody("[{\"IsEnable\":\"true\", \"Name\":\"Test\"},{\"IsEnable\":\"false\", \"Name\":\"Test2\"}]")
              );

            var provider = new WebApiProviderInitialisation(TimeSpan.FromMilliseconds(0), new WebApiUrl(configuration));
            await provider.Start(1);

            await Task.Delay(2000);
            provider.Stop();

            Check.That(provider.GetAll()).Not.IsEmpty();
            Check.That(provider.GetAll().Count).IsEqualTo(2);
        }

        [Test]
        public void Should_throw_Exception_if_InitProvider_if_not_initialized()
        {
            var configuration = Substitute.For<IProviderConfiguration>();
            var provider = new WebApiProviderInitialisation(TimeSpan.FromSeconds(1), new WebApiUrl(configuration));
            Check.ThatCode(() => provider.GetAll()).Throws<Exception>().WithMessage("Toggle are not yet retrieved.");
        }
    }
}
