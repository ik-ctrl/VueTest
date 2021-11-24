<template>
<li id="todoItem">
  {{todo.description}}
  <button v-on:click.prevent="$emit('removeTodo',todo.id)"

  >x</button>
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
