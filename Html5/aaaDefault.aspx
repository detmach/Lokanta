<%@ Page Title="" Language="C#" MasterPageFile="~/ms.Master" AutoEventWireup="true" CodeBehind="aaaDefault.aspx.cs" EnableEventValidation="true" Inherits="Html5.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="sc/giris.js"></script>
    <script>
        $(document).ready(function (event) {
            Default.init();
            
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
   
</asp:Content>
