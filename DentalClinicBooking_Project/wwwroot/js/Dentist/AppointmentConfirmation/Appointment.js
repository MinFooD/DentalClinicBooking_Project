


$(document).ready(function () {
    // Khi nhấn nút xác nhận
    $('#btnConfirm').click(function (event) {
        // Ngăn hành động mặc định của nút xác nhận (gửi form)
        event.preventDefault();

        // Xử lý kiểm tra lỗi
        var hasError = false;
        var code = $('#inputCode').val().trim();

        if (code === "") {
            // Hiển thị thông báo lỗi
            $('#errorMessage').show();
            $('#appointmentInfo').hide();
            $('#buttonGroup').hide();
            $('#errorMessageViewBag').hide();
            hasError = true;
        } else {
            // Ẩn thông báo lỗi nếu không có lỗi
            $('#errorMessage').hide();
        }

        // Nếu không có lỗi, gửi form
        if (!hasError) {
            $('#confirmationForm').submit();
        }
    });
});

