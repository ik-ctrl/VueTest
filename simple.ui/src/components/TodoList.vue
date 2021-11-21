<template>
  <div id="todo-list">
    <h1 class="title">Список дел</h1>
    <hr class="horizontal-line">
    <ul class="todo-list">
      <TodoItem  v-for="(todo, index) of todos" :key="todo.id"
                  v-bind:index="index"
                  v-bind:todo="todo"></TodoItem>
    </ul>
  </div>
</template>

<script>
import  TodoItem from "@/components/TodoItem"

export default {
  name: "TodoList",
  props:{
    todos:{
      type: Object,
      required: true
    }
  },
  components:{
    TodoItem
  }
}
</script>

<style scoped>
.title{
  font-family: italic;
}

.horizontal-line{
  background: #430c14;
  content: '$';
  padding: 0 4px;
  position: relative;
  height: 0.2rem;
}

.todo-list{
  list-style: none
}

</style>
