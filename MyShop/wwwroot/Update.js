const title = document.querySelector("#title")
title.textContent = `${JSON.parse(sessionStorage.getItem('user')).firstName} ${JSON.parse(sessionStorage.getItem('user')).lastName} you logged successfully!`

const toUpdate = () => {
    const update = document.querySelector("#update");
    update.style.visibility = 'visible';
}

const getDataFromForm = () => {
    const userName = document.querySelector("#userName").value;
    const password = document.querySelector("#password").value;
    const firstName = document.querySelector("#firstName").value;
    const lastName = document.querySelector("#lastName").value;
    return { userName, password, firstName, lastName };
}

const updateUser = async () => {
    const user = getDataFromForm();
    try {
        const responsePost = await fetch(`api/users/${JSON.parse(sessionStorage.getItem('user')).userId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        const data = await responsePost.json();
        console.log(data)
        if (!responsePost.ok) {
            if (responsePost.status === 400)
                alert(data.title)
            else
                alert(data.message || "Error, Please try again");
            return;
        }
        else {
            sessionStorage.setItem('user', JSON.stringify(data))
            alert(`${data.userName} updated`);
        }
    }
    catch (error) {
        throw error;
    }
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
        console.log(score.value);
    }
    catch (error) {
        throw error;
    }
}