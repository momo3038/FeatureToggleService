using System.Collections.Generic;
using System.Web.Http;
using FeatureToggleService.Data;

namespace FeatureToggleService.WebApi
{
    public class FeatureToggleController : ApiController
    {
        private readonly IFeatureToggleRepository _repository;
        public FeatureToggleController(IFeatureToggleRepository repository)
        {
            _repository = repository;
        }

        [Route("api/Type/{featureType}")]
        public IEnumerable<FeatureToggle> GetAllByType(string featureType)
        {
            var all = _repository.GetAllByType(featureType);
            return all;
        }
    }
}
