using System.Collections.Generic;
using Nancy.Routing;
using M2 = Swagger.ObjectModel.Model2;
using Newtonsoft.Json;
using System.Reflection;
using Swagger.ObjectModel.ApiDeclaration;

namespace Nancy.Swagger.Modules
{
    [SwaggerApi]
    public class Swagger2Module : NancyModule
    {
        public Swagger2Module(IRouteCacheProvider routeCacheProvider, NancyContext sss)
        {
            Get["SwaggerDocumentation", "/swagger/docs/v1"] = _ =>
            {
                M2.Swagger openAPI = new M2.Swagger();

                openAPI.swagger = "2.0";
                openAPI.host = "www.ejemplo.com"; //TODO
                openAPI.basePath = "/api/falta"; //TODO
                openAPI.schemes = new List<string>() { "http", "https" };
                openAPI.tags = new List<M2.Tag>();
                openAPI.paths = new Dictionary<string, M2.PathItem>();
                openAPI.info = new M2.Info() { version = "1.0.0", contact = new M2.Contact() { name = "Un contacto" }, title = "Esta es la API interesante" }; //TODO

                Dictionary<string, object> cachedDefs = new Dictionary<string, object>();
                string definitionsNamingFormat = "#/definitions/{0}";

                foreach (var module in routeCacheProvider.GetCache())
                {
                    bool shouldProcessThisModule = true;
                    foreach (CustomAttributeData attribute in module.Key.GetCustomAttributesData())
                    {
                        if(attribute.ToString().IndexOf("SwaggerApiAttribute") > -1)
                        {
                            shouldProcessThisModule = false; // don't process this, because they are Nancy Swagger API modules
                            break;
                        }                            
                    }

                    if(!shouldProcessThisModule)
                        continue;
                    
                    openAPI.tags.Add(new M2.Tag() { name = module.Key.Name });

                    foreach (var route in module.Value)
                    {
                        M2.PathItem path = new M2.PathItem();
                        if (openAPI.paths.ContainsKey(route.Item2.Path))
                            path = openAPI.paths[route.Item2.Path];

                        M2.Operation operation = new M2.Operation();

                        operation.tags = new List<string>() { module.Key.Name };
                        operation.operationId = !string.IsNullOrEmpty(route.Item2.Name) ? route.Item2.Name : null;

                        if(route.Item2.Metadata.Has<SwaggerRouteData>())
                        {
                            SwaggerRouteData metadata = route.Item2.Metadata.Retrieve<SwaggerRouteData>();

                            operation.summary = metadata.OperationSummary;
                            operation.description = metadata.OperationNotes;

                            operation.parameters = new List<M2.ParameterOrReference>();
                            foreach(var parameter in metadata.OperationParameters)
                            {
                                M2.ParameterOrReference parameterInfo = new M2.ParameterOrReference()
                                {
                                    name = parameter.Name,
                                    _in = parameter.ParamType.ToString(),
                                    description = parameter.Description,
                                    required = parameter.Required
                                };

                                operation.parameters.Add(parameterInfo);

                                if(parameter.ParamType == ParameterType.Body && parameter.ParameterModel != null)
                                {
                                    string defName = string.Format(definitionsNamingFormat, parameter.ParameterModel.Name);
                                    if (!cachedDefs.ContainsKey(defName))
                                        cachedDefs[defName] = parameter.ParameterModel;

                                    parameterInfo.schema = new M2.Schema()
                                    {
                                        _ref = string.Format(definitionsNamingFormat, parameter.ParameterModel.Name)
                                    };
                                }
                                else
                                {
                                    parameterInfo.type = parameter.ParameterModel.Name;
                                    parameterInfo._default = parameter.DefaultValue != null ? parameter.DefaultValue.ToString() : null;
                                }
                            }

                            operation.produces = new List<string>();
                            foreach (var produce in metadata.OperationProduces)
                            {
                                operation.produces.Add(produce);
                            }

                            operation.responses = new Dictionary<string, M2.Response>();
                            foreach(var response in metadata.OperationResponseMessages)
                            {
                                operation.responses[response.Code.ToString()] = new M2.Response() { description = response.Message };
                            }
                        }
                        else // default
                        {
                            operation.parameters = new List<M2.ParameterOrReference>();
                            operation.produces = new List<string>() { "application/json" };
                            operation.responses = new Dictionary<string, M2.Response>();
                            operation.responses["200"] = new M2.Response() { description = "Success return" };
                        }

                        string method = route.Item2.Method.ToLower();
                        if (method == "get")
                            path.get = operation;
                        else if (method == "put")
                            path.put = operation;
                        else if (method == "post")
                            path.post = operation;
                        else if (method == "delete")
                            path.delete = operation;
                        else if (method == "options")
                            path.options = operation;
                        else if (method == "head")
                            path.head = operation;
                        else if (method == "patch")
                            path.patch = operation;

                        openAPI.paths[route.Item2.Path] = path;
                    }
                }

                if (cachedDefs.Count > 0)
                {
                    openAPI.definitions = new Dictionary<string, M2.Schema>();
                    foreach (KeyValuePair<string, object> definition in cachedDefs)
                    {
                        string definitionName = definition.Key;
                        System.Type def = (System.Type)definition.Value;

                        M2.Schema model = new M2.Schema();
                        openAPI.definitions[definitionName] = model;

                        model.xml = new M2.Xml() { name = def.Name };

                        Dictionary<string, bool> requiredProperties = new Dictionary<string, bool>();

                        // TODO: tomar los Atributos de esta clase 'def'
                        model.properties = new Dictionary<string, M2.Schema>();
                        foreach(PropertyInfo property in def.GetProperties())
                        {
                            M2.Schema propInfo = new M2.Schema();
                            propInfo.xml = new M2.Xml() { name = property.Name };
                            propInfo.type = property.PropertyType.Name;
                            model.properties[property.Name] = propInfo;

                            foreach(object attribute in property.GetCustomAttributes(true))
                            {
                                if(attribute.ToString().IndexOf("RequiredAttribute") > -1) // hackie way :(
                                {
                                    requiredProperties[property.Name] = true;
                                }
                            }
                        }

                        if(requiredProperties.Count > 0)
                        {
                            model.required = new List<string>();
                            foreach(KeyValuePair<string, bool> reqProp in requiredProperties)
                            {
                                model.required.Add(reqProp.Key);
                            }
                        }
                    }
                }

                return JsonConvert.SerializeObject(openAPI,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            };
        }
    }
}
