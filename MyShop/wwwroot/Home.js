const getLoginDataFromForm = () => {
    const userName = document.querySelector("#loginUserName").value;
    const password = document.querySelector("#loginPassword").value;
    return { userName, password };
}

const userLogIn = async () => {
    const { userName, password } = getLoginDataFromForm();
    try {
        const responsePost = await fetch(`api/users/login?userName=${userName}&password=${password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!responsePost.ok)
            alert("User Not Found!")
        else {
            const data = await responsePost.json();
            alert(`${data.userName} logged in`);
            sessionStorage.setItem('user', JSON.stringify(data))
            window.location.href="./update.html"
            }
    }
    catch (error) {
        throw error;
    }
}

const toRegister = () => {
    const register = document.querySelector("#register");
    register.style.visibility = 'visible';
}

const getRegisterDataFromForm = () => { 
    const userName = document.querySelector("#userName").value;
    const password = document.querySelector("#password").value;
    const firstName = document.querySelector("#firstName").value;
    const lastName = document.querySelector("#lastName").value;

    const user = { userName, password, firstName, lastName };
    if (!validateUser(user)) {
        return;
    }
    return user;
}

const createUser = async () => {

    const user = getRegisterDataFromForm();
    if (!user)
        return;
    try {
        const responsePost = await fetch('api/users', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        const data = await responsePost.json();
        console.log(data)
        if (!responsePost.ok)
        {
            if (responsePost.status === 400)
                alert(data.title)
            else
                alert(data.message || "Error, Please try again");
            return; 
        }
        alert(`${data.userName} created`);
    }
    catch (error){
        throw error;
    }
}

 function validateUser(user) {
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(user.userName)) {
        alert("Invalid email address");
        return false;
    }

    if (user.firstName.length < 2 || user.firstName.length > 20) {
        alert("First Name can be between 2 to 20 characters");
        return false;
    }

    if (user.lastName.length < 2 || user.lastName.length > 20) {
        alert("Last Name can be between 2 to 20 characters");
        return false;
    }

    return true;
}

const checkPassword = async () => {
    const password = document.querySelector("#password").value;
    const score = document.querySelector("#score");
    try {
        const response = await fetch('api/users/password', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(password)
        });
        const data = await response.json();
        score.value = data;
    }
    catch (error) {
        throw error;
    }
}