<template>
  <div>
    <div class="view-title">Список задач</div>
    <hr class="hr-separator">
    <ul>
      <TaskItem v-for="(task, index) of tasks" :key="task.id"
                v-bind:task="task"
                v-bind:index="index+1"
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
  }
}
</script>

<style scoped>
.hr-separator{
}
.view-title{
}
</style>
