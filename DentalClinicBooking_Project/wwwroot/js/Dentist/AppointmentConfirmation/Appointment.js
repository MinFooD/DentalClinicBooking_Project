document.addEventListener('DOMContentLoaded', function () {
    const sidebarInfo = document.querySelector('.sidebar-info');
    const buttonGroup = document.querySelector('.button-group');
    const errorMessage = document.getElementById('error-message');

    // Ẩn sidebar và button group khi trang tải
    //sidebarInfo.style.display = 'none';
    //buttonGroup.style.display = 'none';

    document.getElementById('btnConfirm').addEventListener('click', function () {
        const inputCode = document.getElementById('inputCode').value;
        if (inputCode.length >= 6) {
            // Hiển thị sidebar và button group nếu mã hợp lệ
            sidebarInfo.style.display = 'block';
            buttonGroup.style.display = 'flex';
            errorMessage.style.display = 'none';
        } else {
            // Hiển thị thông báo lỗi nếu mã không hợp lệ
            errorMessage.style.display = 'block';
            sidebarInfo.style.display = 'none';
            buttonGroup.style.display = 'none';
        }
    });
});