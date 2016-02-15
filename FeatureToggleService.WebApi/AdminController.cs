using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using FeatureToggleService.Data;

namespace FeatureToggleService.WebApi
{
    public class AdminController : ApiController
    {
        private readonly IFeatureToggleRepository _repository;
        private readonly IFeatureToggleValidator _validator;

        public AdminController(IFeatureToggleRepository repository, IFeatureToggleValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        [HttpGet]
        [Route("api/Admin")]
        public IEnumerable<FeatureToggleByType> GetAll()
        {
            var allFeat = _repository.GetAll().ToList();

            IList<FeatureToggleByType> result = new List<FeatureToggleByType>();

             foreach (IGrouping<string, FeatureToggle> groupedFeat in allFeat.GroupBy(x => x.Type))
               {
                   var featureToogle = new FeatureToggleByType();
                   featureToogle.Features = new List<FeatureToggle>();
                   featureToogle.Type = groupedFeat.Key;

                   foreach (FeatureToggle name in groupedFeat)
                       featureToogle.Features.Add(name);

                   result.Add(featureToogle);
             }
             return result; 
        }

        [HttpDelete]
        [Route("api/Admin/{featureId}")]
        public void Delete(Guid featureId)
        {
            _repository.Delete(featureId);
        }

        [HttpPut]
        [Route("api/Admin/{featureId}/{value}")]
        public void ChangeValue([FromUri]Guid featureId, [FromUri]bool value)
        {
            _repository.ChangeValue(featureId, value);
        }

        [HttpPost]
        [Route("api/Admin")]
        public void Create([FromBody]FeatureToggle featureToCreate)
        {
            _validator.ThrowExceptionIfError(featureToCreate);
            _repository.Create(featureToCreate);
        }
    }
}
