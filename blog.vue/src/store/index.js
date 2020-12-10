import Vue from 'vue'
import Vuex from 'vuex'
import request from '@/plugins/request'
import { SET_TOKEN, 
  GET_MENU, SET_MENU, API_REST_MENU, 
  GET_BUTTON, SET_BUTTON, API_REST_BUTTON } from '@/plugins/const'


Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    token: '',
    menus: [],
    buttons: []
  },
  mutations: {
    [SET_TOKEN]: (state, val) => {
      state.token = val
      request.defaults.headers.common.Authorization = `Bearer ${val}`;
      // this.axios.defaults.headers.common['Authorization'] = "Bearer";
    },
    [SET_MENU]: (state, val) => {
      //console.log("SET_MENU", val);
      
      state.menus = val;
      // this.axios.defaults.headers.common['Authorization'] = "Bearer";
    },
    [SET_BUTTON]: (state, val) => {
      //console.log("SET_MENU", val);
      state.buttons = val;
      // this.axios.defaults.headers.common['Authorization'] = "Bearer";
    },


  },
  actions: {
    [GET_MENU]({ commit }) {
      //console.log("get_menu");
      request
        .get(API_REST_MENU)
        .then((response) => {
          //console.log(response.data.response);
          //this.menus = response.data.response;
          commit(SET_MENU, response.data.response);
        })
        .catch((error) => {
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
          console.log("get_menu", error.data);
        });
    },
    [GET_BUTTON]({ commit }) {
      //console.log("get_menu");
      request
        .get(API_REST_BUTTON)
        .then((response) => {
          //console.log(response.data.response);
          //this.menus = response.data.response;
          commit(SET_BUTTON, response.data.response);
        })
        .catch((error) => {
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
          console.log("get_button", error.data);
        });
    }
  },
  modules: {
  }
})
