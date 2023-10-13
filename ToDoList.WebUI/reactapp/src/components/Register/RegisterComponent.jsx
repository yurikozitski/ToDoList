import React from 'react';

export function RegisterComponent() {
    return (
        <div className="Login">
            <form method="post" action="~/Account/Register">
                <p>
                    <label htmlFor="name">Name:</label><br />
                    <input type="text" id="name" name="Name" required minLength="3" maxLength="20" />
                </p>
                <p>
                    <label htmlFor="email">Email:</label><br />
                    <input type="text" id="email" name="Email" required />
                </p>
                <p>
                    <label htmlFor="phone">Phone:</label><br />
                    <input type="text" id="phone" name="PhoneNumber" required pattern="[0-9]{10}" />
                </p>
                <p>
                    <label htmlFor="password">Password:</label><br />
                    <input type="password" id="password" name="Password" required minLength="3" maxLength="20" />
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