let addToBasketBtn = document.querySelectorAll(".add-to-basket");

addToBasketBtn.forEach(btn => btn.addEventListener("click", function (e) {
    e.preventDefault();

    let url = btn.getAttribute("href");

    fetch(url).then(response => {
        if (response.status == 200) {
            alert("Add basket")
        } else {
            alert("Error")
            window.location.reload(true)
        }
    })

}))