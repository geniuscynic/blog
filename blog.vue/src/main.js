import Vue from 'vue'

import 'normalize.css/normalize.css' // a modern alternative to CSS resets

import '@/plugins/element.js'
import * as filters from '@/plugins/filters.js'

import '@/assets/iconfont/iconfont.js'

import '@/styles/index.scss' // global css

import App from './App.vue'
import router from './router'
import store from './store'

import request from '@/plugins/request'
import VueAxios from 'vue-axios'

Vue.use(VueAxios, request)
Vue.config.productionTip = false

Object.keys(filters).forEach(key => {
  Vue.filter(key, filters[key])
})


new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');
