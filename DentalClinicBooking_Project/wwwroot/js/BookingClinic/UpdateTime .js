$(document).ready(function () {
    var clinicId = null;
    var clinicName = null;
    var basic = null;

    $('.branch-item').click(function () {
        clinicId = '@Model.ClinicId.ToString()';
        clinicName = '@Model.ClinicName';
        basic = $('.info-item:nth-child(2) .info-value').text();
    });

    $('.dates-grid').click(function () {
        var dateString = $('.info-item:nth-child(4) .info-value').text();
        var dateOnlyValue = moment(dateString, 'DD/MM/YYYY').format('YYYY-MM-DD');
        var patientData = {
            clinicId: clinicId,
            BasicName: basic,
            ClinicName: clinicName,
            Day: dateOnlyValue
        };
        console.log('Patient Data:', patientData); 

        $.ajax({
            url: '@Url.Action("CheckSlots", "BookClinic")',
            type: 'POST',
            data: JSON.stringify(patientData),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                 console.log(data);
                //$('#slot option').each(function () {
                //    var slot = $(this).val();
                //    if (data[slot] >= 2) { 
                //        $(this).prop('disabled', true);
                //    } else {
                //        $(this).prop('disabled', false);
                //    }
                //});
            }
        });
    });
});
