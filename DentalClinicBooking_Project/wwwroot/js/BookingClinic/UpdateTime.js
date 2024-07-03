$(document).ready(function () {
    var branch = $('.dates-grid');
    var clinicName = branch.data('clinic-name');
    var basic = null;

    $('.branch-item').click(function () {
        basic = $('.info-item:nth-child(2) .info-value').text();
    });

    $('.dates-grid').click(function () {
        var dateString = $('.info-item:nth-child(4) .info-value').text();
        var dateOnlyValue = moment(dateString, 'DD/MM/YYYY').format('YYYY-MM-DD');
        var url = $(this).data('url');
        var data = {
            BasicName: basic,
            ClinicName: clinicName,
            Day: dateOnlyValue
        };
        $.ajax({
            url: url,
            type: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                console.log('Data: ', data);
                $('.time-slot-item').each(function () {
                    var slotId = $(this).data('slot-id');
                    console.log('Slot: ', slotId);
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