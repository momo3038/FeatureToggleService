using System;
using FeatureToggleService.Db;

namespace FeatureToggleService.WebApi
{
    public interface IFeatureToggleValidator
    {
        void ThrowExceptionIfError(ToggleFeat toggleFeature);
    }

    public class FeatureToggleValidator : IFeatureToggleValidator
    {
        public void ThrowExceptionIfError(ToggleFeat toggleFeature)
        {
            if (toggleFeature.Id == Guid.Empty)
                throw new Exception("Guid cannot be empty");

            if (string.IsNullOrEmpty(toggleFeature.Name))
                throw new Exception("Name is mandatory");
        }
    }
}