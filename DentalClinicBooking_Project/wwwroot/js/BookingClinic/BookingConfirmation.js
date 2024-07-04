$(document).ready(function () {
    $('.confirm-button').click(function (event) {
        let isValid = true;
        $('.info-item .info-value, .info-itemadd .info-valueadd').each(function () {
            if ($(this).text().trim() === '') {
                isValid = false;
                return false;
            }
        });

        if (!isValid) {
            Swal.fire({
                icon: 'warning',
                title: 'Còn bước chưa hoàn thành',
                html: '<ul><li><span style="font-weight: 600;">Bước 1: Chọn cơ sở</span></li><li><span style="font-weight: 600;">Bước 2: Chọn ngày khám</span></li><li><span style="font-weight: 600;">Bước 3: Chọn giờ</span></li><li><span style="font-weight: 600;">Bước 4: Chọn dịch vụ</span></li></ul>',
                customClass: {
                    confirmButton: 'd-none',
                    cancelButton: 'd-none'
                },
                showCloseButton: true,
                timer: 10000,
                timerProgressBar: true,
                allowOutsideClick: true,
                allowEscapeKey: false,
                width: '500px',
                heightAuto: true,
            });
            event.preventDefault();
        } else {
            var clinicName = $('.clinic-name').text();
            var basicName = $('.info-item:nth-child(2) .info-value').text();
            var address = $('.info-itemadd .info-valueadd').text();
            var dateString = $('.info-item:nth-child(4) .info-value').text();
            var dateOnlyValue = moment(dateString, 'DD/MM/YYYY').format('YYYY-MM-DD');
            var time = $('.info-item:nth-child(5) .info-value').text().trim().replace(/\s+/g, ' ');
            var service = $('.info-item:nth-child(6) .info-value').text();
            var url = $(this).data('url');
            const data = {
                ClinicName: clinicName,
                Date: dateOnlyValue,
                Address: address,
                BasicName: basicName,
                SlotName: time,
                Service: service
            };
            console.log('data: ', data);
            $.ajax({
                url: url,
                type: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    console.log('Success:', response);
                    if (response.success) {
                        window.location.href = response.redirectUrl;
                    }
                },
                error: function (xhr, status, error) {
                    console.error('Error:', error);
                }
            });
        }
    });
});