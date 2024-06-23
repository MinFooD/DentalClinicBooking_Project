$(document).ready(function () {
    $('.toggle-button').click(function () {
        var textElement = $('.text');
        if (textElement.hasClass('expanded')) {
            textElement.removeClass('expanded');
            $(this).text('Xem thêm...');
        } else {
            textElement.addClass('expanded');
            $(this).text('...Thu gọn');
        }
    });
});
