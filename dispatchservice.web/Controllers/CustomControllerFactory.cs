using System.Web.Mvc;
using System.Web.Routing;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using dispatchservice.web.Models.CustomerDict;
using prod.web.Tools;

namespace dispatchservice.web.Controllers
{

    public class CustomControllerFactory : DefaultControllerFactory
    {

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (controllerName == "Generic")
            {
                //Use your favourite DI Container to resolve the customcontrollerfactory
                var genericControllerResolver = new CustomGenericControllerFactory();
                return genericControllerResolver.GetControllerType(requestContext);

            }

            return base.CreateController(requestContext, controllerName);
        }

        protected override System.Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            if (controllerName == "Generic")
            {
                return null;
            }

            return base.GetControllerType(requestContext, controllerName);
        }

    }

    //This should implement an interface
    public class CustomGenericControllerFactory
    {
        public IController GetControllerType(RequestContext requestContext)
        {
            //Use your favourite DI container to set up and resolve the concrete controller type using the
            //following genericControllerVariable.
            var genericControllerVariable = requestContext.RouteData.Values["GenericControllerVariable"];

            switch (genericControllerVariable.ToString())
            {
                case "Injener":
                    return new GenericController<Injener>(new InjenerViewModal(),
                                                          new CustomerDictRepository<Injener>( new Repository<Injener>() )
                                                          );
                case "Service":
                    return new GenericController<Service>(new ServiceViewModal(),
                                                          new CustomerDictRepository<Service>(new Repository<Service>())
                                                          );               
                case "Estate":
                    return new GenericController<Estate>(new EstateViewModal(),
                                                          new CustomerDictRepository<Estate>(new Repository<Estate>())
                                                          );
                case "Street":
                    return new GenericController<Street>(new StreetViewModal(),
                                                          new CustomerDictRepository<Street>(new Repository<Street>())
                                                          );               
            
                    
            }

            return null;
        }
    }

}