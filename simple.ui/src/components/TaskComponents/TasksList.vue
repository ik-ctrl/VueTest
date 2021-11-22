<template>
  <div id="taskList">
    {{tasks}}
    <ul>
      <TaskItem v-for="(task) of tasks" :key="task.id"
                v-bind:task="task"
      ></TaskItem>
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

</style>
