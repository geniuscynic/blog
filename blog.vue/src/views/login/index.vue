<template>
  <el-row class="login-container" type="flex" justify="center">
    <el-col :span="8">
      <el-form :model="form" ref="form" :rules="rules" :status-icon="true">
        <h1 class="title">系统登录</h1>
        <el-form-item prop="login">
          <el-input v-model="form.login" placeholder="请输入内容bbbb">
            <i slot="prefix" class="el-input__icon el-icon-s-custom"></i>
          </el-input>
        </el-form-item>

        <el-form-item prop="password">
          <el-input v-model="form.password" placeholder="请输入内容">
          </el-input>
        </el-form-item>

        <el-button type="primary" @click="onSubmit">登入</el-button>
      </el-form>
    </el-col>
  </el-row>
</template>

<script>
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
    };
  },
  methods: {
    onSubmit() {
      console.log(this.form);
      this.axios
        .get("/api.coindesk.com/v1/bpi/currentprice.json")
        .then((response) => 
        (console.log(response.data.bpi)))
        .catch((error) => console.log(error));
    },
  },
};
</script>

<style lang="scss" scoped>
$bg: #2d3a4b;
$light_gray: #eee;
$cursor: #fff;

.login-container {
  background: $bg;
  height: 100%;
  width: 100%;
  padding-top: 150px;

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
$bg: #2d3a4b;
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
