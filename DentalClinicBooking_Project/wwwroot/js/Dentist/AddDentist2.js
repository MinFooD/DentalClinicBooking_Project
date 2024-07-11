function saveRegisterForm(event) {
    event.preventDefault();
  
    var username = document.getElementById("dentist-username").value;
    var password = document.getElementById("dentist-password").value;
    var rePassword = document.getElementById("dentist-re-password").value;
    // Lưu trữ thông tin vào sessionStorage
    sessionStorage.setItem("username", username);
    sessionStorage.setItem("password", password);
    sessionStorage.setItem("rePassword", rePassword);
    // Chuyển sang form chi tiết
    showStep2();
  }
  function showStep2() {
    document.getElementById("register-form").style.display = "none";
    document.getElementById("register-info-form").style.display = "block";
  
    // Điền thông tin vào các trường ẩn
    document.getElementById("hiddenUsername").value =
      sessionStorage.getItem("username");
    document.getElementById("hiddenPassword").value =
      sessionStorage.getItem("password");
    document.getElementById("hiddenRePassword").value =
      sessionStorage.getItem("rePassword");
  }
  function submitForm() {
    // Lấy thông tin từ sessionStorage
    var username = sessionStorage.getItem("username");
    var password = sessionStorage.getItem("password");
    var rePassword = sessionStorage.getItem("rePassword");
    // Điền thông tin vào các trường ẩn trong form chi tiết
    document.getElementById("hiddenUsername").value = username;
    document.getElementById("hiddenPassword").value = password;
    document.getElementById("hiddenRePassword").value = rePassword;
    // Sau đó submit form
    return true;
  }
  function checkInput() {
    var username = document.getElementById("dentist-username").value;
    var password = document.getElementById("dentist-password").value;
    var rePassword = document.getElementById("dentist-re-password").value;
  
    var nextButton = document.getElementById("nextButton");
  
    if (username && password && rePassword) {
      if (password === rePassword) {
        nextButton.disabled = false;
      } else {
        nextButton.disabled = true;
      }
    } else {
      nextButton.disabled = true;
    }
  }
  