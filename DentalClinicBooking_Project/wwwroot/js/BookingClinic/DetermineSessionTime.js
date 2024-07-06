function determineSessionTime() {
    const ddElements = document.querySelectorAll('.info-value.green');

    ddElements.forEach(dd => {
        const timeRange = dd.textContent.trim().split(' ')[0];
        const startTime = timeRange.split('-')[0].trim();
        const [hours] = startTime.split(':').map(Number);

        const spanElement = dd.querySelector('.session-time');
        if (hours >= 12) {
            spanElement.textContent = '(Buổi chiều)';
        } else {
            spanElement.textContent = '(Buổi sáng)';
        }
    });
}

window.onload = determineSessionTime;