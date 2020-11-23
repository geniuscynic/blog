<template>
  <div id="blog-list-container">
    <el-button type="primary" icon="el-icon-plus" @click="add"
      >新增菜单</el-button
    >

    <el-table :data="blogs" style="width: 100%">
      <el-table-column prop="id" label="" width="50"> </el-table-column>
      <el-table-column label="标题" width="180">
        <template slot-scope="scope">
          <div v-if="scope.row.mode == 0">
            <i
              class="el-icon-arrow-right"
              v-if="scope.row.childMenus != null"
            />
            <i v-else class="el-icon-else" />
            <svg
              class="icon icon-menu"
              aria-hidden="true"
              v-if="scope.row.icon != ''"
            >
              <use :xlink:href="`#${scope.row.icon}`"></use>
            </svg>
            <span class="rowitem">{{ scope.row.name }}</span>
          </div>
          <div v-else>
            <el-form :inline="true">
              <el-form-item class="icon-input" label="">
                <el-input v-model="scope.row.editIcon"></el-input>
              </el-form-item>
              <el-form-item class="icon-input" label="">
                <el-input v-model="scope.row.editName"></el-input>
              </el-form-item>
            </el-form>
          </div>
        </template>
      </el-table-column>
      <el-table-column label="作者" width="180">
        <template slot-scope="scope">
          <div v-if="scope.row.mode == 0">
            {{ scope.row.route }}
          </div>
          <div v-else>
            <el-form :inline="true">
              <el-form-item class="icon-input" label="">
                <el-input v-model="scope.row.route"></el-input>
              </el-form-item>
            </el-form>
          </div>
        </template>
      </el-table-column>
      <el-table-column label="分类" width="180">
        <template slot-scope="scope">
          <div v-if="scope.row.mode == 0">
            {{ scope.row.route }}
          </div>
          <div v-else>
            <el-form :inline="true">
              <el-form-item class="icon-input" label="">
                <el-input v-model="scope.row.route"></el-input>
              </el-form-item>
            </el-form>
          </div>
        </template>
      </el-table-column>
      <el-table-column label="修改时间" width="180">
        <template slot-scope="scope">
          <div v-if="scope.row.mode == 0">
            {{ scope.row.route }}
          </div>
          <div v-else>
            <el-form :inline="true">
              <el-form-item class="icon-input" label="">
                <el-input v-model="scope.row.route"></el-input>
              </el-form-item>
            </el-form>
          </div>
        </template>
      </el-table-column>

      <el-table-column prop="seqNum" label="内容"> </el-table-column>
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