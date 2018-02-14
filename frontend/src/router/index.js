import Vue from 'vue'
import Router from 'vue-router'

// TODO: Use the path '@/components/time...' here?
import Timetable from '../components/timetable/Timetable'

Vue.use(Router)

export default new Router({
    routes: [
        {
            path: '/',
            name: 'Timetable',
            component: Timetable
        }
    ]
})
