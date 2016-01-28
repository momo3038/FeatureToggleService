using System;
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
        public void Should_throw_an_exception_if_guid_is_empty()
        {
            var toggle = new FeatureToggleService.Db.FeatureToggle
            {
                Id = Guid.Empty
            };
            
           Check.ThatCode(() => _validator.ThrowExceptionIfError(toggle)).Throws<Exception>().WithMessage("Guid cannot be empty");
        }

        [Test]
        public void Should_throw_an_exception_if_name_is_not_defined()
        {
            var toggle = new FeatureToggleService.Db.FeatureToggle
            {
                Id = Guid.NewGuid(),
                Name = ""
            };

            Check.ThatCode(() => _validator.ThrowExceptionIfError(toggle)).Throws<Exception>().WithMessage("Name is mandatory");
        }
    }
}
