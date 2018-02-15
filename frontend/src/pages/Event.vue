<template>
    <div class="page-event">

        <h1>Event {{ headerSuffix }}</h1>
        <event :event="event" :editable="editable" />

        <router-link v-if="editable" :to="{ name: 'event', params: { id: event.eventId }}">View</router-link>
        <router-link v-else :to="{ name: 'event-edit', params: { id: event.eventId }}">Edit</router-link>

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
    created() {
        this.fetchEvent();
    },
    data() {
        return {
            event: {
                eventId: 1,
                title: '...',
                description: '...'
            }
        }
    },
    computed: {
        headerSuffix: function() {
            return this.event.title.trim()
                ? " - " + this.event.title
                : "";
        }
    },
    methods: {
        fetchEvent () {
            this.$http
                .get('http://localhost:5000/api/v1/event/details/' + this.$route.params.id)
                .then((response) => {
                    this.event = response.data;
                }, (response) => {
                    console.log("ERROR");
                    console.log(response);
                });
        }
    }
}
</script>



<style scoped lang="scss"></style>
