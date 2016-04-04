using FeatureToggleService.Client.Provider;
using Mock4Net.Core;
using NFluent;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FeatureToggleService.Client.Test
{
    public class WebApiProviderInitialisationTest
    {

        [Ignore("")]
        [Test]
        public async Task Should_Call_Configured_Route_to_retreive_toggles()
        {
            var server = FluentMockServer.Start();
            var configuration = Substitute.For<IProviderConfiguration>();
            configuration.WebApiUrl.Returns("http://localhost:" + server.Port + "/api/all");
            configuration.IsInitialized().Returns(true);

            server
              .Given(
                Requests.WithUrl("/api/all").
                UsingGet()
              )
              .RespondWith(
                Responses
                  .WithStatusCode(200)
                  .WithBody("{\"IsEnable\":\"true\", \"Name\":\"Test\"}")
              );

            var provider = new WebApiProviderInitialisation(configuration, TimeSpan.FromSeconds(2));
            await provider.Start();
            Thread.Sleep(100000000);

            Check.That(provider.GetAll()).Not.IsEmpty();
        }
    }
}
