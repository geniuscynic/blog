<template>
  <el-row class="login-container" type="flex" justify="center">
    <el-col :span="8">
      <el-form :model="form" ref="form" :rules="rules" :status-icon="true">
        <h1 class="title">系统登录</h1>
        <el-alert :title="errorMsg" type="error" v-if="showError" show-icon>
        </el-alert>
        <el-form-item prop="login">
          <el-input v-model="form.login" placeholder="请输入账号">
            <i slot="prefix" class="el-input__icon el-icon-s-custom"></i>
          </el-input>
        </el-form-item>

        <el-form-item prop="password">
          <el-input v-model="form.password" placeholder="请输入密码">
          </el-input>
        </el-form-item>

        <el-button type="primary" @click="onSubmit">登入</el-button>
      </el-form>
    </el-col>
  </el-row>
</template>

<script>
import { mapMutations } from "vuex";
import { SET_TOKEN } from "@/plugins/mutation-types";

export default {
  data() {
    return {
      form: {
        login: "",
        password: "",
      },
      rules: {
        login: [{ required: true, message: "请输入账号", trigger: "blur" }],
        password: [{ required: true, message: "请输入密码", trigger: "blur" }],
      },
      showError: false,
      errorMsg: "",
    };
  },
  methods: {
    onSubmit() {
      this.axios
        .post("/api/Account/Login", this.form)
        .then((response) => {
          if (response.data.response === "") {
            this.errorMsg = "账号密码错误";
            this.showError = true;
          } else {
            this[SET_TOKEN](response.data.response);

            this.$router.push({ name: "dashboard" });
          }
        })
        .catch((error) => {
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
        });
    },
    ...mapMutations([SET_TOKEN]),
  },
};
</script>

<style lang="scss" scoped>
@import "@/styles/global.module.scss";
$light_gray: #eee;
$cursor: #fff;

.login-container {
  background: $bg;
  height: 100%;
  width: 100%;
  padding-top: 150px;

  .el-alert {
    margin-bottom: 20px;
  }
  .el-form {
    padding: 0 150px;

    .el-form-item {
      border: 1px solid rgba(255, 255, 255, 0.1);
      background: rgba(0, 0, 0, 0.1);
      border-radius: 5px;
      color: #454545;

      padding-top: 6px;
      padding-bottom: 6px;
    }

    .el-button {
      width: 100%;
    }
  }
}

.title {
  color: $light_gray;
  justify-content: center;
  display: flex;
}
</style>

<style lang="scss">
$light_gray: #eee;
$cursor: #fff;
.el-input {
  background: transparent;

  input {
    border: none;
    background: transparent;
    color: $light_gray;

    font-size: 16px;
  }
}
</style>
