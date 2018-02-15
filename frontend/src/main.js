import axios from 'axios';
import Vue from 'vue';

import App from './App.vue';
import router from './router';

// Build an API client
const api = {
    host: "http://localhost:5000/api/v1/",
    getUrl(context, endpoint) {
        return context.api.host + endpoint;
    },

    /**
     * Send a GET request through AJAX on the given endpoint.
     * Returns a promise with the resulting data.
     */
    ajaxGet(context, endpoint) {
        // Create a promise for the request, return it
        return new Promise(function(resolve, reject) {
            // Make the request
            axios.get(context.api.getUrl(context, endpoint))
                .then(response => {
                    resolve(response.data)
                })
                .catch(error => {
                    // Report the error, and continue
                    console.error("AJAX API request error: " + error);

                    // Reject the promise
                    reject(error);
                });
        });
    },

    event: {
        fetchAll(context) {
            return context.api.ajaxGet(context, "event");
        },
        fetch(context, id) {
            return context.api.ajaxGet(context, "event/details/" + id);
        },
    }
};

// Bind the API client to all components by default
Vue.mixin({
    data () {
        return {
            api,
        }
    },
})

// Start the Vue application
new Vue({
    el: '#app',
    router,
    components: {
        App
    },
    template: '<App />'
});
