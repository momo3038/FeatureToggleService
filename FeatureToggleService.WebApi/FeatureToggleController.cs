using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using FeatureToggleService.Db;

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
        public IEnumerable<ToggleFeat> GetAllByType(string featureType)
        {
          return  _repository.GetAllByType(featureType);
        }


        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
