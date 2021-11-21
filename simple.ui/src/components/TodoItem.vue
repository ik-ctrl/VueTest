<template>
  <li id="todo-item">
    <span>
      <input type="checkbox">
      <strong>{{index+1}}</strong>
      <p>{{todo.task}}</p>
      <button>X</button>
    </span>

  </li>
</template>

<script>
export default {
  name: "TodoItem",
  props: {
    todo:{
     type:Object,
     required:true,
   },
    index:{
     type: Number,
      required:true,
    }
  }
}
</script>

<style scoped>
.todo-item{
  display: inline-flex;
  justify-content: space-between;
}

.remove-button{

}
</style>
