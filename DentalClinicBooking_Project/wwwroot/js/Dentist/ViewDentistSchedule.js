document.addEventListener('DOMContentLoaded', function () {
    updateDisplayDate(new Date()); // Hiển thị ngày hiện tại khi trang tải xong
  });

  function updateSchedule() {
    var selectedDate = new Date(document.getElementById('date').value);
    updateDisplayDate(selectedDate); // Cập nhật ngày được chọn
    console.log('Ngày đã được thay đổi:', selectedDate.toISOString().split('T')[0]);
  }

  function updateDisplayDate(date) {
    var daysOfWeek = ["Chủ Nhật", "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy"];
    var dayOfWeek = daysOfWeek[date.getDay()];
    var day = date.getDate().toString().padStart(2, '0');
    var month = (date.getMonth() + 1).toString().padStart(2, '0');
    var year = date.getFullYear();
    var formattedDate = dayOfWeek + ", ngày " + day + " tháng " + month + " năm " + year;
    document.getElementById('displayDate').innerHTML = formattedDate;
  }