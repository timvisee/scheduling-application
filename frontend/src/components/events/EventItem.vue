<template>
    <div class="event-item">

        <h1>{{ title }}</h1>
        <p>{{ description }}</p>

        <router-link :to="{ name: 'event', params: { id: eventId }}">View</router-link>
        <router-link :to="{ name: 'event-edit', params: { id: eventId }}">Edit</router-link>
        <router-link :to="{ name: 'event-delete', params: { id: eventId }}">Delete</router-link>

    </div>
</template>



<script>
export default {
    name: 'event-item',
    props: [
        "eventId",
        "title",
        "description"
    ],
}
</script>



<style scoped lang="scss">
.event-item {
    border: 1px solid gray;
    display: inline-block;
    padding: 8px;
}

h1 {
    font-size: 1em;
    font-weight: bold;
    margin: 0 0 8px 0;
}

p, a {
    font-size: 0.8em;
    margin: 0;
}
</style>
