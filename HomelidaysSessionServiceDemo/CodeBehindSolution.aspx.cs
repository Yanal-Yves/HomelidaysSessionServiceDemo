namespace HomelidaysSessionServiceDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Homelidays.Web.SessionService;

    public partial class CodeBehindSolution : System.Web.UI.Page
    {
        /// <summary>
        /// The session state : FOR SESSION SERVICE
        /// </summary>
        private AspSession aspSession;

        public AspSession AspSession
        {
            get
            {

                if (this.aspSession == null)
                {
                    this.aspSession = System.Web.HttpContext.Current.Items["HomelidaysAspSession"] as AspSession;

                    if (this.aspSession == null)
                    {
                        throw new Exception("Please make sure that the MyModule1 is configured in web.confg");
                    }
                }

                return this.aspSession;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.AspSession["DotNetIntValue"] = 12;
            foreach (var item in this.AspSession)
            {
                LContentSess.Text += "Key : " + item.Key + " Value : " + item.Value + "<br/>";
            }

            if (((int)this.AspSession["DotNetIntValue"]) == 12)
            {
                Response.Write("blabla");
            }
        }
    }
}