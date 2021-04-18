<template>
  <div id="category-container">
    <el-button type="primary" icon="el-icon-plus" @click="add"
      >新增分类</el-button
    >

    <el-table :data="categories" style="width: 100%">
      <el-table-column prop="id" label="" width="100"> </el-table-column>
      <el-table-column label="分类" width="360">
        <template slot-scope="scope">
          <div v-if="scope.row.mode == 0">
            <span class="rowitem">{{ scope.row.name }}</span>
          </div>
          <div v-else>
            <el-form :inline="true">
              <el-form-item class="icon-input" label="">
                <el-input v-model="scope.row.editName"></el-input>
              </el-form-item>
            </el-form>
          </div>
        </template>
      </el-table-column>

      <el-table-column label="排序">
        <template slot-scope="scope">
          <div v-if="scope.row.mode == 0">
            <span class="rowitem">{{ scope.row.seqNum }}</span>
          </div>
          <div v-else>
            <el-form :inline="true">
              <el-form-item class="icon-input" label="">
                <el-input v-model="scope.row.editSeqNum"></el-input>
              </el-form-item>
            </el-form>
          </div>
        </template>
      </el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <div v-if="scope.row.mode == 0">
            <el-button size="mini" @click="handleEdit(scope.row)"
              >编辑</el-button
            >
            <el-button size="mini" type="danger" @click="handleDelete"
              >删除</el-button
            >
          </div>
          <div v-else>
            <el-button size="mini" @click="handleSave(scope.row)"
              >保存</el-button
            >
            <el-button size="mini" @click="handleCancel(scope.row)"
              >撤销</el-button
            >
          </div>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { API_REST_CATEGORY } from "@/plugins/const";

export default {
  name: "cateory",
  data() {
    return {
      categories: [],
      formLabelWidth: "120px",
      dialogFormVisible: false,
      form: {
        menu: "",
        pid: 0,
      },
    };
  },

  mounted() {
    this.init();
  },
  methods: {
    init() {
      this.axios
        .get(API_REST_CATEGORY)
        .then((response) => {
          //console.log(response.data.response);
          const temp = [];
          for (const item of response.data.response) {
            // console.log(item);
            temp.push(item);

            // for (const im of item.childMenus) {
            //   im.mode = 0;
            //   im.editIcon = im.icon;
            //   im.editName = im.name;
            //   temp.push(im);
            // }
            // item.childMenus = [];
            item.mode = 0;
            //item.editIcon = item.icon;
            //item.editName = item.name;
            //item.editSeqNum = item.seqNum;
          }
          this.categories = temp;
        })
        .catch((error) => {
          console.log(error.response);
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
        });
    },
    add() {
      this.categories.push({
        editName: "",
        floor: 1,
        id: 0,
        mode: 1,
        name: "",
        parentCategory: null,
        parentId: 0,
        seqNum: 1,
      });
    },
    handleEdit(row) {
      // console.log(row);
      // console.log(this.menus);
      row.mode = 1;
      //row.editIcon = row.icon;
      row.editName = row.name;
      row.editSeqNum = row.seqNum;
    },
    handleSave(row) {
      //row.icon = row.editIcon;
      row.name = row.editName;

      this.axios
        .post(API_REST_CATEGORY, row)
        .then((response) => {
          row.mode = 0;
          row.id = response.data.response.id;
          this.$message({
            message: "保存成功",
            type: "success",
          });

          console.log(response.data);
        })
        .catch((error) => {
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
        });
    },
    handleCancel(row) {
      row.mode = 0;
    },
    handleDelete(row) {
      row.mode = 0;
    },
    dialogYes() {
      this.dialogFormVisible = false;

      this.axios
        .post(API_REST_CATEGORY, this.form)
        .then((response) => {
          this.init();
          row.mode = 0;
          this.$message({
            message: "保存成功",
            type: "success",
          });

          //console.log(response.data);
        })
        .catch((error) => {
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
        });
    },
    dialogNo() {
      this.dialogFormVisible = false;
    },
  },
};
</script>

<style lang="scss" scoped>
.icon-menu {
  margin-left: 10px;
}

.rowitem {
  margin-left: 10px;
}

.el-icon-else {
  width: 14px;
  height: 14px;
}

.icon-input {
  width: 150px;
}

#permission-menu-container .el-dialog {
  .el-input,
  .el-select {
    width: 400px;
  }
}
</style>
