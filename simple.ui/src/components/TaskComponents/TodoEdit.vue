<template>
  <div>
    <input
        v-bind:class="{input_disable:menuParameters.isDisableInput}"
        v-on:change.prevent="changeTodoDescription(todo.id)"
        v-model="tmpTodoDescription"
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
  created() {
    this.tmpTodoDescription=this.todo.description;
  },
  methods:{
    changeInputClass(){
      this.menuParameters.isDisableInput=!this.menuParameters.isDisableInput;
    },
    changeTodoDescription(todoId) {
      let newDesc= this.tmpTodoDescription.trim();
      if(newDesc){
       this.$emit("changeTodoDescription",todoId,newDesc);
       this.menuParameters.isDisableInput=!this.menuParameters.isDisableInput;
      }
    }
  },
  data () {
    return {
      menuParameters: {
        isDisableInput: true
      },
      tmpTodoDescription:""
    }
  },
}
</script>

<style scoped>
.input_disable{
  pointer-events: none;
}

</style>
