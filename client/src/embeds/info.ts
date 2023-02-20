import { EmbedBuilder } from 'discord.js'

export default function (value: string) {
  return new EmbedBuilder()
    .setColor('Blue')
    .setTitle('Information')
    .setAuthor({ name: 'DSJ24.PL', iconURL: `https://www.dsj2.com/media-kit/screenshot${~~(Math.random() * 6) + 1}.png`, url: 'https://dsj24.pl/' })
    .addFields({
      name: 'message', value,
    })
}
