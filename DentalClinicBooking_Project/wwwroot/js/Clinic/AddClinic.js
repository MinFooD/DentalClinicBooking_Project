

    document.querySelectorAll(".time-input").forEach((input) => {
        input.addEventListener("input", function (e) {
            const value = e.target.value.replace(/[^0-9:]/g, "");
            const parts = value.split(":");

            if (parts.length === 2) {
                let hours = parts[0];
                let minutes = parts[1];

                if (hours.length === 1) hours = "0" + hours;
                if (minutes.length === 1) minutes = "0" + minutes;

                if (parseInt(hours, 10) > 24) hours = "24";
                if (parseInt(minutes, 10) > 59) minutes = "59";

                e.target.value = hours + ":" + minutes;
            } else {
                e.target.value = value;
            }
        });
    });

    //document.getElementById("edit-clinic-name").addEventListener("input", validateForm);
    //document.getElementById("edit-main-image").addEventListener("input", validateForm);
    //document.getElementById("edit-description").addEventListener("input", validateForm);
    //document.getElementById("select-services").addEventListener("change", validateForm);

    document.querySelector(".form-clinic").addEventListener("submit", function (e) {
        e.preventDefault();

        // Bắt các trường nhập liệu hình ảnh
        let imageUrls = [];
        document.querySelectorAll(".clinic-image").forEach(function (input) {
            if (input.value) {
                imageUrls.push(input.value);
            }
        });

        //document.querySelectorAll("#additional-images input").forEach(function (input) {
        //    if (input.value) {
        //        imageUrls.push(input.value);
        //    }
        //});

        // Chuyển đổi thành JSON và lưu vào input ẩn
        document.getElementById("imageurlsjson").value = JSON.stringify(imageUrls);

        // Gửi form (ví dụ bạn có thể sử dụng AJAX để gửi hoặc chỉ cần gửi form bình thường)
        // e.target.submit(); // uncomment this if you want to actually submit the form
        console.log(document.getElementById("imageurlsjson").value); // In ra để kiểm tra
    });

    function validateForm() {
        const clinicName = document.getElementById("edit-clinic-name").value.trim();
        const mainImage = document.getElementById("edit-main-image").value.trim();
        const description = document.getElementById("edit-description").value.trim();
        const services = $('#select-services').val();
        const isValid = clinicName && mainImage && description && services.length > 0;
        document.getElementById("submit-button").disabled = !isValid;
    }
});

function addImageInput() {
    // Tạo một div mới chứa trường input mới
    var newDiv = document.createElement("div");
    newDiv.classList.add("form-group");
    var newLabel = document.createElement("label");
    newLabel.innerText = "Ảnh:";
    var newInput = document.createElement("input");
    newInput.type = "text";
    newInput.classList.add("form-control", "clinic-image");
    newInput.placeholder = "URL";

    newDiv.appendChild(newLabel);
    newDiv.appendChild(newInput);

    // Thêm div mới vào phần bổ sung ảnh
    document.getElementById("additional-images").appendChild(newDiv);

    newInput.addEventListener("input", validateForm);
}
$(document).ready(function () {
    $('.select2-multiple').select2();
});


function showBasicForm() {
    // Ẩn form phòng khám
    document.getElementById("clinic-form").style.display = "none";
    // Hiển thị form cơ sở
    document.getElementById("basic-form-container").style.display = "block";
}

function toggleForm(element) {
    const form = element.nextElementSibling;
    if (form.style.display === "none" || form.style.display === "") {
        form.style.display = "block";
        element.innerHTML = "&#9660;"; // Mũi tên hướng xuống
    } else {
        form.style.display = "none";
        element.innerHTML = "&#9654;"; // Mũi tên hướng ngang
    }
}