import type { Hill } from './Hill'
import type { User } from './User'

interface Jump {
  place: number
  user: User
  replayCode: string
  hill: Hill
  player: string
  length: number
  crash: boolean
  points: number
  date: Date
  tournamentCode: string
}

interface SentJump {
  replayCode: string
  user: User
}

export { Jump, SentJump }
