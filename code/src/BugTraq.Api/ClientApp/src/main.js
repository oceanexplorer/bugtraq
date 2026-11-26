import { createApp } from 'vue'
import { createBootstrap } from 'bootstrap-vue-next'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faAddressCard, faBug, faPenToSquare, faTable, faTrash, faUser } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap-vue-next/dist/bootstrap-vue-next.css'
import App from './App.vue'
import router from './router'

library.add(faAddressCard, faBug, faPenToSquare, faTable, faTrash, faUser)

createApp(App)
    .use(createBootstrap())
    .use(router)
    .component('font-awesome-icon', FontAwesomeIcon)
    .mount('#app')
