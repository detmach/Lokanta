<%@ Page Title="" Language="C#" MasterPageFile="~/ms.Master" AutoEventWireup="true" CodeBehind="paketsiparis.aspx.cs" Inherits="Html5.paketsiparis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="Content/siparis.css" rel="stylesheet" />
    <script src="sc/paketSiparis.js"></script>
    <style>
        .ui-input-text {
            display: inline-block;
            /*width: 10%;*/
            text-align:inherit;
            vertical-align: inherit;
            float: left;
        }
        .ui-input-text input, .ui-input-search input {
  background: transparent none repeat scroll 0 0;
  border: 0 none;
  border-radius: inherit;
  margin: 0;
  min-height: 2.2em;
  text-align: center;
}
        .ui-listview, .ui-listview > li {
          list-style: outside none none;
          margin: 0;
          padding: 5px;
        }
    </style>
     <script>
        $(document).live("pageinit", "#main", function () {
            paketSiparis.init();            
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div data-role="main" class="rwd-example" id="main">

        <!-- Lead story block -->
        <div class="ui-block-a">
            <div class="ui-body ui-body-d" data-inline="true" id="menu">
            </div>


        </div>
        <!-- secondary story block #1 -->
        <div class="ui-block-b">
            <div class="ui-body ui-body-d">

                <h4>Menü Listesi</h4>
                <div class="ui-field-contain">
                    <div data-role="controlgroup" data-type="horizontal">
                        <input type="text" name="adet" id="adet" data-wrapper-class="controlgroup-textinput" value="1" />
                        <a href="#" class="ui-btn ui-mini ui-icon-arrow-u ui-btn-icon-notext" id="up">Button</a>
                        <a href="#" class="ui-btn ui-mini ui-icon-arrow-d ui-btn-icon-notext" id="down">Button</a>
                    </div>
                </div>
                <ul data-role="listview" data-inset="true" data-filter="true" data-filter-placeholder="Ürün Ara" id="mlist">
                </ul>

            </div>

        </div>
        <div class="ui-block-c">
            <div class="ui-body ui-body-d">
                <h4 for="itutar">İndirim Uygula</h4>
                <div class="ui-field-contain">
                    <input type="text" name="itutar" id="itutar" class="ui-btn-active" placeholder="İndirim Tutarı Giriniz" value="0" data-mini="true" /><br />
                    <br />
                </div>


                <h4>Siparişler</h4>
                <ul data-role="listview" data-inset="true" id="siparis">
                </ul>
            </div>
        </div>
    </div>
    <div data-role="footer" data-position="fixed">
        <div data-role="navbar">
            <ul>
               <%-- <li><a href="#" id="siparisbtn">Sipariş Al</a></li>--%>
                <li><a href="#popupDialog" id="hspkpt" data-rel="popup" data-position-to="window" data-transition="pop" class="ui-btn ui-corner-all ui-shadow ui-btn-inline  ">Hesabı Kapat</a></li>
                <li><a href="#" id="ucret" class="ui-btn-active">Ücret</a></li>
            </ul>
            <div data-role="popup" id="popupDialog" data-mini="true" data-rel="back" data-overlay-theme="a" data-dismissible="true" style="width: 250px; height: 420px">

                <div data-role="header" data-theme="a">
                    <h1>
                        <asp:Label ID="lbl" Text="" runat="server" /></h1>
                </div>
                <div role="main" class="ui-content">
                    <h4 id="ucrets">Toplam Ücret :</h4>
                    <div class="ui-block-a">
                        <fieldset data-role="controlgroup" id="rd" data-mini="true">
                            <legend></legend>
                            <input type="radio" id="1" name="radio1" checked="checked" />
                            <label for="1">Nakit</label>
                            <input type="radio" id="2" name="radio1" />
                            <label for="2">Kredi Kartı</label>
                            <input type="radio" id="3" name="radio1" />
                            <label for="3">Ticket</label>
                        </fieldset>
                        <a href="#" data-rel="back" class="ui-btn ui-btn-inline" style="font-size: 12px;">Vazgeç</a>
                        <a href="#" id="hesapkapat" class="ui-btn ui-btn-inline" style="font-size: 12px;">Hesabı Kapat</a>
                    </div>
                </div>

            </div>
            <!-- /navbar -->

        </div>
    </div>
</asp:Content>
