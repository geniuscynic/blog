<template>
  <div class="navbar">
    <div class="tool" v-on:click="setCollapse ">
      <svg class="icon" aria-hidden="true">
        <use xlink:href="#icon-indent" v-if="collapse"></use>
        <use xlink:href="#icon-outdent" v-else></use>
      </svg>
    </div>
    <div class="bread">
      <el-breadcrumb separator-class="el-icon-arrow-right">
        <el-breadcrumb-item class="bread-item"
                v-for='(name,index) in matchedArr'
                :key='index'
                >
                {{ name }}
            </el-breadcrumb-item>

      </el-breadcrumb>
    </div>
  </div>
</template>

<script>
export default {
  name: 'HeaderNav',
  props: ['collapse'],
  data () {
    return {}
  },
  methods: {
    setCollapse () {
      this.$emit('setCollapse')
    }
  },
  mounted() {
    //console.log(this);
  },
  computed:{
        matchedArr(){
            let temp = [],temps = [];
            this.$route.matched.filter((item,index,self) => {
                // if(item.meta.title){
                //     const title = item.meta.title;
                //     temp.push(title);
                // }
                if(item.name){
                    const name = item.name;
                    temp.push(name);
                }
            });

            
            temp.filter((item,index,self) => {
                if(!temps.includes(item)){
                    temps.push(item);
                }
            });

            //console.log(temp, temps);
            return temps;
        }
}
}
</script>

<style lang="scss" scoped>
.navbar {
  display: flex;
  align-items: center;

  .tool {
    margin-left: 20px;
    .icon {
      color: #ffffff;
      width: 1.5em;
      height: 1.5em;
      cursor: pointer;
    }
  }
  .bread {
    padding: 24px;
  }
}
</style>

<style lang="scss">
span.bread-item {
  .el-breadcrumb__inner {
    color: #ffffff;
  }

  .el-breadcrumb__inner:last-child {
    color: #ffffff;
  }
}

span.bread-item:last-child {
  .el-breadcrumb__inner {
    color: rgb(255, 208, 75);
  }
}
</style>
