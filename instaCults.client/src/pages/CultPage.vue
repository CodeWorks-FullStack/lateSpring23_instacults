<template>
    <div class="container-fluid" v-if="cult">
        <section class="row img-row align-content-center" :style="{ backgroundImage: `url(${cult.coverImg})` }">
            <div class="col-5 text-danger">
                <h1>{{ cult.name }}</h1>
                <button :disabled="isCultist" class="btn btn-outline-danger" @click="becomeCultMember()">JOIN</button>
            </div>
        </section>
        <section class="row">
            <div class="col-md-6">

                <h2 class="text-danger">Popularity: {{ cult.popularity }}</h2>

                <p>{{ cult.description }}</p>
            </div>
            <div class="col-md-6">
                <div>
                    <h2>Cult Leader</h2>
                    <img class="leader-img" :src="cult.leader.picture" alt="">
                </div>
                <div>
                    <h2>Members: {{ cultists.length }}</h2>
                    <div v-for="c in cultists" :key="c.cultMemberId">
                        <img class="cultist-img" :src="c.picture" alt="">
                        <button v-if="cult.leaderId == account.id || c.id == account.id" class="btn btn-outline-danger"
                            @click="removeCultMember(c.cultMemberId)">Remove</button>
                    </div>
                </div>
            </div>
        </section>
    </div>
</template>

<script>
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { cultsService } from '../services/CultsService.js';
import { cultMembersService } from '../services/CultMembersService.js'
import { AppState } from '../AppState.js';
export default {
    setup() {
        const route = useRoute()

        async function getById() {
            try {
                const cultId = route.params.cultId
                // logger.log("getting cult by id", cultId)
                await cultsService.getById(cultId)
            } catch (error) {
                logger.error(error)
                Pop.toast(error.message, 'error')
            }
        }

        async function getCultMembers() {
            try {
                const cultId = route.params.cultId
                await cultMembersService.getCultMembersByCultId(cultId)
            } catch (error) {
                logger.error(error)
                Pop.toast(error.message, 'error')
            }
        }

        onMounted(() => {
            getById()
            getCultMembers()
        })

        return {
            route,
            cult: computed(() => AppState.activeCult),
            cultists: computed(() => AppState.cultists),
            isCultist: computed(() => {
                return AppState.cultists.find(c => c.id == AppState.account.id)
            }),
            account: computed(() => AppState.account),
            // cultMembers: computed(() => AppState.cultMembers),

            async becomeCultMember() {
                try {
                    // logger.log('becoming cult member')
                    let activeCultId = route.params.cultId
                    let cultMemberData = { cultId: activeCultId }
                    await cultMembersService.createCultMember(cultMemberData)
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            },

            async removeCultMember(cultMemberId) {
                try {
                    // logger.log('removing cult member', cultMemberId)
                    if (await Pop.confirm("Are you sure you want to leave?")) {
                        await cultMembersService.removeCultMember(cultMemberId)
                    }
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            }

        };
    },
};
</script>

<style scoped lang ="scss">
.img-row {
    height: 60vh;
}

.leader-img {
    height: 15vh;
    width: 15vh;
    border-radius: 50%;
    border: 2px solid red;
}

.cultist-img {
    height: 10vh;
    width: 10vh;
    border-radius: 50%;
}
</style>