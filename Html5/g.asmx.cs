using Detmach;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Services;

namespace Html5
{
    /// <summary>
    /// Summary description for g
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class g : System.Web.Services.WebService
    {

        [WebMethod]
        public string KullaniciGetir()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = (SqlDataReader)VeriIslemleri.dataReaderSorgu("select * from personeller", System.Data.CommandType.Text);
            sb.Append("<option value=\"\">Lütfen Kullanıcı Seçiniz</option>");
            while (dr.Read())
            {
                sb.Append("<option value=\"" + dr["ID"].ToString() + "\">" + dr["KULLANICIADI"].ToString() + "</option>");
            }
            return sb.ToString();

        }
        [WebMethod]
        public string GirisYap(string id, string ps)
        {

            String ids="", pss ="", donus="", user="";
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = (SqlDataReader)VeriIslemleri.dataReaderSorgu("select * from personeller where ID='" + id + "' and PAROLA='" + ps + "'", System.Data.CommandType.Text);
            while (dr.Read())
            {
                ids = dr["ID"].ToString();
                pss = dr["PAROLA"].ToString();
                user = dr["KULLANICIADI"].ToString();

            }
            if (id == ids && ps == pss)
            {
               
               users us = new users();
               us.WriteCookie("Lokanta", user);               

            }
            else
            {
                donus = "hata";
            }

            return donus;

        }
    }
}
