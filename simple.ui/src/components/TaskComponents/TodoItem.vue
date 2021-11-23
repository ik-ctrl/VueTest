<template>
<li id="todoItem">
  {{todo.description}}
</li>
</template>

<script>
export default {
  name: "TodoItem",
  props:{
    todo:Object,
  }
}
</script>

<style scoped>

</style>
