<template>
  <div id="app">
     <TasksList v-bind:tasks="tasks"></TasksList>
  </div>
</template>

<script>
import TasksList from "./components/TaskComponents/TasksList";
export default {
  name:'App',
  data() {
    return {
      tasks:[
        {
          'id':1 ,
          'title': 'Задание 1' ,
          'confirm': false,
          todos:[
            {'id':1 , 'task': 'тест 1' ,'confirm': false},
            {'id':2 , 'task': 'тест 1' ,'confirm': false},
            {'id':3 , 'task': 'тест 1' ,'confirm': false},
            {'id':4 , 'task': 'тест 1' ,'confirm': false},
            {'id':5 , 'task': 'тест 1' ,'confirm': false},
            {'id':6 , 'task': 'тест 1' ,'confirm': false},
          ]
        },
        {
          'id': 2,
          'title': 'Задание 2' ,
          'confirm': false,
          todos:[
            {'id': 1 , 'task': 'тест 1' ,'confirm': false},
            {'id': 2 , 'task': 'тест 1' ,'confirm': false},
            {'id': 3 , 'task': 'тест 1' ,'confirm': false},
            {'id': 4 , 'task': 'тест 1' ,'confirm': false},
            {'id': 5 , 'task': 'тест 1' ,'confirm': false},
            {'id': 6 , 'task': 'тест 1' ,'confirm': false},
          ]
        },
        {
          'id': 3 ,
          'title': 'Задание 3' ,
          'confirm': false,
          todos:[
            {'id': 1 , 'task': 'тест 1' ,'confirm': false},
            {'id': 2 , 'task': 'тест 1' ,'confirm': false},
            {'id': 3 , 'task': 'тест 1' ,'confirm': false},
            {'id': 4 , 'task': 'тест 1' ,'confirm': false},
            {'id': 5 , 'task': 'тест 1' ,'confirm': false},
            {'id': 6 , 'task': 'тест 1' ,'confirm': false},
          ]
        },
        {
          'id':4 ,
          'title': 'Задание 1' ,
          'confirm': false,
          todos:[
            {'id': 1 , 'task': 'тест 1' ,'confirm': false},
            {'id': 2 , 'task': 'тест 1' ,'confirm': false},
            {'id': 3 , 'task': 'тест 1' ,'confirm': false},
            {'id': 4 , 'task': 'тест 1' ,'confirm': false},
            {'id': 5 , 'task': 'тест 1' ,'confirm': false},
            {'id': 6 , 'task': 'тест 1' ,'confirm': false},
          ]
        },
        {
          'id': 5 ,
          'title': 'Задание 1' ,
          'confirm': false,
          todos:[
            {'id':1 , 'task': 'тест 1' ,'confirm': false},
            {'id':2 , 'task': 'тест 1' ,'confirm': false},
            {'id':3 , 'task': 'тест 1' ,'confirm': false},
            {'id':4 , 'task': 'тест 1' ,'confirm': false},
            {'id':5 , 'task': 'тест 1' ,'confirm': false},
            {'id':6 , 'task': 'тест 1' ,'confirm': false},
          ]
        }
      ]
    }
  },
  components:{
    TasksList,
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
</style>
