<%@ Page Title="" Language="C#" MasterPageFile="~/ms.Master" AutoEventWireup="true" CodeBehind="urunler.aspx.cs" Inherits="Html5.urunler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="sc/jquery.filter_input.js"></script>
    <script src="sc/urunler.js"></script>
    <style>
        .ui-field-contain {
            border-bottom: 0 solid rgba(0, 0, 0, 0.15);
        }

        .ui-input-text input, .ui-input-search input {
            background: transparent none repeat scroll 0 0;
            border: 0 none;
            border-radius: inherit;
            margin: 0;
            min-height: 2.2em;
            /*text-align: center;*/
        }

        .ui-listview, .ui-listview > li {
            list-style: outside none none;
            margin: 0;
            padding: 5px;
        }

        body, input, select, textarea, button, .ui-btn {
            font-family: sans-serif;
            font-size: 0.9em;
            line-height: 1;
        }
    </style>
    <link href="Content/urunler.css" rel="stylesheet" />

    <script>
        $(document).live("pageinit", "#main", function () {
            Urunler.init();
            $("#fiyat").filter_input({ regex: '[0-9,]', live: true });
            $('#kategori').filter_input({ regex: '[a-z A-Z_ğüşıöçĞÜŞİÖÇ]' });
            $('#urun').filter_input({ regex: '[a-z A-Z_ğüşıöçĞÜŞİÖÇ]' });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <fieldset data-role="main" class="ui-grid-a">

        <div class="ui-block-a" style="padding: 0.4em 1em">
            <input type="text" id="kategori" data-mini="true" placeholder="Kategori Adı" data-inputmask-regex="[a-zA-Z]" />
            <a href="#" id="kaekle" class="ui-btn ui-shadow ui-corner-all">Ekle</a>
            <span style="color: darkred; font-weight: bold" id="kahata"></span>
            <br />
            <ul data-role="listview" data-inset="true" id="mlist">
            </ul>
            <div data-role="popup" id="SilKategori" data-dismissible="true">
                <div data-role="header" data-theme="a">
                    <h1>Dikkat</h1>
                </div>
                <div role="main" class="ui-content">
                    <h3 class="ui-title">Bu Kategoriyi Silmek İstiyormusunuz?</h3>
                    <a href="#" class="ui-btn ui-corner-all ui-shadow ui-btn-inline ui-btn-a" data-rel="back">Hayır</a>
                    <a href="#" id="kasil" class="ui-btn ui-corner-all ui-shadow ui-btn-inline ui-btn-a" data-transition="flow">Evet</a>
                </div>
            </div>
        </div>
        <div class="ui-block-b" style="padding: 0.4em 1em">

            <input type="text" id="urun" data-mini="true" placeholder="Ürün Adı" />
            <input type="text" id="fiyat" data-mini="true" placeholder="Fiyatı" />
            <br />
            <br />
            <br />
            <a href="#" id="urekle" class="ui-btn ui-shadow ui-corner-all">Ekle</a>
            <span style="color: darkred; font-weight: bold" id="urhata"></span>
            <br />
            <ul data-role="listview" data-inset="true" id="menuList">
            </ul>
            <div data-role="popup" id="silUrun" data-dismissible="true">
                <div data-role="header" data-theme="a">
                    <h1>Dikkat</h1>
                </div>
                <div role="main" class="ui-content">
                    <h5 class="ui-title">Bu Ürünü Silmek İstiyormusunuz?</h5>
                    <a href="#" class="ui-btn ui-corner-all ui-shadow ui-btn-inline ui-btn-a ui-mini" data-rel="back">Hayır</a>
                    <a href="#" id="ursil" class="ui-btn ui-corner-all ui-shadow ui-btn-inline ui-btn-a" data-transition="flow">Evet</a>
                </div>
            </div>
        </div>



    </fieldset>
    <hr />
    <h2 style="text-align: center">Günün Menüsünü Ayarla</h2>

    <fieldset data-role="main" class="ui-grid-a">
        <div class="ui-block-a" style="padding: 0.4em 1em">
            <fieldset data-role="controlgroup" id="gmenut">
                <%--<legend>Checkboxes, vertical controlgroup:</legend>
                <input type="checkbox" name="checkbox-1a" id="checkbox-1a" checked="">
                <label for="checkbox-1a">Cheetos</label>
                <input type="checkbox" name="checkbox-2a" id="checkbox-2a">
                <label for="checkbox-2a">Doritos</label>
                <input type="checkbox" name="checkbox-3a" id="checkbox-3a">
                <label for="checkbox-3a">Fritos</label>
                <input type="checkbox" name="checkbox-4a" id="checkbox-4a">
                <label for="checkbox-4a">Sun Chips</label>--%>
            </fieldset>
        </div>
        <div class="ui-block-b" style="padding: 0.4em 1em">
            <fieldset data-role="controlgroup" id="gmenup">
                sada
                <%--<legend>Checkboxes, vertical controlgroup:</legend>
                <input type="checkbox" name="checkbox-1ab" id="checkbox-1ab" checked="">
                <label for="checkbox-1ab">Cheetos</label>
                <input type="checkbox" name="checkbox-2ab" id="checkbox-2ab">
                <label for="checkbox-2ab">Doritos</label>
                <input type="checkbox" name="checkbox-3ab" id="checkbox-3ab">
                <label for="checkbox-3ab">Fritos</label>
                <input type="checkbox" name="checkbox-4ab" id="checkbox-4ab">
                <label for="checkbox-4ab">Sun Chips</label>--%>
            </fieldset>
        </div>
    </fieldset>
</asp:Content>

