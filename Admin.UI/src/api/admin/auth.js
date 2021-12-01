import request from '@/utils/request'
import scope from './scope'
const apiPrefix = `${process.env.VUE_APP_BASE_API}/${scope}/auth/`

// 授权

/**
 * 登录
 */
export const login = (params, config = {}) => {
  return request.post(apiPrefix + 'login', params, { ...{ api: { auth: false }}, ...config })
}
export const refresh = (params, config = {}) => {
  return request.get(apiPrefix + 'refresh', { params: params, ...config })
}
export const getVerifyCode = (params, config = {}) => {
  return request.get(apiPrefix + 'getverifycode', { params: params, ...{ api: { auth: false }}, ...config })
}
export const getPassWordEncryptKey = (params, config = {}) => {
  return request.get(apiPrefix + 'getPassWordEncryptKey', { params: params, ...{ api: { auth: false }}, ...config })
}
export const getLoginInfo = (params, config = {}) => {
  return request.get(apiPrefix + 'getUserInfo', { params: params, ...config })
}
export const getCaptcha = (params, config = {}) => {
  return request.get(apiPrefix + 'getCaptcha', { params: params, ...{ api: { auth: false }}, ...config })
}
export const checkCaptcha = (params, config = {}) => {
  return request.get(apiPrefix + 'checkCaptcha', { params: params, ...{ api: { auth: false }}, ...config })
}
