﻿using BackEnd.Example.FeatureHub.Domain.Gateways;
using FeatureHubSDK;
using IO.FeatureHub.SSE.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Example.FeatureHub.Infrastructure.Gateways
{
    public class ControlFeatureHubGateway : IControlFeatureHubGateway
    {
        private readonly IFeatureHubConfig _featureHub;

        public ControlFeatureHubGateway(IFeatureHubConfig featureHub)
        {
            _featureHub = featureHub;
        }

        public async Task<bool?> CallApiPercent()
        {
            var fh = _featureHub.Repository;

            Random random = new Random();
            //UserKey Regex Rule ^(?:[2-9]|\d\d\d*)$
            var percentContext = await _featureHub.NewContext().UserKey(random.Next().ToString()).Build();

            if (fh.Readyness == Readyness.Ready)
            {
                Func<bool?> val = () => percentContext["PERCENT"].BooleanValue;

                percentContext.Close();

                return await Task.FromResult(val.Invoke().Value);
            }
            else
            {
                return null;
            }
        }
    }
}