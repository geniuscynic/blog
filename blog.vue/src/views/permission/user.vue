<template>
  <div id="permission-menu-container">
    <el-button type="primary" icon="el-icon-plus" @click="add"
      >添加用户</el-button
    >

    <el-table :data="users" style="width: 100%">
      <el-table-column prop="id" label="" width="100"> </el-table-column>
      <el-table-column label="账号" width="360">
        <template slot-scope="scope">
          <div v-if="scope.row.mode == 0">
            <span class="rowitem">{{ scope.row.account }}</span>
          </div>
          <div v-else>
            <el-form :inline="true">
              <el-form-item class="icon-input" label="">
                <el-input v-model="scope.row.account"></el-input>
              </el-form-item>
            </el-form>
          </div>
        </template>
      </el-table-column>
      <el-table-column label="昵称" width="180">
        <template slot-scope="scope">
          <div v-if="scope.row.mode == 0">
            {{ scope.row.nickName }}
          </div>
          <div v-else>
            <el-form :inline="true">
              <el-form-item class="icon-input" label="">
                <el-input v-model="scope.row.nickName"></el-input>
              </el-form-item>
            </el-form>
          </div>
        </template>
      </el-table-column>
      <el-table-column label="角色" width="360">
        <template slot-scope="scope">
          <div v-if="scope.row.mode == 0">
            {{ scope.row.role }}
          </div>
          <div v-else>
            <el-form :inline="true">
              <el-form-item class="icon-input" label="" style="width: 100%">
                <el-select
                  v-model="scope.row.roles"
                  multiple
                  placeholder="请选择所属角色"
                >
                  <el-option
                    v-for="role in roles"
                    :key="role.id"
                    :label="role.name"
                    :value="role.id"
                  />
                </el-select>
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

    <el-dialog title="新增用户" :visible.sync="dialogFormVisible">
      <el-form :model="form">
        <el-form-item label="用户名" :label-width="formLabelWidth">
          <el-input v-model="form.account" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="昵称" :label-width="formLabelWidth">
          <el-input v-model="form.nickName" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="密码" :label-width="formLabelWidth">
          <el-input v-model="form.password" auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="角色" :label-width="formLabelWidth">
          <el-select v-model="form.roles" multiple placeholder="请选择所属角色">
            <el-option
              v-for="role in roles"
              :key="role.id"
              :label="role.name"
              :value="role.id"
            />
          </el-select>
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
import { API_REST_ROLE, API_REST_USER } from "@/plugins/const";
import { mapActions, mapState } from "vuex";

export default {
  name: "menu-permission",
  data() {
    return {
      roles: [],
      users: [],
      formLabelWidth: "120px",
      dialogFormVisible: false,
      form: {
        account: "",
        nickName: "",
        password: "",
        roles: [],
      },
    };
  },
  mounted() {
    this.axios
      .get(API_REST_ROLE)
      .then((response) => {
        this.roles = response.data.response;

        this.init();
      })
      .catch((error) => {
        console.log(error);
        this.errorMsg = "服务器异常，请稍后再试";
        this.showError = true;
      });
  },
  methods: {
    init() {
      //const _this = this;
      this.axios
        .get(API_REST_USER)
        .then((response) => {
          //console.log(this.menus);
          let temps = response.data.response;

          for (let button of temps) {
            button.mode = 0;
            button.role = "";
            let tempRoles = this.roles.filter((t) =>
              button.roles.includes(t.id)
            );

            for (let tempRole of tempRoles) {
              button.role += tempRole.name + ", ";
            }

            //button.editAccount = button.account;
            //button.editNickName = button.nickName;
            //button.editDesc = button.description;
            //button.editMenuId = button.menuId;

            //debugger;

            //console.log(button.menuId, this.menus, buttton.menu);
            //button.editMenu = button.menu;
          }

          this.users = temps;
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
      //console.log(row);
      // console.log(this.menus);
      row.mode = 1;
      //row.editCode = row.code;
      //row.editName = row.name;
      //row.editDesc = row.description;

      row.editAccount = row.account;
      row.editNickName = row.nickName;
      //row.editMenuId = row.menuId;
      //buttton.menu = this.menus.find(s=>s.menuId == button.menuId).name;
    },
    handleSave(row) {
      //row.code = row.editCode;
      //row.name = row.editName;
      // row.description = row.editDesc;
      //row.menuId = row.editMenuId;
      //console.log(row);
      //row.menu2 = this.menus.find((s) => s.id == row.menuId).name;
      let tempRoles = this.roles.filter((t) => row.roles.includes(t.id));

      row.role = "";
      for (let tempRole of tempRoles) {
        row.role += tempRole.name + ", ";
      }

      //row.role =
      this.axios
        .put(API_REST_USER, row)
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

      row.account = row.editAccount;
      row.nickName = row.editNickName;
    },
    handleDelete(row) {
      row.mode = 0;
    },
    dialogYes() {
      this.dialogFormVisible = false;

      this.axios
        .post(API_REST_USER, this.form)
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
