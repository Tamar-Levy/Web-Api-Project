let shoppingBag = JSON.parse(sessionStorage.getItem("shoppingBag")) || []
const productList = addEventListener("load", async () => {
    drawProducts();
})

const drawProducts = () => {
    document.getElementById("items").innerHTML = ''
    let itemCount = document.getElementById("itemCount")
    itemCount.textContent = shoppingBag.length;
    let totalSum = 0;
    shoppingBag.forEach(p => totalSum+=p.price)
    let totalAmount = document.getElementById("totalAmount")
    totalAmount.textContent = totalSum;
    shoppingBag.map(p => showOneProduct(p))
} 
const showOneProduct = (product) => {
    let tmp = document.getElementById("temp-row");
    let cloneProduct = tmp.content.cloneNode(true)
    if (product.image) {
        const url = `../Images/Products/${product.categoryCategoryName}/${product.image}.png`
        cloneProduct.querySelector('.image').style.backgroundImage = `url(${url})`
    }
    cloneProduct.querySelector(".price").innerText = product.price + " ₪"  
    cloneProduct.querySelector(".itemName").innerText = product.productName
    cloneProduct.querySelector(".DeleteButton").addEventListener('click', () => { deleteFromCart(product) })
    document.getElementById("items").appendChild(cloneProduct)
}

const deleteFromCart = (product) => {
    let index = shoppingBag.indexOf(product);
    if (index != -1)
        shoppingBag.splice(index, 1);
    sessionStorage.setItem('shoppingBag', JSON.stringify(shoppingBag))
    drawProducts()
}

const sumQuantities = () => {
    return Object.values(shoppingBag.reduce((totalQuantities, product) => {
        // אם המוצר כבר קיים במערך הסיכום, הוסף את הכמות
        if (totalQuantities[product.productId]) {
            totalQuantities[product.productId].quantity += 1;
        } else {
            // אם המוצר לא קיים במערך, הוסף אותו עם הכמות הראשונה
            totalQuantities[product.productId] = {
                productId: product.productId,
                quantity: 1
            };
        }
        return totalQuantities;
    }, {}));
}

const placeOrder = async () => { 
    user = JSON.parse(sessionStorage.getItem("user")) || null
    products = []
    shoppingBag.forEach((item) => {
        products.push({ productId: item.productId , quantity:1 })
    });
    if (!user)
        window.location.href = 'home.html'
    else {
        const order = {
            "orderDate": new Date().toISOString().split('T')[0],
            "orderSum": document.querySelector("#totalAmount").textContent,
            "userId": user.userId,
            "orderItems": sumQuantities()
        }
        try {
            const responsePost = await fetch('api/orders', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(order)
            });
            if (!responsePost.ok)
                alert("Error, Please try again")
            else
            { 
                const data = await responsePost.json();
                console.log(data)
                alert(`Order ${data.orderId} was placed successfully`);
            }
        }
        catch (error) {
            throw error;
        }
    }
}