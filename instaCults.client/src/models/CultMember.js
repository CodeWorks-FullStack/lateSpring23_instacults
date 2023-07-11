export class CultMember {
    constructor(data) {
        this.id = data.id
        this.cultId = data.cultId
        this.accountId = data.accountId
        this.createdAt = new Date(data.createdAt)
        this.updatedAt = new Date(data.updatedAt)
    }
}