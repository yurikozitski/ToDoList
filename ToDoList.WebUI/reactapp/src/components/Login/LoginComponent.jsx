import React from 'react';
import { NavLink } from 'react-router-dom';

export function LoginComponent() {
  return (
      <div className="Login">
          <form>
              <p>
                  <label htmlFor="email">Email:</label><br />
                  <input type="text" id="email" name="Email" required />
              </p>
              <p>
                  <label htmlFor="password">Password:</label><br />
                  <input type="password" id="password" name="Password" required minLength="3" maxLength="20" />
              </p>
                  <div className="button"><input type="submit" value="Log In" /></div>
              <p>Don't have an account? <NavLink to="/Register">Register</NavLink></p>
          </form>
      </div>
  );
}
