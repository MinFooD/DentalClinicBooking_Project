document.addEventListener("DOMContentLoaded", function () {
  var topMucluc = document.querySelector(".top-mucluc");
  var bottomMucluc = document.querySelector(".bottom-mucluc");

  topMucluc.addEventListener("click", function () {
    if (
      bottomMucluc.style.display === "none" ||
      bottomMucluc.style.display === ""
    ) {
      bottomMucluc.style.display = "block";
    } else {
      bottomMucluc.style.display = "none";
    }
  });
});

document.addEventListener("DOMContentLoaded", function () {
  const links = document.querySelectorAll(".dentistryService a");
  const clinics = document.querySelectorAll(".phongKham");

  links.forEach((link) => {
    link.addEventListener("click", function (event) {
      event.preventDefault();
      const category = this.getAttribute("data-category");

      clinics.forEach((clinic) => {
        if (clinic.getAttribute("data-category") === category) {
          clinic.style.display = "flex";
        } else {
          clinic.style.display = "none";
        }
      });
    });
  });
});
