<template>
    <div>
        <div class="row">
            <div class="col">
                <h2>Users</h2>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <AddUserModal class="float-end" @user-added="loadUsers"/>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <BTable :fields="fields" :items="users" class="mt-4" hover striped/>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { onMounted, ref } from 'vue'
    import { BTable } from 'bootstrap-vue-next'
    import AddUserModal from './AddUserModal.vue'
    import api from '../api'

    const fields = ['firstName', 'surname']
    const users = ref([])

    async function loadUsers() {
        const { data } = await api.get('/users')
        users.value = data
    }

    onMounted(loadUsers)
</script>
