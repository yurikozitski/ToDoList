import React from 'react';
import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { connect } from 'react-redux';
import * as actions from './../actions/actions';
import { SideBar } from './../components/MainPage/SideBar';
import { TaskView } from './../components/MainPage/TaskView';
import './../styles/MainPage.css';

function MainPage(props) {

    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem("token");

        if (token === null) {
            navigate('/Login');
        }
        
    }, []);

    return (<>
        <SideBar {...props} />
        <TaskView {...props} />
    </>);
}

function mapStateToProps(state) {
    return {
        tasks: state.get("tasks"),
        taskListName: state.get("taskListName"),
    };
}

export default connect(mapStateToProps, actions)(MainPage);