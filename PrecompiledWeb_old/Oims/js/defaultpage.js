/// <reference path="jquery-1.11.2-vsdoc.js" />

$(document).ready(function () {
    setInterval(function () {

        // Get connection status
        getConnStatust();

        //---- Get posting mode
        getPostingMode();

        //---- Get posting mode
        getLastTenTxns();

    }, 5000);
});

getConnStatust = function () {
    
    $.ajax({
        type: "POST",
        url: "Default.aspx/GetConnStatus",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //----Clear div
            $("#switch_conn_stat").empty();
            $("#switch_conn_stat").html(data.d)
        },
        failure: function (response) {
            //----Clear div
            $("#switch_conn_stat").empty();
            alert(response.d);
        }
    });

}

getPostingMode = function () {
    
    $.ajax({
        type: "POST",
        url: "Default.aspx/GetPostingMode",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //----Clear div
            $("#switch_upload_mode").empty();
            $("#switch_upload_mode").html(data.d)
        },
        failure: function (response) {
            //----Clear div
            $("#switch_upload_mode").empty();
            alert(response.d);
        }
    });
}

getLastTenTxns = function () {
    
    $.ajax({
        type: "POST",
        url: "Default.aspx/GetTopTenTxns",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //----Clear div
            $("#switchtxn_holder").empty();
            $("#switchtxn_holder").html(data.d)
        },
        failure: function (response) {
            //----Clear div
            $("#switchtxn_holder").empty();
            alert(response.d);
        }
    });
}