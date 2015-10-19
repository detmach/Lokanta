using Detmach;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace Html5
{
    public class veriler
    {
        public static string sayfaid { get; set; }
        public static string ADISYONID { get; set; }
        public static string DURUM { get; set; }
        public static DataTable dt = new DataTable();
        public static Json js = new Json();
        static CultureInfo ciTR = new CultureInfo("tr-TR");

        public static string Menuler()
        {
            string veri;
            dt = VeriIslemleri.dataTableSorgu("select * from kategoriler", CommandType.Text);

            veri = JsonConvert.SerializeObject(dt);
            //veri = js.ConvertDataTabletoString(dt);
            return veri;
        }
        public static string menuList(string id)
        {
            string veri="";
            try
            {
                if (id == "yok")
                {
                    dt = VeriIslemleri.dataTableSorgu("select * from urunler", CommandType.Text);
                    veri = JsonConvert.SerializeObject(dt);
                }
                else
                {
                    dt = VeriIslemleri.dataTableSorgu("select * from urunler where KATEGORIID = '" + id + "'", CommandType.Text);
                    veri = JsonConvert.SerializeObject(dt);
                }
            }
            catch (Exception)
            {                
                veri = "hata";
            }
            return veri;
        }
        public static string adisyonOlustur(string SERVISTURNO, string PERSONELID, string MASAID,string ACIKLAMA)
        {
            string aa = VeriIslemleri.tekSatirVeriSorgula("insert into adisyonlar (SERVISTURNO,PERSONELID,MASAID,DURUM,ACIKLAMA) VALUES ('" + SERVISTURNO + "','" + PERSONELID + "','" + MASAID + "','1','"+ACIKLAMA+"') select SCOPE_IDENTITY() ", CommandType.Text).ToString();
            return aa;

        }
        public static int UrunSil(string urunid)
        {
            int b = (int)VeriIslemleri.tekSatirVeriSorgula("select ADET from satislar where ADISYONID = '" + veriler.ADISYONID + "' and URUNID = '" + urunid + "'", CommandType.Text);
            VeriIslemleri.sorguCalistir("delete from satislar where ADISYONID = '" + veriler.ADISYONID + "' and URUNID = '" + urunid + "'", CommandType.Text);
            return b;
        }
        public static void masadurumuguncelle(string masaId, int Durum)
        {
            VeriIslemleri.tekSatirVeriSorgula("update masalar set DURUM = '" + Durum + "' where ID ='" + masaId + "'", CommandType.Text);
        }
        public static void UrunleriGir(string ADISYONNO, string URUNID, string ADET, string MASAID)
        {
            VeriIslemleri.sorguCalistir("insert into satislar(ADISYONID,URUNID,ADET,MASAID) values ('" + ADISYONNO + "','" + URUNID + "','" + ADET + "','" + MASAID + "')", CommandType.Text);
        }
        public static string hesapYap()
        {
            string donus = "";
            int adet = 0;
            double fiyat = 0;
            double tfiyat = 0;
            try
            {
                veriler.DURUM = VeriIslemleri.tekSatirVeriSorgula("select durum from masalar where ID = '" + veriler.sayfaid + "'", CommandType.Text).ToString();
                if (veriler.DURUM == "2")
                {
                    veriler.ADISYONID = VeriIslemleri.tekSatirVeriSorgula("select TOP 1 ID from adisyonlar where MASAID = '" + veriler.sayfaid + "' and DURUM = '1'", CommandType.Text).ToString();
                    SqlDataReader dr = (SqlDataReader)VeriIslemleri.dataReaderSorgu("SELECT satislar.ADET, urunler.FIYAT FROM satislar INNER JOIN urunler ON satislar.URUNID = dbo.urunler.ID WHERE (satislar.MASAID = '" + veriler.sayfaid + "') AND (satislar.ADISYONID = '" + veriler.ADISYONID + "') ", CommandType.Text);
                    while (dr.Read())
                    {
                        adet = (int)dr["ADET"];
                        fiyat = Convert.ToDouble(dr["FIYAT"]);
                        tfiyat += fiyat * adet;
                    }
                }
                else
                {
                    veriler.ADISYONID = "";
                }
            }
            catch (Exception)
            {

            }
            donus = String.Format(ciTR, "{0:c}", (tfiyat));
            return donus;
        }
        public static string musterihesapYap()
        {
            string donus = "";
            int adet = 0;
            double fiyat = 0;
            double tfiyat = 0;
            try
            {
                
                    veriler.ADISYONID = VeriIslemleri.tekSatirVeriSorgula("select TOP 1 ID from adisyonlar where SERVISTURNO = '2' and DURUM = '1'", CommandType.Text).ToString();
                    SqlDataReader dr = (SqlDataReader)VeriIslemleri.dataReaderSorgu("SELECT satislar.ADET, urunler.FIYAT FROM satislar INNER JOIN urunler ON satislar.URUNID = dbo.urunler.ID WHERE (satislar.ADISYONID = '" + veriler.ADISYONID + "') ", CommandType.Text);
                    while (dr.Read())
                    {
                        adet = (int)dr["ADET"];
                        fiyat = Convert.ToDouble(dr["FIYAT"]);
                        tfiyat += fiyat * adet;
                    }
                    
                
            }
            catch (Exception)
            {

            }
            donus = String.Format(ciTR, "{0:c}", (tfiyat));
            return donus;
        }
        public static void hesapKapat(string odemeturu, string indirim, string musteriId)
        {
            try
            {
                double aratoplam = Convert.ToDouble(hesapYap().Replace("₺", ""));
                double kdv = Convert.ToDouble(aratoplam) * 8 / 100;
                double geneltoplam = (aratoplam - Convert.ToDouble(indirim.Replace('.',',')));
                VeriIslemleri.sorguCalistir("insert into hesapOdemeleri (ADISYONID,ODEMETURID,MUSTERIID,ARATOPLAM,KDVTUTARI,INDIRIM,TOPLAMTUTAR) values ('" + veriler.ADISYONID + "','" + odemeturu + "','"+musteriId+"','" + aratoplam + "','" + kdv + "','" + indirim + "','" + geneltoplam + "')", System.Data.CommandType.Text);
                    
            }
            catch (Exception)
            {
            }

        }
        public static void hesapKapatpaket(string odemeturu, string indirim, string musteriId)
        {
            try
            {
                double aratoplam = Convert.ToDouble(musterihesapYap().Replace("₺", ""));
                double kdv = Convert.ToDouble(aratoplam) * 8 / 100;
                double geneltoplam = (aratoplam - Convert.ToDouble(indirim.Replace('.', ',')));
                VeriIslemleri.sorguCalistir("insert into hesapOdemeleri (ADISYONID,ODEMETURID,MUSTERIID,ARATOPLAM,KDVTUTARI,INDIRIM,TOPLAMTUTAR) values ('" + veriler.ADISYONID + "','" + odemeturu + "','" + musteriId + "','" + aratoplam + "','" + kdv + "','" + indirim + "','" + geneltoplam + "')", System.Data.CommandType.Text);

            }
            catch (Exception)
            {
            }

        }
        public static void AdisyonKapat(int durum)
        {
            try
            {
                VeriIslemleri.sorguCalistir("update adisyonlar set DURUM ='" + durum + "' where ID='" + veriler.ADISYONID + "'", System.Data.CommandType.Text);

            }
            catch (Exception)
            {
            }
        }
        public static void paketServisi(string adisonId, string musteriId,string odemeTurId, string aciklama)
        {
            VeriIslemleri.sorguCalistir("insert into paketSiparis(ADISYONID,MUSTERID,ODEMETURID,ACIKLAMA)VALUES('" + adisonId + "','" + musteriId+ "','" + odemeTurId + "','" + aciklama + "')", System.Data.CommandType.Text);
        }
    }
}