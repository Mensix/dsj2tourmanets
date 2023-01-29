import { format } from 'date-fns'
import { EmbedBuilder } from 'discord.js'
import type { Tournament } from '../interfaces'
import { hillToFlag } from '../misc'

export default function (schedule: Tournament) {
  return new EmbedBuilder()
    .setColor('#0088FF')
    .setImage(`https://www.dsj2.com/media-kit/screenshot${Math.floor(Math.random() * 6) + 1}.png`)
    .setTitle('The tournament is now scheduled!')
    .setAuthor({ name: 'DSJ24.PL', iconURL: 'https://yt3.ggpht.com/ytc/AAUvwnhH_kewFZgwM1dBxKloPQ9KWyhPoAiGbZwDLNaa=s176-c-k-c0x00ffffff-no-rj-mo', url: 'https://dsj24.pl/' })
    .setDescription(`Participate in the tournament and do your best! The winner takes it all!\n
• we play on **Deluxe Ski Jump 2 mobile version**
• your player **should** finish with **${schedule.code}** to avoid conflicts
• the bot will send you information about your jump if you have enabled DMs, otherwise it will respond you with just a short message in this channel
• in order to send a jump, **simply send a link to the replay in this channel**
• once a tournament is finished, **results will be shown**`)
    .addFields(
      { name: 'Hill', value: `${hillToFlag(schedule.hill.name)} ${schedule.hill.name}`, inline: true },
      { name: 'Code', value: schedule.code!, inline: true },
      { name: 'Start date', value: format(new Date(schedule.startDate!), 'dd/MM/yyyy kk:mm'), inline: true },
      { name: 'End date', value: format(new Date(schedule.endDate!), 'dd/MM/yyyy kk:mm'), inline: true },
    )
    .setTimestamp()
    .setFooter({ text: 'Good luck!' })
    .toJSON()
}
