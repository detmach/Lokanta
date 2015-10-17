using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    public partial class ms : System.Web.UI.MasterPage
    {
        HttpCookie userCookie = new HttpCookie("lokanta");
        protected void Page_Load(object sender, EventArgs e)
        {
            users h = new users();
            string a = h.ReadCookie("Lokanta");
            
            if ( a == null)
            {
                Response.Redirect("Default.aspx");
            }
        }
        
        
    }
}