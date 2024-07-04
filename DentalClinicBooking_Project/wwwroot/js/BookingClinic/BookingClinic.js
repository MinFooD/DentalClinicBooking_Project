// Đóng mở step items
document.addEventListener('DOMContentLoaded', function () {
    var header = document.querySelector('.step-item:nth-child(1) .step-header');
    header.addEventListener('click', function () {
        var container = document.getElementById('searchFormContainer');
        container.classList.toggle('hidden');
    });
});

document.addEventListener('DOMContentLoaded', function () {
    var header = document.querySelector('.step-item:nth-child(2) .step-header');
    header.addEventListener('click', function () {
        var container = document.getElementById('searchFormContainer2');
        container.classList.toggle('hidden');
    });
});

document.addEventListener('DOMContentLoaded', function () {
    var header = document.querySelector('.step-item:nth-child(4) .step-header');
    header.addEventListener('click', function () {
        var container = document.getElementById('searchFormContainer3');
        container.classList.toggle('hidden');
    });
});


document.addEventListener('DOMContentLoaded', function () {
    var header = document.querySelector('.step-item:nth-child(5) .step-header');
    header.addEventListener('click', function () {
        var container = document.getElementById('searchFormContainer4');
        container.classList.toggle('hidden');
    });
});



//  Lịch
document.addEventListener('DOMContentLoaded', function () {
    const calendarBody = document.querySelector('.dates-grid');
    let currentDate = new Date(); // Biến global để theo dõi thời gian hiện tại
    let selectedDateElement = null; // Lưu trữ ngày đã chọn

    const monthNames = [
        "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
        "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"
    ];

    function generateCalendar(year, month) {
        const startDate = new Date(year, month, 1);
        const endDate = new Date(year, month + 1, 0);
        const daysInMonth = endDate.getDate();
        calendarBody.innerHTML = ''; // Clear existing dates
        const monthTitle = document.querySelector('.month-title');
        monthTitle.textContent = `${monthNames[month]} ${year}`;

        for (let i = 1; i <= daysInMonth; i++) {
            const date = new Date(year, month, i);
            const dayOfWeek = date.getDay();
            const dateElement = document.createElement('div');
            dateElement.classList.add('date');
            dateElement.textContent = i;
            dateElement.setAttribute('data-date', date.toISOString());

            if (dayOfWeek === 0) { // Sunday
                dateElement.classList.add('sunday');
            }
            if (date < new Date().setHours(0, 0, 0, 0)) { // Past dates
                dateElement.classList.add('past-date');
                dateElement.classList.remove('date');
            }
            calendarBody.appendChild(dateElement);
        }
    }

    generateCalendar(currentDate.getFullYear(), currentDate.getMonth());

    const nextMonthBtn = document.querySelector('.next-month-btn');
    nextMonthBtn.addEventListener('click', () => {
        currentDate.setMonth(currentDate.getMonth() + 1);
        generateCalendar(currentDate.getFullYear(), currentDate.getMonth());
    });

    const prevMonthBtn = document.querySelector('.prev-month-btn');
    prevMonthBtn.addEventListener('click', () => {
        currentDate.setMonth(currentDate.getMonth() - 1);
        generateCalendar(currentDate.getFullYear(), currentDate.getMonth());
    });

    // Cập nhật sidebar khi ngày được chọn
    calendarBody.addEventListener('click', function (event) {
        const dateElement = event.target.closest('.date');
        if (dateElement && !dateElement.classList.contains('past-date')) {
            if (selectedDateElement) {
                selectedDateElement.classList.remove('selected');
            }
            selectedDateElement = dateElement;
            selectedDateElement.classList.add('selected');
            const selectedDate = new Date(selectedDateElement.getAttribute('data-date')).toLocaleDateString('vi-VN');
            const dateInfo = document.querySelector('.sidebar .info-item:nth-child(3) .info-value');
            dateInfo.textContent = selectedDate;
            document.querySelector('.sidebar .info-item:nth-child(3)').style.display = 'block';
        }
    });
});

// Cập nhật sang sidebar
document.addEventListener('DOMContentLoaded', function () {
    // Cập nhật thông tin cơ sở khi chọn
    const branchItems = document.querySelectorAll('.branch-item');
    branchItems.forEach(item => {
        item.addEventListener('click', function () {
            const branchName = item.querySelector('.branch-name').textContent;
            const branchAddress = item.querySelector('.branch-address').textContent;
            const sidebarBranchName = document.querySelector('.sidebar .info-value');
            const sidebarBranchAddress = document.querySelector('.sidebar .info-valueadd');
            sidebarBranchName.textContent = branchName;
            sidebarBranchAddress.textContent = branchAddress;
            document.querySelector('.sidebar .info-item').style.display = 'block';
            document.querySelector('.sidebar .info-itemadd').style.display = 'block';
        });
    });

    // Cập nhật thông tin ngày khám khi chọn
    const calendarBody = document.querySelector('.dates-grid');
    calendarBody.addEventListener('click', function (event) {
        const dateElement = event.target.closest('.date');
        if (dateElement) {
            const selectedDate = new Date(dateElement.getAttribute('data-date')).toLocaleDateString('vi-VN');
            const dateInfo = document.querySelector('.sidebar .info-item:nth-child(4) .info-value');
            dateInfo.textContent = selectedDate;
            document.querySelector('.sidebar .info-item:nth-child(4)').style.display = 'block';
        }
    });

    // Cập nhật thông tin giờ khám khi chọn
    const timeSlots = document.querySelectorAll('.time-slot-item');
    timeSlots.forEach(slot => {
        slot.addEventListener('click', function () {
            const timeRange = slot.textContent;
            const timeInfo = document.querySelector('.sidebar .info-item:nth-child(5) .info-value');
            timeInfo.textContent = timeRange;
            document.querySelector('.sidebar .info-item:nth-child(5)').style.display = 'block';
        });
    });
});


