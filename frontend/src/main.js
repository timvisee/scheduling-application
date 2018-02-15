import Vue from 'vue';
import VueResource from 'vue-resource';

import App from './App.vue';
import router from './router';

// Enable Vue resource
Vue.use(VueResource);
Vue.http.options.emulateJSON = true;
const http = Vue.http
export default http

const api = {
    host: "http://localhost:5000/api/v1/",
    getUrl(context, endpoint) {
        return context.api.host + endpoint;
    },

    /**
     * Send a GET request through AJAX on the given endpoint.
     */
    ajaxGet(context, endpoint, callback) {
        // Set the loading state
        context.loading = true;
        context.error = null;

        // Make the request
        context.$http
            .get(context.api.getUrl(context, endpoint))
            .then((response) => {
                // Set the loading state
                context.loading = false;

                // Call back the result
                callback(null, response.data);
            }, (response) => {
                // Set the loading state
                context.loading = false;
                context.error = response;

                // Call back the result
                callback(response);
            });
    },

    event: {
        fetchAll(context, callback) {
            context.api.ajaxGet(context, "event", callback);
        },
        fetch(context, id, callback) {
            context.api.ajaxGet(context, "event/details/" + id, callback);
        },
    }
};

Vue.mixin({
    data () {
        return {
            api,
            loading: false,
            error: null,
        }
    },
})

new Vue({
    el: '#app',
    router,
    components: {
        App
    },
    template: '<App />'
});
