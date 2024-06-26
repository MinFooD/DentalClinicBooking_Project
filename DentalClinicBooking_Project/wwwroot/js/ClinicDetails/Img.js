
    let currentImageIndex = 0;
            //const images = @Html.Raw(JsonConvert.SerializeObject(Model.ClinicImages.Select(m => m.Image).ToList()));

    function prevImage() {
        currentImageIndex = (currentImageIndex - 1 + images.length) % images.length;
    document.getElementById('mainImage').src = images[currentImageIndex];
            }

    function nextImage() {
        currentImageIndex = (currentImageIndex + 1) % images.length;
    document.getElementById('mainImage').src = images[currentImageIndex];
            }

    setInterval(nextImage, 3000); // Tự động chuyển ảnh sau mỗi 3 giây
