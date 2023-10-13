import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import MainPage from './views/MainPage';
import Login from './views/Login';
import Register from './views/Register';

export default function App() {
    return (
        <Routes>
            <Route exact path='/' element={<MainPage />} />
            <Route exact path='/Login' element={<Login />} />
            <Route exact path='/Register' element={<Register />} />
        </Routes>
    );
}

