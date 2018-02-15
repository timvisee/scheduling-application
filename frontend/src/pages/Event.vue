<template>
    <div class="page-event">

        <p v-if="loading">Loading...</p>

        <p v-else-if="error">ERROR!</p>

        <div v-else>
            <h1>Event {{ headerSuffix }}</h1>
            <event :event="event" :editable="editable" />

            <router-link v-if="editable" :to="{ name: 'event', params: { id: event.eventId }}">View</router-link>
            <router-link v-else :to="{ name: 'event-edit', params: { id: event.eventId }}">Edit</router-link> </div>

    </div>
</template>



<script>
import Event from '../components/events/Event'

export default {
    components: {
        Event,
    },
    props: [
        "editable"
    ],
    data() {
        return {
            event: {},
            loading: false,
            error: null,
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
            // Set the loading and error state
            this.loading = true, this.error = null;

            // Fetch the event data
            this.api.event.fetch(this.$route.params.id)
                .then(data => this.event = data)
                .catch(err => this.error = err)
                .finally(() => this.loading = false);
        }
    }
}
</script>



<style scoped lang="scss"></style>
