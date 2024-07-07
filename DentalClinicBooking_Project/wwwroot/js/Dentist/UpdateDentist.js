function showDetails(element) {
  var doctorInfo = element.querySelector(".doctor-info");
  var doctorDetails = document.getElementById("doctor-details");

  var name = element.querySelector("h2").innerText;
  var imgSrc = element.querySelector("img").src;
  var email = doctorInfo.querySelector(".doctor-email").innerText;
  var password = doctorInfo.querySelector(".doctor-password").innerText;
  var experience = doctorInfo.querySelector(".doctor-experience").innerText;
  var description = doctorInfo.querySelector(".doctor-description").innerText;

  document.getElementById("detail-name").innerText = name;
  document.getElementById("detail-img").src = imgSrc;
  document.getElementById("name").value = name;
  document.getElementById("email").value = email;
  document.getElementById("password").value = password;
  document.getElementById("knl_viec").value = experience;
  document.getElementById("description").value = description;

  doctorDetails.hidden = false;
}

function closeDoctorDetail() {
  document.getElementById("doctor-details").hidden = true;
}
function updateHeader() {
  var nameInput = document.getElementById("name").value;
  document.getElementById("detail-name").innerText = nameInput;
}

function openModal() {
  document.getElementById("urlModal").style.display = "block";
  document.getElementById("doctor-details").style.display = "block";
}

function closeModal() {
  document.getElementById("urlModal").style.display = "none";
}
function updateImage() {
  var newImageUrl = document.getElementById("new-image-url").value;
  if (newImageUrl) {
    document.getElementById("detail-img").src = newImageUrl;
    closeModal();
  }
}

// Close the modal when clicking outside of it
window.onclick = function (event) {
  var modal = document.getElementById("urlModal");
  if (event.target == modal) {
    modal.style.display = "none";
  }
};
