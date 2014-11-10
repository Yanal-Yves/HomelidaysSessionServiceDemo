// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomelidaysSessionModule.cs" company="La Petite Fouine">
//   Copyright (c) 2014 HomeAway, Inc.
// </copyright>
// <summary>
//   HomelidaysSessionModule.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HomelidaysSessionServiceDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Homelidays.Web.SessionService;

    /// <summary>
    ///
    /// </summary>
    public class HomelidaysSessionModule : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Initializes the MyModule1 module and prepares it to handle requests.
        /// </summary>
        /// <param name="application">An System.Web.HttpApplication that provides access to the methods, properties,
        /// and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication application)
        {
            application.BeginRequest += this.ApplicationBeginRequest;
            application.EndRequest += this.ApplicationEndRequest;
        }

        #endregion
        /// <summary>
        /// ASP.NET begin request event.
        /// </summary>
        /// <param name="source">An System.Web.HttpApplication that provides access to the methods, properties,
        /// and events common to all application objects within an ASP.NET application</param>
        /// <param name="e">HttpApplication event argument</param>
        private void ApplicationBeginRequest(object source, EventArgs e)
        {
            var application = (HttpApplication)source;
            HttpContext context = application.Context;

            if (this.IsSessionRequest(context))
            {
                var aspSession = new AspSession(context);
                context.Items["HomelidaysAspSession"] = aspSession;
            }
        }

        /// <summary>
        /// ASP.NET end request event.
        /// </summary>
        /// <param name="source">An System.Web.HttpApplication that provides access to the methods, properties,
        /// and events common to all application objects within an ASP.NET application</param>
        /// <param name="e">HttpApplication event argument</param>
        private void ApplicationEndRequest(object source, EventArgs e)
        {
            var application = (HttpApplication)source;
            HttpContext context = application.Context;

            if (this.IsSessionRequest(context))
            {
                var aspSession = context.Items["HomelidaysAspSession"] as AspSession;

                if (aspSession != null)
                {
                    // A l'unload de la page on fait la persistence de la session en base
                    aspSession.PersistSession();
                }
            }
        }

        /// <summary>
        /// Retruns true if the request uses the session (ex: .aspx uses the session but you may define that .ashx will never use the session)
        /// </summary>
        /// <param name="context">Current Http context</param>
        /// <returns>True if the Session object should be linked to the current request, else False</returns>
        private bool IsSessionRequest(HttpContext context)
        {
            // TODO - filter here the queries that uses the Homelidays SessionService

            return true;
        }
    }
}