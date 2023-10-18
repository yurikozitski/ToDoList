import React from 'react';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export function RegisterComponent() {

    const [firstNameInput, firstNameInputHandler] = useState("");
    const [lastNameInput, lastNameInputHandler] = useState("");
    const [emailInput, emailInputHandler] = useState("");
    const [passwordInput, passwordInputHandler] = useState("");

    const navigate = useNavigate();

    function firstNameChange(e) {
        firstNameInputHandler(e.target.value);
    }

    function lastNameChange(e) {
        lastNameInputHandler(e.target.value);
    }

    function emailChange(e) {
        emailInputHandler(e.target.value);
    }

    function passwordChange(e) {
        passwordInputHandler(e.target.value);
    }

    async function handleSubmit(e) {
        e.preventDefault();

        const response = await fetch('https://localhost:44360/Account/Register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "FirstName": firstNameInput,
                "LastName": lastNameInput,
                "Email": emailInput,
                "Password": passwordInput
            })
        });
        if (response.status === 200) {

            let user = await response.json();

            localStorage.clear();

            localStorage.setItem("userName", user.fullName);
            localStorage.setItem("imagePath", user.imagePath);
            localStorage.setItem("token", user.token);

            navigate("/");
        }
        else {
            let mes = await response.json();
            alert(mes.ErrorMessage);
        }
    }

    return (
        <div className="Login">
            <form onSubmit={handleSubmit}>
                <p>
                    <label htmlFor="firstName">First Name:</label><br />
                    <input type="text" id="firstName" onChange={firstNameChange} required minLength="3" maxLength="20" />
                </p>
                <p>
                    <label htmlFor="lastName">Last Name:</label><br />
                    <input type="text" id="lastName" onChange={lastNameChange} required />
                </p>
                <p>
                    <label htmlFor="email">Email:</label><br />
                    <input type="text" id="email" name="Email" onChange={emailChange} required />
                </p>
                <p>
                    <label htmlFor="password">Password:</label><br />
                    <input type="password" id="password" name="Password" onChange={passwordChange} required minLength="3" maxLength="20" />
                </p>
                <p>
                    <label htmlFor="confirmpassword">Confirm Password:</label><br />
                    <input type="password" id="confirmpassword" name="ConfirmPassword" required minLength="3" maxLength="20" />
                </p>
                
                    <div className="button"><input type="submit" value="Register" /></div>
                
            </form>
        </div>
    );
}