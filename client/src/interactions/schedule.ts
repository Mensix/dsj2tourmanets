import { parse } from 'date-fns'
import type { Client, CommandInteraction, ForumChannel } from 'discord.js'
import { ApplicationCommandType, GuildScheduledEventEntityType, GuildScheduledEventPrivacyLevel } from 'discord.js'
import type { FetchError } from 'ofetch'
import { ofetch } from 'ofetch'
import { errorEmbed, tournamentEmbed } from '../embeds'
import type { Tournament, TournamentSettings } from '../interfaces'

function getFixedDate(date: string | null) {
  if (!date)
    return undefined

  return parse(date, 'kk:mm', new Date()) || parse(date, 'dd/MM/yyyy kk:mm', new Date())
}

export default async function (client: Client, interaction: CommandInteraction) {
  await interaction.deferReply()

  if (!interaction.isChatInputCommand())
    return

  if (interaction.commandType !== ApplicationCommandType.ChatInput)
    return

  const { options } = interaction
  const hill = options.getString('hill')!
  const allowedHills = 'Finland K105,Switzerland K170,Czech Republic K135,Belarus K220,Austria K70,USA K130,Latvia K165,Poland K80,Japan K210,Belgium K95,Iceland K190,England K50,Germany K120,Estonia K155,Norway K90,Australia K240,Ireland K125,Ukraine K60,Hungary K180,Sweden K140,Italy K230,Denmark K75,Slovakia K110,Canada K185,Lithuania K145,Kazakhstan K85,China K205,France K160,Holland K100,Russia K200,Korea K150,Slovenia K250'.split(',')
  if (!allowedHills.includes(hill)) {
    await interaction.editReply({ content: 'Invalid hill field value.' })
    return
  }

  const schedule: Tournament = {
    createdBy: {
      userId: parseInt(interaction.user.id),
      username: interaction.user.username,
    },
    hill: {
      name: hill,
    },
  }

  const [startDate, endDate, liveBoard] = [options.getString('start_date'), options.getString('end_date'), options.getBoolean('live_board')]
  if (startDate)
    schedule.startDate = getFixedDate(startDate)
  if (endDate)
    schedule.endDate = getFixedDate(endDate)
  if (liveBoard) {
    schedule.settings = {} as TournamentSettings
    schedule.settings.liveBoard = liveBoard
  }

  try {
    const tournament = await ofetch<Tournament>('/tournament', { baseURL: process.env.BASE_URL!, method: 'POST', body: schedule, headers: { 'content-type': 'application/json' } })

    await (client.channels.cache.get(process.env.WORKING_CHANNEL!) as ForumChannel).threads.create({
      name: `${tournament.hill.name} (${tournament.code})`,
      message: { embeds: [tournamentEmbed(tournament)] },
    })
    await client.guilds.cache.get(interaction.guildId!)!.scheduledEvents.create({
      entityType: GuildScheduledEventEntityType.External,
      name: 'DSJ2 Mobile Tournament #dsj2tournaments',
      scheduledStartTime: tournament.startDate!,
      scheduledEndTime: tournament.endDate!,
      privacyLevel: GuildScheduledEventPrivacyLevel.GuildOnly,
      entityMetadata: {
        location: tournament.hill.name,
      },
    })
  }
  catch (error) {
    await interaction.editReply({ embeds: [errorEmbed(error as FetchError)] })
  }
}
