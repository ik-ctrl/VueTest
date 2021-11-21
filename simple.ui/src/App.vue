<template>
  <div id="app">
    <TodoList v-bind:todos="todos"></TodoList>
  </div>
</template>

<script>
import  TodoList from "@/components/TodoList"

export default {
  name:'App',
  data() {
    return {
      todos:[
        {'id':1 , "task": "тест 1" ,"confirm": false},
        {'id':1 , "task": "тест 1" ,"confirm": false},
        {'id':1 , "task": "тест 1" ,"confirm": false},
        {'id':1 , "task": "тест 1" ,"confirm" :false},
        {'id':1 , "task": "тест 1" ,"confirm": false}
      ]
    }
  },
  components:{
    TodoList
  }
}

</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
@import'~bootstrap/dist/css/bootstrap.css';
</style>
