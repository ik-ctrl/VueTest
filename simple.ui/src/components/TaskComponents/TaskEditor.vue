<template>
  <div id="task-editor" class="task-editor">
    <strong>Описание задачи</strong>
    <div>
      <div>Задача:</div>
      <input type="text" v-model="task.taskTitle"/>
    </div>
    <TodoEdit v-for="(todo,index) in task.todos" :key="index"
                v-bind:todo="todo"
                v-on:changeTodoDescription="changeTodoDescription"></TodoEdit>
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
      console.log(todoId,newDescription);
      let changedTodo= this.todos.filter(item=>item.id===todoId)[0];
      changedTodo.description=newDescription;
      let todoIndex=this.todos.indexOf(i=>i.id===todoId);
      this.todos[todoIndex]=changedTodo;
      console.log(this.todos[todoIndex]);
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
