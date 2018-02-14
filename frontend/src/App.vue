<template>
    <div id="app">
        <timetable></timetable>
    </div>
</template>



<script>
import Timetable from './components/timetable/Timetable.vue'

export default {
    name: 'app',
    data () {
        return {}
    },
    components: {
        Timetable
    }
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
