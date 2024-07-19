
//document.addEventListener('DOMContentLoaded', function () {
//    document.getElementById('confirmationForm').addEventListener('submit', function (event) {
//        var inputCode = document.getElementById('inputCode').value;
//        var errorMessage = document.getElementById('errorMessage');

//        if (inputCode.trim() === "") {
//            errorMessage.style.display = 'block';
//            event.preventDefault(); // Ngăn chặn việc gửi form nếu có
//        } else {
//            errorMessage.style.display = 'none';
//        }
//    });
//});

//document.addEventListener('DOMContentLoaded', function () {
//    document.getElementById('confirmationForm').addEventListener('submit', function (event) {
//        var inputCode = document.getElementById('inputCode').value;
//        var errorMessage = document.getElementById('errorMessage');
//        var appointmentInfo = document.getElementById('appointmentInfo');
//        var buttonGroup = document.getElementById('buttonGroup');

//        if (inputCode.trim() === "") {
//            errorMessage.style.display = 'block';
//            appointmentInfo.style.display = 'none';
//            buttonGroup.style.display = 'none';
//            event.preventDefault(); // Ngăn chặn việc gửi form nếu có
//        } else {
//            errorMessage.style.display = 'none';
//            appointmentInfo.style.display = 'block';
//            buttonGroup.style.display = 'block';
//        }
//    });
//});

//$(document).ready(function () {
//     Ẩn thông báo lỗi khi trang tải
//    $('#errorMessage').hide();

//     Khi nhấn nút xác nhận
//    $('#confirmButton').click(function (event) {
//        event.preventDefault();
//         Kiểm tra điều kiện để hiển thị thông báo lỗi
//        var hasError = true; // Thay đổi điều kiện này theo yêu cầu của bạn

//        if (hasError) {
//             Hiển thị thông báo lỗi
//            $('#errorMessage').show();
//             Ẩn phần tử với id appointmentInfo
//            $('#appointmentInfo').hide();
//        } else {
//             Nếu không có lỗi, có thể thực hiện các hành động khác
//            console.log('Không có lỗi.');
//             Bạn có thể hiện phần tử appointmentInfo nếu cần
//            $('#appointmentInfo').show();
//            $('#myForm').submit();
//        }
//    });
//});


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

