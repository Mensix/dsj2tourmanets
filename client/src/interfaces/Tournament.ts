import type { Hill } from './Hill'
import type { Jump } from './Jump'
import type { User } from './User'

interface Tournament {
  createdBy?: User
  settings?: TournamentSettings
  hill: Hill
  code?: string
  createdDate?: Date
  startDate?: Date
  endDate?: Date
  isFinished?: boolean
  jumps?: Jump[]
}

interface TournamentSettings {
  liveBoard: boolean
}

export { Tournament, TournamentSettings }
