import React from 'react';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export function RegisterComponent() {

    const [firstNameInput, firstNameInputHandler] = useState("");
    const [lastNameInput, lastNameInputHandler] = useState("");
    const [emailInput, emailInputHandler] = useState("");
    const [imageInput, imageInputHandler] = useState("");
    const [passwordInput, passwordInputHandler] = useState("");
    const [confirmPasswordInput, confirmPasswordInputHandler] = useState("");

    const url = import.meta.env.VITE_API_URL;

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

    function imageChange(e) {
        imageInputHandler(e.target.files[0]);
    }

    function passwordChange(e) {
        passwordInputHandler(e.target.value);
    }

    function confirmPasswordChange(e) {
        confirmPasswordInputHandler(e.target.value);
    }

    async function handleSubmit(e) {
        e.preventDefault();

        if (passwordInput !== confirmPasswordInput) {
            alert('Confirm your password');
            return;
        }

        let registerForm = new FormData();
        registerForm.append('FirstName', firstNameInput);
        registerForm.append('LastName', lastNameInput);
        registerForm.append('Email', emailInput);
        registerForm.append('Image', imageInput);
        registerForm.append('Password', passwordInput);

        const response = await fetch(url + '/Account/Register', {
            method: 'POST',
            body: registerForm
        });
        if (response.status === 200) {

            let user = await response.json();

            localStorage.clear();

            localStorage.setItem("userName", user.fullName);
            localStorage.setItem("imageData", user.imageData);
            localStorage.setItem("token", user.token);
            localStorage.setItem("refreshToken", user.refreshToken);

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
                    <input type="text" id="firstName" onChange={firstNameChange} required minLength="2" maxLength="20" />
                </p>
                <p>
                    <label htmlFor="lastName">Last Name:</label><br />
                    <input type="text" id="lastName" onChange={lastNameChange} required minLength="2" maxLength="20" />
                </p>
                <p>
                    <label htmlFor="email">Email:</label><br />
                    <input type="text" id="email" name="Email" onChange={emailChange} required />
                </p>
                <p>
                    <label htmlFor="image">Profile Picture:</label><br />
                    <label className="custom-file-upload">
                        <input type="file" name="Image" onChange={imageChange} accept=".jpg, .jpeg, .png" />
                        {imageInput === "" || imageInput === undefined ? "Choose file" : imageInput.name.slice(0, 18)}
                    </label>
                </p>
                <p>
                    <label htmlFor="password">Password:</label><br />
                    <input type="password" id="password" name="Password" onChange={passwordChange} required minLength="8" maxLength="50" />
                </p>
                <p>
                    <label htmlFor="confirmpassword">Confirm Password:</label><br />
                    <input type="password" id="confirmpassword" name="ConfirmPassword" onChange={confirmPasswordChange} required minLength="8" maxLength="50" />
                </p>
                
                    <div className="button"><input type="submit" value="Register" /></div>
                
            </form>
        </div>
    );
}