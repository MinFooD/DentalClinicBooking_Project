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
                    var currentDate = new Date();
                    console.log(slotId);
                    console.log(currentDate);
                    if (data[slotId] >= 2) {
                        $(this).addClass('disabled')
                    } else {
                        $(this).removeClass('disabled')
                    }
                });
            }
        });
    });
});