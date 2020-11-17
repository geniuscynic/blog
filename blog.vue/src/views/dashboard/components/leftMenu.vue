<template>
  <div class="sidebar-container">
    <div class="title">XJJXMM</div>

    <el-menu
      default-active="2"
      :background-color="bg"
      text-color="#fff"
      active-text-color="#ffd04b"
      :collapse="collapse"
    >
      <el-submenu v-for="menu in menus" :key="menu.id"   :index="menu.id.toString()">
        <template slot="title">
          <svg class="icon" aria-hidden="true">
            <use :xlink:href="`#${menu.icon}`"></use>
          </svg>
          <span>{{ menu.name}}</span>
        </template>
        <el-menu-item v-for="childMenu in menu.childMenus" :key="childMenu.id" :index="childMenu.id.toString()">
          {{ childMenu.name}}
        </el-menu-item>
      </el-submenu>

      
    </el-menu>
  </div>
</template>


<script>
import styles from "@/styles/global.module.scss";

// @ is an alias to /src

export default {
  name: "LeftMenu",
  props: ["collapse"],
  data() {
    return {
      bg: styles.bg,
      menus: []
    };
  },
  mounted() {
    this.axios
        .get("/api/Menu")
        .then((response) => {
          //console.log(response.data.response);
          this.menus = response.data.response;
        })
        .catch((error) => {
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
        });
    },
  
};
</script>





<style lang="scss" scoped>
@import "@/styles/global.module.scss";
.sidebar-container {
  height: 100%;
  background: $bg;

  .title {
    padding: 20px 0 20px 0;
    color: #fff;
    font-size: 24px;
    text-align: center;

    border-right: solid 1px #e6e6e6;
  }
}
</style>

