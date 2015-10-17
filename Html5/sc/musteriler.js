var Musteriler = function () {
    var id =""
    var adi = "";
    var sadi = "";
    var tel = "";
    var eposta = "";
    var adres = "";
    var kr1 = "";
    var kr2 = "";
    var genel = function () {
        
        musteriGetir();        
        $("#kayit").on('click', function () {            
            adi = $("#adi").val();
            sadi = $("#sadi").val();
            tel = $("#tel").val();
            eposta = $("#eposta").val();
            adres = $("#adres").val();
            if (tel == "") {
                $("#hata").html("Telefon Alanı Boş Bırakılamaz");
            }
            else if (tel.length < 10) {
                $("#hata").html("Telefon Alanı 10 Haneden Az Olamaz");
            }
            else if (adi == "") {
                $("#hata").html("İsim Alanı Boş Bırakılamaz");
            }
            else if (sadi == "") {
                $("#hata").html("Soyisim Alanı Boş Bırakılamaz");
            }
            else {
                $("#hata").html("");
                $.ajax({
                    type: "POST",
                    url: "musteri.aspx/musteriEkle",
                    dataType: "json",
                    data: JSON.stringify({
                        "adi": adi,
                        "soyadi": sadi,
                        "telefon": tel,
                        "email": eposta,
                        "adres": adres
                    }),
                    async: true,
                    contentType: "application/json; charset=UTF-8",
                    success: function (response) {
                        $("#hata").html(response.d);
                        console.log(response);
                        musteriGetir();
                        
                    },
                    error: function () {

                    }
                });

            }
        })
        $("#silkisi").click(function () {
            $("#KisiSil").popup("close");
            $.ajax({
                type: "POST",
                url: "musteri.aspx/musteriSil",
                dataType: "json",
                data: JSON.stringify({
                    "id": id
                }),
                async: true,
                contentType: "application/json; charset=UTF-8",
                success: function (response) {
                    $("#hata").html(response.d);
                    musteriGetir();
                    gizlebtn();
                },
                error: function () {

                }
            });

        })
        $("#duzenle").click(function () {
            $("#adi").val(adi);
            $("#sadi").val(sadi);
            $("#tel").val(tel);
            $("#eposta").val(eposta);
            $("#adres").val(adres);
            $("#kayit").addClass("ui-hidden-accessible");
            $("#vazgec").removeClass("ui-hidden-accessible");
            $("#guncelle").removeClass("ui-hidden-accessible");
            gizlebtn();
        })
        $("#vazgec").click(function () {
            $("#kayit").removeClass("ui-hidden-accessible");
            $("#vazgec").addClass("ui-hidden-accessible");
            $("#guncelle").addClass("ui-hidden-accessible");
            $("#adi").val("");
            $("#sadi").val("");
            $("#tel").val("");
            $("#eposta").val("");
            $("#adres").val("");
            gizlebtn();
        })
        $("#guncelle").click(function () {
            adi = $("#adi").val();
            sadi = $("#sadi").val();
            tel = $("#tel").val();
            eposta = $("#eposta").val();
            adres = $("#adres").val();
            if (tel == "") {
                $("#hata").html("Telefon Alanı Boş Bırakılamaz");
            }
            else if (tel.length < 10) {
                $("#hata").html("Telefon Alanı 10 Haneden Az Olamaz");
            }
            else if (adi == "") {
                $("#hata").html("İsim Alanı Boş Bırakılamaz");
            }
            else if (sadi == "") {
                $("#hata").html("Soyisim Alanı Boş Bırakılamaz");
            }
            else {
                $("#hata").html("");
                $.ajax({
                    type: "POST",
                    url: "musteri.aspx/musteriGuncelle",
                    dataType: "json",
                    data: JSON.stringify({
                        "adi": adi,
                        "soyadi": sadi,
                        "telefon": tel,
                        "email": eposta,
                        "adres": adres,
                        "id": id
                    }),
                    async: true,
                    contentType: "application/json; charset=UTF-8",
                    success: function (response) {
                        $("#hata").html(response.d);
                        $("#kayit").removeClass("ui-hidden-accessible");
                        $("#vazgec").addClass("ui-hidden-accessible");
                        $("#guncelle").addClass("ui-hidden-accessible");
                        $("#adi").val("");
                        $("#sadi").val("");
                        $("#tel").val("");
                        $("#eposta").val("");
                        $("#adres").val("");
                        gizlebtn();
                        musteriGetir();

                    },
                    error: function () {

                    }
                });

            }
        })
        $("#yerkaydet").click(function () {
            alert("sadasd");
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    console.log(position)
                    var options = {
                        enableHighAccuracy: true,
                        timeout: 55000,
                        maximumAge: 0
                    };
                    var pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };
                    console.log(pos.lat +" - " + pos.lng)
                    $.ajax({
                        type: "POST",
                        url: "musteri.aspx/haritaGuncelle",
                        dataType: "json",
                        data: JSON.stringify({
                            "lat": pos.lat,
                            "lut": pos.lng,
                            "id":id
                        }),
                        async: true,
                        contentType: "application/json; charset=UTF-8",
                        success: function (response) {
                            $("#hata").html(response.d);
                            gizlebtn();
                            musteriGetir();

                        },
                        error: function () {

                        }
                    });
                })
            };
            
        })
        $("#yergoster").click(function () {
            
            var myLatLng = { lat: parseFloat(kr1), lng: parseFloat(kr2) };
            console.log(myLatLng)
            // Create a map object and specify the DOM element for display.
            var map = new google.maps.Map(document.getElementById('map-canvas'), {
                center: myLatLng,
                scrollwheel: true,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                zoom: 16
            });

            // Create a marker and set its position.
            var marker = new google.maps.Marker({
                map: map,
                position: myLatLng,
                title: adi + " " + sadi
            });
            $("#map-page").popup("open");

        })
        
    }
    var musteriGetir = function () {
                $.ajax({
                    type: "POST",
                    url: "musteri.aspx/musteriGetir",
                    dataType: "json",
                    //data: JSON.stringify({
                    //    "kategoriAdi": kategori
                    //}),
                    async: true,
                    contentType: "application/json; charset=UTF-8",
                    success: function (response) {
                        var veri = JSON.parse(response.d);
                        var veris;
                        $.each(veri, function (index, val) {
                            veris += "<tr id='" + val.ID + "' lat='"+val.LAT+"' lut='"+val.LUT+"'>" +
                                "<td>" + val.AD + "</td>" +
                                "<td>" + val.SOYAD + "</td>" +
                                "<td>" + val.ADRES + "</td>" +
                                "<td>" + val.TELEFON + "</td>";
                                //"<td>"+val.EMAIL+"</td></tr>";
                        });
                        $("#mtablo").html(veris)
                        $("#musteritablo").table('refresh');
                        btn();
                    },
                    error: function () {
                        
                    }
                });
    }
    var btn = function () {
        $("#mtablo tr").click(function (e) {
            id = $(this).attr('id');

            if (id != undefined) {
                $(this).closest("tr").siblings().removeClass("ui-focus");
                $(this).toggleClass("ui-focus");
                adi = $(this).find("td:nth-child(1)").text().slice(3);
                sadi = $(this).find("td:nth-child(2)").text().slice(6);
                adres = $(this).find("td:nth-child(3)").text().slice(5);
                tel = $(this).find("td:nth-child(4)").text().slice(7);
                eposta = $(this).find("td:nth-child(5)").text().slice(5);
                kr1 = $(this).attr('lat');
                kr2 = $(this).attr('lut');
                gosterbtn();
            }
            
            //class='ui-focus'
        })
        $("#sil").click(function () {
            //alert(id);
            $("#KisiSil").popup("open");
            $("#silB").html(adi + " " + sadi);
            
        })
    }
    var gosterbtn = function () {
        $("#sil").removeClass("ui-hidden-accessible");
        $("#duzenle").removeClass("ui-hidden-accessible");
        $("#yerkaydet").removeClass("ui-hidden-accessible");
        $("#yergoster").removeClass("ui-hidden-accessible");
    }
    var gizlebtn = function () {
        $("#sil").addClass("ui-hidden-accessible");
        $("#duzenle").addClass("ui-hidden-accessible");
        $("#yerkaydet").addClass("ui-hidden-accessible");
        $("#yergoster").addClass("ui-hidden-accessible");
    }
    
    
    return {
        //main function to initiate the module
        init: function () {
            genel();
        }

    };

}();