/* Set rates + misc */
var taxRate = 0.05;
var shippingRate = 15.00;
var fadeTime = 300;

/* Assign actions */
$('.product-quantity input').change(function () {
    updateQuantity(this);
});
$('.product-removal button').click(function () {
    removeItem(this);
});


/* Recalculate cart */
function recalculateCart() {
    var subtotal = 0;

    /* Sum up row totals */
    $('.product-container').each(function () {
        subtotal += parseFloat($(this).children('.product-price').text());
    });
}