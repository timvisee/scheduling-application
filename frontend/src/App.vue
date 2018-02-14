<template>
    <div id="app">
        <router-view></router-view>

        <router-link to="/">Go to Home</router-link>
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
