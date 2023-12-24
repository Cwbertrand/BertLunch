function addToBasket(Id) {
    fetch(`/basket/addtobasket/${Id}`, { method: 'put' })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            updateUI(data);
        })
        .catch(error => {
            console.error('Error adding item to basket:', error);
        });
}


function removeFromBasket(Id) {
    fetch(`/basket/removefrombasket/${Id}`, { method: 'put' })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            updateUI(data);
        })
        .catch(error => {
            console.error('Error removing item from basket:', error);
        });
}
function updateUI(data) {

    console.log('Data received:', data);

    // Calculate total price
    const totalPrice = calculateTotalPrice(data);
    document.getElementById('totalPrice').textContent = totalPrice.toFixed(2) + '€';

        // Because data is an array, I have to loop through it to get each Id of the menuItem
    data.forEach(item => {

        var quantityInput = document.getElementById(`quantity_${item.productId}`);

        if (quantityInput) {
            quantityInput.value = item.quantity;
        } else {
            console.log('Element not found');
        }
    });
}

function calculateTotalPrice(data) {
    let totalPrice = 0;

    data.forEach(item => {
        totalPrice += item.menuPrice * item.quantity;
    });

    return totalPrice;
}