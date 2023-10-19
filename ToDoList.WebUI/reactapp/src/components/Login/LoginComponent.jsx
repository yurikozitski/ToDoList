import React from 'react';
import { NavLink, useNavigate } from 'react-router-dom';
import { useState } from 'react';

export function LoginComponent() {

    const [emailInput, emailInputHandler] = useState("");
    const [passwordInput, passwordInputHandler] = useState("");

    const navigate = useNavigate();

    function emailChange(e) {
        emailInputHandler(e.target.value);
    }

    function passwordChange(e) {
        passwordInputHandler(e.target.value);
    }

    async function handleSubmit(e) {
        e.preventDefault();

        const response = await fetch('https://localhost:44360/Account/Login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
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
                  <label htmlFor="email">Email:</label><br />
                  <input type="text" id="email" name="Email" onChange={emailChange} required />
              </p>
              <p>
                  <label htmlFor="password">Password:</label><br />
                  <input type="password" id="password" name="Password" onChange={passwordChange} required minLength="3" maxLength="20" />
              </p>
              <div className="button"><input type="submit" value="Log In" /></div>
              <p>Don't have an account? <NavLink to="/Register">Register</NavLink></p>
          </form>
      </div>
  );
}
