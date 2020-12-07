<template>
  <div id="permission-menu-container">
    <el-row :gutter="24">
      <el-col :span="12">
        <el-tree
          :data="menus"
          show-checkbox
          node-key="id"
          :default-expand-all="true"
          :default-checked-keys="checkMenuId"
          :props="defaultProps"
        >
        </el-tree>
         <el-button type="primary" @click="saveMenuPermission">保存</el-button>
      </el-col>
      <el-col :span="12"></el-col>
    </el-row>
  </div>
</template>

<script>
import { API_REST_ROLE, API_REST_MENU } from "@/plugins/const";
import { mapActions, mapState } from "vuex";

export default {
  name: "menu-permission",
  data() {
    return {
      roles: [],
      roleId: 0,
      checkMenuId: [],
      // menus: [],
      formLabelWidth: "120px",
      defaultProps: {
        children: "childMenus",
        label: "name",
      },
    };
  },
  mounted() {
    //console.log(this.$route.params);

    this.roleId = this.$route.params.id;

    this.initRole();
    this.init();
  },
  computed: {
    ...mapState(["menus"]),
  },
  methods: {
    initRole() {
      //const _this = this;
      this.axios
        .get(API_REST_ROLE)
        .then((response) => {
          //console.log(this.menus);
          let temps = response.data.response;

          this.roles = temps;
        })
        .catch((error) => {
          console.log(error);
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
        });
    },

    init() {
      this.axios
        .get(`${API_REST_ROLE}/${this.roleId}/menu`)
        .then((response) => {
          const temp = [];

          //console.log(response.data);
          for (const data of response.data.response) {
            temp.push(data.menuId);
          }

          this.checkMenuId = temp;
          console.log(this.checkMenuId);
        })
        .catch((error) => {
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
        });
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

    saveMenuPermission() {
      
    }
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
