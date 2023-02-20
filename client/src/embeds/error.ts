import { EmbedBuilder } from 'discord.js'
import type { FetchError } from 'ofetch'

export default function (error: FetchError) {
  return new EmbedBuilder()
    .setColor('Red')
    .setTitle('Error occured!')
    .setAuthor({ name: 'DSJ24.PL', iconURL: `https://www.dsj2.com/media-kit/screenshot${~~(Math.random() * 6) + 1}.png`, url: 'https://dsj24.pl/' })
    .addFields(
      { name: 'Trace', value: error.response?._data.message },
      { name: 'Input', value: `\`\`\`${JSON.stringify(error.response?._data.input)}\`\`\`` },
    )
}
