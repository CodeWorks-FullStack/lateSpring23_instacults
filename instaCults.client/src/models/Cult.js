import { Profile } from "./Account.js"

export class Cult {
    constructor(data) {
        this.id = data.id
        this.name = data.name
        this.tags = data.tags
        this.description = data.description
        this.popularity = data.popularity
        this.coverImg = data.coverImg
        this.createdAt = new Date(data.createdAt)
        this.updatedAt = new Date(data.updatedAt)
        this.leaderId = data.leaderId
        this.leader = new Profile(data.leader)
    }
}

