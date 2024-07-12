document.addEventListener("DOMContentLoaded", () => {
    const deleteClinicModal = document.getElementById("deleteClinicModal");
    const closeButtons = document.querySelectorAll(".close");

    closeButtons.forEach((button) => {
        button.onclick = () => {    
            deleteClinicModal.style.display = "none";
        };
    });

    window.onclick = (event) => {
        if (event.target === deleteClinicModal) {
            deleteClinicModal.style.display = "none";
        }
    };
    


    window.openDeleteModal = (id) => {
        deleteClinicModal.style.display = "block";
        const confirmDeleteButton = document.getElementById("confirmDelete");
        confirmDeleteButton.onclick = () => {
            window.location.href = `/Clinic/DeleteClinic?id=${id}`; // Chuyển hướng đến hành động xóa với ID của Phòng khám
        };
    };
    window.openManageBasic = (id) => {
        window.location.href = `/Basic/ShowAllBasicForOwner?id=${id}`; // Chuyển hướng đến hành động showAllbasic với ID của Phòng khám
    };
    // Function to create image input
    function createImageInput(value, container) {
        const inputContainer = document.createElement("div");
        inputContainer.classList.add("image-input-wrapper");

        const imgInput = document.createElement("input");
        imgInput.type = "url";
        imgInput.value = value;
        imgInput.name = "additionalImages";
        imgInput.required = true;
        inputContainer.appendChild(imgInput);

        const deleteButton = document.createElement("button");
        deleteButton.type = "button";
        deleteButton.textContent = "Delete";
        deleteButton.classList.add("delete-image");
        deleteButton.addEventListener("click", () => {
            inputContainer.remove();
        });
        inputContainer.appendChild(deleteButton);

        container.appendChild(inputContainer);
    }
});
