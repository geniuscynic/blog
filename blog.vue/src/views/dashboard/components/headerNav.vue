<template>
  <div class="navbar">
    <div class="tool" v-on:click="setCollapse ">
      <svg class="icon" aria-hidden="true">
        <use xlink:href="#icon-indent" v-if="collapse"></use>
        <use xlink:href="#icon-outdent" v-else></use>
      </svg>
    </div>
    <div class="bread">
      <el-breadcrumb separator-class="el-icon-arrow-right">
        <el-breadcrumb-item :to="{ path: '/' }" class="bread-item"
          >首页</el-breadcrumb-item
        >
        <el-breadcrumb-item class="bread-item">活动管理</el-breadcrumb-item>
        <el-breadcrumb-item class="bread-item">活动列表</el-breadcrumb-item>
        <el-breadcrumb-item class="bread-item">活动详情</el-breadcrumb-item>
      </el-breadcrumb>
    </div>
  </div>
</template>


<script>
export default {
  name: "HeaderNav",
  props: ['collapse'],
  data() {
    return {};
  },
  methods: {
    setCollapse() {
        this.$emit('setCollapse')
    },
  },
};
</script>


<style lang="scss" scoped>
.navbar {
  display: flex;
  align-items: center;

  .tool {
    margin-left: 20px;
    .icon {
      color: #ffffff;
      width: 1.5em;
      height: 1.5em;
      cursor: pointer;
    }
  }
  .bread {
    padding: 24px;
  }
}
</style>

<style lang="scss">
span.bread-item {
  .el-breadcrumb__inner {
    color: #ffffff;
  }

  .el-breadcrumb__inner:last-child {
    color: #ffffff;
  }
}

span.bread-item:last-child {
  .el-breadcrumb__inner {
    color: rgb(255, 208, 75);
  }
}
</style>