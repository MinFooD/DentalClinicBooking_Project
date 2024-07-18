document.addEventListener('DOMContentLoaded', function () {
    var appointmentItems = document.querySelectorAll('.appointment-item');
    var detailsDisplay = document.querySelector('.details-display');
    var firstDetailsContainer = document.querySelector('.appointment-details-container');

    // Hiển thị chi tiết đầu tiên
    if (firstDetailsContainer) {
        var clonedFirstDetails = firstDetailsContainer.cloneNode(true);
        clonedFirstDetails.style.display = 'block'; // Đảm bảo chi tiết đầu tiên được hiển thị
        detailsDisplay.appendChild(clonedFirstDetails);

        var cancelButton = clonedFirstDetails.querySelector('.cancel-button');
        cancelButton.addEventListener('click', function (event) {
            event.preventDefault();
            $('#confirmModal').modal('show');
        });
    }

    // Thêm sự kiện click cho mỗi appointment-item
    appointmentItems.forEach(function (item) {
        item.addEventListener('click', function () {
            var container = this.querySelector('.appointment-details-container');
            // Xóa nội dung hiện tại và thêm nội dung mới
            detailsDisplay.innerHTML = '';
            var clonedContainer = container.cloneNode(true);
            clonedContainer.style.display = 'block'; // Đảm bảo chi tiết được hiển thị
            detailsDisplay.appendChild(clonedContainer);

            var cancelButton = clonedContainer.querySelector('.cancel-button');
            cancelButton.addEventListener('click', function (event) {
                event.preventDefault();
                $('#confirmModal').modal('show');
            });
        });
    });

    $("#confirmCancel").on("click", function (event) {
        // Thực hiện hành động hủy lịch
        console.log("Lịch đã được hủy");
        // Bạn có thể thêm mã để gửi yêu cầu hủy lịch đến server ở đây
        $('#confirmModal').modal('hide');
    });
});