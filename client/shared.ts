import { ofetch } from 'ofetch'

const $fetch = ofetch.create({
  baseURL: process.env.BASE_URL!,
})

export { $fetch }
