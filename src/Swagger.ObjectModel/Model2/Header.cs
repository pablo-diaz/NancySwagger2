﻿using System;
using System.Collections.Generic;

namespace Swagger.ObjectModel.Model2
{
    public class Header
    {
        public String description;
        public String type;
        public String format;
        public Items items;
        public String collectionFormat;
        [Newtonsoft.Json.JsonProperty("default")]
        public Object _default;
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
        [Newtonsoft.Json.JsonProperty("enum")]
        public List<Object> _enum;
        public decimal? multipleOf;
    }
}