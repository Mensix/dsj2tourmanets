import type { Message } from 'discord.js'
import type { FetchError } from 'ofetch'
import { ofetch } from 'ofetch'
import { errorEmbed, jumpEmbed } from '../embeds'
import type { Jump, SentJump } from '../interfaces'

export default async function (message: Message) {
  const replayCode = message.content.split('/').at(-1)!
  const sentResponse = await message.author.send('Fetching jump data...').catch(() => message.reply('Fetching jump data...'))

  const body: SentJump = {
    replayCode,
    user: {
      username: message.author.tag,
      userId: parseInt(message.author.id, 10),
    },
  }

  try {
    const jump = await ofetch<Jump>('/jump', { baseURL: process.env.BASE_URL, method: 'POST', body })
    await sentResponse.edit({ embeds: [jumpEmbed(jump)] })
      .catch(() => message.reply('You have succesfully sent your jump. Enable DMs in order to have full information about it.'))
  }
  catch (error) {
    await sentResponse.edit({ embeds: [errorEmbed(error as FetchError)] })
  }
}
