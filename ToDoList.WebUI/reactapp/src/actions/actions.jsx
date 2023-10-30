
export function updateTasks(tasks) {
    return {
        type: "UPDATE_TASKS",
        tasks
    }
}

export function updateTaskListName(taskListName) {
    return {
        type: "UPDATE_TASKLISTNAME",
        taskListName
    }
}

export function updateTaskListId(taskListId) {
    return {
        type: "UPDATE_TASKLISTID",
        taskListId
    }
}