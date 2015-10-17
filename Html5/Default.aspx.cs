using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            users h = new users();
            string a = h.ReadCookie("Lokanta");

            if (a != null)
            {
                Response.Redirect("anasayfa.aspx");
            }
        }
    }
}