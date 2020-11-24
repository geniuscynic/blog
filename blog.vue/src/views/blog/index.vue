<template>
  <div id="blog-list-container">
    <el-button type="primary" icon="el-icon-plus" @click="add"
      >新增菜单</el-button
    >

    <el-table :data="blogs" style="width: 100%">
      <el-table-column prop="id" label="" width="50"> </el-table-column>
      <el-table-column label="标题" width="180" prop="title"></el-table-column>
      <el-table-column label="作者" width="180" prop="author"></el-table-column>
      <el-table-column label="分类" width="180" prop="category"></el-table-column>
      <el-table-column label="修改时间" width="180" prop="publishTime"></el-table-column>

      <el-table-column label="内容" prop="content"></el-table-column>
    </el-table>
  </div>
</template>


<script>
import { API_REST_BLOG } from "@/plugins/const";

export default {
  name: "blog-list",
  data() {
    return {
      blogs: [],
    };
  },
  methods: {
    add() {},
  },
  mounted() {
    this.axios
      .get(API_REST_BLOG)
      .then((response) => {
        this.blogs = response.data.response;
        console.log(response.data.response);
      })
      .catch((error) => {
        console.log(error.response);
        this.errorMsg = "服务器异常，请稍后再试";
        this.showError = true;
      });
  },
};
</script>