<template>
  <div>
    <div class="view-title">Список задач</div>
    <hr class="title-separator">
    <ul class="list">
      <TaskItem v-for="(task, index) of tasks" :key="task.id"
                v-bind:task="task"
                v-bind:index="index+1"
                v-on:removeTask="removeTask"
                v-on:removeTodo="removeTodo"
      />
    </ul>

  </div>
</template>

<script>
import TaskItem from "./TaskItem";
export default {
  name: "TasksList",
  components: {TaskItem},
  props:{
    tasks:{
      type: Object,
      required: true
    }
  },
  methods:{
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

.list{

}

.view-title {
  font-size: 4rem;
}
</style>
