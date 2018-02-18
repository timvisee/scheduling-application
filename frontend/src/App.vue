<template>
    <div id="app">

        <router-link :to="{ name: 'overview' }">Overview</router-link>
        <router-link :to="{ name: 'schedule' }">Schedule</router-link>

        <!-- Main page view, pages are rendered on here -->
        <transition name="fade">
            <router-view></router-view>
        </transition>

        <!-- Page progress bar, on the top -->
        <vue-progress-bar></vue-progress-bar>

    </div>
</template>



<script>
export default {
    name: 'app',
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

// Fade transition styles
.fade-enter-active, .fade-leave-active {
    transition-property: opacity;
    transition-duration: .25s;
}

.fade-enter-active {
    transition-delay: .25s;
}

.fade-enter, .fade-leave-active {
    opacity: 0
}
</style>
