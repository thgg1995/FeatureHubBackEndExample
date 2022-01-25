using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Example.FeatureHub.Domain.Gateways
{
    public interface IControlFeatureHubGateway
    {
        Task<bool?> CallApiPercent();
    }
}
