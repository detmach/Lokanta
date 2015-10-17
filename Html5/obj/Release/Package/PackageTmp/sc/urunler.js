var Urunler = function () {
    
    var kaid = "";
    var urid = "";
    var id = "yok";
    var durum = "";
    var genel = function () {
        kategoriEkle();
        menuListe(id);
        $("#kaekle").click(function () {
            var kategori = $("#kategori").val().toUpperCase();
            $("#kategori").val("");
            if (kategori != "" && kategori != undefined) {
                $.ajax({
                    type: "POST",
                    url: "urunler.aspx/kategoriEkle",
                    dataType: "json",
                    data: JSON.stringify({
                        "kategoriAdi": kategori
                    }),
                    async: true,
                    contentType: "application/json; charset=UTF-8",
                    success: function (response) {
                        if (response.d == "OK") {
                            $("#kahata").html("Kayıt Başarılı");
                            kategoriEkle();
                        }
                        else {
                            $("#kahata").html("Hata Oluştu");
                        }
                    },
                    error: function () {
                        $("#kahata").html("Hata Oluştu");
                    }
                });
            }
            else {
                $("#kahata").html("Ürün Adı Boş Geçilemez");
            }

        })
        $("#kasil").click(function () {
            if (kaid != "") {
                $.ajax({
                    type: "POST",
                    url: "urunler.aspx/kategoriSil",
                    dataType: "json",
                    data: JSON.stringify({
                        "kategoriId": kaid
                    }),
                    async: true,
                    contentType: "application/json; charset=UTF-8",
                    success: function (response) {
                        if (response.d == "OK") {
                            kategoriEkle();
                            $("#SilKategori").popup("close");
                            $("#kahata").html("Kayıt Başarı İle Silindi");
                            //$(location).attr('href', 'urunler.aspx');
                        }
                        else {
                            $("#kahata").html("Hata Oluştu");
                        }
                    },
                    error: function () {
                        //$("#kahata").html("Hata Oluştu");
                    }
                });
            }
        })
        $("#ursil").click(function () {
            if (urid != "") {
                $.ajax({
                    type: "POST",
                    url: "urunler.aspx/urunSil",
                    dataType: "json",
                    data: JSON.stringify({
                        "urunId": urid
                    }),
                    async: true,
                    contentType: "application/json; charset=UTF-8",
                    success: function (response) {
                        if (response.d == "OK") {
                            menuListe(id);
                            $("#silUrun").popup("close");
                            $("#urhata").html("Kayıt Başarı İle Silindi");
                            //$(location).attr('href', 'urunler.aspx');
                        }
                        else {
                            $("#urhata").html("Hata Oluştu");
                        }
                    },
                    error: function () {
                        $("#urhata").html("Hata Oluştu");
                    }
                });
            }
        })
        $("#urekle").click(function () {
            var urun = $("#urun").val().toUpperCase()
            var fiyat = $("#fiyat").val()
            if (kaid == "") {
                $("#urhata").html("Önce Kategori Seçiniz");
            }
            else if (urun == "") {
                $("#urhata").html("Ürün Adı Boş Geçilemez");
            }
            else if (fiyat == "") {
                $("#urhata").html("Ürün Fiyatı Boş Geçilemez");
            }
            else {
                $("#urun").val("");
                $("#fiyat").val("");
                $("#urhata").html("");
                urunEkle(urun, fiyat);
            }
        })
    }
    var kategoriEkle = function () {
        $.ajax({
            type: "POST",
            url: "urunler.aspx/Menuler",
            //async: true,
            contentType: "application/json; charset=UTF-8",
            dataType: "json",
            success: function (response) {
                var veri = "";
                var veris = JSON.parse(response.d);
                $.each(veris, function (index, val) {
                    veri += "<li><a id='" + val.ID + "' href='#' class='ui-btn '>" + val.KATEGORIADI + "<span data-rel='popup' data-position-to='window' class='silKategori ui-icon-delete ui-btn-icon-right'></span></a></li>"
                });
                $("#mlist").html(veri);
                $("#mlist").listview('refresh');
                btn();
            },
            error: function () {
            }

        });
    }
    var menuListe = function (a) {
        $.ajax({
            type: "POST",
            url: "urunler.aspx/menuList",
            //async: true,
            contentType: "application/json; charset=UTF-8",
            dataType: "json",
            data: JSON.stringify({
                "id": a
            }),
            success: function (response) {
                var artir = 0;
                var veri = "", veri2 ="", veri3 = "";
                var veris = JSON.parse(response.d);
                console.log(veris);
                $.each(veris, function (index, val) {
                    veri += "<li><a id='" + val.ID + "' href='#' class='ui-btn '>" + val.URUNADI + "<span data-rel='popup' data-position-to='window' class='silUrun ui-icon-delete ui-btn-icon-right'></span><span class='ui-li-count ui-body-inherit' id='fyt'>" + val.FIYAT + " TL</span></a></li>"
                    if (val.DURUM == 0) {
                        veri2 += "<input type='checkbox' name='" + val.ID + "' id='" + val.ID + "'>" +
                            "<label for='" + val.ID + "'>" + val.URUNADI + "</label>";
                    }
                    if (val.DURUM == 1) {
                        veri3 += "<input type='checkbox' name='" + val.ID + "' id='" + val.ID +"' checked=''>" +
                            "<label for='" + val.ID + "'>" + val.URUNADI + "</label>";
                    }
                    console.log(val.DURUM)
                });
                
                //gmenut
                $("#menuList").html(veri);
                $("#gmenut").html(veri2);
                $("#gmenut").trigger("create");
                $("#gmenup").html(veri3);
                $("#gmenup").trigger("create");
                $("#menuList").listview('refresh');
                btn();
            },
            error: function () {
            }

        });

    }
    var urunEkle = function (a,b) {
        $.ajax({
            type: "POST",
            url: "urunler.aspx/urunEkle",
            //async: true,
            contentType: "application/json; charset=UTF-8",
            dataType: "json",
            data: JSON.stringify({
                "kategoriId": kaid,
                "urunAdi": a,
                "fiyat": b
            }),
            success: function (response) {
                menuListe(id);
            },
            error: function () {
            }

        });

    }
    var durumGuncelle = function (a, b) {
        $.ajax({
            type: "POST",
            url: "urunler.aspx/gunMenusu",
            //async: true,
            contentType: "application/json; charset=UTF-8",
            dataType: "json",
            data: JSON.stringify({
                "urunId": a,
                "durum": b,
            }),
            success: function (response) {
                menuListe(id);
            },
            error: function () {
            }

        });

    }
    var btn = function () {
        $("li .silKategori").click(function (ev) {
            var c = $(this).parent().html();
            kaid = $(this).parent().attr('id');
            $("#SilKategori").popup("open");
        });
        $("li .silUrun").click(function (ev) {
            var c = $(this).parent().html();
            urid = $(this).parent().attr('id');
            $("#silUrun").popup("open");
        });
        $("#mlist li a").click(function () {
            kaid = $(this).attr('id');
            var c = $("#mlist li");
            if ($("#mlist li a").hasClass("ui-btn-active")) {
                $("#mlist li a").removeClass("ui-btn-active");
            }
            $(this).addClass("ui-btn-active");
        })
        //$("#gmenut").check('change')
        $('#gmenut input[type=checkbox]').click(
            function () {
                $('#gmenut input[type=checkbox]').each(
            function () {
                if ($(this).is(':checked')) {
                    durum = "1";
                    urid = $(this).attr('id');
                    durumGuncelle(urid, durum);
                }
            }
        );
                // Call a function onclick of the radio button
            }
        );
        $('#gmenup input[type=checkbox]').click(
            function () {
                $('#gmenup input[type=checkbox]').each(
            function () {
                if ($(this).is(':checked')) { }
                else {
                    durum = "0";
                    urid = $(this).attr('id');
                    durumGuncelle(urid, durum);
                }
            }
        );
                // Call a function onclick of the radio button
            }
        );
    }
    return {
        //main function to initiate the module
        init: function () {
            genel();
        }

    };

}();