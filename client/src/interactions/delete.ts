import type { Interaction } from 'discord.js'
import { ApplicationCommandType } from 'discord.js'
import type { FetchError } from 'ofetch'
import { ofetch } from 'ofetch'
import consola from 'consola'
import { errorEmbed, infoEmbed } from '../embeds'
import type { User } from '../interfaces'

export default async function (interaction: Interaction) {
  if (!interaction.isChatInputCommand())
    return

  if (interaction.commandType !== ApplicationCommandType.ChatInput)
    return

  const code = interaction.options.getString('code')

  try {
    await ofetch(`/tournament/${code}`, {
      baseURL: process.env.BASE_URL,
      method: 'DELETE',
      body: {
        userId: parseInt(interaction.user.id),
        username: interaction.user.username,
      } as User,
    })
    await interaction.reply({ embeds: [infoEmbed(`Tournament with code **${code}** has been deleted.`)] })
  }
  catch (error) {
    consola.info(error)
    await interaction.reply({ embeds: [errorEmbed(error as FetchError)] })
  }
}
