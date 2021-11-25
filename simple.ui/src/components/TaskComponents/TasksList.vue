<template>
  <div>
    <div class="view-title">Список задач</div>
    <hr class="title-separator">
    <ul class="list">
      <div class="flex-table">
        <div class="half-view">
          <TaskEditor></TaskEditor>
        </div>
        <div class="half-view">
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

.flex-table{
  display:inline-flex;
}

.half-view{
  width: 50%;
}


.view-title {
  font-size: 4rem;
}
</style>
