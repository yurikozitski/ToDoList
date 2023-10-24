import React from 'react';

export function TaskView(props) {
    return (
        <div className="taskView">
            {
                props.taskListName === "" ? (<div className="emptyTaskView">Click on the task list to the left to see your tasks here.</div>)
                    : (<div className="taskListNameBlock">{props.taskListName}</div>)
            }
            {props.tasks.map(item =>
                <div className="userTask" key={item.id}>
                    <div className="doneMark">
                        <svg viewBox="0 0 24 24" height="20px" width="20px" xmlns="http://www.w3.org/2000/svg" fill="#000000">
                            <g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round">
                            </g><g id="SVGRepo_iconCarrier"> <rect x="0" fill="none" width="24" height="24"></rect> <g>
                                <path d="M9 19.414l-6.707-6.707 1.414-1.414L9 16.586 20.293 5.293l1.414 1.414"></path>
                            </g>
                            </g>
                        </svg>
                    </div>
                    <div className="userTaskText">{item.text}</div>
                </div>  
            )}
        </div>
    )
}