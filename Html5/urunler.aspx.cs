using Detmach;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    public partial class urunler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string Menuler()
        {
            string a = veriler.Menuler();
            return a;
        }
        [WebMethod]
        public static string kategoriEkle(string kategoriAdi)
        {
            string donus = "";
            try
            {
                VeriIslemleri.sorguCalistir("INSERT INTO [kategoriler]([KATEGORIADI],[ACIKLAMA])VALUES('" + kategoriAdi + "','" + kategoriAdi + "')", CommandType.Text);
                donus = "OK";
            }
            catch (Exception)
            {
                donus = "Hata";
            }
            return donus;
        }

        [WebMethod]
        public static string kategoriSil(string kategoriId)
        {
            string donus = "";
            try
            {
                VeriIslemleri.sorguCalistir("DELETE FROM [kategoriler] WHERE ID = '" + kategoriId + "'", CommandType.Text);
                donus = "OK";
            }
            catch (Exception)
            {
                donus = "Hata";
            }
            return donus;
        }

        [WebMethod]
        public static string menuList(string id)
        {
            string veri = "";
            veri = veriler.menuList(id);
            return veri;
        }
        [WebMethod]
        public static string urunEkle(string kategoriId, string urunAdi, string fiyat)
        {
            string donus = "";
            try
            {
                VeriIslemleri.sorguCalistir("INSERT INTO [urunler]([KATEGORIID],[URUNADI],[ACIKLAMA],[FIYAT])VALUES('" + kategoriId + "','" + urunAdi + "','" + urunAdi + "','"+fiyat.Replace(",",".")+"')", CommandType.Text);
                donus = "OK";
            }
            catch (Exception)
            {
                donus = "Hata";
            }
            return donus;
        }
        [WebMethod]
        public static string urunSil(string urunId)
        {
            string donus = "";
            try
            {
                VeriIslemleri.sorguCalistir("DELETE FROM [urunler] WHERE ID = '" + urunId + "'", CommandType.Text);
                donus = "OK";
            }
            catch (Exception)
            {
                donus = "hata";  
            }
            return donus;
        }
        [WebMethod]
        public static string gunMenusu(string urunId, string durum)
        {
            try
            {
                if (durum == "0")
                {
                    VeriIslemleri.sorguCalistir("UPDATE [urunler] SET [DURUM] = '0' WHERE ID ='"+urunId+"'", CommandType.Text);
                    
                }
                else if (durum == "1")
                {
                    VeriIslemleri.sorguCalistir("UPDATE [urunler] SET [DURUM] = '1' WHERE ID ='" + urunId + "'", CommandType.Text);
                }
            }
            catch (Exception)
            {
                
            }

            return "";
        }
    }
}