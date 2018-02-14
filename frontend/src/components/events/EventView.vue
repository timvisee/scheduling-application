<!-- See: https://router.vuejs.org/en/advanced/data-fetching.html -->

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
    created () {
        this.fetchEvents();
    },
    data () {
        return {
            events: []
        }
    },
    components: {
        EventItem
    },
    methods: {
        fetchEvents () {
            this.$http
                .get('/api/v1/event')
                .then((response) => {
                    this.events = response.data;
                }, (response) => {
                    console.log("ERROR");
                    console.log(response);
                });
        }
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
