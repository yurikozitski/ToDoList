import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import store from './store/store';
import { Provider } from 'react-redux';
import MainPage from './views/MainPage';
import Login from './views/Login';
import Register from './views/Register';

store.dispatch({
    type: "SET_STATE",
    state: {
        tasks: [],
        taskListName: "",
        taskListId: ""
    }
});

export default function App() {
    return (
        <Routes>
            <Route exact path='/' element={<Provider store={store}><MainPage /></Provider>} />
            <Route exact path='/Login' element={<Login />} />
            <Route exact path='/Register' element={<Register />} />
        </Routes>
    );
}

