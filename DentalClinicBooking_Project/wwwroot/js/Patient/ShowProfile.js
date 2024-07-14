document.addEventListener('DOMContentLoaded', function () {
    // Toggle password visibility
    document.querySelectorAll('.toggle-password').forEach(item => {
        item.addEventListener('click', event => {
            event.preventDefault(); // Ngăn chặn hành vi mặc định
            const inputField = event.target.previousElementSibling;
            if (inputField.type === "password") {
                inputField.type = "text";
                event.target.src = "https://youmed.vn/dat-kham/assets/img/booking/svg/Show.svg"; // URL to the 'show' icon
            } else {
                inputField.type = "password";
                event.target.src = "https://youmed.vn/dat-kham/assets/img/booking/svg/Hide.svg"; // URL to the 'hide' icon
            }
        });
    });

    // Update button state based on password input
    const currentPasswordInput = document.getElementById('currentPassword');
    const newPasswordInput = document.getElementById('newPassword');
    const changeButton = document.querySelector('.change-password-btn');
    const modalOverlay = document.getElementById('modalOverlay');
    const cancelBtn = document.querySelector('.cancel-btn');
    const okBtn = document.querySelector('.ok-btn');

    function updateButtonState() {
        const isCurrentPasswordValid = currentPasswordInput.value.length >= 6;
        const isNewPasswordValid = newPasswordInput.value.length >= 6;
        changeButton.disabled = !(isCurrentPasswordValid && isNewPasswordValid);
        if (!changeButton.disabled) {
            changeButton.classList.remove('btn-disabled');
            changeButton.classList.add('btn-primary');
        } else {
            changeButton.classList.add('btn-disabled');
            changeButton.classList.remove('btn-primary');
        }
    }

    currentPasswordInput.addEventListener('input', updateButtonState);
    newPasswordInput.addEventListener('input', updateButtonState);

    changeButton.addEventListener('click', function () {
        modalOverlay.style.display = 'flex';
    });

    cancelBtn.addEventListener('click', function () {
        modalOverlay.style.display = 'none';
    });

    okBtn.addEventListener('click', function () {
        // Thực hiện hành động thay đổi mật khẩu ở đây
        modalOverlay.style.display = 'none';
    });

    // Kiểm tra và khôi phục trạng thái của modal khi tải lại trang
    if (localStorage.getItem('modalShown') === 'true') {
        modalOverlay.style.display = 'flex';
    }

    // Hiển thị modal và lưu trạng thái khi nhấn nút "Thay đổi"
    changeButton.addEventListener('click', function () {
        modalOverlay.style.display = 'flex';
        localStorage.setItem('modalShown', 'true');
    });

    // Xử lý khi nhấn nút "Huỷ" trong modal
    cancelBtn.addEventListener('click', function () {
        modalOverlay.style.display = 'none';
        localStorage.removeItem('modalShown');
    });

    // Xử lý khi nhấn nút "OK" trong modal
    okBtn.addEventListener('click', function () {
        // Thực hiện hành động thay đổi mật khẩu ở đây
        modalOverlay.style.display = 'none';
        localStorage.removeItem('modalShown');
    });
    // giới hạn kí tự
    document.getElementById('currentPassword').addEventListener('input', function () {
        const currentPassword = document.getElementById('currentPassword');
        const currentPasswordError = document.getElementById('currentPasswordError');
        currentPasswordError.style.display = currentPassword.value.length < 6 && currentPassword.value.length >= 1 ? 'block' : 'none';
        validateForm();
    });

    document.getElementById('newPassword').addEventListener('input', function () {
        const newPassword = document.getElementById('newPassword');
        const newPasswordError = document.getElementById('newPasswordError');
        newPasswordError.style.display = newPassword.value.length < 6 && newPassword.value.length >= 1 ? 'block' : 'none';
        validateForm();
    });

    function validateForm() {
        const currentPassword = document.getElementById('currentPassword');
        const newPassword = document.getElementById('newPassword');
        const isFormValid = currentPassword.value.length >= 6 && newPassword.value.length >= 6;
        document.querySelector('.change-password-btn').disabled = !isFormValid;
    }
});