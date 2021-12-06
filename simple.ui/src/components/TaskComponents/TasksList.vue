<template>
  <div class="task-list-component">
    <div class="view-title">Список задач</div>
    <hr class="title-separator">
    <ul class="list">
      <div class="task-table">
        <div class="new-task-container">
          <TaskEditor v-on:createTask="createNewTask"></TaskEditor>
        </div>
        <div class="task-list-container">
          <TaskItem v-for="(task, index) of tasks" :key="task.id"
                    v-bind:task="task"
                    v-bind:index="index+1"
                    v-on:removeTask="removeTask"
                    v-on:removeTodo="removeTodo"
          />
        </div>
      </div>
    </ul>

  </div>
</template>

<script>
import TaskItem from "./TaskItem";
import TaskEditor from "./TaskEditor"
export default {
  name: "TasksList",
  components: {
    TaskItem,
    TaskEditor
  },
  props:{
    tasks:{
      type: Object,
      required: true
    }
  },
  methods:{
    createNewTask(newTask){
      this.$emit('createTask',newTask);
    },
    removeTask(taskId){
      this.$emit("removeTask",taskId);
    },
    removeTodo(todoId,taskId){
      this.$emit("removeTodo",todoId,taskId);
    }
  }
}
</script>

<style scoped>
.title-separator{
  size: 1rem;
}

.task-table{
  display:flex;
}

.new-task-container{
  flex:1 0 auto;
  width: 50%;
  margin: 1%;
}

.task-list-container{
  flex:1 1 auto;
  text-align: center;
  margin: 1%;
}

.view-title {
  font-size: 4rem;
}

.hidden{
  visibility: hidden;
}




</style>
