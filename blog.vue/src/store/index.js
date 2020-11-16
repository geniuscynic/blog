import Vue from 'vue'
import Vuex from 'vuex'

import { SET_TOKEN } from '@/plugins/mutation-types'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    token: ''
  },
  mutations: {
    [SET_TOKEN]: (state, val) => {
      state.token = val
    }
  },
  actions: {
  },
  modules: {
  }
})
