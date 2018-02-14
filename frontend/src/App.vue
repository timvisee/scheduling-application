<template>
    <div id="app">

        <router-link :to="{ name: 'overview' }">Overview</router-link>
        <router-link :to="{ name: 'schedule' }">Schedule</router-link>

        <!-- Main page view, pages are rendered on here -->
        <router-view></router-view>

    </div>
</template>



<script>
export default {
    name: 'app'
}
</script>



<style lang="scss">
#app {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
}

h1, h2 {
    font-weight: normal;
}
</style>
