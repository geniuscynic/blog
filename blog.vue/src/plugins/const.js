import store from '@/store'


export const SET_TOKEN = 'setToken'
export const GET_MENU = 'GET_MENU'
export const SET_MENU = 'SET_MENU'

export const GET_BUTTON = 'GET_BUTTON'
export const SET_BUTTON = 'SET_BUTTON'


export const API_LOGIN = '/api/Account/Login'
//export const API_GET_MENU = '/api/Permission/GetMenu'
//export const API_EDIT_MENU = '/api/Permission/EditMenu'
//export const API_ADD_MENU = '/api/Permission/AddMenu'
export const API_REST_MENU = '/api/Menu'
export const API_REST_BUTTON = '/api/Button'
export const API_REST_ROLE = '/api/Role'
export const API_REST_USER = '/api/User'

export const API_REST_CATEGORY = '/api/Category'
export const API_REST_BLOG = '/api/Blog'



export const hasPermission = function(code) {
    return store.state.buttons.includes(t=>t.code == code);
}

