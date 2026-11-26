<template>
  <div>
    <BButton @click="visible = true">Add Bug</BButton>

    <BModal v-model="visible" title="Add A Bug" size="lg" no-footer @hidden="modalHidden">
      <BAlert v-model="success" variant="success" dismissible>
        Bug has been added successfully
      </BAlert>
      <BAlert v-model="errored" variant="danger" dismissible>
        Sorry an error has occurred
      </BAlert>
      <BForm @submit.prevent="onSubmit" v-if="show" class="form-horizontal">
        <BFormGroup label="Title" label-for="title" label-cols="3">
          <BFormInput id="title" v-model="form.title" type="text" required/>
        </BFormGroup>

        <BFormGroup label="Description" label-for="description" label-cols="3">
          <BFormInput id="description" v-model="form.description" type="text" required/>
        </BFormGroup>

        <BFormGroup label="Status" label-for="status" label-cols="3">
          <BFormSelect id="status" v-model="form.status" :options="statusOptions" required/>
        </BFormGroup>

        <BFormGroup label="Users" label-for="user" label-cols="3">
          <UsersDropdown v-model="form.userId"/>
        </BFormGroup>
        <div class="float-end">
          <BButton variant="danger" class="me-2" @click="visible = false">Close</BButton>
          <BButton type="submit" variant="primary">Submit</BButton>
        </div>
      </BForm>
    </BModal>
  </div>
</template>

<script setup>
  import { nextTick, reactive, ref } from 'vue'
  import { BAlert, BButton, BForm, BFormGroup, BFormInput, BFormSelect, BModal } from 'bootstrap-vue-next'
  import UsersDropdown from './UsersDropdown.vue'
  import { statusOptions } from './statuses'
  import api from '../api'

  const emit = defineEmits(['bug-added'])

  const visible = ref(false)
  const form = reactive({ title: '', description: '', status: null, userId: null })
  const show = ref(true)
  const errored = ref(false)
  const success = ref(false)

  async function onSubmit() {
    try {
      await api.post('/bugs', form)
      success.value = true
      reset()
      emit('bug-added')
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
    form.title = ''
    form.description = ''
    form.status = null
    form.userId = null

    // Trick to reset/clear native browser form validation state
    show.value = false
    nextTick(() => show.value = true)
  }
</script>
