<template>
    <div class="overview">

        <h1>Overview page</h1>
        <p>
            This is the overview page.<br /><br />
            Here is some schedule summary:
        </p>

        <event-view />

        <h1>Other stuff...</h1>
        <p>Here is some other stuff...</p>

    </div>
</template>



<script>
import EventView from '../components/events/EventView'

export default {
    components: {
        EventView,
    }
}
</script>



<style scoped lang="scss"></style>
