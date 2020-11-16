<template>
  <el-container>
    <el-aside :width="width">
      <LeftMenu :collapse= "collapse" />
    </el-aside>
    <el-container>
      <el-header>
       <HeaderNav :collapse= "collapse" v-on:setCollapse="setCollapse" />
      </el-header>
      <el-main>Main</el-main>
    </el-container>
  </el-container>
</template>

<script>
// @ is an alias to /src
import LeftMenu from "@/views/dashboard/components/leftMenu";
import HeaderNav from "@/views/dashboard/components/headerNav";

export default {
  name: "dashboard",
  data() {
    return {
      collapse:false,
      width: '200px'

    }
  },
  methods: {
    setCollapse() {
      this.collapse = !this.collapse;
      if(this.collapse) {
        this.width = "64px";
      }
      else {
        this.width = "200px";
      }
    }
  },
  components: {
    LeftMenu,
    HeaderNav
  },
};
</script>


<style lang="scss" scoped>
@import "@/styles/global.module.scss";
.el-container {
  height: 100%;

  .el-header {
    padding: 0px;
    background: $bg;
  }
}
</style>
