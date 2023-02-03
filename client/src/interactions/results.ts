import type { Interaction } from 'discord.js'
import { ApplicationCommandType } from 'discord.js'
import type { FetchError } from 'ofetch'
import { ofetch } from 'ofetch'
import { errorEmbed, resultsEmbed } from '../embeds'
import type { Tournament } from '../interfaces'

export default async function (interaction: Interaction) {
  if (!interaction.isChatInputCommand())
    return

  if (interaction.commandType !== ApplicationCommandType.ChatInput)
    return

  const code = interaction.options.getString('code')

  try {
    const results = await ofetch<Tournament>(`/tournament/${code}`, { baseURL: process.env.BASE_URL })
    resultsEmbed(interaction, results)
  }
  catch (error) {
    await interaction.reply({ embeds: [errorEmbed(error as FetchError)] })
  }
}
