using System;
using System.Collections.Generic;
using System.Web.Http;
using FeatureToggleService.Db;

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
        [Route("api/Admin/All")]
        public IEnumerable<ToggleFeat> GetAll()
        {
            return _repository.GetAll();
        }

        [HttpDelete]
        [Route("api/Admin/{featureId}")]
        public void Delete(Guid featureId)
        {
            _repository.Delete(featureId);
        }

        [HttpGet]
        [Route("api/Admin/{featureId}/{value}")]
        public void ChangeValue(Guid featureId, bool value)
        {
            _repository.ChangeValue(featureId, value);
        }

        [HttpPost]
        [Route("api/Admin")]
        public void Create([FromBody]ToggleFeat featureToCreate)
        {
            _validator.ThrowExceptionIfError(featureToCreate);
            _repository.Create(featureToCreate);
        }
    }
}
