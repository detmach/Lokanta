var Default = function () {

    var genel = function () {
        $.ajax({
            type: "POST",
            url: "g.asmx/KullaniciGetir",
            dataType: "json",
            //data: JSON.stringify({
            //    "kadi": window.kadi
            //}),
            //async: true,
            contentType: "application/json; charset=UTF-8",
            success: function (msg) {
                $("#select-choice-1").html(msg.d);                
            },
            error: function () {
            }
        });

    }
    $("#select-choice-1").focus();
    var p, k;
    var btn = function () {
        $('#giris').on("click", function () {
            
            $("#hata").html("");
            p = $('#pass').val();
            k = $('#select-choice-1').val()
            $('#select-choice-1').on("change", function () {
                k = $('#select-choice-1').val();
                $("#pass").focus();
            });
            if (k == "") {
                $("#hata").html("Lütfen Kullanıcı Seçiniz");
            }
            else if (p == "") {
                $("#hata").html("Lütfen Şifre Giriniz");
            }            
            else {
                $.ajax({
                    type: "POST",
                    url: "g.asmx/GirisYap",
                    dataType: "json",
                    data: JSON.stringify({
                        "id": k,
                        "ps": p,
                    }),
                    contentType: "application/json; charset=UTF-8",
                    success: function (msg) {
                        if (msg.d == "hata") {
                            
                            $("#hata").html("Şifre Hatalı");
                        }
                        else {
                            $(location).attr('href', 'anasayfa.aspx');
                        }
                    },
                    error: function () {
                        $("#hata").html("Hata Oluştu");
                    }
                });
            }
            
        });
    }

    return {
        //main function to initiate the module
        init: function () {

            genel();
            btn();
        }

    };

}();