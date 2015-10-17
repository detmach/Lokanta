<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Html5.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />  
    <link href="themes/Bootstrap.min.css" rel="stylesheet" />
    <link href="Content/jquery.mobile.structure-1.4.5.min.css" rel="stylesheet" />
    
    <link href="Content/jquery.mobile-1.4.5.min.css" rel="stylesheet" />
    <link href="Content/jquery.mobile.icons-1.4.5.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/jquery.mobile-1.4.5.min.js"></script>
    <script src="sc/giris.js"></script>
    <script>
        $(document).ready(function (event) {
            Default.init();

        });

    </script>
    
    <title></title>
</head>
<body>
     <div id="content" data-role="content">
        <div id="login" data-role="fieldcontain">
            <label for="select-choice-1" class="select">Kullanıcı Adı</label>
            <select name="select-choice-1" id="select-choice-1"  tabindex="0">
                <option value="standard">Lütfen Kullanıcı Seçiniz</option>
            </select>
            <label for="select-choice-1" class="select">Şifre</label>
            <input id="pass" name="select-choice-1" type="password"  tabindex="1"/>
            <label for="select-choice-1" class="select"></label>
            <div id="hata" style="text-align:center; color:darkred"></div>
        </div>
        <a href="#" id="giris" data-role="button" tabindex="2">Giriş Yap</a> 
    </div>
    
</body>
</html>
