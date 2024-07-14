
    document.querySelectorAll('.toggle-password').forEach(item => {
        item.addEventListener('click', event => {
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

    document.addEventListener('DOMContentLoaded', function () {
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
    });


//  khi load modal không bị đóng lại 
    document.addEventListener('DOMContentLoaded', function () {
        const modalOverlay = document.getElementById('modalOverlay');
        const changeButton = document.querySelector('.change-password-btn');
        const cancelBtn = document.querySelector('.cancel-btn');
        const okBtn = document.querySelector('.ok-btn');

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
    });