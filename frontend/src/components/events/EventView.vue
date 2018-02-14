<template>
    <div class="event-view">

        <div v-for="event in events">
            <event-item v-bind="event"></event-item>
        </div>

    </div>
</template>



<script>
import EventItem from './EventItem.vue'

export default {
    name: 'event-view',
    data () {
        return {
            events: [
                {
                    id: 1,
                    name: "Event #1",
                    description: "Description #1"
                },
                {
                    id: 2,
                    name: "Event #2",
                    description: "Description #2"
                },
            ]
        }
    },
    components: {
        EventItem
    }
}
</script>



<style scoped lang="scss">
.event-view {
    border: 1px solid gray;
    padding: 8px;
    display: inline-block;
}

h1 {
    margin: 0 0 8px 0;
    padding: 0;
}
</style>
