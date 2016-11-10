using System;

namespace Swagger.ObjectModel.Model2
{
    public class ParameterOrReference: Parameter
    {
        [Newtonsoft.Json.JsonProperty("$ref")]
        public String _ref;
    }
}