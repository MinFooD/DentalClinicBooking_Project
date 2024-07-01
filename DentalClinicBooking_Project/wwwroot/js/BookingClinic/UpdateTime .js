$(document).ready(function () {
    var clinicId = null;
    var clinicName = null;
    var basic = null;

    $('.branch-item').click(function () {
        clinicId = $(this).data('clinic-id').ToString();
        clinicName = $(this).data('clinic-name');
        basic = $('.info-item:nth-child(2) .info-value').text();
        console.log('Id: \nName: ', clinicId, clinicName)
    });

    $('.dates-grid').click(function () {
        var url = $(this).data('url');
        var dateString = $('.info-item:nth-child(4) .info-value').text();
        var dateOnlyValue = moment(dateString, 'DD/MM/YYYY').format('YYYY-MM-DD');
        var patientData = {
            clinicId: clinicId,
            BasicName: basic,
            ClinicName: clinicName,
            Day: dateOnlyValue
        };

        $.ajax({
            url: url,
            type: 'POST',
            data: JSON.stringify(patientData),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('.time-slot-item').each(function () {
                    var slotId = $(this).data('slot-id');
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