using CurateMyCommunity_Api.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CurateMyCommunity_Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<CMC_DB_Connection>(null);
        }
        protected void Application_AuthenticateRequest()
        {
            
            if (Context.User == null) { return; } // exit out since no user is set
            //get current user username
            string username = Context.User.Identity.Name;

            bool authenticated = Context.User.Identity.IsAuthenticated;
            ////setup a dbcontext
            //string[] roles = null;
            //using (CMC_DB_Connection context = new CMC_DB_Connection())
            //{
            //    //add our roles to IPrincipal
            //    User userDTO = context.Users.FirstOrDefault(row => row.username == username);
            //    if (userDTO != null)
            //    {
            //        roles = context.UserRoles.Where(row => row.id_user_roles == userDTO.id_users)
            //            .Select(row => row.RoleId.role_name)
            //            .ToArray();
            //    }
            //}
            ////build a IPrincipal object
            //IIdentity userIdentity = new GenericIdentity(username);
            //IPrincipal newUserObj = new System.Security.Principal.GenericPrincipal(userIdentity, roles);

            //update the context.user with our IPrincipal
            //Context.User = newUserObj;
        }

    }
}
