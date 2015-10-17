<%@ Page Title="" Language="C#" MasterPageFile="~/ms.Master" AutoEventWireup="true" CodeBehind="anasayfa.aspx.cs" Inherits="Html5.anasayfa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   
    <script src="sc/anasayfa.js"></script>
    <script>
        $(document).ready(function (event) {
            Anasayfa.init();

            //refreshPage();
        });
        function refreshPage() {
            jQuery.mobile.changePage(window.location.href, {
                allowSamePageTransition: true,
                transition: 'none',
                reloadPage: true

            });
        }
    </script>
     <link href='<%= ResolveUrl("~/Content/anasayfa.css") %>' rel="stylesheet" type="text/css" />
     <%--<link href="Content/anasayfa.css" rel="stylesheet" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="my-page">
      
    <div data-role="content" class="ui-content">
    <ul data-role="listview" data-inset="true" id="masalard">
        
        </ul></div></div>
</asp:Content>
