using Detmach;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Html5
{
    public class musteri
    {
        public string ID { get; set; }
        public string AD { get; set; }
        public string SOYAD { get; set; }
        public string ADRES { get; set; }
        public string TELEFON { get; set; }
        public string ILKSIPARIS { get; set; }
        public string EMAIL { get; set; }
        public string SIPARISSAYISI { get; set; }
        public string LAT { get; set; }
        public string LUT { get; set; }
        public string DURUM { get; set; }
        public static DataTable dt = new DataTable();
        static CultureInfo ciTR = new CultureInfo("tr-TR");
        static string donus = "";
        public static string musterileriListele()
        {
            donus = "";
            try
            {
                dt = VeriIslemleri.dataTableSorgu("select * from musteriler", CommandType.Text);
                donus = JsonConvert.SerializeObject(dt);
            }
            catch (Exception)
            {
                donus = "Hata Oluştu";
            }
            
            return donus;
        }
        public static string musteriEkle(musteri Bilgiler)
        {
            string a ="";
            try
            {
                try
                {
                    a = VeriIslemleri.tekSatirVeriSorgula("select TELEFON from musteriler where TELEFON = '" + Bilgiler.TELEFON + "'", CommandType.Text).ToString();
                    donus = "Bu Telefon Numarasına Ait Bilgiler Bulunmakta";

                }
                catch (Exception)
                {
                    VeriIslemleri.sorguCalistir("INSERT INTO [musteriler]([AD],[SOYAD],[ADRES],[TELEFON],[EMAIL])VALUES('" + Bilgiler.AD + "','" + Bilgiler.SOYAD + "','" + Bilgiler.ADRES + "','" + Bilgiler.TELEFON + "','" + Bilgiler.EMAIL + "')", CommandType.Text);
                        donus = "Kayıt Başarılı";
                    
                }
                
               
                
            }
            catch (Exception)
            {
                donus = "Kayıt Başarısız";
            }
            return donus;
        }
        public static string musteriSil(musteri Bilgiler)
        {
            donus = "";
            try
            {
                VeriIslemleri.sorguCalistir("delete musteriler where ID ='"+Bilgiler.ID+"'", CommandType.Text);
                donus = "Müşteri Başarı İle Silindi";
            }
            catch (Exception)
            {
                donus = "Müşteri Silinirken Bir Hata Oluştu";
            }
            return donus;
        }
        public static string musteriGuncelle(musteri Bilgiler)
        {
            donus = "";
            try
            {
                VeriIslemleri.sorguCalistir("UPDATE [musteriler] SET [AD] = '"+Bilgiler.AD+"',[SOYAD] = '"+Bilgiler.SOYAD+"',[ADRES] = '"+Bilgiler.ADRES+"',[TELEFON] = '"+Bilgiler.TELEFON+"',[EMAIL] = '"+Bilgiler.EMAIL+"' WHERE ID = '"+Bilgiler.ID+"'", CommandType.Text);
                donus = "Kişi Bilgileri Başarı ile Güncellenmiştir";
            }
            catch (Exception)
            {
                donus = "Kişi Bilgileri Güncellenirken Bir Hata Oluştu";
            }
            return donus;
        }
        public static string haritaGuncelle(musteri Bilgiler)
        {
            donus = "";
            try
            {
                VeriIslemleri.sorguCalistir("UPDATE [musteriler] SET [LAT] = '" + Bilgiler.LAT + "',[LUT] = '" + Bilgiler.LUT + "' WHERE ID = '" + Bilgiler.ID + "'", CommandType.Text);
                donus = "Kişi Adresi Başarı ile Güncellenmiştir";
            }
            catch (Exception)
            {
                donus = "Kişi Adresi Güncellenirken Bir Hata Oluştu";
            }
            return donus;
        }

        

        
    }
}