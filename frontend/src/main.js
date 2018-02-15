import Vue from 'vue';
import VueResource from 'vue-resource';

import App from './App.vue';
import router from './router';

// Enable Vue resource
Vue.use(VueResource);
Vue.http.options.emulateJSON = true;
const http = Vue.http
export default http

new Vue({
    el: '#app',
    router,
    components: {
        App
    },
    template: '<App />'
});
