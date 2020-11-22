<template>
  <div id="permission-menu-container">
    <el-button type="primary" icon="el-icon-plus" @click="add"
      >新增菜单</el-button
    >

    <el-table :data="menus" style="width: 100%">
      <el-table-column prop="id" label="" width="50"> </el-table-column>
      <el-table-column label="菜单" width="360">
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
      <el-table-column label="路由" width="180">
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
      <el-table-column prop="seqNum" label="排序"> </el-table-column>
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

    <el-dialog title="新增菜单" :visible.sync="dialogFormVisible">
      <el-form :model="form">
        <el-form-item label="父菜单" :label-width="formLabelWidth">
          <el-select v-model="form.pid" placeholder="请选择父菜单">
            <el-option label="默认" :value="0">
              <i class="el-icon-arrow-right" />

              <svg class="icon icon-menu" aria-hidden="true">
                <use xlink:href="#icon-home"></use>
              </svg>
              <span class="rowitem">默认</span>
            </el-option>
            <el-option
              v-for="menu in menus"
              :key="menu.id"
              :label="menu.name"
              :value="menu.id"
            >
              <i class="el-icon-arrow-right" v-if="menu.childMenus != null" />
              <i v-else class="el-icon-else" />
              <svg
                class="icon icon-menu"
                aria-hidden="true"
                v-if="menu.icon != ''"
              >
                <use :xlink:href="`#${menu.icon}`"></use>
              </svg>
              <span class="rowitem">{{ menu.name }}</span>
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="新菜单" :label-width="formLabelWidth">
          <el-input v-model="form.menu" auto-complete="off"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogNo">取 消</el-button>
        <el-button type="primary" @click="dialogYes">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { API_REST_MENU } from "@/plugins/const";

export default {
  name: "menu-permission",
  data() {
    return {
      menus: [],
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
        .get(API_REST_MENU)
        .then((response) => {
          // console.log(response.data.response);
          const temp = [];
          for (const item of response.data.response) {
            // console.log(item);
            temp.push(item);

            for (const im of item.childMenus) {
              im.mode = 0;
              im.editIcon = im.icon;
              im.editName = im.name;
              temp.push(im);
            }
            item.childMenus = [];
            item.mode = 0;
            item.editIcon = item.icon;
            item.editName = item.name;
          }
          this.menus = temp;
        })
        .catch((error) => {
          console.log(error.response);
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
        });
    },
    add() {
      this.dialogFormVisible = true;
    },
    handleEdit(row) {
      // console.log(row);
      // console.log(this.menus);
      row.mode = 1;
      row.editIcon = row.icon;
      row.editName = row.name;
    },
    handleSave(row) {
      row.icon = row.editIcon;
      row.name = row.editName;

      this.axios
        .put(API_REST_MENU, row)
        .then((response) => {
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
    handleCancel(row) {
      row.mode = 0;
    },
    handleDelete(row) {
      row.mode = 0;
    },
    dialogYes() {
      this.dialogFormVisible = false;

      this.axios
        .post(API_REST_MENU, this.form)
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
