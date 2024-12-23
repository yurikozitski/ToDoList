import { jwtDecode } from 'jwt-decode'

export async function validateTokenAsync(token, navigate){
    var isExpired = false;
    var decodedToken = jwtDecode(token);
    var dateNow = new Date();

    if(decodedToken.exp * 1000 < dateNow.getTime()){
        isExpired = true;
    }

    if (isExpired){
        const url = import.meta.env.VITE_API_URL;
        const refreshToken = localStorage.getItem("refreshToken");
        
        const response = await fetch(url + '/Token/Refresh', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "token": token,
                "refreshToken": refreshToken
            })
        });
        
        if (response.status === 200) {

            const tokens = await response.json();

            localStorage.setItem("token", tokens.token);
            localStorage.setItem("refreshToken", tokens.refreshToken);
        }
        else {
            navigate("/Login");
            alert('Please relogin');
        }
    }
}