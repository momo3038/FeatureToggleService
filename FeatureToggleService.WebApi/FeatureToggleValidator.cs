using System;
using FeatureToggleService.Data;

namespace FeatureToggleService.WebApi
{
    public interface IFeatureToggleValidator
    {
        void ThrowExceptionIfError(FeatureToggle featureToggleFeature);
    }

    public class FeatureToggleValidator : IFeatureToggleValidator
    {
        public void ThrowExceptionIfError(FeatureToggle featureToggleFeature)
        {
            if (featureToggleFeature.Id == Guid.Empty)
                throw new Exception("Guid cannot be empty");

            if (string.IsNullOrEmpty(featureToggleFeature.Name))
                throw new Exception("Name is mandatory");
        }
    }
}