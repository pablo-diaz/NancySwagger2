using System;
using System.Collections.Generic;

namespace Swagger.ObjectModel.Model2
{
    // Based on https://github.com/hgwood/fanfaron
    public class Swagger
    {
        public String swagger;
        public Info info;
        public String host;
        public String basePath;
        public List<String> schemes;
        public List<String> consumes;
        public List<String> produces;
        public Dictionary<String, PathItem> paths;
        public Dictionary<String, Schema> definitions;
        public Dictionary<String, Parameter> parameters;
        public Dictionary<String, Response> responses;
        public Dictionary<String, SecurityScheme> securityDefinitions;
        public List<Dictionary<String, List<String>>> security;
        public List<Tag> tags;
        public ExternalDocumentation externalDocs;
    }
}
