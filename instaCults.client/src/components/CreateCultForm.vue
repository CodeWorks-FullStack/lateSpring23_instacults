<template>
    <div class="modal-content">
        <div class="modal-header">
            <h5 class="modal-title">Create Cult</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <form @submit.prevent="createCult()">
            <div class="modal-body">
                <div class="mb-3">
                    <label for="name" class="form-label">Name</label>
                    <input v-model="editable.name" type="text" class="form-control" id="name" placeholder="Name">
                </div>
                <div class="mb-3">
                    <label for="coverImg" class="form-label">Cover Image</label>
                    <input v-model="editable.coverImg" type="text" class="form-control" id="coverImg"
                        placeholder="Cover Image">
                </div>
                <div class="mb-3">
                    <label for="tags" class="form-label">Tags (comma and space separated)</label>
                    <input v-model="editable.tags" type="text" class="form-control" id="tags" placeholder="Tags">
                </div>
                <textarea v-model="editable.description" class="form-control w-100" name="description" id="description"
                    cols="30" rows="10"></textarea>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                <button type="submit" class="btn btn-danger">Create the Cult!</button>
            </div>
        </form>
    </div>
</template>

<script>
import { ref } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { cultsService } from '../services/CultsService.js';
import { Modal } from 'bootstrap';
export default {
    setup() {
        const editable = ref({})
        return {
            editable,

            async createCult() {
                try {
                    const cultData = editable.value
                    await cultsService.createCult(cultData)
                    editable.value = {}
                    Modal.getOrCreateInstance('#modal').hide()
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            }
        };
    },
};
</script>

<style></style>