import React from 'react';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { validateTokenAsync } from '../Services/JwtValidator';
import { ThinCheckMark } from '../../svg/ThinCheckMark';
import { SharpStar } from '../../svg/SharpStar';
import { DeleteMark } from '../../svg/DeleteMark';
import { RightArrow } from '../../svg/RightArrow';
import { BackButton } from '../../svg/BackButton';


export function TaskView(props) {

    const [manipulatedTaskId, manipulatedTaskIdHandler] = useState();
    const [addNewTaskText, addNewTaskTextHandler] = useState("");

    const url = import.meta.env.VITE_API_URL;

    const navigate = useNavigate();

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

            await validateTokenAsync(token, navigate);

            const response = await fetch(url + '/Tasks/AddUserTask', {
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

                await validateTokenAsync(token, navigate);

                const response = await fetch(url + '/Tasks/GetUserTasksByTaskList?' + new URLSearchParams({ taskListId: props.taskListId }),
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

        await validateTokenAsync(token, navigate);

        const time = e.type == "click" ? '0001-01-01' : e.target.value

        const response = await fetch(url + '/Tasks/AddUserTaskPlannedTime', {
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

        await validateTokenAsync(token, navigate);

        const response = await fetch(url + '/Tasks/MarkUserTaskAsDone', {
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
                    return { ...task, isDone: !task.isDone };
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

        await validateTokenAsync(token, navigate);

        const response = await fetch(url + '/Tasks/MarkUserTaskAsImportant', {
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
                    : (<div className="taskListNameBlock">
                        <div className="backButton" onClick={() => props.updateTaskListId("")}>
                            <BackButton />
                        </div>
                        {props.taskListName}
                    </div>)
            }
            {
                props.tasks.length === 0 && props.taskListName!="" ? (<div className="emptyTaskView">There are no tasks in this task list.</div>)
                    : null
            }
            {props.tasks.map(task =>
                !task.isDone ? (
                    <div className="userTask" key={task.id}>
                        <div className="doneMark" onClick={() => markTaskAsDone(task.id)}>
                            <ThinCheckMark/>
                    </div>
                        <div className="userTaskText">{task.text}</div>
                        <div className="importantMark" onClick={() => markTaskAsImportant(task.id)} style={{
                            backgroundColor:
                                task.isImportant==true ? 'var(--btn-pressed-color)' : '',
                        }}>
                            <SharpStar/>
                        </div>
                        <div className="plannedTime" onClick={() => manipulatedTaskIdHandler(task.id)}>
                            <div className={
                                task.plannedTime == '0001-01-01T00:00:00' ? "removeDate-hidden" : "removeDate"
                            } onClick={setPlannedTime}>
                                <DeleteMark/>
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
                            <ThinCheckMark />
                        </div>
                        <div className="userTaskText"><del>{task.text}</del></div>
                        <div className="importantMark" onClick={() => markTaskAsImportant(task.id)} style={{
                            backgroundColor:
                                task.isImportant == true ? 'var(--btn-pressed-color)' : '',
                        }}>
                            <SharpStar />
                        </div>
                        <div className="plannedTime" onClick={() => manipulatedTaskIdHandler(task.id)}>
                            <div className={
                                task.plannedTime == '0001-01-01T00:00:00' ? "removeDate-hidden" : "removeDate"
                            } onClick={setPlannedTime}>
                                <DeleteMark />
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
                            <RightArrow/>
                        </div>
                    </div>
                )
            }
        </div>
    )
}