
// document.addEventListener("DOMContentLoaded", function () {
//     const sidebar = document.getElementById("sidebar");
//     const sidebarToggle = document.getElementById("sidebarToggle");
//     const mainContent = document.querySelector(".main-content");

//     sidebarToggle.addEventListener("click", function () {
//         sidebar.classList.toggle("active");
//         mainContent.classList.toggle("shifted");

//         // Adjust main content width dynamically
//         if (sidebar.classList.contains("active")) {
//             mainContent.style.marginLeft = "250px";
//             mainContent.style.width = "calc(100% - 250px)";
//         } else {
//             mainContent.style.marginLeft = "0";
//             mainContent.style.width = "100%";
//         }
//     });

//     // Dropdown Menus
//     document.querySelectorAll(".dropdown-toggle").forEach(dropdown => {
//         dropdown.addEventListener("click", function () {
//             let submenu = this.nextElementSibling;
//             submenu.style.display = submenu.style.display === "block" ? "none" : "block";
//         });
//     });
//     setTimeout(() => {
//         document.querySelectorAll(".card-text").forEach((element, index) => {
//             if (index === 0) element.innerText = "150";  // Employees
//             if (index === 1) element.innerText = "23";   // Tasks
//             if (index === 2) element.innerText = "$45,000";  // Revenue
//         });
//     }, 2000);

//     document.querySelectorAll(".submenu li a").forEach(link => {
//         link.addEventListener("click", function () {
//             // Remove "active" class from all links
//             document.querySelectorAll(".submenu li a").forEach(a => a.classList.remove("active"));

//             // Add "active" class to the clicked link
//             this.classList.add("active");
//         });
//     });


// });


// //document.addEventListener("DOMContentLoaded", function () {
// //    const sidebar = document.getElementById("sidebar");
// //    const sidebarToggle = document.getElementById("sidebarToggle");
// //    const mainContent = document.querySelector(".main-content");

// //    sidebarToggle.addEventListener("click", function () {
// //        sidebar.classList.toggle("active");
// //        mainContent.classList.toggle("shifted");
// //    });

// //    // Dropdown Menus
// //    document.querySelectorAll(".dropdown-toggle").forEach(dropdown => {
// //        dropdown.addEventListener("click", function () {
// //            let submenu = this.nextElementSibling;
// //            submenu.style.display = submenu.style.display === "block" ? "none" : "block";
// //        });
// //    });
  
// //});