document.addEventListener('DOMContentLoaded', function () {
    var header = document.querySelector('.step-item:nth-child(4) .step-header');
    var container = document.getElementById('searchFormContainer4');
    var serviceItems = document.querySelectorAll('.service-item');

    header.addEventListener('click', function () {
        container.classList.toggle('hidden');
    });

    serviceItems.forEach(item => {
        item.addEventListener('click', function () {
            // Bỏ chọn tất cả các service item khác
            serviceItems.forEach(otherItem => {
                if (otherItem !== this) {
                    otherItem.classList.remove('selected');
                }
            });

            // Chọn item hiện tại
            this.classList.add('selected');
            updateSidebar();
        });
    });

    function updateSidebar() {
        const selectedServices = Array.from(document.querySelectorAll('.service-item.selected')).map(item => item.dataset.service);
        const sidebarServiceInfo = document.querySelector('.sidebar .info-item:nth-child(6) .info-value');
        sidebarServiceInfo.textContent = selectedServices.join(', ');
        document.querySelector('.sidebar .info-item:nth-child(6)').style.display = 'block';
    }
});


document.addEventListener('DOMContentLoaded', function () {
    const stepItems = document.querySelectorAll('.step-item');
    const stepHeaders = document.querySelectorAll('.step-header');
    const stepDescriptions = document.querySelectorAll('.step-description');

    // Ẩn tất cả các step-item từ bước 2 trở đi
    stepItems.forEach((item, index) => {
        if (index > 0) item.classList.add('hidden');
    });

    // Mở bước 1 ngay khi trang tải xong
    stepDescriptions[0].classList.remove('hidden');

    // Sự kiện cho phép chỉnh sửa bước 1
    stepHeaders[0].addEventListener('click', () => {
        stepDescriptions[0].classList.remove('hidden');
        // Ẩn tất cả các bước từ bước 2 trở đi
        for (let i = 1; i < stepItems.length; i++) {
            stepItems[i].classList.add('hidden');
            stepDescriptions[i].classList.add('hidden');
        }
    });

    // Tương tự cho bước 2, 3, và 4
    stepHeaders[1].addEventListener('click', () => {
        stepDescriptions[1].classList.remove('hidden');
        // Ẩn tất cả các bước từ bước 3 trở đi
        for (let i = 2; i < stepItems.length; i++) {
            stepItems[i].classList.add('hidden');
            stepDescriptions[i].classList.add('hidden');
        }
    });

    stepHeaders[2].addEventListener('click', () => {
        stepDescriptions[2].classList.remove('hidden');
        // Ẩn bước 4
        stepItems[3].classList.add('hidden');
        stepDescriptions[3].classList.add('hidden');
    });

    // Logic để mở các bước tiếp theo khi hoàn thành bước hiện tại
    // Ví dụ: Khi hoàn thành bước 1
    const branchItems = document.querySelectorAll('.branch-item'); // Các mục lựa chọn cho bước 1
    branchItems.forEach(item => {
        item.addEventListener('click', () => {
            stepDescriptions[0].classList.add('hidden'); // Đóng mô tả bước 1
            stepItems[1].classList.remove('hidden'); // Hiển thị bước 2
            stepDescriptions[1].classList.remove('hidden'); // Mở mô tả bước 2
        });
    });

    // Khi hoàn thành bước 2
    //const dateItems = document.querySelectorAll('.date');
    //dateItems.forEach(item => {
    //    item.addEventListener('click', () => {
    //        stepDescriptions[1].classList.add('hidden');
    //        stepItems[2].classList.remove('hidden');
    //        stepDescriptions[2].classList.remove('hidden');
    //    });
    //});
    // Khi hoàn thành bước 2
    const calendarBody = document.querySelector('.dates-grid');
    calendarBody.addEventListener('click', function (event) {
        const dateElement = event.target.closest('.date');
        if (dateElement && !dateElement.classList.contains('past-date')) {
            stepDescriptions[1].classList.add('hidden'); // Đóng mô tả bước 2
            stepItems[2].classList.remove('hidden'); // Hiển thị bước 3
            stepDescriptions[2].classList.remove('hidden'); // Mở mô tả bước 3
        }
    });

    // Khi hoàn thành bước 3
    const timeSlots = document.querySelectorAll('.time-slot-item');
    timeSlots.forEach(slot => {
        slot.addEventListener('click', () => {
            stepDescriptions[2].classList.add('hidden');
            stepItems[3].classList.remove('hidden');
            stepDescriptions[3].classList.remove('hidden');
        });
    });
});

