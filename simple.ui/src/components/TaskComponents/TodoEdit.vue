<template>
  <div>
    <button сlass="base-button">тест</button>
    <input
        v-bind:class="{input_disable:menuParameters.isDisableInput}"
        v-on:change.prevent="changeTodoDescription(todo.id)"
        v-model="tmpTodoDescription"
        type="text">
    <button class="base-button" v-on:click.prevent="changeInputClass">edit</button>
    <button v-on:click.prevent="deleteDescription(todo.id)">del</button>
  </div>
</template>

<script>
export default {
  name: "TodoEdit",
  created() {
    this.tmpTodoDescription=this.todo.description;
  },
  props:{
    todo:{
      type:Object
    },
  },
  data () {
    return {
      menuParameters: {
        isDisableInput: true
      },
      tmpTodoDescription:""
    }
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
    },
    deleteDescription(todoId){
      console.log(todoId);
      this.$emit("deleteDescription",todoId)
    }
  },

}
</script>

<style scoped>
.input_disable{
  pointer-events: none;
}

</style>
