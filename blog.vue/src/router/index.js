import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '@/store'

import dashboard from '@/views/dashboard'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'dashboard',
    component: dashboard
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  {
    path: '/login',
    name: 'login',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '@/views/login')
  }
]

const router = new VueRouter({
  routes
})

//console.log(this, Vue)

router.beforeEach((to, from, next) => {
  const token = store.state.token
  //console.log("token", token);

  if (to.name !== 'Login' && token === '') {
    //next({ name: 'Login' })
    next()
  } else { next() }
})

export default router
