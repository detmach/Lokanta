var Siparis = function () {
   
    var id = "yok";
    var ucr;
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
        $.ajax({
            type: "POST",
            url: "siparis.aspx/UcretHesapla",
            dataType: "json",
            async: true,
            contentType: "application/json; charset=UTF-8",
            success: function (msg) {
                ucr = msg.d;
                $("#ucret").html("Tutar : " + msg.d);                
            },
            error: function () {
            }
        });
        var sipars = function () {
            $('#siparis li a').on('click', function () {
                if ($(this).find('span').attr('id')== "adt") {
                    $(this).parents('li').remove();
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
                    slist += "<li><a class='ui-btn ui-mini' ucr='"+d+"' href='#' id='" + a + "'>" + b + "<span id='adt' class='ui-li-count'>" + c + "</span></a></li>"
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
            var aaa = 0;
            var eble = 0;
            $.each($('#siparis a'), function () {
                sayi = parseInt($(this).attr('ucr'));
                ucr = parseFloat($(this).text().replace(/[A-Z a-zĞÜŞİÖÇğüşıçö-]+/g, "").replace("₺", "").replace(",", "."))
                if ($(this).attr('ucr') != 0 && $(this).attr('ucr') != undefined) {
                    console.log(ucr * sayi);
                    eble = ucr * sayi;
                    aaa += eble;
                }
                
            });
            console.log(aaa);
        }
        var btn = function () {
            $("#up").on('click', function () {
                $("#adet").val(parseInt($("#adet").val()) + 1);
            })
            $("#down").on('click', function () {
                $("#adet").val(parseInt($("#adet").val()) - 1);
            })            
            $('#siparisbtn').on('click', function () {
                var c = "";                
                $.each($('#siparis a'), function () {
                    var a = $(this);
                    if ($(this).attr('id') != undefined) {
                        c +=$(this).attr('id');
                        c +='-';  
                    }
                    if ($(a).find('span').attr('id') == 'adt') {
                        c += $(a).find('span').html();
                        c +='-';
                    }
                })
                if (c != '') {
                    $.ajax({
                        type: "POST",
                        url: "siparis.aspx/SiparisAl",
                        dataType: "json",
                        data: JSON.stringify({
                            "veri": c
                        }),
                        async: true,
                        contentType: "application/json; charset=UTF-8",
                        success: function (msg) {
                            if (msg.d == 'OK') {
                                $(location).attr('href', 'anasayfa.aspx');
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
            $("#flip-3").on('change', function (event, ui) {
                var a = $(this).val();
                if (a == "0") {
                    $('#itutar').css("display", "none")
                }
                else {
                    $('#itutar').css("display", "table")
                    $('#itutar').focus();
                }
            });   
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
        var siparislerigetir = function () {
            $.ajax({
                type: "POST",
                url: "siparis.aspx/Siparisler",
                dataType: "json",
                //data: JSON.stringify({
                //    "id": id
                //}),
                async: true,
                contentType: "application/json; charset=UTF-8",
                success: function (msg) {
                    $("#siparis").html(msg.d);
                    $("#siparis").listview('refresh');                    
                    sipars();
                    btn();
                },
                error: function () {
                    sipars();
                    btn();
                }
            });
        }
        siparislerigetir();
        menuliste(id);
        

    }
    var hspmenu = function () {
        var odemetipi = "";
        var indirimtutar = "";
        $("input[type='radio']").bind("change", function (event, ui) {
            odemetipi = $(this).attr('id');
        });
        $('#hesapkapat').on('click', function () {
            $("#popupDialog").find('h4').html("asdds");
            if (odemetipi == "") {
                odemetipi = "1";
            }
            $('input[name=rd]:checked').val()
            indirimtutar = $("#itutar").val();
            if (indirimtutar != "") {
                $.ajax({
                    type: "POST",
                    url: "siparis.aspx/OdemeYap",
                    dataType: "json",
                    data: JSON.stringify({
                        "odemeturu": odemetipi,
                        "indirim": indirimtutar
                    }),
                    async: true,
                    contentType: "application/json; charset=UTF-8",
                    success: function (msg) {
                        if (msg.d == 'OK') {
                            $(location).attr('href', 'anasayfa.aspx');
                        }
                        else {

                        }
                    },
                    error: function () {
                    }
                });
            }
            
        })
    }
    return {
        //main function to initiate the module
        init: function () {
            genel();
            hspmenu();
        }

    };

}();