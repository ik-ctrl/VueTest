<template>
  <div>
    <input type="text" v-model="todoDescription">
    <button v-on:click.prevent="addTodo">+</button>
  </div>
</template>

<script>
export default {
  name: "TodoEmpty",
  methods:{
    addTodo(){
      let description= this.todoDescription.trim();
      if(description){
        this.$emit("addTodo",description);
      }
    }
  },
  data() {
    return {
      todoDescription:""
    }
  }
}
</script>

<style scoped>

</style>
