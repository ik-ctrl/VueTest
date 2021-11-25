<template>
  <div id="task-editor" class="task-editor">
    <strong>Описание задачи</strong>
    <div>
      <div>Задача:</div>
      <input type="text" v-model="task.taskTitle"/>
    </div>
    <TodoEditor v-for="todo in task.todos" :key="todo"
                v-bind:todo="todo"
                v-on:addTodo="addTodo"></TodoEditor>
    <button>Добавить</button>
  </div>
</template>

<script>
import TodoEditor from "./TodoEditor"
export default {
  name: "TaskEditor",
  components:{
    TodoEditor
  },
  methods:{
    addTodo(description) {
      const newTodo={
        id:Date.now(),
        description:description,
        confirm: false
      };
      this.task.todos.push(newTodo);
    }
  },
  data() {
    return{
      task:{
        id:-1 ,
        title:'' ,
        confirm: false,
        todos:[]
      }
    }
  }
}
</script>

<style scoped>
.title-editor{
  display: inline-flex;
}

.task-editor{
  display: flow;
}
</style>
