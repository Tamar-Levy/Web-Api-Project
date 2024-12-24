//products
const productList = addEventListener("load", async () => {
    drawProducts()
    drawCategories();

    //let categoryIdArr = [];
    //sessionStorage.setItem("categoryIds", JSON.stringify(categoryIdArr))
})

let urlString = "api/Products";
const drawProducts = async () => {
    try {
        const getProducts = await fetch(urlString);
        const products = await getProducts.json()
        products.map(p => showOneProduct(p))
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
const drawCategories = async () => {
    try {
        const getCategories = await fetch(`api/Categories`);
        const categories = await getCategories.json()
        categories.map(c => showOneCategory(c))
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
let categoriesId = []
const filterProductsByCategory = async (id) => {
    console.log(categoriesId)
    let index = categoriesId.indexOf(id);
    if (index != -1)
        categoriesId.splice(index, 1);
    else
        categoriesId[categoriesId.length] = id;
}

//filter
const getFilterInputs = () => {
    const minPrice = document.querySelector("#minPrice").value;
    const maxPrice = document.querySelector("#maxPrice").value;
    const nameSearch = document.querySelector("#nameSearch").value;
    return { minPrice, maxPrice, nameSearch };
}

const buildUrl = () => {

}

const filterProducts = async () =>
{
    const { minPrice, maxPrice, nameSearch } = getFilterInputs();
    if (minPrice || maxPrice || nameSearch || categoriesId != undefined) {
        urlString += '?'
        if (minPrice)
            urlString += `&minPrice=${minPrice}`
        if (maxPrice)
            urlString += `&maxPrice=${maxPrice}`
        if (nameSearch)
            urlString += `&name=${nameSearch}`
        if (categoriesId) {
            categoriesId.map(c => urlString += `&categoriesId=${c}`)
        }
    }
    drawProducts()
}