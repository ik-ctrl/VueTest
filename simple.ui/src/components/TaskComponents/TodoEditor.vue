<template>
  <div>

    <input type="text" v-model="description"/>
    <button v-on:click.prevent="addTodo">+</button>
  </div>
</template>

<script>
export default {
  name: "TodoEditor",
  props:{
    todos:{
      type:Object
    }
  },
  methods:{
    addTodo(){
      let tmpDescription= this.description.trim();
      if(tmpDescription){
        console.log(tmpDescription);
        this.$emit('addTodo',tmpDescription);
      }
    }
  },
  data () {
    return {
      description:''
    }
  },
}
</script>

<style scoped>

</style>
