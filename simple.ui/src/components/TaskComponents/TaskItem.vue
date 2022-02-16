<template>
  <li class="task-item">
    <div class="task-body">
      <div class="task-menu">
        <strong class="menu-item-margin">{{index}}.</strong>
        <div class="menu-item-margin">{{task?.['title']}}</div>
        <button v-on:click.prevent="$emit('removeTask',task.id)"
            class="menu-item-margin remove-button">X</button>
      </div>
      <div class="task-description">
        <TodoList v-bind:todos="task.todos"
                  v-on:removeTodo="removeTodo"
        />
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
  },
  methods:{
    removeTodo(todoId){
      this.$emit("removeTodo",todoId,this.task.id)
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
.task-menu{
  display: flex;
}
.menu-item-margin{
  margin: 20px 10px 20px 10px;
}
.remove-button{
  background: darkred;
  border-radius: 0.4rem;
  border:0.1rem solid whitesmoke;
  color: whitesmoke;
}

</style>
