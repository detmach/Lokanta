using Detmach;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    public partial class siparis : System.Web.UI.Page
    {
        static CultureInfo ciTR = new CultureInfo("tr-TR");
        
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
                Page.Header.Title = "Masa " + veriler.sayfaid + " - " + "Toplam Tutar " + veriler.hesapYap();
            }
            
        }
        [WebMethod]
        public static string Menuler()
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = (SqlDataReader)VeriIslemleri.dataReaderSorgu("select * from kategoriler", CommandType.Text);
            sb.Append("<h4>Menü</h4>");
            while (dr.Read())
            {
                sb.Append("<a href='#' id='"+dr["ID"].ToString()+"' class='ui-btn ui-corner-all ui-mini '>"+dr["KATEGORIADI"].ToString()+"</a>");
            }
            return sb.ToString();
        }        
        [WebMethod]
        public static string menuList(string id)
        {
            StringBuilder sb = new StringBuilder();
            if (id == "yok")
            {
                SqlDataReader dr = (SqlDataReader)VeriIslemleri.dataReaderSorgu("select * from urunler where durum = '1'", CommandType.Text);
                while (dr.Read())
                {
                    sb.Append("<li class='cbs'><a class='ui-btn ui-mini' href='#' id='" + dr["ID"].ToString() + "'>" + dr["URUNADI"].ToString() + "<span id='fyt' class='ui-li-count'>" + String.Format(ciTR, "{0:c} ₺", (dr["FIYAT"] == DBNull.Value ? "0,00" : Convert.ToDouble(dr["FIYAT"]).ToString().Replace("-", ""))) + "</span></a></li>");
                    
                }
            }
            else
            {
                SqlDataReader dr = (SqlDataReader)VeriIslemleri.dataReaderSorgu("select * from urunler where KATEGORIID = '" + id + "' and durum = '1'", CommandType.Text);
                while (dr.Read())
                {
                    sb.Append("<li class='cbs'><a class='ui-btn ui-mini' href='#' id='" + dr["ID"].ToString() + "'>" + dr["URUNADI"].ToString() + "<span id='fyt' class='ui-li-count'>" + String.Format(ciTR, "{0:c} ₺", (dr["FIYAT"] == DBNull.Value ? "0,00" : Convert.ToDouble(dr["FIYAT"]).ToString().Replace("-", ""))) + "</span></a></li>");
                }
            }
            
            
            return sb.ToString();
        }
        [WebMethod]
        public static string Siparisler()
        {
            string veri = "";
            StringBuilder sb = new StringBuilder();
            try
            {                
                if (veriler.sayfaid != "")
                {
                    veriler.DURUM = VeriIslemleri.tekSatirVeriSorgula("select durum from masalar where ID = '" + veriler.sayfaid + "'", CommandType.Text).ToString();
                    if (veriler.DURUM == "2")
                    {
                        veriler.ADISYONID = VeriIslemleri.tekSatirVeriSorgula("select ID from adisyonlar where MASAID = '" + veriler.sayfaid + "' and DURUM = '1'", CommandType.Text).ToString();
                        DataTable dt = VeriIslemleri.dataTableSorgu("SELECT urunler.URUNADI, urunler.FIYAT, satislar.ID, satislar.URUNID, satislar.ADET, adisyonlar.ACIKLAMA FROM satislar INNER JOIN urunler ON satislar.URUNID = urunler.ID INNER JOIN adisyonlar ON satislar.ADISYONID = adisyonlar.ID WHERE (satislar.ADISYONID = '"+veriler.ADISYONID+"')", CommandType.Text);
                        veri = JsonConvert.SerializeObject(dt);

                        //SqlDataReader dr = (SqlDataReader)VeriIslemleri.dataReaderSorgu("select URUNADI,FIYAT,satislar.ID,satislar.URUNID,satislar.ADET from satislar inner join urunler on satislar.URUNID=urunler.ID where ADISYONID='" + veriler.ADISYONID + "'", CommandType.Text);
                        //sb.Append("<li><a class='ui-btn ui-mini' href='#'>Ürün Adı<span class='ui-li-count'>Adet</span></a></li>");
                        //while (dr.Read())
                        //{
                        //    sb.Append("<li><a class='ui-btn ui-mini' href='#' ucr='"+Convert.ToDouble(dr["FIYAT"])+"' id='" + dr["URUNID"].ToString() + "'>" + dr["URUNADI"].ToString() + "<span id='adt' class='ui-li-count'>" + dr["ADET"].ToString() + "</span></a></li>");
                        //}
                    }
                    else
                    {
                        veriler.ADISYONID = "";
                    }
                }
            }
            catch (Exception)
            {
                veri = "Hata Oluştu";
            }
            return veri;
        }
        [WebMethod]
        public static string SiparisAl(string veri,string aciklama)
        {
            try
            {
                if (veriler.ADISYONID == "")
                {
                    veriler.ADISYONID = veriler.adisyonOlustur("1", "1", veriler.sayfaid,aciklama);
                }
                else
                {
                    VeriIslemleri.sorguCalistir("delete from satislar where ADISYONID = '" + veriler.ADISYONID + "'", CommandType.Text);
                }
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

                        veriler.UrunleriGir(veriler.ADISYONID, URUNID, ADET, veriler.sayfaid);
                        s = 0;

                    }
                }
                veriler.masadurumuguncelle(veriler.sayfaid, 2);
            }
            catch (Exception)
            {
            }
            
            return "OK";
        }
        [WebMethod]
        public static string UcretHesapla()
        {
            string donus = veriler.hesapYap();
            
            return donus;
        }
        [WebMethod]
        public static string OdemeYap(string odemeturu, string indirim)
        {
            string donus = "";
            try
            {
                veriler.hesapKapat(odemeturu, indirim, "1");
                veriler.masadurumuguncelle(veriler.sayfaid, 1);
                veriler.AdisyonKapat(0);
                donus = "OK";
            }
            catch (Exception)
            {
                
            }
            return donus;
        }

    }
}