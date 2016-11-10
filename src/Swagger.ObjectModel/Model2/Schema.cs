using System;
using System.Collections.Generic;

namespace Swagger.ObjectModel.Model2
{
    public class Schema
    {
        [Newtonsoft.Json.JsonProperty("$ref")]
        public String _ref;
        public String format;
        public String title;
        public String description;
        [Newtonsoft.Json.JsonProperty("default")]
        public Object _default;
        public decimal? multipleOf;
        public decimal? maximum;
        public bool? exclusiveMaximum;
        public decimal? minimum;
        public bool? exclusiveMinimum;
        public decimal? maxLength;
        public decimal? minLength;
        public String pattern;
        public decimal? maxItems;
        public decimal? minItems;
        public bool? uniqueItems;
        public decimal? maxProperties;
        public decimal? minProperties;
        public List<String> required;
        [Newtonsoft.Json.JsonProperty("enum")]
        public List<Object> _enum;
        public String type;
        public SchemaOrListOfSchemas items;
        public List<Schema> allOf;
        public Dictionary<String, Schema> properties;
        public AdditionalProperties additionalProperties;
        public String discriminator;
        public bool? readOnly;
        public Xml xml;
        public ExternalDocumentation externalDocs;
        public Object example;
    }
}