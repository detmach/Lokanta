<%@ Page Title="" Language="C#" MasterPageFile="~/ms.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="Html5.test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                <div class="ui-grid-b">
                    <ul data-role="listview" data-count-theme="b" data-inset="true">
    <li><a href="#">Inbox <span class="ui-li-count">12</span></a></li>
    <li><a href="#">Outbox <span class="ui-li-count">0</span></a></li>
    <li><a href="#">Drafts <span class="ui-li-count">4</span></a></li>
    <li><a href="#">Sent <span class="ui-li-count">328</span></a></li>
    <li><a href="#">Trash <span class="ui-li-count">62</span></a></li>
</ul>
                </div>
</asp:Content>
