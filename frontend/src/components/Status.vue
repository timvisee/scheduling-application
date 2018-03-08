<template>
    <div class="event-item">

        <!-- A nice loading indicator -->
        <loader v-if="loading" />

        <p v-else-if="error">
			<!-- TODO: render the error that occurred here -->
			ERROR
		</p>
    </div>
</template>



<script>
import Loader from './Loader.vue';

export default {
    name: 'status',
    props: [
        "loading",
        "error",
    ],
    components: {
        Loader,
    },
}
</script>
