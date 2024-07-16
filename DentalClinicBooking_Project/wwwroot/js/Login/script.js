// function checkInput() {
//   var name = document.getElementById("username").value;
//   var password = document.getElementById("password").value;
//   var rePassword = document.getElementById("re-password").value;
//   var terms = document.getElementById("terms").checked;

//   var registerButton = document.getElementById("registerButton");

//   if (username && password && rePassword && terms) {

//     if (password === rePassword) {
//       registerButton.disabled = false;
//     } else {
//       registerButton.disabled = true;
//     }
//   } else {
//     registerButton.disabled = true;
//   }
// }
function checkInput() {
  var username = document.getElementById("username").value;
  var password = document.getElementById("password").value;
  var rePassword = document.getElementById("re-password").value;
  var terms = document.getElementById("terms").checked;

    var nextButton = document.getElementById("nextButton");
    const isNewPasswordValid = password.length >= 6;
    if (username && password && rePassword && terms && isNewPasswordValid) {
    if (password === rePassword) {
      nextButton.disabled = false;
    } else {
      nextButton.disabled = true;
    }
  } else {
    nextButton.disabled = true;
  }
}

function showRegisterForm() {
  document.getElementById("login-form").style.display = "none";
  document.getElementById("register-form").style.display = "block";
  document.getElementById("forgot-password-form").style.display = "none";
  document.getElementById("reset-password-form").style.display = "none";
  document.getElementsByClassName("form-container").style.display = "none";
}

function showOTPForm() {
  document.getElementById("register-form").style.display = "none";
  document.getElementById("otp-form").style.display = "block";
}

function showLoginForm() {
  document.getElementById("register-form").style.display = "none";
  document.getElementById("login-form").style.display = "block";
  document.getElementById("forgot-password-form").style.display = "none";
  document.getElementById("reset-password-form").style.display = "none";
}

function showForgotPasswordForm() {
  document.getElementById("login-form").style.display = "none";
  document.getElementById("forgot-password-form").style.display = "block";
  document.getElementById("reset-password-form").style.display = "none";
}

function showResetPasswordForm() {
  document.getElementById("forgot-password-form").style.display = "none";
  document.getElementById("reset-password-form").style.display = "block";
}

function checkOTPInput() {
  var otp = document.getElementById("otp").value;
  var verifyOTPButton = document.getElementById("verifyOTPButton");

  if (otp) {
    verifyOTPButton.disabled = false;
  } else {
    verifyOTPButton.disabled = true;
  }
}

function checkForgotEmailInput() {
  var forgotEmail = document.getElementById("forgotEmail").value;
  var sendOTPButton = document.getElementById("sendOTPButton");

  if (forgotEmail) {
    sendOTPButton.disabled = false;
  } else {
    sendOTPButton.disabled = true;
  }
}

function checkResetOTPInput() {
  var resetOTP = document.getElementById("resetOTP").value;
  var newPassword = document.getElementById("newPassword").value;
  var resetPasswordButton = document.getElementById("resetPasswordButton");

  if (resetOTP && newPassword) {
    resetPasswordButton.disabled = false;
  } else {
    resetPasswordButton.disabled = true;
  }
}

function login() {}

function sendForgotOTP() {
  showResetPasswordForm();
}

function resetPassword() {
  showLoginForm();
}

function verifyOTP() {
  showLoginForm();
  document.getElementById("otp-form").style.display = "none";
}

function saveRegisterForm(event) {
  event.preventDefault();

  var username = document.getElementById("username").value;
  var password = document.getElementById("password").value;

  // Lưu trữ thông tin vào sessionStorage
  sessionStorage.setItem("username", username);
  sessionStorage.setItem("password", password);

  // Chuyển sang form chi tiết
  showStep2();
}

function showStep2() {
  document.getElementById("register-form").style.display = "none";
  document.getElementById("register-details-form").style.display = "block";
}

function checkDetailInput() {
  var name = document.getElementById("name").value;
  var phone = document.getElementById("phone").value;
  var birthday = document.getElementById("birthday").value;
  var gender = document.getElementById("gender").value;
  var address = document.getElementById("address").value;
  //   var citizenId = document.getElementById("citizenId").value;
  //   var nation = document.getElementById("nation").value;
  //   var job = document.getElementById("job").value;
  //   var healthInsurance = document.getElementById("healthInsurance").value;
  var terms = document.getElementById("terms-detail").checked;

  var registerButton = document.getElementById("registerButton");

  if (name && phone && birthday && gender && address && terms) {
    registerButton.disabled = false;
  } else {
    registerButton.disabled = true;
  }
}

function submitForm() {
  // Lấy thông tin từ sessionStorage
  var username = sessionStorage.getItem("username");
  var password = sessionStorage.getItem("password");

  // Điền thông tin vào các trường ẩn trong form chi tiết
  document.getElementById("hiddenUsername").value = username;
  document.getElementById("hiddenPassword").value = password;

  // Sau đó submit form
  return true;
}
