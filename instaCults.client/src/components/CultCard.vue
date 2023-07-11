<template>
    <router-link :to="{ name: 'CultPage', params: { cultId: cult.id } }">
        <div class="cult-card my-4">
            <div class="d-flex align-items-center p-2">
                <img class="rounded rounded-circle leader-img" :src="cult.leader.picture" alt="">
                <h1>{{ cult.leader.name }}</h1>
            </div>
            <img class="cult-img" :src="cult.coverImg" alt="">
            <div class="p-2">
                <h2>{{ cult.name }}</h2>
                <p>{{ cult.description }}</p>
            </div>
        </div>
    </router-link>>
</template>

<script>
import { Cult } from '../models/Cult.js';
export default {
    props: {
        cult: { required: true, type: Cult }
    },
    setup() {
        return {};
    },
};
</script>
<style>
.cult-card {
    box-shadow: 2px 4px 4px red;
    border-radius: 15px;
}

.leader-img {
    height: 15vh;
    width: 15vh;
}

.cult-img {
    height: 45vh;
    width: 100%;
    /* aspect-ratio: auto; */
    object-fit: cover;
    object-position: center;
}
</style>