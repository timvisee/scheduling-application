<template>
    <div class="page-event">

        <h1>Event {{ headerSuffix }}</h1>
        <event :event="event" :editable="editable" />

        <router-link v-if="editable" :to="{ name: 'event', params: { id: event.id }}">View</router-link>
        <router-link v-else :to="{ name: 'event-edit', params: { id: event.id }}">Edit</router-link>

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
            event: {
                id: 1,
                name: 'Some name',
                description: 'Dingen'
            }
        }
    },
    computed: {
        headerSuffix: function() {
            return this.event.name.trim()
                ? " - " + this.event.name
                : "";
        }
    }
}
</script>



<style scoped lang="scss"></style>
