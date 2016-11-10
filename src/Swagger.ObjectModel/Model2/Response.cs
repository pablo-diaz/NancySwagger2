using System;
using System.Collections.Generic;

namespace Swagger.ObjectModel.Model2
{
    public class Response
    {
        public String description;
        public Schema schema;
        public Dictionary<String, Header> headers;
        public Dictionary<String, Object> examples;
    }
}