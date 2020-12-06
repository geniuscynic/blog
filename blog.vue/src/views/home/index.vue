<template>
  <el-container>
    <el-aside :width="width">
      <LeftMenu :collapse= "collapse" />
    </el-aside>
    <el-container>
      <el-header>
       <HeaderNav :collapse= "collapse" v-on:setCollapse="setCollapse" />
      </el-header>
      <el-main>
        <router-view />
      </el-main>
    </el-container>
  </el-container>
</template>

<script>
// @ is an alias to /src
import LeftMenu from '@/views/home/components/leftMenu'
import HeaderNav from '@/views/home/components/headerNav'

export default {
  name: 'home',
  data () {
    return {
      collapse: false,
      width: '210px'

    }
  },
  methods: {
    setCollapse () {
      this.collapse = !this.collapse
      if (this.collapse) {
        this.width = '64px'
      } else {
        this.width = '210px'
      }
    }
  },
  mounted() {
    console.log("routeHome", this.$router.params);
  },
  components: {
    LeftMenu,
    HeaderNav
  }
}
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
