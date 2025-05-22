document.addEventListener("DOMContentLoaded", function () {
    updateCartBadge();

    function updateCartBadge() {
        const cart = JSON.parse(sessionStorage.getItem("cart")) || [];
        const totalCount = cart.reduce((sum, item) => sum + item.quantity, 0);
        const totalPrice = cart.reduce((sum, item) => sum + (item.quantity * item.price), 0);

        const cartCount = document.getElementById("cartCount");
        const cartTotal = document.getElementById("cartTotalPrice");

        if (cartCount) cartCount.innerText = totalCount;
        if (cartTotal) cartTotal.innerText = `€${totalPrice.toFixed(2)}`;
    }

    window.addToCart = function (productId, quantity = 1, name = '', price = 0) {
        let cart = JSON.parse(sessionStorage.getItem("cart")) || [];
        const existing = cart.find(item => item.id === productId);
        if (existing) {
            existing.quantity += quantity;
        } else {
            cart.push({ id: productId, quantity, name, price });
        }
        sessionStorage.setItem("cart", JSON.stringify(cart));
        updateCartBadge();
    };

    function loadCartModal() {
        const cart = JSON.parse(sessionStorage.getItem("cart")) || [];
        const container = document.getElementById("cartItems");
        container.innerHTML = "";

        if (cart.length === 0) {
            container.innerHTML = "<p>Je winkelmand is leeg.</p>";
            return;
        }

        const table = document.createElement("table");
        table.className = "table table-sm";

        table.innerHTML = `
            <thead>
                <tr>
                    <th>Product</th>
                    <th>Aantal</th>
                    <th>Prijs</th>
                    <th>Totaal</th>
                </tr>
            </thead>
            <tbody>
                ${cart.map(item => `
                    <tr>
                        <td>${item.name}</td>
                        <td>${item.quantity}</td>
                        <td>€${item.price.toFixed(2)}</td>
                        <td>€${(item.price * item.quantity).toFixed(2)}</td>
                    </tr>
                `).join("")}
            </tbody>
        `;

        container.appendChild(table);

        const totalPrice = cart.reduce((sum, item) => sum + item.quantity * item.price, 0);
        const totalDiv = document.createElement("div");
        totalDiv.className = "text-end fw-bold mt-3";
        totalDiv.innerText = `Totaal: €${totalPrice.toFixed(2)}`;
        container.appendChild(totalDiv);
    }

    const cartModal = document.getElementById('cartModal');
    if (cartModal) {
        cartModal.addEventListener('show.bs.modal', loadCartModal);
    }

    window.resetCart = function () {
        sessionStorage.removeItem("cart");
        updateCartBadge();
        const cartItems = document.getElementById("cartItems");
        if (cartItems) {
            cartItems.innerHTML = "<p>Je winkelmand is leeg.</p>";
        }
    };
});
