<%@ Page Title="" Language="C#" MasterPageFile="~/ms.Master" AutoEventWireup="true" CodeBehind="musteri.aspx.cs" Inherits="Html5.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ui-input-text input, .ui-input-search input {
            background: transparent none repeat scroll 0 0;
            border: 0 none;
            border-radius: inherit;
            margin: -12px;
            min-height: 2.2em;
            /*text-align: center;*/

        }
        #map-page, #map-canvas { width: 100%; height: 100%; padding: 0; }
      }

    </style>
     <script async defer
      src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCHNo7g4HVbnu0Dq7UpbTtSQ524HH8LElE&callback=initMap">
    </script>
    <script src="sc/musteriler.js"></script>
    <script src="sc/jquery.filter_input.js"></script>
    

    <script>
        $(document).live("pageinit", "#musteri", function () {
            Musteriler.init();
            $("#adi").filter_input({ regex: '[a-z A-Z_ğüşıöçĞÜŞİÖÇ]', live: true });
            $("#sadi").filter_input({ regex: '[a-z A-Z_ğüşıöçĞÜŞİÖÇ]', live: true });
            $("#tel").filter_input({ regex: '[0-9]', live: true });
            $("#eposta").filter_input({ regex: '[a-z0-9_\.\-@]', live: true });
            var koordinat;
            
        });
        function initMap() {           
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="musteri">
        <div data-role="popup" id="map-page" data-dismissible="true" style="width:500px;height:500px">
                <div data-role="header" data-theme="a">
                    <a href="#" data-rel="back" class="ui-btn ui-corner-all ui-shadow ui-btn-a ui-icon-delete ui-btn-icon-notext ui-btn-right">Kapat</a>
                    <h1 id="harita"></h1>
                </div>
                <div role="main" id="map-canvas" class="ui-content">
                    <a href="#" class="ui-btn ui-mini ui-corner-all ui-shadow ui-btn-inline ui-btn-a" data-rel="back">Hayır</a>
                </div>
            </div>
        <div data-role="popup" id="KisiSil" data-dismissible="true">
                <div data-role="header" data-theme="a">
                    <h1 id="silB"></h1>
                </div>
                <div role="main" class="ui-content">
                    <h5>Bu Müşteriyi Silmek İstiyormusunuz?</h5>
                    <a href="#" class="ui-btn ui-mini ui-corner-all ui-shadow ui-btn-inline ui-btn-a" data-rel="back">Hayır</a>
                    <a href="#" id="silkisi" class="ui-btn ui-mini ui-corner-all ui-shadow ui-btn-inline ui-btn-a" data-transition="flow">Evet</a>
                </div>
            </div>
<div data-role="controlgroup" data-type="horizontal">
    <input id="adi" data-wrapper-class="controlgroup-textinput ui-btn" placeholder="Adı" type="text"/>
    <input id="sadi" data-wrapper-class="controlgroup-textinput ui-btn" placeholder="Soyadı" type="text"/>
    <input id="tel" data-wrapper-class="controlgroup-textinput ui-btn" placeholder="Telefon" type="text"/>
    <input id="eposta" data-wrapper-class="controlgroup-textinput ui-btn" placeholder="E-Mail" type="text"/>
    <a id="kayit" href="#" class="ui-btn ui-mini" data-mini="true">Kaydet</a>
    <a id="vazgec" href="#" class="ui-btn ui-mini ui-hidden-accessible" data-mini="true">Vazgeç</a>
    <a id="guncelle" href="#" class="ui-btn ui-mini ui-hidden-accessible" data-mini="true">Güncelle</a>
    <br />
    <span style="color: darkred; font-weight: bold" id="hata"></span>
</div>
        <div data-role="controlgroup" data-type="horizontal">
    <textarea cols="44" rows="20" name="textarea"  id="adres" placeholder="Adres"></textarea>
            
    
</div>
        <div data-role="controlgroup" data-type="horizontal">
            <a href="#" id="sil" class="ui-btn ui-mini ui-hidden-accessible">Sil</a>
            <a href="#" id="duzenle" class="ui-btn ui-mini ui-hidden-accessible">Düzenle</a>
            <a href="#" id="yerkaydet" class="ui-btn ui-mini ui-hidden-accessible">Haritadaki Yerini Kaydet</a>
            <a href="#" id="yergoster" class="ui-btn ui-mini ui-hidden-accessible">Haritadaki Yerini Göster</a>
            <a href="#" id="paketSiparis" class="ui-btn ui-mini ui-hidden-accessible">Paket Sipariş</a>



        </div>
        <div>
            <table data-role="table" data-mode="reflow" id="musteritablo"  class="ui-responsive table-stroke ui-table ui-table-reflow">
  <thead>
    <tr >
      <th>ADI</th>
      <th>SOYADI</th>
      <th data-priority="1">Adres</th>
      <th data-priority="2">Telefon</th>
      <th data-priority="3">Email</th>
    </tr>
  </thead>
  <tbody id ="mtablo">

  </tbody>
</table>
        </div>
        
    </div>
    
</asp:Content>
