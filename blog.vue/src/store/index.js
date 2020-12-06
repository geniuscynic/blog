import Vue from 'vue'
import Vuex from 'vuex'
import request from '@/plugins/request'
import { SET_TOKEN, GET_MENU, SET_MENU, API_REST_MENU } from '@/plugins/const'


Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    token: '',
    menus: []
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

    }
  },
  modules: {
  }
})
