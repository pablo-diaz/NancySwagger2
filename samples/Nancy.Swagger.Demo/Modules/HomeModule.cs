using Nancy.ModelBinding;
using Nancy.Swagger.Demo.Models;

namespace Nancy.Swagger.Demo.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["Home", "/"] = _ => "Hello Swagger!";

            Get["Prueba01", "/prueba/{nombre:string}"] = x => "Hola " + x.nombre;

            Get["/prueba/ping"] =
            Get["Prueba02", "/prueba/todo"] = _ => "Esto es todo lo que hay";

            Get["GetUsers", "/users"] = _ => new[] { new User { Name = "Vincent Vega", Age = 45 } };

            Post["/create/user"] =
            Post["PostUsers", "/users"] = _ =>
            {
                var result = this.BindAndValidate<User>();

                if (!ModelValidationResult.IsValid)
                {
                    return Negotiate.WithModel(new { Message = "Oops" })
                        .WithStatusCode(HttpStatusCode.UnprocessableEntity);
                }

                return Negotiate.WithModel(result).WithStatusCode(HttpStatusCode.Created);
            };
        }
    }
}