using System;
using System.Collections.Generic;

namespace Swagger.ObjectModel.Model2
{
    public class SecurityScheme
    {
        public String type;
        public String description;
        public String name;
        [Newtonsoft.Json.JsonProperty("in")]
        public String _in;
        public String flow;
        public String authorizationUrl;
        public String tokenUrl;
        public Dictionary<String, String> scopes;
    }
}