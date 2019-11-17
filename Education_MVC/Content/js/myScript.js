function Pay(MaLH) {
    var link = "/GiaSuOnline/XacNhanThanhToan/" + MaLH;
    $.get(link, function (data, status) {
        if (data.result != null) {
            alert(data.result);
        }
        else {
            alert("Thanh toán thành công");
            location.replace("http://localhost:57768/GiaSuOnline/Chat/" + data.hash);
        }    
    })
}