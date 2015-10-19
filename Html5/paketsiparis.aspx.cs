using Detmach;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    public partial class paketsiparis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] == null)
            {
                Response.Redirect("anasayfa.aspx");
            }
            else
            {
                veriler.sayfaid = Request.QueryString["ID"].ToString();
                lbl.Text = "Masa " + veriler.sayfaid;
            }
        }


        [WebMethod]
        public static string SiparisAl(string veri,string aciklama)
        {
            try
            {
                
                veriler.ADISYONID = veriler.adisyonOlustur("2", "1", "",aciklama);
                string URUNID = "", ADET = "";
                string[] gelenler = veri.Remove(veri.Length - 1).Split('-');
                int s = 0;
                for (int i = 0; i < gelenler.Length; i++)
                {
                    s++;
                    if (s == 1)
                    {
                        URUNID = gelenler[i];
                    }
                    else if (s == 2)
                    {
                        ADET = gelenler[i];
                        if (Convert.ToInt32(i) > 2)
                        {
                            if (gelenler[i - 1] == gelenler[i - 3])
                            {
                                int b = veriler.UrunSil(URUNID);
                                ADET = (b + Convert.ToInt32(gelenler[i])).ToString();
                            }
                        }

                        veriler.UrunleriGir(veriler.ADISYONID, URUNID, ADET, "");
                        s = 0;

                    }
                }
            }
            catch (Exception)
            {
            }

            return "OK";
        }

        [WebMethod]
        public static string OdemeYap(string odemeturu, string indirim)
        {
            string donus = "";
            try
            {
                veriler.hesapKapatpaket(odemeturu, indirim, "1");
                veriler.AdisyonKapat(0);
                veriler.paketServisi(veriler.ADISYONID, veriler.sayfaid, odemeturu, "");
                veriler.ADISYONID = "";
                donus = "OK";
            }
            catch (Exception)
            {

            }
            return donus;
        }

    }
    
}