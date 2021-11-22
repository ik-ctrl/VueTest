<template>
  <li id="taskItem">
    {{task?.['title']}}
  </li>
</template>

<script>
export default {
  name: "TaskItem",
  props:{
    task:{
      type: Object,
      required: true
    },
    index:{
      type: Number,
      required: true
    }
  }
}
</script>

<style scoped>

</style>
