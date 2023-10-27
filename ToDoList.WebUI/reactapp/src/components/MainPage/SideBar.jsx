import React from 'react';
import { NavLink, useNavigate } from 'react-router-dom';
import { useRef } from 'react';
import { useState } from 'react';
import { useEffect } from 'react';
import { render } from 'react-dom';

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
        navigate("/Login");
    }

   async function addNewUserTaskList(e) {
       if ((e.key === 'Enter') || (e.type === 'click')) {

           const token = localStorage.getItem("token");

           if (addNewTaskListInput.length < 3) {
               alert('Task list name is too short');
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
        <div className="sideBar">
            <div className="logo-and-themeToggle">
				<svg height="30px" width="30px" version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink"
					viewBox="0 0 32 32" xmlSpace="preserve">
					<g>
						<g id="check">
							<g>
								<polygon  points="11.941,28.877 0,16.935 5.695,11.24 11.941,17.486 26.305,3.123 32,8.818" />
							</g>
						</g>
					</g>
				</svg>
				<span>ToDoList</span>
                <div className="themeToggleContainer">
                    <div className="themeToggle" id="themeToggle" onClick={themeToggleBtnPress}>
                        <svg viewBox="0 0 24 24" version="1.1" fill="#000000">
                            <path d="M12,22 C17.5228475,22 22,17.5228475 22,12 C22,6.4771525 17.5228475,
                              2 12,2 C6.4771525,2 2,6.4771525 2,12 C2,17.5228475 6.4771525,22 12,
                              22 Z M12,20 L12,4 C16.418278,4 20,7.581722 20,12 C20,16.418278 16.418278,
                              20 12,20 Z" id="Color">
                            </path>
                        </svg>
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
                <img src="C:\MyApps\NatterLite\wwwroot\Images\profilepicexample.png" alt="" />
                <h3>Matt Jackson</h3>
            </div>

            <div className="defaultTaskLists">
                <div className="taskList">
                        <svg fill="#000000" height="30px" width="30px" version="1.1" id="Capa_1"
                            xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink"
                            viewBox="0 0 511 511" xmlSpace="preserve"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                            <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                            <g id="SVGRepo_iconCarrier"> <g>
                                <path d="M255.5,144C194.019,144,144,194.019,144,255.5S194.019,367,255.5,367S367,316.981,367,255.5S316.981,144,255.5,144z M255.5,
                                352c-53.21,0-96.5-43.29-96.5-96.5s43.29-96.5,96.5-96.5s96.5,43.29,96.5,96.5S308.71,352,255.5,352z"></path>
                                <path d="M255.5,119c4.142,0,7.5-3.357,7.5-7.5V7.5c0-4.143-3.358-7.5-7.5-7.5S248,3.357,248,7.5v104 C248,115.643,251.358,119,255.5,119z"></path>
                                <path d="M255.5,392c-4.142,0-7.5,3.357-7.5,7.5v104c0,4.143,3.358,7.5,7.5,7.5s7.5-3.357,7.5-7.5v-104 C263,395.357,259.642,392,255.5,392z"></path>
                                <path d="M503.5,248h-104c-4.142,0-7.5,3.357-7.5,7.5s3.358,7.5,7.5,7.5h104c4.142,0,7.5-3.357,7.5-7.5S507.642,248,503.5,248z"></path>
                                <path d="M119,255.5c0-4.143-3.358-7.5-7.5-7.5H7.5c-4.142,0-7.5,3.357-7.5,7.5s3.358,7.5,7.5,7.5h104 C115.642,263,119,259.643,119,255.5z"></path>
                                <path d="M357.323,161.177c1.919,0,3.839-0.732,5.303-2.196l73.539-73.539c2.929-2.93,2.929-7.678,0-10.607 c-2.929-2.928-7.678-2.928-10.606,0l-73.539,
                                73.539c-2.929,2.93-2.929,7.678,0,10.607 C353.484,160.444,355.404,161.177,357.323,161.177z"></path>
                                <path d="M148.374,352.02l-73.539,73.539c-2.929,2.93-2.929,7.678,0,10.607c1.464,1.464,3.384,2.196,5.303,2.196 s3.839-0.732,5.303-2.196l73.539-73.539c2.929-2.93,
                                2.929-7.678,0-10.607C156.051,349.092,151.302,349.092,148.374,352.02z"></path>
                                <path d="M148.374,158.98c1.464,1.464,3.384,2.196,5.303,2.196s3.839-0.732,5.303-2.196c2.929-2.93,2.929-7.678,0-10.607 L85.441,
                                74.834c-2.929-2.928-7.678-2.928-10.606,0c-2.929,2.93-2.929,7.678,0,10.607L148.374,158.98z"></path>
                                <path d="M362.626,352.02c-2.929-2.928-7.678-2.928-10.606,0c-2.929,2.93-2.929,7.678,0,10.607l73.539,73.539 c1.464,1.464,
                                3.384,2.196,5.303,2.196s3.839-0.732,5.303-2.196c2.929-2.93,2.929-7.678,0-10.607L362.626,352.02z"></path> </g> </g>
                        </svg>
                        <span>My Day</span>
                </div>
                <div className="taskList"
                    onClick={() => { fetchImportantTasks(); setSelectedTaskListId("important"); }}
                    style={{
                        backgroundColor:
                            selectedTaskListId === "important" ? 'var(--btn-pressed-color)' : '',
                    }}
                >

                        <svg fill="#000000" height="25px" width="25px" version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg"
                            xmlnsXlink="http://www.w3.org/1999/xlink" viewBox="0 0 49.94 49.94" xmlSpace="preserve"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                            <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier">
                                <path d="M48.856,22.731c0.983-0.958,1.33-2.364,0.906-3.671c-0.425-1.307-1.532-2.24-2.892-2.438l-12.092-1.757 c-0.515-0.075-0.96-0.398-1.19-0.865L28.182,
                                3.043c-0.607-1.231-1.839-1.996-3.212-1.996c-1.372,0-2.604,0.765-3.211,1.996 L16.352,14c-0.23,0.467-0.676,0.79-1.191,0.865L3.069,16.623C1.71,16.82,0.603,
                                17.753,0.178,19.06 c-0.424,1.307-0.077,2.713,0.906,3.671l8.749,8.528c0.373,0.364,0.544,0.888,0.456,1.4L8.224,44.702 c-0.232,1.353,0.313,2.694,1.424,3.502c1.11,0.809,2.555,
                                0.914,3.772,0.273l10.814-5.686c0.461-0.242,1.011-0.242,1.472,0 l10.815,5.686c0.528,0.278,1.1,0.415,1.669,0.415c0.739,0,1.475-0.231,2.103-0.688c1.111-0.808,1.656-2.149,
                                1.424-3.502 L39.651,32.66c-0.088-0.513,0.083-1.036,0.456-1.4L48.856,22.731z M37.681,32.998l2.065,12.042c0.104,0.606-0.131,1.185-0.629,1.547 c-0.499,0.361-1.12,0.405-1.665,
                                0.121l-10.815-5.687c-0.521-0.273-1.095-0.411-1.667-0.411s-1.145,0.138-1.667,0.412l-10.813,5.686 c-0.547,0.284-1.168,
                                0.24-1.666-0.121c-0.498-0.362-0.732-0.94-0.629-1.547l2.065-12.042c0.199-1.162-0.186-2.348-1.03-3.17 L2.48,21.299c-0.441-0.43-0.591-1.036-0.4-1.621c0.19-0.586,
                                0.667-0.988,1.276-1.077l12.091-1.757 c1.167-0.169,2.176-0.901,2.697-1.959l5.407-10.957c0.272-0.552,0.803-0.881,1.418-0.881c0.616,0,1.146,0.329,1.419,0.881 l5.407,10.957c0.521,
                                1.058,1.529,1.79,2.696,1.959l12.092,1.757c0.609,0.089,1.086,0.491,1.276,1.077 c0.19,0.585,0.041,1.191-0.4,1.621l-8.749,8.528C37.866,30.65,37.481,31.835,37.681,32.998z"></path>
                            </g></svg>
                        <span>Important</span>

                </div>
                <div className="taskList"
                    onClick={() => { fetchPlannedTasks(); setSelectedTaskListId("planned"); }}
                    style={{
                        backgroundColor:
                            selectedTaskListId === "planned" ? 'var(--btn-pressed-color)' : '',
                    }}
                >

                        <svg fill="#000000" height="25px" width="25px" version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink"
                            viewBox="0 0 64 64" enableBackground="new 0 0 64 64" xmlSpace="preserve"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                            <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"> <g id="Calendar">
                                <path d="M60.0002518,6.0037999h-15V1c0-0.5527-0.4472008-1-1-1c-0.5527,0-1,0.4473-1,1v5.0037999H22.0002499V1 c0-0.5527-0.4472008-1-1-1c-0.5527,0-1,
                                0.4473-1,1v5.0037999H3.9997499c-2.2090998,0-4,1.7908001-4,4.0000005V60 c0,2.2090988,1.7909,4,4,4h56.0005035c2.209198,0,4-1.7909012,4-4V10.0038004 C64.0002518,7.7946,62.2094498,
                                6.0037999,60.0002518,6.0037999z M3.9997499,8.0038004h16.0004997V11c0,0.5527,0.4473,1,1,1 c0.5527992,0,1-0.4473,1-1V8.0038004h21.0000019V11c0,0.5527,
                                0.4473,1,1,1c0.5527992,0,1-0.4473,1-1V8.0038004h15 c1.1027985,0,2,0.8970995,2,2V17h-60L1.99975,17.0000992v-6.9962988C1.99975,8.9008999,2.8970499,8.0038004,3.9997499,
                                8.0038004z M62.0002518,60c0,1.1027985-0.8972015,2-2,2H3.9997499c-1.1027,0-1.9999999-0.8972015-1.9999999-2V18.9999008L2.0002501,19h60V60z "></path>
                                <path d="M18.0002499,24h-8v8h8V24z M16.0002499,30h-4v-4h4V30z"></path>
                                <path d="M36.0002518,24h-8.0000019v8h8.0000019V24z M34.0002518,30h-4.0000019v-4h4.0000019V30z"></path>
                                <path d="M54.0002518,24h-8v8h8V24z M52.0002518,30h-4v-4h4V30z"></path>
                                <path d="M18.0002499,36h-8v8h8V36z M16.0002499,42h-4v-4h4V42z"></path>
                                <path d="M36.0002518,36h-8.0000019v8h8.0000019V36z M34.0002518,42h-4.0000019v-4h4.0000019V42z"></path>
                                <path d="M18.0002499,48h-8v8h8V48z M16.0002499,54h-4v-4h4V54z"></path>
                                <path d="M36.0002518,48h-8.0000019v8h8.0000019V48z M34.0002518,54h-4.0000019v-4h4.0000019V54z"></path>
                                <path d="M54.0002518,36h-8v8h8V36z M52.0002518,42h-4v-4h4V42z"></path>
                                <path d="M54.0002518,48h-8v8h8V48z M52.0002518,54h-4v-4h4V54z"></path>
                            </g> </g>
                        </svg>
                        <span>Planned</span>

                </div>
                <div className="taskList"
                    onClick={() => { fetchAllTasks(); setSelectedTaskListId("all_tasks"); }}
                    style={{
                        backgroundColor:
                            selectedTaskListId === "all_tasks" ? 'var(--btn-pressed-color)' : '',
                    }}
                >

                        <svg fill="#000000" height="25px" width="25px" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                            <g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                            <g id="SVGRepo_iconCarrier"><path d="M8.591,20.706a1,1,0,0,0,1.385.03l12.7-11.674a1,1,0,0,0,.061-1.412L18.785,
                            3.325a1,1,0,0,0-1.415-.061l-7.865,7.23L6.844,7.827a1.03,1.03,0,0,0-1.416,0L1.292,11.974a1,1,0,0,0,0,1.412ZM6.136,9.949l2.631,
                            2.637a1,1,0,0,0,1.384.031l7.834-7.2,2.6,2.849L9.33,18.614,3.412,12.68Z"></path></g>
                        </svg>
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
                            
                            <svg viewBox="0 0 24 24" fill="none" height="30px" width="30px" xmlns="http://www.w3.org/2000/svg">
                                <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                                <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                                <g id="SVGRepo_iconCarrier">
                                    <path d="M5 6H12H19M5 12H19M5 18H19" stroke="var(--svg-icon-color)" strokeWidth="0.72" strokeLinecap="round"></path>
                                </g>
                            </svg>
                            <span>{taskList.taskListName}</span>
                            
                        </div>
                    ))
                } 
            </div>
            <div className="addNewTaskList" onClick={addNewTaskListPress}>
                <svg viewBox="0 0 24 24"  height="40px" width="40px" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                    <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier">
                        <path d="M12 6V18" stroke="var(--svg-icon-color)" strokeLinecap="round" strokeLinejoin="round">
                        </path> <path d="M6 12H18" stroke="var(--svg-icon-color)" strokeLinecap="round" strokeLinejoin="round"></path>
                    </g>
                </svg>
                <span>Add Task List</span>
                <div className="inputTaskListName-hidden" ref={addNewTaskList} onMouseOut={inputTaskListMouseOut} onMouseOver={inputTaskListMouseOver}>

                    <input type="text" value={addNewTaskListInput} onChange={addNewTaskListInputName} onKeyDown={addNewUserTaskList}></input>

                    <div className="addTaskListSVGContainer" onClick={addNewUserTaskList} >
                        <svg viewBox="0 0 48 48"  xmlns="http://www.w3.org/2000/svg" stroke="#000000" strokeWidth="0.00048000000000000007">
                            <g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                            <g id="SVGRepo_iconCarrier">
                                <path d="M16.1161 39.6339C15.628 39.1457 15.628 38.3543 16.1161 37.8661L29.9822 24L16.1161 10.1339C15.628 9.64573 15.628 8.85427 
                                16.1161 8.36612C16.6043 7.87796 17.3957 7.87796 17.8839 8.36612L32.6339 23.1161C33.122 23.6043 33.122 24.3957 32.6339 24.8839L17.8839
                                39.6339C17.3957 40.122 16.6043 40.122 16.1161 39.6339Z" ></path> </g>
                        </svg>
                    </div>

                </div>
            </div>
        </div>
    );
}