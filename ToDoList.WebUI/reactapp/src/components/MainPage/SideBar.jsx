import React from 'react';
import { NavLink, useNavigate } from 'react-router-dom';
import { useRef } from 'react';
import { useState } from 'react';
import { useEffect } from 'react';
import { BlackFilledCheckMark } from '../../svg/BlackFilledCheckMark';
import { ThemeToggler } from '../../svg/ThemeToggler';
import { Sun } from '../../svg/Sun';
import { Star } from '../../svg/Star';
import { Calendar } from '../../svg/Calendar';
import { EmptyCheckMark } from '../../svg/EmptyCheckMark';
import { ThreeLineList } from '../../svg/ThreeLineList';
import { Plus } from '../../svg/Plus';
import { RightArrow } from '../../svg/RightArrow';

export function SideBar(props) {

    const dropdownMenu = useRef(null);
    const addNewTaskList = useRef(null);

    const [addNewTaskListInput, addNewTaskListInputHandler] = useState("");
    const [userTaskLists, userTaskListsHandler] = useState([]);
    const [selectedTaskListId, setSelectedTaskListId] = useState(null);

    const navigate = useNavigate();

    useEffect(() => {
        fetchTaskLists();
    }, []);

    async function fetchTaskLists() {
        const token = localStorage.getItem("token");

        const response = await fetch('https://localhost:44360/Tasks/GetTaskLists', {
            method: 'GET',
            headers: {
                'Authorization': 'Bearer ' + token
            },
        });
        if (response.status === 200) {

            let taskLists = await response.json();

            userTaskListsHandler(taskLists);
        }
        else {
            alert('Error occured while fething your tasklists');
        }
    }

    async function fetchTasks(taskListID, taskListNAME) {
        const token = localStorage.getItem("token");

        const response = await fetch('https://localhost:44360/Tasks/GetUserTasksByTaskList?' + new URLSearchParams({taskListId: taskListID}),
        {
            method: 'GET',
            headers: {
                'Authorization': 'Bearer ' + token
            },
        });
        if (response.status === 200) {

            let tasks = await response.json();

            props.updateTasks(tasks);
            props.updateTaskListName(taskListNAME);
            props.updateTaskListId(taskListID);
        }
        else {
            alert('Error occured while fething your tasks');
        }
    }

    async function fetchAllTasks() {
        const token = localStorage.getItem("token");

        const response = await fetch('https://localhost:44360/Tasks/GetAllUserTasks',
            {
                method: 'GET',
                headers: {
                    'Authorization': 'Bearer ' + token
                },
            });
        if (response.status === 200) {

            let tasks = await response.json();

            props.updateTasks(tasks);
            props.updateTaskListName("All Tasks");
            props.updateTaskListId("all_tasks");
        }
        else {
            alert('Error occured while fething your tasks');
        }
    }

    async function fetchPlannedTasks() {
        const token = localStorage.getItem("token");

        const response = await fetch('https://localhost:44360/Tasks/GetPlannedTasks',
            {
                method: 'GET',
                headers: {
                    'Authorization': 'Bearer ' + token
                },
            });
        if (response.status === 200) {

            let tasks = await response.json();

            props.updateTasks(tasks);
            props.updateTaskListName("Planned");
            props.updateTaskListId("planned");
        }
        else {
            alert('Error occured while fething your tasks');
        }
    }

    async function fetchImportantTasks() {
        const token = localStorage.getItem("token");

        const response = await fetch('https://localhost:44360/Tasks/GetImportantTasks',
            {
                method: 'GET',
                headers: {
                    'Authorization': 'Bearer ' + token
                },
            });
        if (response.status === 200) {

            let tasks = await response.json();

            props.updateTasks(tasks);
            props.updateTaskListName("Important");
            props.updateTaskListId("important");
        }
        else {
            alert('Error occured while fething your tasks');
        }
    }

    function profileButtonPress() {
        dropdownMenu.current.className = "dropdown-content";
    };

    function profileButtonMouseOut() {
        dropdownMenu.current.className = "dropdown-content-closed";
    }

    function dropdownMenuMouseOut() {
        dropdownMenu.current.className = "dropdown-content-closed";
    }

    function dropdownMenuMouseOver() {
        dropdownMenu.current.className = "dropdown-content";
    }

    function addNewTaskListPress() {
        addNewTaskList.current.className = "inputTaskListName";
    }

    function inputTaskListMouseOut() {
        addNewTaskList.current.className = "inputTaskListName-hidden";
    }

    function inputTaskListMouseOver() {
        addNewTaskList.current.className = "inputTaskListName";
    }

    function addNewTaskListInputName(e) {
        addNewTaskListInputHandler(e.target.value);
    }

    function signOut() {
        localStorage.clear();
        props.updateTasks([]);
        navigate("/Login");
    }

   async function addNewUserTaskList(e) {
       if ((e.key === 'Enter') || (e.type === 'click')) {

           const token = localStorage.getItem("token");

           if (addNewTaskListInput.length < 3) {
               alert('Task list name is too short');
               return;
           }
           else if (addNewTaskListInput.length > 25) {
               alert('Task list name is too long');
               return;
           }
           else if (addNewTaskListInput === 'All Tasks' || addNewTaskListInput === 'Planned' || addNewTaskListInput === 'Important') {
               alert("Task list name should not be 'All Tasks','Planned' or'Important'");
               return;
           }

            const response = await fetch('https://localhost:44360/Tasks/AddTaskList', {
                method: 'POST',
                headers: {
                    'Authorization': 'Bearer ' + token,
                    'Content-Type':'application/json'
                },
                body: JSON.stringify({
                    "taskListName": addNewTaskListInput
                })
            });
           if (response.status === 200) {
                await fetchTaskLists();
                addNewTaskListInputHandler("");
            }
            else {
                alert('Error occured while adding new tasklist');
            }
       }
   }

    function themeToggleBtnPress() {
        if (document.documentElement.hasAttribute("theme")) {
            document.documentElement.removeAttribute("theme");
        }
        else {
            document.documentElement.setAttribute("theme", "dark");
        }
    }

    return (
        <div className="sideBar"
            style={{
                zIndex:
                    props.taskListId === "" ? 2 : 0,
            }}
        >
            <div className="logo-and-themeToggle">
                <BlackFilledCheckMark/>
				<span>ToDoList</span>
                <div className="themeToggleContainer">
                    <div className="themeToggle" id="themeToggle" onClick={themeToggleBtnPress}>
                        <ThemeToggler/>
                    </div>
                </div>
            </div>

            <div className="userblock">
                <div className="dropdown" id="profileButton" onClick={profileButtonPress} onMouseOut={profileButtonMouseOut} > 
                    <div className="threedot"></div>
                    <div className="dropdown-content-closed" ref={dropdownMenu} onMouseOut={dropdownMenuMouseOut} onMouseOver={dropdownMenuMouseOver} >
                        <div className="userLink">My profile</div>
                        <div className="userLink">Edit profile</div>
                        <div className="userLink" onClick={signOut}>Sign out</div>
                    </div>
                </div>
                <img src={`data:image/jpeg;base64,${localStorage.getItem("imageData")}`} alt="" />
                <h3>{localStorage.getItem("userName")}</h3>
            </div>

            <div className="defaultTaskLists">
                <div className="taskList">
                    <Sun/>
                    <span>My Day</span>
                </div>
                <div className="taskList"
                    onClick={() => { fetchImportantTasks(); setSelectedTaskListId("important"); }}
                    style={{
                        backgroundColor:
                            selectedTaskListId === "important" ? 'var(--btn-pressed-color)' : '',
                    }}
                >
                    <Star/>
                    <span>Important</span>
                </div>
                <div className="taskList"
                    onClick={() => { fetchPlannedTasks(); setSelectedTaskListId("planned"); }}
                    style={{
                        backgroundColor:
                            selectedTaskListId === "planned" ? 'var(--btn-pressed-color)' : '',
                    }}
                >
                    <Calendar/>
                    <span>Planned</span>

                </div>
                <div className="taskList"
                    onClick={() => { fetchAllTasks(); setSelectedTaskListId("all_tasks"); }}
                    style={{
                        backgroundColor:
                            selectedTaskListId === "all_tasks" ? 'var(--btn-pressed-color)' : '',
                    }}
                >
                    <EmptyCheckMark/>
                    <span>All Tasks</span>
                </div>
            </div>

            <div className="userTaskLists">
                {
                    userTaskLists.map((taskList) => (
                        <div className="taskList" key={taskList.id}
                            onClick={() => { fetchTasks(taskList.id, taskList.taskListName); setSelectedTaskListId(taskList.id); }}
                            style={{
                            backgroundColor:
                                    selectedTaskListId === taskList.id ? 'var(--btn-pressed-color)' : '', 
                            }}
                        >
                            <ThreeLineList/>
                            <span>{taskList.taskListName}</span>
                        </div>
                    ))
                } 
            </div>
            <div className="addNewTaskList" onClick={addNewTaskListPress}>
                <Plus/>
                <span>Add Task List</span>
                <div className="inputTaskListName-hidden" ref={addNewTaskList} onMouseOut={inputTaskListMouseOut} onMouseOver={inputTaskListMouseOver}>

                    <input type="text" value={addNewTaskListInput} onChange={addNewTaskListInputName} onKeyDown={addNewUserTaskList}></input>

                    <div className="addTaskListSVGContainer" onClick={addNewUserTaskList} >
                        <RightArrow/>
                    </div>

                </div>
            </div>
        </div>
    );
}