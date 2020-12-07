import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '@/store'

import home from '@/views/home'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: home,

    children: [
      {
        path: 'permission/menu',
        name: 'menu',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '@/views/permission/menu')
      },
      {
        path: 'permission/button',
        name: 'button',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '@/views/permission/button')
      },
      {
        path: 'permission/role',
        name: 'role',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '@/views/permission/role')
      },
      {
        path: 'permission/user',
        name: 'user',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '@/views/permission/user')
      },
      {
        path: 'permission/assign',
        name: 'assign',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '@/views/permission/assign')
      },
      {
        path: 'blog/add',
        name: 'blog-add',  
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '@/views/blog/add')
      },
      {
        path: 'blog/edit/:id',
        name: 'blog-edit',
        props: true,
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '@/views/blog/edit')
      },
      {
        path: 'blog/',
        name: 'blog-list',
        //props: true,
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '@/views/blog')
      },
      {
        path: 'category/',
        name: 'category',
        //props: true,
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '@/views/blog/category')
      },
      {
        path: '',
        name: 'dashboard',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '@/views/dashboard')
      }
    ]
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

// console.log(this, Vue)

router.beforeEach((to, from, next) => {
  const token = store.state.token
   //console.log("token", to, from, next);

  if (to.name !== 'login' && token === '') {
    next({ name: 'login' })
    // next()
  } else { next() }
})

export default router
