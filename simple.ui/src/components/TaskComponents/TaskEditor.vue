<template>
  <div id="task-editor" class="task-editor">
    <strong>Описание задачи</strong>
    <div class="task-title">
      <div >Задача:</div>
      <input class="task-description" type="text" v-model="taskTemplate.title"/>
      <button class="button" v-on:click.prevent="createTask">+</button>
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

.task-title{
  display: flex;
}
.task-description{

}

.button{
  background: #2c3e50;
  color: whitesmoke;
  margin-left: 5px;
  height: 20px;
  width: 20px;
  border-radius: 40%;
}

.task-editor{
  display: flow;
}


</style>
