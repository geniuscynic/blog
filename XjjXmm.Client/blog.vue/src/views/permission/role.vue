<template>
  <div id="permission-menu-container">
    <el-button type="primary" icon="el-icon-plus" @click="add"
      >添加角色</el-button
    >

    <el-table :data="roles" style="width: 100%">
      <el-table-column prop="id" label="" width="100"> </el-table-column>
      <el-table-column label="角色" width="360">
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
      <el-table-column label="Code" width="180">
        <template slot-scope="scope">
          <div v-if="scope.row.mode == 0">
            {{ scope.row.code }}
          </div>
          <div v-else>
            <el-form :inline="true">
              <el-form-item class="icon-input" label="">
                <el-input v-model="scope.row.editCode"></el-input>
              </el-form-item>
            </el-form>
          </div>
        </template>
      </el-table-column>
      <el-table-column label="描述" width="180">
        <template slot-scope="scope">
          <div v-if="scope.row.mode == 0">
            {{ scope.row.description }}
          </div>
          <div v-else>
            <el-form :inline="true">
              <el-form-item class="icon-input" label="">
                <el-input v-model="scope.row.editDesc"></el-input>
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
            <el-button size="mini" type="danger" @click="handleDelete(scope.row)"
              >删除</el-button
            >

            <el-button size="mini" type="primary" @click="handleAssign(scope.row)">权限分配</el-button>
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

    <el-dialog title="新增角色" :visible.sync="dialogFormVisible">
      <el-form :model="form">
        <el-form-item label="角色名" :label-width="formLabelWidth">
          <el-input v-model="form.name" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="角色Code" :label-width="formLabelWidth">
          <el-input v-model="form.code" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="描述" :label-width="formLabelWidth">
          <el-input v-model="form.desc" auto-complete="off"></el-input>
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
import { API_REST_ROLE } from "@/plugins/const";
import { mapActions, mapState } from "vuex";

export default {
  name: "menu-permission",
  data() {
    return {
      roles: [],
      formLabelWidth: "120px",
      dialogFormVisible: false,
      form: {
        name: "",
        code: "",
        description: "",
      },
    };
  },
  mounted() {
    this.init();
  },
  methods: {
    init() {
      //const _this = this;
      this.axios
        .get(API_REST_ROLE)
        .then((response) => {
          //console.log(this.menus);
          let temps = response.data.response;

          for (let button of temps) {
            button.mode = 0;
            button.editName = button.name;
            button.editCode = button.code;
            button.editDesc = button.description;
            //button.editMenuId = button.menuId;

            //debugger;

            //console.log(button.menuId, this.menus, buttton.menu);
            //button.editMenu = button.menu;
          }

          this.roles = temps;
        })
        .catch((error) => {
          console.log(error);
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
        });
    },
    add() {
      this.dialogFormVisible = true;
    },
    handleEdit(row) {
      console.log(row);
      // console.log(this.menus);
      row.mode = 1;
      row.editCode = row.code;
      row.editName = row.name;
      row.editDesc = row.description;
      //row.editMenuId = row.menuId;
      //buttton.menu = this.menus.find(s=>s.menuId == button.menuId).name;
    },
    handleSave(row) {
      row.code = row.editCode;
      row.name = row.editName;
      row.description = row.editDesc;
      //row.menuId = row.editMenuId;
      //console.log(row);
      //row.menu2 = this.menus.find((s) => s.id == row.menuId).name;

      this.axios
        .put(API_REST_ROLE, row)
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
    handleAssign(row) {
      this.$router.push({
        name: "assign",
        params: {
          id: row.id,
        },
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
        .post(API_REST_ROLE, this.form)
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
