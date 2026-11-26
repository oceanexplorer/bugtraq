<template>
    <div>
        <div class="row mb-3">
            <div class="col-6">
                <BButtonGroup>
                    <BButton @click="displayMethod = 'cards'" aria-label="Card view">
                        <font-awesome-icon icon="address-card"/>
                    </BButton>
                    <BButton @click="displayMethod = 'table'" aria-label="Table view">
                        <font-awesome-icon icon="table"/>
                    </BButton>
                </BButtonGroup>
            </div>
            <div class="col-6">
                <AddBugModal class="float-end ps-2" @bug-added="getTickets"/>
            </div>
        </div>
        <div class="row" v-if="displayMethod === 'cards'">
            <div class="col" :key="status" v-for="(tickets, status) in statuses">
                <BCard class="h-100">
                    <template #header>
                        <h6 class="mb-0">{{ capitalize(status) }}</h6>
                    </template>
                    <div class="d-flex justify-content-center mb-3" v-if="loading">
                        <BSpinner label="Loading..."/>
                    </div>
                    <draggable
                            :list="tickets"
                            @change="onChange($event, status)"
                            class="dragArea"
                            item-key="bugId"
                            group="description"
                            :animation="150"
                    >
                        <template #item="{ element: ticket }">
                            <BCard class="mb-2 draggable draggable-card">
                                <h6 class="text-start">{{ ticket.title }}</h6>
                                <div class="row">
                                    <div class="col text-start">
                                        <font-awesome-icon class="text-primary" icon="user"/>
                                        <span class="ps-3">{{ ticket.userFirstName }} {{ ticket.userSurname }}</span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col text-end">
                                        <a @click.prevent="showEditBugModal(ticket.bugId)" class="pe-2" href="#"
                                           aria-label="Edit bug">
                                            <font-awesome-icon class="text-primary" icon="pen-to-square"/>
                                        </a>
                                        <a @click.prevent="showDeleteConfirmation(ticket.bugId)" href="#"
                                           aria-label="Delete bug">
                                            <font-awesome-icon class="text-danger" icon="trash"/>
                                        </a>
                                    </div>
                                </div>
                            </BCard>
                        </template>
                    </draggable>
                </BCard>
            </div>
        </div>
        <div class="row" v-if="displayMethod === 'table'">
            <div class="col">
                <BTable :items="allTickets" :fields="fields"/>
            </div>
        </div>
        <EditBugModal v-model:visible="editVisible" :bug-id="idForEdit" @bug-updated="getTickets"/>
        <BModal v-model="deleteVisible" title="Delete bug" ok-variant="danger" ok-title="Delete" @ok="deleteTicket">
            Are you sure you want to delete this bug?
        </BModal>
    </div>
</template>

<script setup>
    import { computed, onMounted, reactive, ref } from 'vue'
    import { BButton, BButtonGroup, BCard, BModal, BSpinner, BTable } from 'bootstrap-vue-next'
    import draggable from 'vuedraggable'
    import EditBugModal from './EditBugModal.vue'
    import AddBugModal from './AddBugModal.vue'
    import api from '../api'

    const displayMethod = ref('cards')
    const statuses = reactive({
        new: [],
        active: [],
        resolved: [],
        closed: []
    })
    const loading = ref(true)
    const idForEdit = ref(null)
    const editVisible = ref(false)
    const idForDelete = ref(null)
    const deleteVisible = ref(false)
    const fields = [
        { key: 'title', sortable: true },
        { key: 'status', sortable: true },
        { key: 'userFirstName', label: 'First Name', sortable: true },
        { key: 'userSurname', label: 'Surname', sortable: true }
    ]

    const allTickets = computed(() => Object.values(statuses).flat())

    function capitalize(value) {
        if (!value) return ''
        value = value.toString()
        return value.charAt(0).toUpperCase() + value.slice(1)
    }

    function onChange(event, newStatus) {
        if (event.added) {
            api.put(`/bugs/updatestatus/${event.added.element.bugId}/${newStatus}`)
        }
    }

    async function getTickets() {
        try {
            const { data } = await api.get('/bugs')
            for (const status of Object.keys(statuses)) {
                statuses[status] = data.filter(t => t.status.toLowerCase() === status)
            }
        } finally {
            loading.value = false
        }
    }

    function showEditBugModal(id) {
        idForEdit.value = id
        editVisible.value = true
    }

    function showDeleteConfirmation(id) {
        idForDelete.value = id
        deleteVisible.value = true
    }

    async function deleteTicket() {
        await api.delete(`/bugs/${idForDelete.value}`)
        await getTickets()
    }

    onMounted(getTickets)
</script>

<style>
    .draggable {
        cursor: move;
    }

    .dragArea {
        min-height: 15rem;
    }

    .draggable-card {
        border-left: 5px solid blue;
    }
</style>
