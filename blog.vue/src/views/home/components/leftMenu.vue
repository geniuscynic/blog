<template>
  <div class="sidebar-container">
    <div class="title">XJJXMM</div>

    <el-menu
      default-active="2"
      :background-color="bg"
      text-color="#fff"
      active-text-color="#ffd04b"
      :collapse="collapse"
      :router="true"
    >
      <template v-for="menu in menus">
        <el-menu-item
          :key="menu.id"
          :index="menu.route"
          v-if="menu.childMenus.length == 0"
        >
          <template slot="title">
            <svg class="icon" aria-hidden="true">
              <use :xlink:href="`#${menu.icon}`"></use>
            </svg>
            <span>{{ menu.name }}</span>
          </template>
        </el-menu-item>
        <el-submenu :key="menu.id" :index="menu.id.toString()" v-else>
          <template slot="title">
            <svg class="icon" aria-hidden="true">
              <use :xlink:href="`#${menu.icon}`"></use>
            </svg>
            <span>{{ menu.name }}</span>
          </template>
          <el-menu-item
            v-for="childMenu in menu.childMenus"
            :key="childMenu.id"
            :index="childMenu.route"
          >
            {{ childMenu.name }}
          </el-menu-item>
        </el-submenu>
      </template>
      <!-- <el-submenu v-for="menu in menus" :key="menu.id"  :index="menu.id.toString()">
        <template slot="title">
          <svg class="icon" aria-hidden="true">
            <use :xlink:href="`#${menu.icon}`"></use>
          </svg>
          <span>{{ menu.name}}</span>
        </template>
        <el-menu-item v-for="childMenu in menu.childMenus" :key="childMenu.id" :index="childMenu.route">
          {{ childMenu.name}}
        </el-menu-item>
      </el-submenu> -->
    </el-menu>

    <div class="other"></div>
  </div>
</template>

<script>
import styles from "@/styles/global.module.scss";
import { API_REST_MENU,GET_MENU } from "@/plugins/const";
import { mapActions,mapState  } from 'vuex'
// @ is an alias to /src

export default {
  name: "LeftMenu",
  props: ["collapse"],
  data() {
    return {
      bg: styles.bg,
      //menus: [],
    };
  },
  computed: {
    ...mapState([
      'menus'
    ])
  },
  mounted() {
    this[GET_MENU]();
    // this.axios
    //   .get(API_REST_MENU)
    //   .then((response) => {
    //     // console.log(response.data.response);
    //     this.menus = response.data.response;
    //   })
    //   .catch((error) => {
    //     this.errorMsg = "服务器异常，请稍后再试";
    //     this.showError = true;
    //   });
  },
  methods: {
    ...mapActions([
      GET_MENU
    ]),
  }
};
</script>

<style lang="scss" scoped>
@import "@/styles/global.module.scss";
.sidebar-container {
  height: 100%;
  background: $bg;
  border-right: solid 1px #e6e6e6;

  .title {
    padding: 20px 0 20px 0;
    color: #fff;
    font-size: 24px;
    text-align: center;
  }
}
</style>

<style lang="scss">
.sidebar-container {
  .el-submenu,
  .el-menu {
    border-right: none;
  }
}
</style>
