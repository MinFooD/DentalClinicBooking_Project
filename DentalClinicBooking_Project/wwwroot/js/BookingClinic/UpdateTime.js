$(document).ready(function () {
    var basic = null;
    var dateString = null;
    var dateOnlyValue = null;
    var clinicId = $('.clinic-info').data('idclinic');

    $('.branch-item').click(function () {
        basic = $(this).data('idbasic');
    });
    $('.dates-grid').click(function () {
        dateString = $('.info-item:nth-child(4) .info-value').text();
        dateOnlyValue = moment(dateString, 'DD/MM/YYYY').format('YYYY-MM-DD');
        var url = $(this).data('url');
        var data = {
            basicId: basic,
            clinicId: clinicId,
            date: dateOnlyValue
        };
        $.ajax({
            url: url,
            type: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('.time-slot-item').each(function () {
                    var slotId = $(this).data('slot-id');
                    var pastDateTimeString = data[slotId].dateTime;

                    var parts = pastDateTimeString.split('-');
                    var timeParts = parts[0].split(':');
                    var year = parseInt(parts[1], 10);
                    var month = parseInt(parts[2], 10) - 1; // Tháng bắt đầu từ 0 trong JavaScript
                    var day = parseInt(parts[3], 10);

                    // Tạo đối tượng Date từ chuỗi
                    var pastDateTime = new Date(year, month, day, timeParts[0], timeParts[1]);

                    // Lấy thời gian hiện tại
                    var currentDate = new Date();

                    // Tạo một đối tượng Date chỉ chứa ngày hiện tại (không có giờ)
                    var currentDayOnly = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate());

                    if (pastDateTime.getFullYear() === currentDayOnly.getFullYear() &&
                        pastDateTime.getMonth() === currentDayOnly.getMonth() &&
                        pastDateTime.getDate() === currentDayOnly.getDate()) {
                        // Cùng ngày
                        if (pastDateTime <= currentDate || data[slotId].count >= 2) {
                            $(this).addClass('disabled');
                        } else {
                            $(this).removeClass('disabled');
                        }
                    } else if (data[slotId].count >= 2) {
                        // Khác ngày
                        $(this).addClass('disabled');
                    }
                    else {
                        $(this).removeClass('disabled');
                    }
                });
            }
        });
    });
});