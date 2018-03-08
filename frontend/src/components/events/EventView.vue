<!-- See: https://router.vuejs.org/en/advanced/data-fetching.html -->

<template>
    <div class="event-view">

        <!-- A loading indicator -->
        <status v-bind="status" />

        <div v-if="status.ok" v-for="event in events">
            <event-item v-bind="event"></event-item>
        </div>

    </div>
</template>



<script>
import EventItem from './EventItem.vue'
import Status from './../Status.vue'

export default {
    name: 'event-view',
    created() {
        this.fetchEvents();
    },
    data() {
        return {
            events: [],
            status: {
                loading: false,
                error: null,
                ok: false,
            },
        }
    },
    components: {
        EventItem,
        Status,
    },
    methods: {
        fetchEvents() {
            // Fetch the data
            this.api.event.fetchAll({
                progress: this,
                status: this.status,
            }).then(data => this.events = data);
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
