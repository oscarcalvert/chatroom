
let accordion_btn = document.querySelectorAll(".accordion-btn")
let closeButtons = document.querySelectorAll(".btn-close")
let dropdownButtons = document.querySelectorAll(".dropdown-toggle")
let checkboxButtons = document.querySelectorAll(".checkbox-label")
let body = document.querySelector("body")
accordion_btn.forEach((btn, i) => {
    btn.innerHTML = btn.innerHTML + '<i class="fa-solid fa-chevron-down"></i>'
    btn.addEventListener('click', () => {
        if (btn.classList.contains("active")) {
            btn.classList.remove("active")
        } else {
            btn.classList.add("active")
            accordion_btn.forEach((btn, j) => {
                if (i != j)
                    btn.classList.remove("active")
            })
        }
    })
});

closeButtons.forEach(btn => {
    btn.innerHTML = '<i class="fa-solid fa-xmark"></i>'
});

dropdownButtons.forEach(btn => {
    btn.innerHTML = btn.innerHTML + '<i class="fa-solid fa-chevron-down"></i>'
    btn.addEventListener('click', () => {
        if (btn.classList.contains("active")) {
            btn.classList.remove("active")
        } else {
            btn.classList.add("active")
        }
    })
});

checkboxButtons.forEach(btn => {
    btn.innerHTML = btn.innerHTML + '<i class="fa-solid fa-check"></i>'
})




