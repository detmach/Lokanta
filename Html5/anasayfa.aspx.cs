using Detmach;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    public partial class anasayfa : System.Web.UI.Page
    {
        static CultureInfo ciTR = new CultureInfo("tr-TR");
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        [WebMethod]
        public static string  masalar()
        {
            String MASAAD, ID, DURUM, RESIM, MASADURUMU;
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = (SqlDataReader)VeriIslemleri.dataReaderSorgu("select * from masalar", CommandType.Text);
              //          .my-page .ui-listview li.ui-li-has-thumb .ui-li-thumb {
              //height: auto;
              //max-height: none;
              //max-width: 100%;
            while (dr.Read())
            {
                MASAAD = dr["MASAAD"].ToString();
                ID = dr["ID"].ToString();
                DURUM = dr["DURUM"].ToString();
                if (DURUM =="1")
                {
                    RESIM = "<img class='ui-li-thumb' src='/img/acik.jpg'/>";
                    MASADURUMU = "KAPALI";
                }
                else if (DURUM == "2")
                {
                    RESIM = "<img class='ui-li-thumb' src='/img/kapali.jpg' />";
                    MASADURUMU = "AÇIK";
                }
                else
                {
                    RESIM = "<img class='ui-li-thumb' src='/img/rezerve.jpg' />";
                    MASADURUMU = "REZERVE";
                }
                //<li><a href="#">
                //<img src="/img/acik.jpg" class="ui-li-thumb"/>
                //<h2>iOS 6.1</h2>
                //<p>Apple released iOS 6.1</p>
                //<p class="ui-li-aside">iOS</p>
                //</a></li>
                sb.Append("<li><a href='siparis.aspx?id="+ID+"'>" +
                    RESIM +
                    //"<h2>iOS 6.1</h2>" +
                    "<p>" + MASADURUMU + "<br/>"+hesapYap(ID)+"</p>" +
                    "<p class='ui-li-aside'>"+MASAAD+"</p>"+
                    "</a></li>");
            }
            return sb.ToString();
        }

        public static string hesapYap(string sayfaid)
        {
            string ADISYONID = "";
            string DURUM = "";
            string donus = "";
            int adet = 0;
            double fiyat = 0;
            double tfiyat = 0;
            try
            {
                DURUM = VeriIslemleri.tekSatirVeriSorgula("select durum from masalar where ID = '" + sayfaid + "'", CommandType.Text).ToString();
                if (DURUM == "2")
                {
                    ADISYONID = VeriIslemleri.tekSatirVeriSorgula("select TOP 1 ID from adisyonlar where MASAID = '" + sayfaid + "' and DURUM = '1'", CommandType.Text).ToString();
                    SqlDataReader dr = (SqlDataReader)VeriIslemleri.dataReaderSorgu("SELECT satislar.ADET, urunler.FIYAT FROM satislar INNER JOIN urunler ON satislar.URUNID = dbo.urunler.ID WHERE (satislar.MASAID = '" + sayfaid + "') AND (satislar.ADISYONID = '" + ADISYONID + "') ", CommandType.Text);
                    while (dr.Read())
                    {
                        adet = (int)dr["ADET"];
                        fiyat = Convert.ToDouble(dr["FIYAT"]);
                        tfiyat += fiyat * adet;
                    }
                }
                else
                {
                    ADISYONID = "";
                }
            }
            catch (Exception)
            {

            }
            donus = String.Format(ciTR, "{0:c}", (tfiyat));
            return donus;
        }
    }

}