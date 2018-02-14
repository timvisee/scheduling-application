<template>
    <div class="event">

        <h1>{{ name }}</h1>
        <p>{{ description }}</p>

        <a href="#">Edit</a>
        <a href="#">Delete</a>

    </div>
</template>



<script>
export default {
    name: 'event',
    props: [
        "name",
        "description",
    ],
}
</script>



<style scoped lang="scss">
.event {
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
