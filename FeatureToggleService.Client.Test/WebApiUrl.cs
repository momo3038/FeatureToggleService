using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggleService.Client.Provider;
using NFluent;
using NSubstitute;
using NUnit.Framework;

namespace FeatureToggleService.Client.Test
{
    internal class WebApiUrlTest
    {
        [Test]
        public void Should_get_url_without_feature_type()
        {
            var configuration = Substitute.For<IProviderConfiguration>();
            configuration.FeatureType.Returns((string)null);
            configuration.WebApiHost.Returns("http://localhost:8080/api");
            var webApiUrl = new WebApiUrl(configuration);
            var finalUrl = webApiUrl.Get();
            Check.That(finalUrl.AbsoluteUri).IsEqualTo("http://localhost:8080/api/all");
        }

        [Test]
        public void Should_get_url_with_feature_type()
        {
            var configuration = Substitute.For<IProviderConfiguration>();
            configuration.FeatureType.Returns("demo");
            configuration.WebApiHost.Returns("http://localhost:8080/api");
            var webApiUrl = new WebApiUrl(configuration);
            var finalUrl = webApiUrl.Get();
            Check.That(finalUrl.AbsoluteUri).IsEqualTo("http://localhost:8080/api/demo");
        }

        [Test]
        public void Should_get__clean_url_if_host_provided_with_slash()
        {
            var configuration = Substitute.For<IProviderConfiguration>();
            configuration.FeatureType.Returns("demo");
            configuration.WebApiHost.Returns("http://localhost:8080/api/");
            var webApiUrl = new WebApiUrl(configuration);
            var finalUrl = webApiUrl.Get();
            Check.That(finalUrl.AbsoluteUri).IsEqualTo("http://localhost:8080/api/demo");
        }
    }
}
