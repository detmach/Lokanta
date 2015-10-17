using Detmach;
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
        public static string sayfaid = "";
        public static string ADISYONID = "";
        public static string DURUM = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (Request.QueryString["ID"] == null)
            {
                Response.Redirect("anasayfa.aspx");
            }
            else
            {
                sayfaid = Request.QueryString["ID"].ToString();
                lbl.Text = "Masa " + sayfaid;
                Page.Header.Title = "Masa " + sayfaid + " - " + "Toplam Tutar " + hesapYap();
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
            StringBuilder sb = new StringBuilder();
            try
            {                
                if (sayfaid != "")
                {
                    DURUM = VeriIslemleri.tekSatirVeriSorgula("select durum from masalar where ID = '" + sayfaid + "'", CommandType.Text).ToString();
                    if (DURUM == "2")
                    {
                        ADISYONID = VeriIslemleri.tekSatirVeriSorgula("select ID from adisyonlar where MASAID = '" + sayfaid + "' and DURUM = '1'", CommandType.Text).ToString();
                        SqlDataReader dr = (SqlDataReader)VeriIslemleri.dataReaderSorgu("select URUNADI,FIYAT,satislar.ID,satislar.URUNID,satislar.ADET from satislar inner join urunler on satislar.URUNID=urunler.ID where ADISYONID='" + ADISYONID + "'", CommandType.Text);
                        sb.Append("<li><a class='ui-btn ui-mini' href='#'>Ürün Adı<span class='ui-li-count'>Adet</span></a></li>");
                        while (dr.Read())
                        {
                            sb.Append("<li><a class='ui-btn ui-mini' href='#' ucr='"+Convert.ToDouble(dr["FIYAT"])+"' id='" + dr["URUNID"].ToString() + "'>" + dr["URUNADI"].ToString() + "<span id='adt' class='ui-li-count'>" + dr["ADET"].ToString() + "</span></a></li>");
                        }
                    }
                    else
                    {
                        ADISYONID = "";
                    }
                }
            }
            catch (Exception)
            {
                sb.Append("Hata Oluştu");
            }
            return sb.ToString();
        }
        [WebMethod]
        public static string SiparisAl(string veri)
        {
            try
            {
                if (ADISYONID == "")
                {
                    ADISYONID = adisyonOlustur("1", "1", sayfaid);
                }
                else
                {
                    VeriIslemleri.sorguCalistir("delete from satislar where ADISYONID = '" + ADISYONID + "'", CommandType.Text);
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
                                int b = UrunSil(URUNID);
                                ADET = (b + Convert.ToInt32(gelenler[i])).ToString();
                            }
                        }

                        UrunleriGir(ADISYONID, URUNID, ADET, sayfaid);
                        s = 0;

                    }
                }
                masadurumuguncelle(sayfaid, 2);
            }
            catch (Exception)
            {
            }
            
            return "OK";
        }
        [WebMethod]
        public static string UcretHesapla()
        {
            string donus = hesapYap();
            
            return donus;
        }
        [WebMethod]
        public static string OdemeYap(string odemeturu, string indirim)
        {
            string donus = "";
            try
            {
                hesapKapat(odemeturu, indirim);
                donus = "OK";
            }
            catch (Exception)
            {
                
            }
            return donus;
        }
        public static void masadurumuguncelle(string masaId, int Durum)
        {
            VeriIslemleri.tekSatirVeriSorgula("update masalar set DURUM = '" + Durum + "' where ID ='" + masaId + "'", CommandType.Text);
        }
        public static string adisyonOlustur(string SERVISTURNO,string PERSONELID, string MASAID)
        {
            string aa  = VeriIslemleri.tekSatirVeriSorgula("insert into adisyonlar (SERVISTURNO,PERSONELID,MASAID,DURUM) VALUES ('" + SERVISTURNO + "','" + PERSONELID + "','" + MASAID + "','1') select SCOPE_IDENTITY() ", CommandType.Text).ToString();
            return aa;

        }
        public static void UrunleriGir(string ADISYONNO,string URUNID, string ADET, string MASAID)
        {
            VeriIslemleri.sorguCalistir("insert into satislar(ADISYONID,URUNID,ADET,MASAID) values ('" + ADISYONNO + "','" + URUNID + "','" + ADET + "','" + MASAID + "')", CommandType.Text);
        }
        public static int UrunSil(string urunid)
        {
            int b = (int)VeriIslemleri.tekSatirVeriSorgula("select ADET from satislar where ADISYONID = '" + ADISYONID + "' and URUNID = '" + urunid + "'", CommandType.Text);
            VeriIslemleri.sorguCalistir("delete from satislar where ADISYONID = '" + ADISYONID + "' and URUNID = '"+urunid+"'",CommandType.Text);
            return b;
        }
        public static string hesapYap()
        {
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
        public static void hesapKapat(string odemeturu, string indirim)
        {
            try
            {
                DURUM = VeriIslemleri.tekSatirVeriSorgula("select durum from masalar where ID = '" + sayfaid + "'", CommandType.Text).ToString();
                if (DURUM == "2")
                {
                    double aratoplam = Convert.ToDouble(hesapYap().Replace("₺", ""));
                    double kdv = Convert.ToDouble(aratoplam) * 8 / 100;
                    double geneltoplam = (aratoplam - Convert.ToDouble(indirim));
                    VeriIslemleri.sorguCalistir("insert into hesapOdemeleri (ADISYONID,ODEMETURID,MUSTERIID,ARATOPLAM,KDVTUTARI,INDIRIM,TOPLAMTUTAR) values ('" + ADISYONID + "','" + odemeturu + "','1','" + aratoplam + "','" + kdv + "','" + indirim + "','" + geneltoplam + "')", System.Data.CommandType.Text);
                    masadurumuguncelle(sayfaid, 1);
                    AdisyonKapat(0);
                }
            }
            catch (Exception)
            {
            }
            
        }
        public static void AdisyonKapat(int durum)
        {
            try
            {
                VeriIslemleri.sorguCalistir("update adisyonlar set DURUM ='" + durum + "' where ID='" + ADISYONID + "'", System.Data.CommandType.Text);

            }
            catch (Exception)
            {
            }
        }
    }
}