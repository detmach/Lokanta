using Detmach;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Html5
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        static string donus = "";
        [WebMethod]
        public static string musteriGetir()
        {
            donus = musteri.musterileriListele();
            return donus;
        }
        [WebMethod]
        public static string musteriEkle(string adi, string soyadi, string adres, string telefon, string email)
        {
            musteri ms = new musteri();
            ms.AD = adi;
            ms.SOYAD = soyadi;
            ms.ADRES = adres;
            ms.TELEFON = telefon;
            ms.EMAIL = email;
            donus = musteri.musteriEkle(ms);
            return donus;
        }
        [WebMethod]
        public static string musteriSil(string id)
        {
            musteri ms = new musteri();
            ms.ID = id;
            return musteri.musteriSil(ms);
        }
        [WebMethod]
        public static string musteriGuncelle(string adi, string soyadi, string adres, string telefon, string email, string id)
        {
            musteri ms = new musteri();
            ms.AD = adi;
            ms.SOYAD = soyadi;
            ms.ADRES = adres;
            ms.TELEFON = telefon;
            ms.EMAIL = email;
            ms.ID = id;
            donus = musteri.musteriGuncelle(ms);
            return donus;
        }
        [WebMethod]
        public static string haritaGuncelle(string lat, string lut, string id)
        {
            donus = "";
            musteri ms = new musteri();
            ms.LAT = lat;
            ms.LUT = lut;
            ms.ID = id;
            donus = musteri.haritaGuncelle(ms);
            return donus;
        }
        

    }
}