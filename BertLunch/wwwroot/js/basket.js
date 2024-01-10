function updateUI() {
    fetch('/basket/product_summary', { method: 'get' })
        .then(response => response.json())
        .then(data => {
            //console.log(data)
            data.totalQuantity == 0 ? '' : document.getElementById("cart_quantity").textContent = data.totalQuantity;
            document.getElementById("cart_total_price").textContent = `${data.totalPrice.toFixed(2)} \u20AC`;
        })
        .catch(error => {
            console.log("Error fetching total quantity:", error);
        });
};
function updateInputUI(data) {
    data.forEach(item => {
        
        var totQtyPerItem = document.getElementById(`quantity_${item.productId}`);
        
        if (totQtyPerItem) {
            totQtyPerItem.textContent = item.quantity;
        } else {
            console.log('Element not found');
        }
    });
};

function addToBasket(event, Id) {
    event.preventDefault();
    fetch(`/basket/addtobasket/${Id}`, { method: 'put' })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            updateUI();
            updateInputUI(data);

            data.forEach(item => {
                const itemId = `item_${Id}`;
                const minusElement = document.querySelector(`#${itemId} .minus_block`);
                const trashElement = document.querySelector(`#${itemId} .trash_block`);

                if (item.quantity >= 2) {
                    minusElement.classList.add('display_icon');
                    trashElement.classList.add('dont_display_icon');
                } else {
                    trashElement.classList.add('dont_display_icon');
                    minusElement.classList.add('display_icon');
                }
            });
        })
        .catch(error => {
            console.error('Error adding item to basket:', error);
        });
}

function removeFromBasket(event, Id) {
    event.preventDefault();
    fetch(`/basket/removefrombasket/${Id}`, { method: 'put' })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            // Get the specific item you're clicking on
            const item = data.find(i => i.productId == Id);

            //Select the Id of the item
            const deleteItem = document.querySelector(`#delete_item_${Id}`);
            if (item && item.quantity) {
                
                updateUI();
                updateInputUI(data);
                
            } else {
                deleteItem.style.display = 'none';
                updateUI();
            }
        })
        .catch(error => {
            console.error('Error removing item from basket:', error);
        });
}

function deleteFromBasket(event, Id) {
    event.preventDefault();
    fetch(`/basket/deletebasket/${Id}`, { method: 'delete' })
        .then(response => {
            console.log(response);
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            const deleteItem = document.querySelector(`#delete_item_${Id}`);
            if (deleteItem) {
                deleteItem.style.display = 'none';
                updateUI(data);
            }
        })
        .catch(error => {
            console.error('Error removing item from basket:', error);
        });
}

document.addEventListener('DOMContentLoaded', function () {
    updateUI()
});












// Time selection functionality
document.addEventListener('DOMContentLoaded', function () {
    var dateContainer = document.querySelector('.date_select p')
    var selectedDateInput = document.getElementById('selectedDate');
    var timeSlotsContainer = document.getElementById('timeSlotsContainer');
    var selectedTimeSlotInput = document.getElementById('selectedTimeSlot');
    var checkoutButton = document.getElementById('disable_checkout_btn');
    var timeSlots = ['10:00', '10:30', '11:00', '11:30', '12:00', '12:30', '13:00', '13:30', '14:00', '14:30'];


    var now = new Date();

    // Getting the last time slot
    var lastTimeSlot = timeSlots[timeSlots.length - 1];

    // Getting the last date and time slot
    var lastTimeSlotDate = new Date(now.toDateString() + ' ' + lastTimeSlot);

    var displayDate = new Date(now);
    if (now > lastTimeSlotDate) {
        // Set the date to the next day
        displayDate.setDate(displayDate.getDate() + 1);
    }
    dateContainer.textContent = formatDate(displayDate);
    selectedDateInput.value = formatDate(displayDate);

    timeSlots.forEach(function (slot) {
        var p = document.createElement('p');
        p.textContent = slot;
        p.classList.add('time');
        p.addEventListener('click', function() {

            // Remove 'selected' class from all time slots
            document.querySelectorAll('.time').forEach(function (time) {
                time.classList.remove('selected')
            });
            p.classList.add('selected');
            selectedTimeSlotInput.value = slot;
            checkoutButton.disabled = false;
        });
        timeSlotsContainer.appendChild(p);
    });
});

// Helper function to format a date as 'dd/mm/yyyy'
function formatDate(date) {
    if (!(date instanceof Date)) {
        console.error('formatDate expects a Date object');
        return '';
    }

    var day = ('0' + date.getDate()).slice(-2);
    var month = ('0' + (date.getMonth() + 1)).slice(-2);
    var year = date.getFullYear();
    return `${day}/${month}/${year}`;
}