<template>
  <div id="task-editor" class="task-editor">
    <strong>Описание задачи</strong>
    <div>
      <div>Задача:</div>
      <input type="text" v-model="task.taskTitle"/>
    </div>
    <TodoEdit v-for="(todo,index) in task.todos" :key="index"
                v-bind:todo="todo"
                v-on:changeTodoDescription="changeTodoDescription"
                v-on:deleteDescription="deleteDescription"></TodoEdit>
    <button>Добавить</button>
    <TodoEmpty v-on:addTodo="addTodo"/>
  </div>
</template>

<script>
import TodoEdit from "./TodoEdit"
import TodoEmpty from "./TodoEmpty"
export default {
  name: "TaskEditor",
  components:{
    TodoEdit,
    TodoEmpty
  },
  methods:{
    addTodo(description) {
      console.log(description)
      const newTodo={
        id:Date.now(),
        description:description,
        confirm: false
      };
      this.task.todos.push(newTodo);
    },
    changeTodoDescription(todoId,newDescription){
      let changedTodo= this.task.todos.filter(item=>item.id===todoId);
      changedTodo.description=newDescription;
      let todoIndex=this.task.todos.indexOf(i=>i.id===todoId);
      this.task.todos[todoIndex]=changedTodo;
      console.log(this.task.todos[todoIndex]);
    },
    deleteDescription(todoId){
      let todos= this.task.todos;
      todos=todos.filter(i=>i.id!==todoId);
      this.task.todos=todos;
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
