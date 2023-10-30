import React from 'react';
import { useState } from 'react';


export function TaskView(props) {

    const [manipulatedTaskId, manipulatedTaskIdHandler] = useState();
    const [addNewTaskText, addNewTaskTextHandler] = useState("");

    function addNewTaskInput(e) {
        addNewTaskTextHandler(e.target.value);
    }

    async function addNewUserTask(e) {
        if ((e.key === 'Enter') || (e.type === 'click')) {

            const token = localStorage.getItem("token");

            if (addNewTaskText.length > 60) {
                alert('Task description is too long');
                return;
            }
            else if (addNewTaskText.length < 2){
                alert('Task description is too short');
                return;
            }

            const response = await fetch('https://localhost:44360/Tasks/AddUserTask', {
                method: 'POST',
                headers: {
                    'Authorization': 'Bearer ' + token,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    "text": addNewTaskText,
                    "taskListId": props.taskListId
                })
            });
            if (response.status === 200) {
                const response = await fetch('https://localhost:44360/Tasks/GetUserTasksByTaskList?' + new URLSearchParams({ taskListId: props.taskListId }),
                    {
                        method: 'GET',
                        headers: {
                            'Authorization': 'Bearer ' + token
                        },
                    });
                if (response.status === 200) {

                    let tasks = await response.json();

                    props.updateTasks(tasks);
                }
                else {
                    alert('Error occured while fething your tasks');
                }
                addNewTaskTextHandler("");
            }
            else {
                alert('Error occured while adding new task');
            }
        }
    }

    async function setPlannedTime(e) {

        const token = localStorage.getItem("token");

        const time = e.type == "click" ? '0001-01-01' : e.target.value

        const response = await fetch('https://localhost:44360/Tasks/AddUserTaskPlannedTime', {
            method: 'PATCH',
            headers: {
                'Authorization': 'Bearer ' + token,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "Time": time + 'T00:00:00',
                "UserTaskId": manipulatedTaskId
            })
        });
        if (response.status === 200) {

            let updatedTasks;

            if (props.taskListName === "Planned" && time === '0001-01-01') {
                updatedTasks = props.tasks.filter(task => task.id != manipulatedTaskId);
            }
            else {
                updatedTasks = props.tasks.map((task) => {
                    if (task.id === manipulatedTaskId) {
                        return { ...task, plannedTime: time + 'T00:00:00' };
                    }
                    return task;
                });
            }

            props.updateTasks(updatedTasks);
        }
        else {
            alert('Error occured while adding planned time');
        }
    }

    async function markTaskAsDone(taskId){
        const token = localStorage.getItem("token");

        const response = await fetch('https://localhost:44360/Tasks/MarkUserTaskAsDone', {
            method: 'PATCH',
            headers: {
                'Authorization': 'Bearer ' + token,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "UserTaskId": taskId
            })
        });
        if (response.status === 200) {

            let updatedTasks = props.tasks.map((task) => {
                if (task.id === taskId) {
                    return { ...task, isDone: task.isDone ? false : true };
                }
                return task;
            });
            props.updateTasks(updatedTasks);
        }
        else {
            alert('Error occured while marking task as done');
        }
    }

    async function markTaskAsImportant(taskId) {
        const token = localStorage.getItem("token");

        const response = await fetch('https://localhost:44360/Tasks/MarkUserTaskAsImportant', {
            method: 'PATCH',
            headers: {
                'Authorization': 'Bearer ' + token,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "UserTaskId": taskId
            })
        });
        if (response.status === 200) {

            let updatedTasks;

            if (props.taskListName === "Important") {
                updatedTasks = props.tasks.filter(task => task.id != taskId);
            }
            else {
                updatedTasks = props.tasks.map((task) => {
                    if (task.id === taskId) {
                        return { ...task, isImportant: task.isImportant == true ? false : true };
                    }
                    return task;
                });
            }
            props.updateTasks(updatedTasks);
        }
        else {
            alert('Error occured while marking task as important');
        }
    }

    return (
        <div className="taskView">
            {
                props.taskListName === "" ? (<div className="emptyTaskView">Click on the task list to the left to see your tasks here.</div>)
                    : (<div className="taskListNameBlock">{props.taskListName}</div>)
            }
            {
                props.tasks.length === 0 && props.taskListName!="" ? (<div className="emptyTaskView">There are no tasks in this task list.</div>)
                    : null
            }
            {props.tasks.map(task =>
                !task.isDone ? (
                    <div className="userTask" key={task.id}>
                    <div className="doneMark" onClick={() => markTaskAsDone(task.id)}>
                        <svg viewBox="0 0 24 24" height="20px" width="20px" xmlns="http://www.w3.org/2000/svg" fill="#000000">
                            <g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round">
                            </g><g id="SVGRepo_iconCarrier"> <rect x="0" fill="none" width="24" height="24"></rect> <g>
                                <path d="M9 19.414l-6.707-6.707 1.414-1.414L9 16.586 20.293 5.293l1.414 1.414"></path>
                            </g>
                            </g>
                        </svg>
                    </div>
                        <div className="userTaskText">{task.text}</div>
                        <div className="importantMark" onClick={() => markTaskAsImportant(task.id)} style={{
                            backgroundColor:
                                task.isImportant==true ? 'var(--btn-pressed-color)' : '',
                        }}>
                            <svg fill="#000000" height="30px" width="30px" version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg"
                                xmlnsXlink="http://www.w3.org/1999/xlink" viewBox="0 0 526.673 526.673" xmlSpace="preserve"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                                <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                                <g id="SVGRepo_iconCarrier"> <g> <g> <path d="M526.673,204.221l-195.529-7.76L263.337,12.885l-67.798,183.577L0,204.221l153.635,121.202l-53.048,
                                188.365 l162.75-108.664l162.75,108.664l-53.048-188.365L526.673,204.221z M392.683,467.808l-129.346-86.356L133.99,467.808 l42.163-149.692L54.058,221.779l155.404-6.163l53.875-145.885l53.885,
                                145.885l155.394,6.163l-122.096,96.337L392.683,467.808z"></path> </g> </g> </g>
                            </svg>
                        </div>
                        <div className="plannedTime" onClick={() => manipulatedTaskIdHandler(task.id)}>
                            <div className={
                                task.plannedTime == '0001-01-01T00:00:00' ? "removeDate-hidden" : "removeDate"
                            } onClick={setPlannedTime}>
                                <svg fill="#000000" height="20px" width="20px" viewBox="0 0 32 32" version="1.1" xmlns="http://www.w3.org/2000/svg">
                                    <g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round">
                                    </g><g id="SVGRepo_iconCarrier"> <title>remove</title>
                                        <path d="M11.188 4.781c6.188 0 11.219 5.031 11.219 11.219s-5.031 11.188-11.219 11.188-11.188-5-11.188-11.188 5-11.219 
                                            11.188-11.219zM11.25 17.625l3.563 3.594c0.438 0.438 1.156 0.438 1.594 0 0.406-0.406 0.406-1.125 0-1.563l-3.563-3.594
                                            3.563-3.594c0.406-0.438 0.406-1.156 0-1.563-0.438-0.438-1.156-0.438-1.594 0l-3.563 3.594-3.563-3.594c-0.438-0.438-1.156-0.438-1.594 0-0.406 0.406-0.406 1.125 0
                                            1.563l3.563 3.594-3.563 3.594c-0.406 0.438-0.406 1.156 0 1.563 0.438 0.438 1.156 0.438 1.594 0z">
                                        </path>
                                    </g>
                                </svg>
                            </div>
                            <div className="date">{task.plannedTime == '0001-01-01T00:00:00' ? '' : task.plannedTime.slice(0, -9)}</div>
                            <input type="date" onChange={setPlannedTime} />
                        </div>
                    </div>
                ) : null
            )}
            {props.tasks.find(task => task.isDone) ? (
                <div className="doneTasksHeader">Done</div>
            ) : null}
            {props.tasks.map(task =>
                task.isDone ? (
                    <div className="userTask" key={task.id}>
                        <div className="doneMark" style={{ backgroundColor: 'var(--btn-pressed-color)' }} onClick={() => markTaskAsDone(task.id)}>
                            <svg viewBox="0 0 24 24" height="20px" width="20px" xmlns="http://www.w3.org/2000/svg" fill="#000000">
                                <g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round">
                                </g><g id="SVGRepo_iconCarrier"> <rect x="0" fill="none" width="24" height="24"></rect> <g>
                                    <path d="M9 19.414l-6.707-6.707 1.414-1.414L9 16.586 20.293 5.293l1.414 1.414"></path>
                                </g>
                                </g>
                            </svg>
                        </div>
                        <div className="userTaskText"><del>{task.text}</del></div>
                        <div className="importantMark" onClick={() => markTaskAsImportant(task.id)} style={{
                            backgroundColor:
                                task.isImportant == true ? 'var(--btn-pressed-color)' : '',
                        }}>
                            <svg fill="#000000" height="30px" width="30px" version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg"
                                xmlnsXlink="http://www.w3.org/1999/xlink" viewBox="0 0 526.673 526.673" xmlSpace="preserve"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                                <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                                <g id="SVGRepo_iconCarrier"> <g> <g> <path d="M526.673,204.221l-195.529-7.76L263.337,12.885l-67.798,183.577L0,204.221l153.635,121.202l-53.048,
                            188.365 l162.75-108.664l162.75,108.664l-53.048-188.365L526.673,204.221z M392.683,467.808l-129.346-86.356L133.99,467.808 l42.163-149.692L54.058,221.779l155.404-6.163l53.875-145.885l53.885,
                            145.885l155.394,6.163l-122.096,96.337L392.683,467.808z"></path> </g> </g> </g>
                            </svg>
                        </div>
                        <div className="plannedTime" onClick={() => manipulatedTaskIdHandler(task.id)}>
                            <div className={
                                task.plannedTime == '0001-01-01T00:00:00' ? "removeDate-hidden" : "removeDate"
                            } onClick={setPlannedTime}>
                                <svg fill="#000000" height="20px" width="20px" viewBox="0 0 32 32" version="1.1" xmlns="http://www.w3.org/2000/svg">
                                    <g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round">
                                    </g><g id="SVGRepo_iconCarrier"> <title>remove</title>
                                        <path d="M11.188 4.781c6.188 0 11.219 5.031 11.219 11.219s-5.031 11.188-11.219 11.188-11.188-5-11.188-11.188 5-11.219 
                                            11.188-11.219zM11.25 17.625l3.563 3.594c0.438 0.438 1.156 0.438 1.594 0 0.406-0.406 0.406-1.125 0-1.563l-3.563-3.594
                                            3.563-3.594c0.406-0.438 0.406-1.156 0-1.563-0.438-0.438-1.156-0.438-1.594 0l-3.563 3.594-3.563-3.594c-0.438-0.438-1.156-0.438-1.594 0-0.406 0.406-0.406 1.125 0
                                            1.563l3.563 3.594-3.563 3.594c-0.406 0.438-0.406 1.156 0 1.563 0.438 0.438 1.156 0.438 1.594 0z">
                                        </path>
                                    </g>
                                </svg>
                            </div>
                            <div className="date">{task.plannedTime == '0001-01-01T00:00:00' ?'':task.plannedTime.slice(0, -9)}</div>
                            <input type="date" onChange={setPlannedTime} />
                        </div>
                    </div>
                ) : null
            )}
            {
                props.taskListName === "" || props.taskListName === "All Tasks" || props.taskListName === "Important" || props.taskListName === "Planned" ? null : (
                    <div className="addNewTask">
                        <input type="text" value={addNewTaskText} onChange={addNewTaskInput} onKeyDown={addNewUserTask}></input>
                        <div className="addTaskSVGContainer" onClick={addNewUserTask} >
                            <svg viewBox="0 0 48 48" xmlns="http://www.w3.org/2000/svg" stroke="#000000" strokeWidth="0.00048000000000000007">
                                <g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                                <g id="SVGRepo_iconCarrier">
                                    <path d="M16.1161 39.6339C15.628 39.1457 15.628 38.3543 16.1161 37.8661L29.9822 24L16.1161 10.1339C15.628 9.64573 15.628 8.85427 
                                16.1161 8.36612C16.6043 7.87796 17.3957 7.87796 17.8839 8.36612L32.6339 23.1161C33.122 23.6043 33.122 24.3957 32.6339 24.8839L17.8839
                                39.6339C17.3957 40.122 16.6043 40.122 16.1161 39.6339Z" ></path> </g>
                            </svg>
                        </div>
                    </div>
                )
            }
        </div>
    )
}