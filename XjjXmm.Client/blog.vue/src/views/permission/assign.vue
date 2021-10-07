<template>
  <div id="permission-menu-container">
    <el-row :gutter="24">
      <el-col :span="4">
        <el-tree
          :data="menus"
          show-checkbox
          node-key="id"
          ref="menuTree"
          :default-expand-all="true"
          :default-checked-keys="checkMenuId"
          :props="defaultProps"
        >
        </el-tree>
        <el-button type="primary" @click="saveMenuPermission">保存</el-button>
      </el-col>
      <el-col :span="4">
        <el-tree
          :data="menus"
          show-checkbox
          node-key="id"
          ref="buttonTree"
          :default-expand-all="true"
          :default-checked-keys="checkButtonId"
          :props="defaultProps2"
        >
        </el-tree>
        <el-button type="primary" @click="saveButtonPermission">保存</el-button>
      </el-col>
      <el-col :span="12">
        <el-table :data="apis" style="width: 100%" ref="multipleTable" @selection-change="handleSelectionChange">
          <el-table-column type="selection" width="55"> </el-table-column>
          <el-table-column prop="id" label="id" width="80"> </el-table-column>
          <el-table-column
            prop="controller"
            label="控制器"
            width="180"
            column-key="controller"
            :filters="filterController"
            :filter-method="filterHandler"
          >
          </el-table-column>
          <el-table-column prop="routePath" label="RoutePath" width="180">
          </el-table-column>
          <el-table-column prop="httpMethod" label="HttpMethod" width="180">
          </el-table-column>
        </el-table>
        <el-button type="primary" @click="saveApiPermission">保存</el-button>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import { API_REST_ROLE, API_REST_MENU, API_REST_BUTTON } from "@/plugins/const";
import { mapActions, mapState } from "vuex";

export default {
  name: "menu-permission",
  data() {
    return {
      roles: [],

      buttons: [],
      checkButtonId: [],

      roleId: 0,
      checkMenuId: [],

      apis: [],
      filterController: [],
      checkApis: [],

      // menus: [],
      formLabelWidth: "120px",
      defaultProps: {
        children: "childMenus",
        label: "name",
      },
      defaultProps2: {
        children: "buttons",
        label: "name",
      },
      defaultProps3: {
        children: "buttons",
        label: "RoutePath",
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
          //console.log(this.checkMenuId);
        })
        .catch((error) => {
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
        });

      this.axios
        .get(`${API_REST_ROLE}/${this.roleId}/button`)
        .then((response) => {
          const temp = [];

          //console.log(response.data);
          for (const data of response.data.response) {
            temp.push(data.buttonId);
          }

          this.checkButtonId = temp;
          //console.log(this.checkMenuId);
        })
        .catch((error) => {
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
        });

      this.axios
        .get(`${API_REST_ROLE}/GetApiMethods`)
        .then((response) => {
          //console.log(this.menus);
          let temps = response.data.response;

          for (let api of temps) {
            //console.log(this.api);

            if (
              this.filterController.findIndex((t) => t.text == api.controller) <
              0
            ) {
              let temp = {
                text: api.controller,
                value: api.controller,
              };

              this.filterController.push(temp);
            }
          }

          this.apis = temps;

          this.axios
            .get(`${API_REST_ROLE}/${this.roleId}/apis`)
            .then((response) => {
              //const temp = [];

              //console.log(response.data);
              for (const data of response.data.response) {
                let row = this.apis.find(t=>t.id == data.apiId);

                this.$refs.multipleTable.toggleRowSelection(row);
              }

              //this.checkApis = temp;
              //console.log(this.checkMenuId);
            })
            .catch((error) => {
              this.errorMsg = "服务器异常，请稍后再试";
              this.showError = true;
            });
        })
        .catch((error) => {
          console.log(error);
          this.errorMsg = "服务器异常，请稍后再试";
          this.showError = true;
        });
    },

    filterHandler(value, row, column) {
      //console.log(value, row, column);

      const property = column["property"];
      return row[property] === value;
    },
    handleSelectionChange(val) {
        this.checkApis = val;
      },
    saveMenuPermission() {
      // console.log( this.$refs.menuTree.getHalfCheckedNodes(),1,
      // this.$refs.menuTree.getHalfCheckedKeys(),2,
      // this.$refs.menuTree.getCurrentKey(),3,
      // this.$refs.menuTree.getCurrentNode(),4,
      // this.$refs.menuTree.getCheckedKeys(),5,
      // this.$refs.menuTree.getCheckedNodes(),);

      this.axios
        .post(
          `${API_REST_ROLE}/${this.roleId}/menu`,
          this.$refs.menuTree.getCheckedKeys()
        )
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
    saveButtonPermission() {
      // console.log( this.$refs.menuTree.getHalfCheckedNodes(),1,
      // this.$refs.menuTree.getHalfCheckedKeys(),2,
      // this.$refs.menuTree.getCurrentKey(),3,
      // this.$refs.menuTree.getCurrentNode(),4,
      // this.$refs.menuTree.getCheckedKeys(),5,
      // this.$refs.menuTree.getCheckedNodes(),);

      this.axios
        .post(
          `${API_REST_ROLE}/${this.roleId}/button`,
          this.$refs.menuTree.getCheckedKeys()
        )
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
    saveApiPermission() {
      console.log(this.checkApis);
      const ids = [];

      for(const apis of this.checkApis) {
        ids.push(apis.id);
      }
      this.axios
        .post(
          `${API_REST_ROLE}/${this.roleId}/apis`,
          ids
        )
        .then((response) => {
         
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
