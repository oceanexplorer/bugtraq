import { createRouter, createWebHistory } from 'vue-router'
import Bugs from '../views/Bugs.vue'

const routes = [
    {
        path: '/',
        name: 'bugs',
        component: Bugs
    },
    {
        path: '/users',
        name: 'users',
        // route level code-splitting: lazy-loaded when the route is visited
        component: () => import('../views/Users.vue')
    }
];

export default createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes
})
