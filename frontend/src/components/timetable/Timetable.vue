<template>
    <div class="timetable">

        <h1>Timetable</h1>

        <div v-for="event in events">
            <event v-bind="event"></event>
        </div>

    </div>
</template>



<script>
import Event from './Event.vue'

export default {
    name: 'timetable',
    data () {
        return {
            events: [
                {
                    name: "Event #1",
                    description: "Description #1"
                },
                {
                    name: "Event #2",
                    description: "Description #2"
                },
            ]
        }
    },
    components: {
        Event
    }
}
</script>



<style scoped lang="scss">
.timetable {
    border: 1px solid gray;
    padding: 8px;
}

h1 {
    margin: 0 0 8px 0;
    padding: 0;
}
</style>
