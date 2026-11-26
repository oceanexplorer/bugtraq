<template>
    <BModal v-model="visible" no-footer size="lg" title="Edit Bug" @show="loadBug" @hidden="modalHidden">
        <div class="edit-bug-content">
            <div class="d-flex justify-content-center mb-3" v-if="isLoading">
                <BSpinner label="Loading..."/>
            </div>
            <div v-else>
                <BAlert dismissible v-model="errored" variant="danger">
                    Sorry an error has occurred
                </BAlert>
                <BForm @submit.prevent="onSubmit" class="form-horizontal" v-if="show">
                    <BFormGroup label="Title" label-cols="3" label-for="title">
                        <BFormInput id="title" required type="text" v-model="form.title"/>
                    </BFormGroup>

                    <BFormGroup label="Description" label-cols="3" label-for="description">
                        <BFormInput id="description" required type="text" v-model="form.description"/>
                    </BFormGroup>

                    <BFormGroup label="Status" label-cols="3" label-for="status">
                        <BFormSelect :options="statusOptions" id="status" required v-model="form.status"/>
                    </BFormGroup>

                    <BFormGroup label="Users" label-cols="3" label-for="user">
                        <UsersDropdown v-model="form.userId"/>
                    </BFormGroup>
                    <div class="float-end">
                        <BButton @click="visible = false" class="me-2" variant="danger">Close</BButton>
                        <BButton type="submit" variant="primary">
                            <span>Submit</span>
                            <BSpinner class="ms-2" small v-if="isSubmitting"/>
                        </BButton>
                    </div>
                </BForm>
            </div>
        </div>
    </BModal>
</template>

<script setup>
    import { nextTick, reactive, ref } from 'vue'
    import { BAlert, BButton, BForm, BFormGroup, BFormInput, BFormSelect, BModal, BSpinner } from 'bootstrap-vue-next'
    import UsersDropdown from './UsersDropdown.vue'
    import { statusOptions } from './statuses'
    import api from '../api'

    const props = defineProps({ bugId: { type: Number, default: null } })
    const emit = defineEmits(['bug-updated'])
    const visible = defineModel('visible', { type: Boolean, default: false })

    const form = reactive({ bugId: null, title: null, description: null, status: null, userId: null })
    const show = ref(true)
    const errored = ref(false)
    const isLoading = ref(false)
    const isSubmitting = ref(false)

    async function loadBug() {
        isLoading.value = true
        try {
            const { data } = await api.get(`/bugs/${props.bugId}`)
            Object.assign(form, {
                bugId: props.bugId,
                title: data.title,
                description: data.description,
                status: data.status,
                userId: data.userId
            })
        } catch {
            errored.value = true
        } finally {
            isLoading.value = false
        }
    }

    async function onSubmit() {
        isSubmitting.value = true
        try {
            await api.put(`/bugs/${props.bugId}`, form)
            visible.value = false
            emit('bug-updated')
        } catch {
            errored.value = true
        } finally {
            isSubmitting.value = false
        }
    }

    function modalHidden() {
        Object.assign(form, { bugId: null, title: '', description: '', status: null, userId: null })
        errored.value = false

        // Trick to reset/clear native browser form validation state
        show.value = false
        nextTick(() => show.value = true)
    }
</script>

<style>
    .edit-bug-content {
        min-height: 260px;
    }
</style>
