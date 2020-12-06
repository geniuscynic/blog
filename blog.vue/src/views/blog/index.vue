<template>
  <div id="blog-list-container">
    <el-button type="primary" icon="el-icon-plus" @click="add"
      >新增博客</el-button
    >

    <el-table :data="blogs" style="width: 100%">
      <el-table-column type="selection" width="55"></el-table-column>
      <el-table-column prop="id" label="" width="50"> </el-table-column>
      <el-table-column label="标题" width="180" prop="title"></el-table-column>
      <el-table-column label="作者" width="180" prop="author"></el-table-column>
      <el-table-column
        label="分类"
        width="180"
        prop="category"
      ></el-table-column>
      <el-table-column label="修改时间" width="180" prop="publishDate">
        <template slot-scope="scope">
          {{ scope.row.publishDate | blogDateFormat }}
        </template>
      </el-table-column>

      <el-table-column label="内容" prop="content"></el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.row)">编辑</el-button>
          <el-button
            size="mini"
            type="danger"
            @click="handleDelete(scope.$index, scope.row)"
            >删除</el-button
          >
        </template>
      </el-table-column>
    </el-table>

    <el-pagination
      background
      layout="prev, pager, next"
      :total="pageModel.dataCount"
      :page-size="currentPageSize"
      :current-page.sync="currentPage"
      @current-change = "handleCurrentChange"
    >
    </el-pagination>
  </div>
</template>


<script>
//import router from '@/router'

import { API_REST_BLOG } from "@/plugins/const";

export default {
  name: "blog-list",
  data() {
    return {
      blogs: [],
      pageModel: {},
      currentPage: 1,
      currentPageSize: 5
    };
  },
  methods: {
    add() {
      this.$router.push({
        name: "blog-add"
      });
    },
    handleEdit(row) {
      this.$router.push({
        name: "blog-edit",
        params: {
          id: row.id,
        },
      });
    },
    init() {
      this.axios
      .get(`${API_REST_BLOG}?pageIndex=${this.currentPage}&pageSize=${this.currentPageSize}`)
      .then((response) => {
        this.pageModel = response.data.response;
        this.blogs = this.pageModel.data;

        //console.log(response.data.response);
      })
      .catch((error) => {
        console.log(error.response);
        this.errorMsg = "服务器异常，请稍后再试";
        this.showError = true;
      });
    },
    handleCurrentChange(val) {
           // 改变默认的页数
          this.currentPage=val;
          this.init();
          console.log(val);
       },
  },
  mounted() {
    this.init();
  }
};
</script>