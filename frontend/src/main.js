import Vue from 'vue';
import VueProgessBar from 'vue-progressbar';

import api from './api'
import App from './App.vue';
import router from './router';

// Bind the API client to all components by default
Vue.mixin({
    data() {
        return {
            api,
        }
    },
});

// Page progress bar options
const options = {
    color: '#4863A0',
    failedColor: '#B31D1D',
    thickness: '2px',
    transition: {
        speed: '0.2s',
        opacity: '0.6s',
        termination: 300,
    },
    autoRevert: true,
};

// Load the page progress bar
Vue.use(VueProgessBar, options);

// Start the Vue application
export default new Vue({
    el: '#app',
    router,
    components: {
        App,
    },
    template: '<App />'
});
