using System;
using System.Collections.Generic;

namespace Swagger.ObjectModel.Model2
{
    public class Operation
    {
        public List<String> tags;
        public String summary;
        public String description;
        public ExternalDocumentation externalDocs;
        public String operationId;
        public List<String> consumes;
        public List<String> produces;
        public List<ParameterOrReference> parameters;
        public Dictionary<String, Response> responses;
        public List<String> schemes;
        public bool? deprecated;
        public List<Dictionary<String, List<String>>> security;
    }
}