import type { Hill } from './Hill'
import type { User } from './User'

interface Jump {
  place: number
  user: User
  replayCode: string
  hill: Hill
  length: number
  crash: boolean
  points: number
  date: Date
  tournamentCode: string
}

export { Jump }
