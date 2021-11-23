<template>
  <li class="task-item">
    <div class="task-body">
      {{index}} {{task?.['title']}}
      <button>X</button>
      <div class="task-description">
        <TodoList v-bind:todos="task.todos"/>
      </div>
    </div>
  </li>
</template>

<script>
import TodoList from "./TodoList"
export default {
  name: "TaskItem",
  components:{
    TodoList
  },
  props:{
    task:{
      type: Object,
      required: true
    },
    index:{
      type: Number,
      required: true
    }
  }
}
</script>

<style scoped>
.task-item {
  list-style-type: none;
}

.task-body{
  background: gray;
  display: block;
  border: 0.2rem solid #2c3e50;
  border-radius: 1rem;
  padding: 10px;
}
</style>
