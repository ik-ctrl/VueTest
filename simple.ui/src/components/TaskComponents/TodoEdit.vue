<template>
  <div>
    <input
        v-bind:class="{input_disable:this.menuParameters.isDisableInput}"
        v-bind:value="this.todo.description"
        v-on:change.prevent="changeTodo"
        type="text">
    <button v-on:click.prevent="changeInputClass">edit</button>
    <button>del</button>
  </div>
</template>

<script>
export default {
  name: "TodoEdit",
  props:{
    todo:{
      type:Object
    },
  },
  methods:{
    changeInputClass(){
      this.menuParameters.isDisableInput=!this.menuParameters.isDisableInput;
    },
    changeTodo() {
      console.log(this.todo.description);
    }
  },
  data () {
    return {
      menuParameters: {
        isDisableInput: true
      }
    }
  },
}
</script>

<style scoped>
.input_disable{
  pointer-events: none;
}

</style>
