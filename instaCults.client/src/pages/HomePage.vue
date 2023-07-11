<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-9" v-for="c in cults" :key="c.id">
        <CultCard :cult="c" />
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { logger } from '../utils/Logger.js'
import { cultsService } from '../services/CultsService.js'
import Pop from '../utils/Pop.js'
import { AppState } from '../AppState.js'
export default {
  setup() {

    async function getCults() {
      try {
        // console.log('getting cults')
        await cultsService.getCults()
      } catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    }

    onMounted(() => {
      getCults()
    })

    return {
      cults: computed(() => AppState.cults)
    }
  }
}
</script>

<style scoped lang="scss"></style>
