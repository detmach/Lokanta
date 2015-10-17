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

    }
}