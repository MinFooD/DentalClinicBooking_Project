$(document).ready(function () {
    var clinicId = $('.clinic-info').data('idclinic');
    var basicId = null;
    var dateString = null;
    var dateOnlyValue = null;
    var slotId = null;
    var serviceId = null;
    $('.branch-item').click(function () {
        basicId = $(this).data('idbasic');
    });

    $('.dates-grid').click(function () {
        dateString = $('.info-item:nth-child(4) .info-value').text();
        dateOnlyValue = moment(dateString, 'DD/MM/YYYY').format('YYYY-MM-DD');
    });

    $('.time-slot-item').click(function () {
        slotId = $(this).data('slot-id');
        console.log(slotId);
    });

    $('.service-item').click(function () {
        serviceId = $(this).data('serviceid');
    });

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
            var url = $(this).data('url');
            const data = {
                ClinicId: clinicId,
                Date: dateOnlyValue,
                BasicId: basicId,
                SlotId: slotId,
                ServiceId: serviceId
            };
            //console.log(data);
            $.ajax({
                url: url,
                type: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    console.log(response.success);
                    if (response.success === true) {
                        window.location.href = response.redirectUrl;
                    } else {
                        Swal.fire({
                            title: 'Thông báo',
                            html: '<span style="font-weight: 450;">Bệnh nhân này đã đặt lịch trước đó, nếu bạn muốn đặt lịch khám mới, vui lòng huỷ lịch khám hiện tại và thử lại.</span>',
                            icon: 'warning',
                            customClass: {
                                confirmButton: 'd-none',  // Ẩn nút xác nhận
                                cancelButton: 'd-none'  // Ẩn nút hủy
                            },
                            showCloseButton: true,  // Hiển thị nút đóng
                            timer: 10000,  // Thời gian hiển thị thông báo (10000ms = 10 giây)
                            timerProgressBar: true,  // Hiển thị thanh tiến trình của bộ đếm thời gian
                            allowOutsideClick: true,  // Cho phép đóng thông báo khi nhấp ra ngoài
                            allowEscapeKey: false,  // Không cho phép đóng thông báo khi nhấn phím Esc
                            width: '500px',  // Chiều rộng của thông báo
                            heightAuto: true,  // Tự động điều chỉnh chiều cao dựa trên nội dung
                        });
                    }
                },
                error: function () {
                    console.log("An error occurred");
                }
            });
        }
    });
});