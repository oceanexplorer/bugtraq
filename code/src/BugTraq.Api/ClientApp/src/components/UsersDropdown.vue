<template>
    <BFormSelect
            :options="users"
            id="user"
            required
            v-model="selectedId"
    />
</template>

<script setup>
    import { onMounted, ref } from 'vue'
    import { BFormSelect } from 'bootstrap-vue-next'
    import api from '../api'

    const selectedId = defineModel({ type: String, default: null })
    const users = ref([{ text: 'Select One...', value: null }])

    onMounted(async () => {
        const { data } = await api.get('/users')
        users.value.push(...data.map(user => ({ text: `${user.firstName} ${user.surname}`, value: user.userId })))
    })
</script>
