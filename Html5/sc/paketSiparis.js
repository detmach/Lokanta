var paketSiparis = function () {

    var id = "yok";
    var ucr;
    var indirimtutar = "";
    var aciklama = "";
    var genel = function () {
        $.ajax({
            type: "POST",
            url: "siparis.aspx/Menuler",
            dataType: "json",
            async: true,
            contentType: "application/json; charset=UTF-8",
            success: function (msg) {
                $("#menu").html(msg.d);
                $("#mlist").listview('refresh');
                $("#menu a").on('click', function () {
                    var a = $(this).attr('id');
                    menuliste(a);
                });
            },
            error: function () {
            }
        });
        $("#itutar").on('input', function () {
            ucrcontrol();
        })
        var sipars = function () {
            $('#siparis li a').on('click', function () {
                if ($(this).find('span').attr('id') == "adt") {
                    $(this).parents('li').remove();
                    ucrcontrol();
                }
            })
        }
        var urunler = function () {
            $("#mlist a").on('click', function () {
                var tl;
                var a = $(this).attr('id');
                var b = $(this).text().replace(/\d+/g, '').replace(",", '').replace("₺", "");
                var c = $("#adet").val();
                var d = $(this).text().replace(/[A-Z a-zĞÜŞİÖÇğüşıçö-]+/g, "").replace("₺", "").replace(",", ".");
                if (c != "0" && c != undefined && c != "") {
                    var slist = $('#siparis').html();
                    slist += "<li><a class='ui-btn ui-mini' ucr='" + d + "' href='#' id='" + a + "'>" + b + "<span id='adt' class='ui-li-count'>" + c + "</span></a></li>"
                    $('#siparis').html(slist);
                    $("#siparis").listview('refresh');
                    sipars();
                    $("#adet").val("1");
                    ucrcontrol();
                }

            })
        }
        var ucrcontrol = function () {
            var sayi;
            var ucr;
            var aaa = 0.00;
            var eble = 0.00;
            $.each($('#siparis a'), function () {
                ucr = $(this).attr('ucr');
                sayi = $(this).text().replace(/[A-Z a-zĞÜŞİÖÇğüşıçö-]+/g, "");
                if (typeof $(this).attr('ucr') != "" && typeof $(this).attr('ucr') != "undefined" && typeof $(this).text() != "undefined" && typeof $(this).text() != "") {
                    ucr = ucr.replace(",", ".");
                    eble = parseFloat(ucr).toFixed(2) * parseFloat(sayi).toFixed(2);
                    aaa = aaa + eble;
                }
            });
            indirimtutar = $("#itutar").val();
            var son = aaa.toFixed(2) - parseFloat(indirimtutar);
            $("#ucret").html("Tutar : " + son.toFixed(2) + " ₺");
            $("#ucrets").html("Tutar : " + son.toFixed(2) + " ₺");
        }
        var btn = function () {
            $("#up").on('click', function () {
                $("#adet").val(parseInt($("#adet").val()) + 1);
            })
            $("#down").on('click', function () {
                $("#adet").val(parseInt($("#adet").val()) - 1);
            })
            
        }
        var menuliste = function (id) {
            //alert(id);
            $.ajax({
                type: "POST",
                url: "siparis.aspx/menuList",
                dataType: "json",
                data: JSON.stringify({
                    "id": id
                }),
                async: true,
                contentType: "application/json; charset=UTF-8",
                success: function (msg) {
                    $("#mlist").html(msg.d);
                    $("#mlist").listview('refresh');
                    urunler();

                },
                error: function () {
                }
            });
        }
        menuliste(id);

        
    }
    var hesapkapa = function () {
        $('#hesapkapat').on('click', function () {
            aciklama = $('#aciklama').val();
            alert(aciklama);
            debugger;
            var c = "";
            $.each($('#siparis a'), function () {
                var a = $(this);
                if ($(this).attr('id') != undefined) {
                    c += $(this).attr('id');
                    c += '-';
                }
                if ($(a).find('span').attr('id') == 'adt') {
                    c += $(a).find('span').html();
                    c += '-';
                }
            })
            if (c != '') {
                $.ajax({
                    type: "POST",
                    url: "paketsiparis.aspx/SiparisAl",
                    dataType: "json",
                    data: JSON.stringify({
                        "veri": c,
                        "aciklama": aciklama
                    }),
                    async: false,
                    contentType: "application/json; charset=UTF-8",
                    success: function (msg) {
                        if (msg.d == 'OK') {
                            var odemetipi = "";
                            $("input[type='radio']").bind("change", function (event, ui) {
                                odemetipi = $(this).attr('id');
                            });
                            if (odemetipi == "") {
                                odemetipi = "1";
                            }
                            $('input[name=rd]:checked').val()
                            indirimtutar = $("#itutar").val();
                            if (indirimtutar != "") {
                                $.ajax({
                                    type: "POST",
                                    url: "paketsiparis.aspx/OdemeYap",
                                    dataType: "json",
                                    data: JSON.stringify({
                                        "odemeturu": odemetipi,
                                        "indirim": indirimtutar
                                    }),
                                    async: false,
                                    contentType: "application/json; charset=UTF-8",
                                    success: function (msg) {
                                        if (msg.d == 'OK') {
                                            //$(location).attr('href', 'anasayfa.aspx');
                                        }
                                        else {

                                        }
                                    },
                                    error: function () {
                                    }
                                });
                            }

                        }
                        else {

                        }
                    },
                    error: function () {
                    }
                });
            }
            console.log(c);
        })
    }

    return {
        //main function to initiate the module
        init: function () {
            genel();
            hesapkapa();
        }

    };

}();