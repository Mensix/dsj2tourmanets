import { EmbedBuilder } from 'discord.js'
import type { FetchError } from 'ofetch'

export default function (error: FetchError) {
  return new EmbedBuilder()
    .setColor('Red')
    .setTitle('Error occured!')
    .setAuthor({ name: 'DSJ24.PL', iconURL: 'https://yt3.ggpht.com/ytc/AAUvwnhH_kewFZgwM1dBxKloPQ9KWyhPoAiGbZwDLNaa=s176-c-k-c0x00ffffff-no-rj-mo', url: 'https://dsj24.pl/' })
    .addFields(
      { name: 'Trace', value: error.response?._data.message },
      { name: 'Input', value: `\`\`\`${JSON.stringify(error.response?._data.input)}\`\`\`` },
    )
}
