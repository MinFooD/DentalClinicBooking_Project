document.addEventListener("DOMContentLoaded", () => {
    const editClinicModal = document.getElementById("editClinicModal");
    const deleteClinicModal = document.getElementById("deleteClinicModal");
    const closeButtons = document.querySelectorAll(".close");

    closeButtons.forEach((button) => {
        button.onclick = () => {
            editClinicModal.style.display = "none";
            deleteClinicModal.style.display = "none";
        };
    });

    window.onclick = (event) => {
        if (
            event.target === editClinicModal ||
            event.target === deleteClinicModal
        ) {
            editClinicModal.style.display = "none";
            deleteClinicModal.style.display = "none";
        }
    };

    window.openEditModal = (id) => {
        const clinicCard = document.querySelector(`.clinic-card[data-id='${id}']`);
        const name = clinicCard.getAttribute("data-name");
        const description = clinicCard.getAttribute("data-description");
        // const address = clinicCard.getAttribute("data-address");
        // const addressUrl = clinicCard.getAttribute("data-address-url");
        const mainImageURL = clinicCard.getAttribute("data-main-image-url");
        const additionalImages = clinicCard
            .getAttribute("data-additional-images")
            .split(",");

        document.getElementById("clinicName").value = name;
        document.getElementById("clinicDescription").value = description;
        // document.getElementById("clinicAddress").value = address;
        // document.getElementById("clinicAddressUrl").value = addressUrl;
        document.getElementById("mainImageURL").value = mainImageURL;

        const additionalImagesContainer = document.getElementById(
            "additionalImagesInputs"
        );
        additionalImagesContainer.innerHTML = ""; // Clear previous inputs

        additionalImages.forEach((url) => {
            createImageInput(url.trim(), additionalImagesContainer);
        });

        // Add button to add more image inputs
        // Add button to add more image inputs
        const addImageButton = document.getElementById("addImageButton");
        addImageButton.addEventListener("click", () => {
            // Remove existing inputs before adding new ones
            const existingInputs = document.querySelectorAll(
                ".additional-image-input"
            );
            existingInputs.forEach((input) => input.remove());

            createImageInput("", additionalImagesContainer);
        });

        editClinicModal.style.display = "block";
    };

    window.openDeleteModal = (id) => {
        deleteClinicModal.style.display = "block";
        document.getElementById("confirmDelete").onclick = () => {
            const clinicCard = document.querySelector(
                `.clinic-card[data-id='${id}']`
            );
            clinicCard.remove();
            deleteClinicModal.style.display = "none";
        };
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
