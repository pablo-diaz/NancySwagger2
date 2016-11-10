using System;
using System.Collections.Generic;

namespace Swagger.ObjectModel.Model2
{
    public class PathItem
    {
        [Newtonsoft.Json.JsonProperty("$ref")]
        public String _ref;
        public Operation get;
        public Operation put;
        public Operation post;
        public Operation delete;
        public Operation options;
        public Operation head;
        public Operation patch;
        public List<ParameterOrReference> parameters;
    }
}
