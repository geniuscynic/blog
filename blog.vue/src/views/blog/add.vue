<template>
  <el-row type="flex" class="menu-add" justify="center">
    <el-col :span="12">
      <h1>添加文章</h1>
      <el-form ref="form" :model="form" label-width="80px">
        <el-form-item label="标题">
          <el-input v-model="form.title"></el-input>
        </el-form-item>
        <el-form-item label="作者">
          <el-input v-model="form.author"></el-input>
        </el-form-item>
        <el-form-item label="引用">
          <el-input v-model="form.quote"></el-input>
        </el-form-item>

        <el-form-item label="分类">
          <el-select v-model="form.category" placeholder="请选择分类">
            <el-option v-for="category in categories" :key="category.id" :label="category.name" :value="category.id"></el-option>
          </el-select>
        </el-form-item>

        <el-form-item label="内容">
          <el-input
            type="textarea"
            :autosize="{ minRows: 2, maxRows: 10 }"
            v-model="form.content"
          ></el-input>
        </el-form-item>
        <el-form-item label="标签">
         <el-select class="tag"
            v-model="form.tags"
            multiple
            filterable
            allow-create
            default-first-option
            placeholder="请添加标签">
    
  </el-select>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="onSubmit">立即创建</el-button>
          <el-button>取消</el-button>
        </el-form-item>
      </el-form>
    </el-col>
  </el-row>
</template>

<script>
import { API_REST_CATEGORY, API_REST_BLOG } from "@/plugins/const";

export default {
  name: "blog-add",
  data() {
    return {
      form: {
        title: "",
        author: "",
        quote: "",
        category: "",
        content: "",
        tags: []
      },
      categories: []
    };
  },
  methods: {
    onSubmit() {
      this.axios
      .post(API_REST_BLOG, this.form)
      .then((response) => {
        this.$message({
            message: "保存成功",
            type: "success",
        });

        
        //this.categories = response.data.response; 
        //console.log(response.data.response);
      })
      .catch((error) => {
        console.log(error.response);
        this.errorMsg = "服务器异常，请稍后再试";
        this.showError = true;
      });

    },
  },
  mounted() {
    this.axios
      .get(API_REST_CATEGORY)
      .then((response) => {
        this.categories = response.data.response; 
        //console.log(response.data.response);
      })
      .catch((error) => {
        console.log(error.response);
        this.errorMsg = "服务器异常，请稍后再试";
        this.showError = true;
      });
  },
};
</script>

<style lang="scss" scoped>
input {
  min-width: 300px;
}
</style>


<style lang="scss">
.menu-add {
  h1 {
    text-align: center;
  }

  input {
    min-width: 200px;
  }
}

.tag {
  display: flex;
}
</style>