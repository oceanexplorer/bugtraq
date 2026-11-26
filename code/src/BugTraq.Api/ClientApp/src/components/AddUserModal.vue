<template>
    <div>
        <BButton @click="visible = true">Add User</BButton>

        <BModal v-model="visible" no-footer size="lg" title="Add A User" @hidden="modalHidden">
            <BAlert dismissible v-model="success" variant="success">
                User has been added successfully
            </BAlert>
            <BAlert dismissible v-model="errored" variant="danger">
                Sorry an error has occurred
            </BAlert>
            <BForm @submit.prevent="onSubmit" class="form-horizontal" v-if="show">
                <BFormGroup label="First Name" label-cols="3" label-for="firstName">
                    <BFormInput id="firstName" required type="text" v-model="form.firstName"/>
                </BFormGroup>

                <BFormGroup label="Surname" label-cols="3" label-for="surname">
                    <BFormInput id="surname" required type="text" v-model="form.surname"/>
                </BFormGroup>
                <div class="float-end">
                    <BButton @click="visible = false" class="me-2" variant="danger">Close</BButton>
                    <BButton type="submit" variant="primary">Submit</BButton>
                </div>
            </BForm>
        </BModal>
    </div>
</template>

<script setup>
    import { nextTick, reactive, ref } from 'vue'
    import { BAlert, BButton, BForm, BFormGroup, BFormInput, BModal } from 'bootstrap-vue-next'
    import api from '../api'

    const emit = defineEmits(['user-added'])

    const visible = ref(false)
    const form = reactive({ firstName: '', surname: '' })
    const show = ref(true)
    const errored = ref(false)
    const success = ref(false)

    async function onSubmit() {
        try {
            await api.post('/users', form)
            success.value = true
            reset()
            emit('user-added')
        } catch {
            errored.value = true
        }
    }

    function modalHidden() {
        reset()
        errored.value = false
        success.value = false
    }

    function reset() {
        form.firstName = ''
        form.surname = ''

        // Trick to reset/clear native browser form validation state
        show.value = false
        nextTick(() => show.value = true)
    }
</script>
