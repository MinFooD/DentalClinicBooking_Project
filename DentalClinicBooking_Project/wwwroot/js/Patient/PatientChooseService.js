document.querySelectorAll('.btn-book').forEach(function(button) {
    button.addEventListener('click', function(event) {
        event.preventDefault();
        $('#confirmModal').modal('show');
    });
});

$("#confirmCancel").on("click", function(event) {
    // Thực hiện hành động hủy lịch
    console.log("Lịch đã được hủy");
    // Bạn có thể thêm mã để gửi yêu cầu hủy lịch đến server ở đây
    $('#confirmModal').modal('hide');
});