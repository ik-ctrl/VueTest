<template>
  <body  class="app-body">
  <div id="app">
    <TasksList v-bind:tasks="tasks"
                v-on:removeTask="removeTask"
                v-on:removeTodo="removeTodo"
    ></TasksList>
  </div>
  </body>
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
            {'id': 1 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. A aliquam, aut cumque dicta dignissimos distinctio dolor, doloremque ducimus eligendi eos exercitationem facere ipsum libero necessitatibus neque nulla numquam perferendis quae quas quod quos recusandae repellendus sapiente similique velit voluptatem voluptates. Beatae distinctio harum ipsam modi omnis quam sapiente voluptatem voluptatum!' ,'confirm': false},
            {'id': 2 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. A aliquam, aut cumque dicta dignissimos distinctio dolor, doloremque ducimus eligendi eos exercitationem facere ipsum libero necessitatibus neque nulla numquam perferendis quae quas quod quos recusandae repellendus sapiente similique velit voluptatem voluptates. Beatae distinctio harum ipsam modi omnis quam sapiente voluptatem voluptatum!' ,'confirm': false},
            {'id': 3 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. A aliquam, aut cumque dicta dignissimos distinctio dolor, doloremque ducimus eligendi eos exercitationem facere ipsum libero necessitatibus neque nulla numquam perferendis quae quas quod quos recusandae repellendus sapiente similique velit voluptatem voluptates. Beatae distinctio harum ipsam modi omnis quam sapiente voluptatem voluptatum!' ,'confirm': false},
            {'id': 4 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. A aliquam, aut cumque dicta dignissimos distinctio dolor, doloremque ducimus eligendi eos exercitationem facere ipsum libero necessitatibus neque nulla numquam perferendis quae quas quod quos recusandae repellendus sapiente similique velit voluptatem voluptates. Beatae distinctio harum ipsam modi omnis quam sapiente voluptatem voluptatum!' ,'confirm': false},
            {'id': 5 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. A aliquam, aut cumque dicta dignissimos distinctio dolor, doloremque ducimus eligendi eos exercitationem facere ipsum libero necessitatibus neque nulla numquam perferendis quae quas quod quos recusandae repellendus sapiente similique velit voluptatem voluptates. Beatae distinctio harum ipsam modi omnis quam sapiente voluptatem voluptatum!' ,'confirm': false},
            {'id': 6 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. A aliquam, aut cumque dicta dignissimos distinctio dolor, doloremque ducimus eligendi eos exercitationem facere ipsum libero necessitatibus neque nulla numquam perferendis quae quas quod quos recusandae repellendus sapiente similique velit voluptatem voluptates. Beatae distinctio harum ipsam modi omnis quam sapiente voluptatem voluptatum!' ,'confirm': false},
          ]
        },
        {
          'id': 2,
          'title': 'Задание 2' ,
          'confirm': false,
          todos:
              [
            {'id': 1 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Numquam placeat sunt velit vitae! Assumenda est in necessitatibus qui repellendus voluptas?' ,'confirm': false},
            {'id': 2 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Numquam placeat sunt velit vitae! Assumenda est in necessitatibus qui repellendus voluptas?','confirm': false},
            {'id': 3 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Numquam placeat sunt velit vitae! Assumenda est in necessitatibus qui repellendus voluptas?','confirm': false},
            {'id': 4 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Numquam placeat sunt velit vitae! Assumenda est in necessitatibus qui repellendus voluptas?','confirm': false},
            {'id': 5 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Numquam placeat sunt velit vitae! Assumenda est in necessitatibus qui repellendus voluptas?','confirm': false},
            {'id': 6 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Numquam placeat sunt velit vitae! Assumenda est in necessitatibus qui repellendus voluptas?','confirm': false},
          ]
        },
        {
          'id': 3 ,
          'title': 'Задание 3' ,
          'confirm': false,
          todos:[
            {'id': 1 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 2 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 3 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 4 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 5 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 6 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
          ]
        },
        {
          'id':4 ,
          'title': 'Задание 4' ,
          'confirm': false,
          todos:[
            {'id': 1 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 2 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 3 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 4 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 5 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 6 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
          ]
        },
        {
          'id': 5 ,
          'title': 'Задание 5' ,
          'confirm': false,
          todos:[
            {'id': 1 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 2 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 3 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 4 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 5 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
            {'id': 6 , 'description': 'Lorem ipsum dolor sit amet, consectetur adipisicing elit. Architecto assumenda, blanditiis excepturi fugiat illo neque nostrum provident temporibus totam veritatis. A blanditiis enim fugiat nam vel! Assumenda aut commodi culpa possimus provident repellat vitae. At deleniti doloribus eum laudantium modi, nam unde voluptate. Asperiores, autem.' ,'confirm': false},
          ]
        }
      ]
    }
  },
  components:{
    TasksList
  },
  methods:{
    removeTask(taskId){
      this.tasks=this.tasks.filter(i=>i.id!==taskId);
    },
    removeTodo(todoId,taskId){
      let task= this.tasks.filter(task=>task.id==taskId)[0];
      console.log(task.todos);
      task.todos= task.todos.filter(todo=>todo.id!=todoId);
      console.log(task.todos);
      let itemIndex= this.tasks.indexOf(item=>item.id===task.id);
      this.tasks[itemIndex]=task;
    }
  }
}

</script>

<style>

.app-body{
  margin:0px;
  padding: 0px;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
  font-family: "Fira Code Retina";
}
</style>
