<template>
  <div id="task-editor" class="task-editor">
    <strong>Описание задачи</strong>
    <div>
      <div>Задача:</div>
      <input type="text" v-model="taskTemplate.title"/>
      <button v-on:click.prevent="createTask">Добавить</button>
    </div>
    <TodoEdit v-for="(todo,index) in taskTemplate.todos" :key="index"
                v-bind:todo="todo"
                v-on:changeTodoDescription="changeTodoDescription"
                v-on:deleteDescription="deleteDescription"></TodoEdit>
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
    createTask(){
      if(this.taskTemplate.title){
        const newTask= {
          id:Date.now(),
          title:this.taskTemplate.title ,
          confirm: false,
          todos:this.taskTemplate.todos
        }
        console.log(newTask);
        this.$emit('createTask',newTask);
      }
    },
    addTodo(description) {
      console.log(description)
      const newTodo={
        id:Date.now(),
        description:description,
        confirm: false
      };
      this.taskTemplate.todos.push(newTodo);
    },
    changeTodoDescription(todoId,newDescription){
      let changedTodo= this.taskTemplate.todos.filter(item=>item.id===todoId);
      changedTodo.description=newDescription;
      let todoIndex=this.taskTemplate.todos.indexOf(i=>i.id===todoId);
      this.taskTemplate.todos[todoIndex]=changedTodo;
      console.log(this.taskTemplate.todos[todoIndex]);
    },
    deleteDescription(todoId){
      let todos= this.taskTemplate.todos;
      todos=todos.filter(i=>i.id!==todoId);
      this.taskTemplate.todos=todos;
    }
  },
  data() {
    return{
      taskTemplate:{
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
