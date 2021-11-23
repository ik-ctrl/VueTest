<template>
  <ul id="todo-list">
    <TodoItem v-for="todo of todos" :key="todo.id"
              v-bind:todo="todo"
    />
  </ul>

</template>

<script>
import TodoItem from "./TodoItem"
export default {
  name: "TodoList",
  components:{
    TodoItem
  },
  props:{
    todos:{
      type:Object,
      required:true
    }
  }
}
</script>

<style scoped>

</style>
