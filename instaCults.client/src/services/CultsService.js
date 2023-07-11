import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { Cult } from '../models/Cult.js'

class CultsService {


    async getCults() {
        const res = await api.get('api/cults')
        logger.log('[GETTING CULTS]', res.data)
        AppState.cults = res.data.map(d => new Cult(d))
    }

    async getById(cultId) {
        const res = await api.get(`api/cults/${cultId}`)
        logger.log('[GET BY ID]', res.data)
        AppState.activeCult = new Cult(res.data)
    }

    async createCult(cultData) {
        const res = await api.post('api/cults', cultData)
        logger.log('[CREATING CULT]', res.data)
        AppState.cults.unshift(new Cult(res.data))
    }

}

export const cultsService = new CultsService()