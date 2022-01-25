using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Example.FeatureHub.Application.UseCases
{
    public interface IUseCaseAsync
    {
        Task ExecuteFeaturePercentAsync();
        Task ExecuteFeatureBooleanAsync();
    }
}
