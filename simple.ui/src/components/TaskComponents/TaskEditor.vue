<template>
  <div id="task-editor" class="task-editor">
    <strong>Описание задачи</strong>
    <div class="task-title">
      <div >Задача:</div>
      <input class="task-description" type="text" v-model="taskTemplate.title"/>
      <button class="simple-button bg-dark font-light " v-on:click.prevent="createTask">+</button>
    </div>
    <TodoEdit v-for="(todo,index) in taskTemplate.todos" :key="index"
                v-bind:todo="todo"
                v-on:changeTodoDescription="changeTodoDescription"
                v-on:deleteDescription="deleteDescription"></TodoEdit>
    <div>
      <input type="text" v-model="todoDescription">
      <button  class="simple-button bg-dark font-light" v-on:click.prevent="createTodo">+</button>
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
      let tmpTodo= this.todoDescription.trim();
      if(tmpTodo){
        const newTodo={
          id:Date.now(),
          description:this.todoDescription,
          confirm: false
        };
        this.taskTemplate.todos.push(newTodo);
        this.todoDescription="";
      }
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
        todos:[
          {
            id:1,
            description:"sdsdsdsd"
          }
        ]
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

::v-deep(.bg-dark){
  background: #2c3e50;
}

::v-deep(.font-light){
  color: whitesmoke;
}

::v-deep(.simple-button){
  margin-left: 0.5vh;
  height: 1.5rem;
  width: 2rem;
  border-radius: 0.4rem;
}

.task-editor{
  display: block;
}


</style>
