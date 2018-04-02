

(function () {

    $(function () {
        var timeSpanCollection = {};
                

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
                timeSpanCollection = data.result;

                $.each(data.result, function (i) {
                    $('#timespan-container').append("<option value=" + data.result[i].id + ">" + data.result[i].text + "</option>");
                });
                $('#timespan-container').selectpicker('refresh');
            },

            error: function (data) {
                console.log('data: ' + data);
            }
        });

        $('#btnTake').click(function () {

            var dto = {};
            dto.id = $("#timespan-container option:selected").val();
            dto.ntId = getParameterByName('ntid');

            //validation for take fast token which should be later than now
            var now = new Date();
            var selectedTimeSpan = getTimeSpanById(timeSpanCollection,dto.id);

            var startTime = new Date(Date.parse(selectedTimeSpan.startTime));
            var endTime = new Date(selectedTimeSpan.endTime);

            if(now < startTime || now > endTime){
               abp.message.warn('领取失败，不可以申领已过期的快速令牌。');
               return;
            }

            var selectText = $("#timespan-container option:selected").text();

            $.ajax({
                url: "../api/services/app/queueSystemService/TakeFastToken",
                // 数据发送方式
                type: "POST",
                // 接受数据格式
                dataType: "json",
                // 要传递的数据
                data: dto,
                // 回调函数，接受服务器端返回给客户端的值，即result值
                success: function (data) {
                    if (data.result.success == false) {
                        abp.message.warn(data.result.message);
                    }
                    else {
                        abp.message.success('领取成功，快速通行令牌：' + selectText + '，在此时间段将享有优先访问。');
                    }
                    console.log('data received: ' + data);
                },

                error: function (data) {
                    console.log('data: ' + data);
                }
            });

        });



        var queueHistoryHub = $.connection.queueHistoryHub; // Get a reference to the hub

        queueHistoryHub.client.getMessage = function (message) { // Register for incoming messages
            console.log('received message: ' + message);
        };

        queueHistoryHub.client.setQueueInformation = function (uersCountBeforeMe,predictedMinutes) { // Register for incoming messages
            $('#UersCountBeforeMe').text(uersCountBeforeMe);
            $('#PredictedMinutes').text(predictedMinutes);
        };

        abp.event.on('abp.signalr.connected', function () { // Register to connect event
            var ntid = getParameterByName('ntid');
            queueHistoryHub.server.queue(ntid); // Send a message to the server
        });



        function StartRefreshQueueInformation() {
            var connectionId = $.connection.hub.id;
            var dto = {};
            dto.connectionId = connectionId;

            $.ajax({
                url: "../api/services/app/queueSystemService/GetQueueInfomation",
                // 数据发送方式
                type: "POST",
                // 接受数据格式
                dataType: "json",
                // 要传递的数据
                data: dto,
                // 回调函数，接受服务器端返回给客户端的值，即result值
                success: function (data) {
                    if (data.result.redirectable) {
                        var returnUrl = getParameterByName("returnUrl");
                        window.location.href = returnUrl;
                    }
                    else {
                        $('#UersCountBeforeMe').text(data.result.usersCountBeforeMe);
                        $('#PredictedMinutes').text(data.result.predictedMinutes);
                    }
                },

                error: function (data) {
                    console.log('data: ' + data);
                }
            });
            setTimeout(StartRefreshQueueInformation, 5000);
        }

        StartRefreshQueueInformation();

        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }

        function getTimeSpanById(timeSpanCollection,id) {
            var timeSpan = {};
            for (var i=0;i<timeSpanCollection.length;i++)
            {
               if(timeSpanCollection[i].id == id)
               {
                  timeSpan = timeSpanCollection[i];
               }
            }
            return timeSpan;
        }

    });


})();