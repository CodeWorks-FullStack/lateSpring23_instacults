import { AppState } from "../AppState.js"
import { Cultist } from "../models/Account.js"
import { CultMember } from "../models/CultMember.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class CultMembersService {

    async getCultMembersByCultId(cultId) {
        const res = await api.get(`api/cults/${cultId}/cultmembers`)
        logger.log('[GETTING CULT MEMBERS]', res.data)
        AppState.cultists = res.data.map(d => new Cultist(d))
    }

    async createCultMember(cultMemberData) {
        debugger
        const res = await api.post('api/cultmembers', cultMemberData)
        logger.log('[CREATE CULT MEMBER]', res.data)
        // TODO make a new 'cultist' object using the account and the res.data to save to appstate
        const newCultist = new Cultist(AppState.account)
        logger.log(newCultist, 'new cultist')
        newCultist.cultMemberId = res.data.id
        logger.log(newCultist, 'new cultist with id')
        AppState.cultists.push(newCultist)
        // AppState.cultMembers.push(new CultMember(res.data))
    }


    async removeCultMember(cultMemberId) {
        const res = await api.delete(`api/cultmembers/${cultMemberId}`)
        logger.log('[DELETING CULT MEMBER]', res.data)
        const foundIndex = AppState.cultists.findIndex(c => c.cultMemberId == cultMemberId)
        AppState.cultists.splice(foundIndex, 1)
    }
}

export const cultMembersService = new CultMembersService()