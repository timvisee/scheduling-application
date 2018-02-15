import Vue from 'vue';

import api from './api/index.js'
import App from './App.vue';
import router from './router';

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
