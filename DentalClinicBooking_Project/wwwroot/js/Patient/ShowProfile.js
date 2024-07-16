//document.addEventListener('DOMContentLoaded', function () {
//    const currentPasswordInput = document.getElementById('currentPassword');
//    const newPasswordInput = document.getElementById('newPassword');
//    const modalOverlay = document.getElementById('modalOverlay');
//    const cancelBtn = document.querySelector('.cancel-btn');
//    const changePasswordBtn = document.getElementById('submitButton');
//    const okBtn = document.querySelector('.ok-btn');

//    function updateButtonState() {
//        const isCurrentPasswordValid = currentPasswordInput.value.length >= 6;
//        const isNewPasswordValid = newPasswordInput.value.length >= 6;
//        changePasswordBtn.disabled = !(isCurrentPasswordValid && isNewPasswordValid);
//    }

//    currentPasswordInput.addEventListener('input', function () {
//        const currentPasswordError = document.getElementById('currentPasswordError');
//        currentPasswordError.style.display = currentPasswordInput.value.length < 6 && currentPasswordInput.value.length >= 1 ? 'block' : 'none';
//        updateButtonState();
//    });

//    newPasswordInput.addEventListener('input', function () {
//        const newPasswordError = document.getElementById('newPasswordError');
//        newPasswordError.style.display = newPasswordInput.value.length < 6 && newPasswordInput.value.length >= 1 ? 'block' : 'none';
//        updateButtonState();
//    });

//    function showModal() {
//        modalOverlay.style.display = 'flex';
//    }

//    function hideModal() {
//        modalOverlay.style.display = 'none';
//    }

//    function submitForm() {
//        const currentPassword = currentPasswordInput.value;
//        const newPassword = newPasswordInput.value;

//        const isCurrentPasswordValid = currentPassword.length >= 6;
//        const isNewPasswordValid = newPassword.length >= 6;

//        if (isCurrentPasswordValid && isNewPasswordValid) {
//            document.getElementById('changePasswordForm').submit();
//            hideModal();
//        } else {
//            console.log('Please enter valid passwords.');
//        }
//    }

//    cancelBtn.addEventListener('click', hideModal);

//    okBtn.addEventListener('click', submitForm);

//    const togglePasswordBtns = document.querySelectorAll('.toggle-password');
//    togglePasswordBtns.forEach(item => {
//        item.addEventListener('click', event => {
//            event.preventDefault();
//            const inputField = event.target.previousElementSibling;
//            if (inputField.type === "password") {
//                inputField.type = "text";
//                event.target.src = "https://youmed.vn/dat-kham/assets/img/booking/svg/Show.svg";
//            } else {
//                inputField.type = "password";
//                event.target.src = "https://youmed.vn/dat-kham/assets/img/booking/svg/Hide.svg";
//            }
//        });
//    });
//});
