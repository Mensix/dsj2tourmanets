import { EmbedBuilder } from 'discord.js'

export default function (value: string) {
  return new EmbedBuilder()
    .setColor('Blue')
    .setTitle('Information')
    .setAuthor({ name: 'DSJ24.PL', iconURL: 'https://yt3.ggpht.com/ytc/AAUvwnhH_kewFZgwM1dBxKloPQ9KWyhPoAiGbZwDLNaa=s176-c-k-c0x00ffffff-no-rj-mo', url: 'https://dsj24.pl/' })
    .addFields({
      name: 'message', value,
    })
}
