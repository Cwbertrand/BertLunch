//document.addEventListener('DOMContentLoaded', function () {
//    var header = document.getElementById('sticky-header');

//    window.addEventListener('scroll', function () {

//    if (window.scrollY > 0) {
//        header.classList.add('sticky');
//    } else if (window.scrollY === 0) {
//        header.classList.remove('sticky');
//    }
//    });

//    // var widthSize =
//});

document.addEventListener('DOMContentLoaded', function () {
    var showNavButton = document.getElementById('show_nav');
    var closeNavButton = document.getElementById('close_nav');
    var navbarCollapse = document.getElementById('nav_items');

    showNavButton.addEventListener('click', function () {
        navbarCollapse.classList.add('show');
    });

    closeNavButton.addEventListener('click', function () {
        navbarCollapse.classList.remove('show');
    });
});


/* Sidebar Toggler*/
$('.sidebar-toggler').click(function () {
    $('.sidebar, .content').toggleClass("open");
    return false;
});


function populateAndShowModal(itemId) {
    fetch(`/detail/item/${itemId}`, { method: 'get' })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            var detailImageDiv = document.querySelector('.detail_image');
            detailImageDiv.style.backgroundImage = `url('${data.image}')`;
            console.log(detailImageDiv.style.backgroundImage);

            document.querySelector('#item-name').textContent = data.menuName;
            document.querySelector('#item-price').textContent = data.price + ' \u20AC';
            document.querySelector('#item-description').textContent = data.description;
            document.querySelector('#ingredient-list').textContent = data.ingredient

            var myModal = new bootstrap.Modal(document.getElementById('detailsItemModal'));
              
            myModal.show();
        })
        .catch(error => {
            console.log('Error:', error);
        });
}

function closeModal() {
    var myModalEl = document.getElementById('detailsItemModal');
    var myModal = bootstrap.Modal.getInstance(myModalEl);
    if (myModal) {
        myModal.hide();
    }
    myModalEl.addEventListener('hidden.bs.modal', function () {
        var backdrops = document.querySelectorAll('.modal-backdrop');
        backdrops.forEach(function (backdrop) {
            backdrop.remove();
        });
        document.body.classList.remove('modal-open');
        document.body.style.paddingRight = '';
        document.body.style.overflow = 'auto';
    });
}

