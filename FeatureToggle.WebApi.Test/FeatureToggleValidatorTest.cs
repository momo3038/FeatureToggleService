using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggleService.Db;
using FeatureToggleService.WebApi;
using NFluent;
using NUnit.Framework;

namespace FeatureToggle.WebApi.Test
{
    public class FeatureToggleValidatorTest
    {
        private FeatureToggleValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new FeatureToggleValidator();            
        }

        [Test]
        public void ShouldThrowAnExceptionIfGuidIsEmpty()
        {
            var toggle = new ToggleFeat
            {
                Id = Guid.Empty
            };
            
           Check.ThatCode(() => _validator.ThrowExceptionIfError(toggle)).Throws<Exception>().WithMessage("Guid cannot be empty");
        }

        [Test]
        public void ShouldThrowAnExceptionIfNameIsNotDefined()
        {
            var toggle = new ToggleFeat
            {
                Id = Guid.NewGuid(),
                Name = ""
            };

            Check.ThatCode(() => _validator.ThrowExceptionIfError(toggle)).Throws<Exception>().WithMessage("Name is mandatory");
        }
    }
}
