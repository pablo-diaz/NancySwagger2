using System;

namespace Swagger.ObjectModel.Model2
{
    public class Xml
    {
        public String name;
        [Newtonsoft.Json.JsonProperty("namespace")]
        public String _namespace;
        public String prefix;
        public bool? attribute;
        public bool? wrapped;
    }
}