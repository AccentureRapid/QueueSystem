

(function () {

    $(function () {
        $.ajax({
            url: "../api/services/app/queueSystemService/GetTimeSpanCollection",
            // 数据发送方式
            type: "POST",
            // 接受数据格式
            dataType: "json",
            // 要传递的数据
            data: 'result',
            // 回调函数，接受服务器端返回给客户端的值，即result值
            success: function (data) {
                $.each(data.result, function (i) {
                    $('#timespan-container').append("<option value=" + data.result[i].id + ">" + data.result[i].text + "</option>");
                });
                $('#timespan-container').selectpicker('refresh');
            },

            error: function (data) {
                console.log('data: ' + data);
            }
        })

    });

})();