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
    <div>
      <input type="text" v-model="todoDescription">
      <button v-on:click.prevent="createTodo">+</button>
    </div>
  </div>
</template>

<script>
import TodoEdit from "./TodoEdit"
export default {
  name: "TaskEditor",
  components:{
    TodoEdit
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
        this.$emit('createTask',newTask);
        this.clearTodoTemplate();
      }
    },
    createTodo(){
      console.log(this.todoDescription)
      const newTodo={
        id:Date.now(),
        description:this.todoDescription,
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
    },
    clearTodoTemplate(){
      this.todoDescription="";
      this.taskTemplate.title="";
      this.taskTemplate.todos= [];
    }
  },
  data() {
    return{
      taskTemplate:{
        title:'' ,
        todos:[]
      },
      todoDescription:""
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
