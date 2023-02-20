import { EmbedBuilder } from 'discord.js'
import type { Jump } from '../interfaces'

export default function (jump: Jump) {
  const output = new EmbedBuilder()
    .setColor('Blue')
    .setTitle(`${jump.user.username}, you have succesfully sent your jump!`)
    .setAuthor({ name: 'DSJ24.PL', iconURL: `https://www.dsj2.com/media-kit/screenshot${~~(Math.random() * 6) + 1}.png`, url: 'https://dsj24.pl/' })
    .setDescription('See details of your jump below.')
    .addFields(
      { name: 'Hill', value: `${jump.hill.name}`, inline: true },
      { name: 'Player', value: jump.player, inline: true },
    )
    .setTimestamp()

  output.addFields(
    { name: 'Length', value: `${jump.length}${jump.crash === true ? '*' : ''}m`, inline: true },
    { name: 'Points', value: jump.points.toString(), inline: true },
    { name: 'Replay link', value: `https://replay.dsj2.com/r/${jump.replayCode}` },
  )

  return output.toJSON()
}
