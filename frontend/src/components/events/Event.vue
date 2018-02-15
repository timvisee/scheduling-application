<template>
    <div class="event">

        <table>
            <tr>
                <td>Title:</td>
                <td v-if="editable"><input type="text" v-model="event.title" /></td>
                <td v-else>{{ event.title }}</td>
            </tr>
            <tr>
                <td>Description:</td>
                <td v-if="editable"><input type="text" v-model="event.description" /></td>
                <td v-else>{{ event.description }}</td>
            </tr>
        </table>

    </div>
</template>



<script>
export default {
    name: 'event',
    props: [
        "event",
        "editable"
    ],
}
</script>



<style scoped lang="scss">
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
