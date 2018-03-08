<template>
    <div class="page-event">

        <!-- A loading indicator -->
        <status v-bind="status" />

        <div v-if="status.ok">
            <h1>Event {{ headerSuffix }}</h1>
            <event :event="event" :editable="editable" />

            <router-link v-if="editable" :to="{ name: 'event', params: { id: event.eventId }}">View</router-link>
            <router-link v-else :to="{ name: 'event-edit', params: { id: event.eventId }}">Edit</router-link> </div>

    </div>
</template>



<script>
import Event from '../components/events/Event';
import Status from '../components/Status.vue';

export default {
    components: {
        Event,
        Status,
    },
    props: [
        "editable"
    ],
    data() {
        return {
            event: {},
            status: {
                loading: false,
                error: null,
            },
        }
    },
    computed: {
        headerSuffix: function() {
            return this.event.title
                ? " - " + this.event.title
                : "";
        }
    },
    created() {
        this.fetchEvent();
    },
    methods: {
        fetchEvent() {
            // Fetch the data
            this.api.event.fetch(this.$route.params.id, {
                progress: this,
                status: this.status,
            }).then(data => this.event = data);
        }
    },
}
</script>
