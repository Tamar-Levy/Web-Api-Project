//products
const productList = addEventListener("load", async () => {
    const shoppingBag = JSON.parse(sessionStorage.getItem("shoppingBag")) || []
    let itemsCountText = document.getElementById("ItemsCountText");
    itemsCountText.textContent = shoppingBag.length;
    drawProducts()
    drawCategories();
})

let urlString = "api/Products";
const drawProducts = async () => {//this function is get and needs to be called getProducts, you can have another func for draw.
    try {
        console.log(urlString)
        const getProducts = await fetch(urlString);
        const products = await getProducts.json()
        products.map(p => showOneProduct(p))//use forEach instead of map
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
const drawCategories = async () => {//this function is get and needs to be called getCategories, you can have another func for draw.
    try {
        const getCategories = await fetch(`api/Categories`);
        const categories = await getCategories.json()
        categories.map(c => showOneCategory(c))//use forEach instead of map
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
        selectedCategoryIds[selectedCategoryIds.length] = id;//push
    filterProducts()
}

//filter
const getFilterInputs = () => {
    document.getElementById("PoductList").innerHTML=''
    const minPrice = document.querySelector("#minPrice").value;
    const maxPrice = document.querySelector("#maxPrice").value;
    const nameSearch = document.querySelector("#nameSearch").value;
    return { minPrice, maxPrice, nameSearch };
}

const filterProducts = () =>//call it buildUrl or something like that...
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
    drawProducts()
}

//Add To Cart
const shoppingBag = JSON.parse(sessionStorage.getItem("shoppingBag")) || []
const addToCart = (product) => {
    shoppingBag.push(product)
    sessionStorage.setItem('shoppingBag', JSON.stringify(shoppingBag))
    let itemsCountText = document.getElementById("ItemsCountText");
    itemsCountText.textContent = shoppingBag.length;

}
