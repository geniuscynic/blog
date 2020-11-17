import Vue from 'vue'
import Vuex from 'vuex'
import request from '@/plugins/request'
import { SET_TOKEN } from '@/plugins/mutation-types'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    token: ''
  },
  mutations: {
    [SET_TOKEN]: (state, val) => {
      state.token = val;
      request.defaults.headers.common['Authorization'] = `Bearer ${val}`;
      //this.axios.defaults.headers.common['Authorization'] = "Bearer";
    }
  },
  actions: {
  },
  modules: {
  }
})
