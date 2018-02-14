import Vue from 'vue'
import Router from 'vue-router'

// TODO: Use the path '@/components/time...' here?
import OverviewPage from '../pages/Overview'
import SchedulePage from '../pages/Schedule'
import EventPage from '../pages/Event'

Vue.use(Router)

export default new Router({
    routes: [
        {
            name: 'overview',
            path: '/',
            component: OverviewPage
        },
        {
            name: 'schedule',
            path: '/schedule',
            component: SchedulePage
        },
        {
            name: 'event',
            path: '/event/:id',
            component: EventPage,
            props: {
                editable: false
            }
        },
        {
            name: 'event-edit',
            path: '/event/:id/edit',
            component: EventPage,
            props: {
                editable: true
            }
        },
        {
            name: 'event-delete',
            path: '/event/:id/delete'
            // TODO: implement component to show
        }
    ]
})
