using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Example.FeatureHub.Infrastructure.Configurations
{
    public class LogFilterConfiguration
    {
        public HttpRequestResponseFilterConfiguration HttpRequestFilter { get; set; }
        public HttpRequestResponseFilterConfiguration HttpResponseFilter { get; set; }
    }
}
