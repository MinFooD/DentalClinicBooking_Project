document.addEventListener("DOMContentLoaded", () => {
    const deleteBasicModal = document.getElementById("deleteBasicModal");
    const closeButtons = document.querySelectorAll(".close");

    closeButtons.forEach((button) => {
        button.onclick = () => {
            deleteBasicModal.style.display = "none";
        };
    });

    window.onclick = (event) => {
        if (event.target === deleteBasicModal) {
            deleteBasicModal.style.display = "none";
        }
    };

    window.openDeleteModal = (id) => {
        deleteBasicModal.style.display = "block";
        const confirmDeleteButton = document.getElementById("confirmDelete");
        confirmDeleteButton.onclick = () => {
            window.location.href = `/Basic/DeleteBasic?id=${id}`; // Chuyển hướng đến hành động DeleteBasic với ID của cơ sở
        };
    };

    window.openAddBasicForm = (id) => {
        window.location.href = `/Basic/AddBasic?id=${id}`; // Chuyển hướng đến hành động AddBasic với ID của Phòng khám
    };

    window.openUpdateBasicForm = (id) => {
        window.location.href = `/Basic/UpdateBasic?id=${id}`; // Chuyển hướng đến hành động UpdateBasic với ID của cơ sở
    };
});