<template>
  <div class="row">
    <textarea
        class="description_size"
              v-bind:class="{input_disable:menuParameters.isDisableInput}"
              v-on:change.prevent="changeTodoDescription(todo.id)"
              v-model="tmpTodoDescription">
    </textarea>
<!--    <input-->
<!--        class="description_size"-->
<!--        v-bind:class="{input_disable:menuParameters.isDisableInput}"-->
<!--        v-on:change.prevent="changeTodoDescription(todo.id)"-->
<!--        v-model="tmpTodoDescription"-->
<!--        type="text">-->
    <div class="button-control">
      <button class="simple-button bg-dark font-light" v-on:click.prevent="changeInputClass">edit</button>
      <button class="simple-button bg-dark font-light" v-on:click.prevent="deleteDescription(todo.id)">del</button>
    </div>
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

.description_size{
  margin-top: 0.5rem;
  margin-left: 2rem;
  width:60%;
  align-content:center;

}

.input_disable{
  pointer-events: none;
}

.button-control{
  vertical-align: center;
}

.row{
  display: flex;
}
</style>
