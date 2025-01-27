//products
const productList = addEventListener("load", async () => {
    const shoppingBag = JSON.parse(sessionStorage.getItem("shoppingBag")) || []
    let itemsCountText = document.getElementById("ItemsCountText");
    itemsCountText.textContent = shoppingBag.length;
    getProducts()
    getCategories();
})

let urlString = "api/Products";
const getProducts = async () => {
    try {
        console.log(urlString)
        const getProducts = await fetch(urlString);
        const products = await getProducts.json()
        products.forEach(p => showOneProduct(p));
    }
    catch (error) {
        throw error;
    }
}

const showOneProduct = async (product) => {
    let tmp = document.getElementById("temp-card");
    let cloneProduct = tmp.content.cloneNode(true)
    if (product.image)
        cloneProduct.querySelector("img").src = `./Images/Products/${product.categoryCategoryName}/${product.image}.png`
    cloneProduct.querySelector("h1").textContent = product.name
    cloneProduct.querySelector(".price").innerText = product.price
    cloneProduct.querySelector(".description").innerText = product.description
    cloneProduct.querySelector("button").addEventListener('click', () => { addToCart(product) })
    document.getElementById("PoductList").appendChild(cloneProduct)
}

//categories
const getCategories= async () => {
    try {
        const getCategories = await fetch(`api/Categories`);
        const categories = await getCategories.json()
        categories.forEach(c => showOneCategory(c));
    }
    catch (error) {
        throw error;
    }
}

const showOneCategory = async (category) => {
    let tmp = document.getElementById("temp-category");
    let cloneCategory = tmp.content.cloneNode(true)
    cloneCategory.querySelector(".OptionName").textContent = category.categoryName
    cloneCategory.querySelector(".opt").addEventListener('change', () => { filterProductsByCategory(category.categoryId) }) 
    document.getElementById("CategoryList").appendChild(cloneCategory)
}

//filterings

//by category
let selectedCategoryIds = []
const filterProductsByCategory = async (id) => {
    console.log(selectedCategoryIds)
    let index = selectedCategoryIds.indexOf(id);
    if (index != -1)
        selectedCategoryIds.splice(index, 1);
    else
        selectedCategoryIds.push(id);
    buildUrl()
}

//filter
const getFilterInputs = () => {
    document.getElementById("PoductList").innerHTML=''
    const minPrice = document.querySelector("#minPrice").value;
    const maxPrice = document.querySelector("#maxPrice").value;
    const nameSearch = document.querySelector("#nameSearch").value;
    return { minPrice, maxPrice, nameSearch };
}

const buildUrl= () =>
{
    urlString = "api/Products";
    const { minPrice, maxPrice, nameSearch } = getFilterInputs();
    if (minPrice || maxPrice || nameSearch || selectedCategoryIds ) {
        urlString += '?'
        if (minPrice)
            urlString += `&minPrice=${minPrice}`
        if (maxPrice)
            urlString += `&maxPrice=${maxPrice}`
        if (nameSearch)
            urlString += `&name=${nameSearch}`
        if (selectedCategoryIds) {
            selectedCategoryIds.map(c => urlString += `&categoriesId=${c}`)
        }
    }
    getProducts()
}

//Add To Cart
const shoppingBag = JSON.parse(sessionStorage.getItem("shoppingBag")) || []
const addToCart = (product) => {
    shoppingBag.push(product)
    sessionStorage.setItem('shoppingBag', JSON.stringify(shoppingBag))
    let itemsCountText = document.getElementById("ItemsCountText");
    itemsCountText.textContent = shoppingBag.length;

}
