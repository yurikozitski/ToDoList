import {Map } from 'immutable';

export function reducer (state = Map(), action) {
    switch (action.type) {
        case "SET_STATE":
            return state.merge(action.state);
        case "UPDATE_TASKS":
            return state.update("tasks", () => action.tasks);
        case "UPDATE_TASKLISTNAME":
            return state.update("taskListName", () => action.taskListName);
    }
    return state;
}