let DeleteBtn = document.querySelectorAll(".delete-imege-btn");

DeleteBtn.forEach(btn => btn.addEventListener("click", function () {
    btn.parentElement.remove()
}))

let itemDeletBtn = document.querySelectorAll(".item-delete");

itemDeletBtn.forEach(btn => btn.addEventListener("click", function (e) {
    e.preventDefault();

 
    
    Swal.fire({
        title: 'Silmek Istediyinize eminsiz ?',
        text: "Geri Qaytara Bilmeyeceksiz!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Beli,sil!',
        cancelButtonText: "Imtina"
    }).then((result) => {
        if (result.isConfirmed) {
            let url = btn.getAttribute("href");

            fetch(url)
                .then(response => {
                    if (response.status == 200) {
                        window.location.reload(true)

                    } else {
                        alert("Silmek Istediyiniz Item tapilmadi!")
                    }
                })
        }
    })
}))

let addToBasketBtns = document.querySelectorAll(".add-to-basket-btn")

addToBasketBtns.forEach(btn => btn.addEventListener("click", function (e) {
    e.preventDefault();
    let url = btn.getAttribute("href");

    fetch(url)
        .then(response => {
            if (response.status == 200) {
                alert("Added in to basket")
            } else {
                alert("Error!")
            }
        })

}))