using Nancy.Metadata.Modules;
using Nancy.Responses.Negotiation;
using Nancy.Swagger.Demo.Models;

namespace Nancy.Swagger.Demo.Modules
{
    public class HomeMetadataModule : MetadataModule<SwaggerRouteData>
    {
        public HomeMetadataModule()
        {
            Describe["Prueba01"] = description => description.AsSwagger(with =>
            {
                with.Summary("Retorna el nombre del usuario que se pasa por parametros");
                with.Notes("Esta es una nota adicional para este metodo");
                with.PathParam<string>("nombre", "Nombre de la persona", true);
                with.Consumes(new MediaType("application/json"));
                with.Produces(new MediaType("application/json"));
                with.Response(200, "Exito de respuesta 200");
            });

            Describe["GetUsers"] = description => description.AsSwagger(with =>
            {
                with.ResourcePath("/users");
                with.Summary("The list of users");
                with.Notes("This returns a list of users from our awesome app");
                with.Model<User>();
                with.Model<Role>();
                with.Response(200, "");
            });

            Describe["PostUsers"] = description => description.AsSwagger(with =>
            {
                with.ResourcePath("/users");
                with.Summary("Create a User");
                with.Response(201, "Created a User");
                with.Response(422, "Invalid input");
                with.Model<User>();
                with.Model<Role>();
                with.BodyParam<User>("Usuario a crear", true);
                with.Notes("Creates a user with the shown schema for our awesome app");
            });
        }
    }
}