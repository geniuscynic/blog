import axios from 'axios'
import router from '@/router'
// import store from '@/store'

const service = axios.create({
  baseURL: process.env.VUE_APP_BASE_API, // url = base url + request url
  headers: {
    // 'Authorization': 'Bearer ' + store.state.token
  },
  // withCredentials: true, // send cookies when cross-domain requests
  timeout: 5000 // request timeout
})

// 添加响应拦截器
service.interceptors.response.use(function (response) {
  // 对响应数据做点什么
  return response
}, function (error) {
  console.log(error.response)
  if (error.response.status) {
    switch (error.response.status) {
      // 401: 未登录
      // 未登录则跳转登录页面，并携带当前页面的路径
      // 在登录成功后返回当前页面，这一步需要在登录页操作。
      case 401:
        router.replace({
          path: '/login',
          query: {
            redirect: router.currentRoute.fullPath
          }
        })
        break

        // 403 token过期
        // 登录过期对用户进行提示
        // 清除本地token和清空vuex中token对象
        // 跳转登录页面
      case 403:
        console.log(error.response.status)
        router.push({ name: 'login' })
        break

        // 404请求不存在
      case 404:
        router.replace({
          path: '/login',
          query: {
            redirect: router.currentRoute.fullPath
          }
        })
        break
        // 其他错误，直接抛出错误提示
      default:
        router.replace({
          path: '/login',
          query: {
            redirect: router.currentRoute.fullPath
          }
        })
    }
    return Promise.reject(error.response)
  }

  // 对响应错误做点什么
  return Promise.reject(error)
})

export default service
