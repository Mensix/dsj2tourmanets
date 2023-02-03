import type { ButtonInteraction, CommandInteraction, EmbedField } from 'discord.js'
import { ActionRowBuilder, ButtonBuilder, ButtonStyle, Colors, ComponentType, EmbedBuilder } from 'discord.js'
import type { Jump, Tournament } from '../interfaces'
import { hillToFlag } from '../misc'

export default async function (interaction: CommandInteraction, tournament: Tournament) {
  if (!interaction.deferred)
    await interaction.deferReply()

  let page = 1
  const pagesCount = Math.floor((tournament.jumps!.length + 9) / 10)
  let paginatedJumps: EmbedField[] = tournament.jumps!
    .slice(((page - 1) * 10), ((page - 1) * 10) + 10)
    .map((x: Jump) => ({
      name: `${x.place}. ${x.user.username}`,
      value: `${x.length}m / ${x.points}pt\nhttps://replay.dsj2.com/${x.replayCode}`,
      inline: false,
    }))

  let description = ''
  if (tournament.jumps!.length > 0) {
    if (tournament.settings!.liveBoard && !tournament.isFinished)
      description = 'You are seeing live scoreboard!'
  }
  else {
    description = 'No results are available at the moment.'
  }

  const embed = new EmbedBuilder()
    .setColor(Colors.Blue)
    .setTitle('Tournament results')
    .setAuthor({ name: 'DSJ24.PL', iconURL: 'https://yt3.ggpht.com/ytc/AAUvwnhH_kewFZgwM1dBxKloPQ9KWyhPoAiGbZwDLNaa=s176-c-k-c0x00ffffff-no-rj-mo', url: 'https://dsj24.pl/' })
    .addFields(
      { name: 'Hill', value: `${hillToFlag(tournament.hill.name)}  ${tournament.hill.name}`, inline: true },
      { name: 'Code', value: tournament.code!, inline: true },
    )
    .setTimestamp()
    .setFooter({ text: `Page: ${page}/${pagesCount}` })

  if (description)
    embed.setDescription(description)

  if (paginatedJumps.length > 0) {
    embed.addFields(paginatedJumps)
      .toJSON()
  }
  else {
    embed.setDescription('')
  }

  const previousPageButton = new ButtonBuilder()
    .setCustomId('previousPage')
    .setLabel('◀️')
    .setStyle(ButtonStyle.Primary)
    .setDisabled(true)

  const nextPageButton = new ButtonBuilder()
    .setCustomId('nextPage')
    .setLabel('▶️')
    .setStyle(ButtonStyle.Primary)
    .setDisabled(pagesCount <= 1 || page === pagesCount)

  const buttons = new ActionRowBuilder<ButtonBuilder>().addComponents(previousPageButton, nextPageButton)

  const currentPage = await interaction.editReply({
    embeds: [embed],
    components: [buttons],
  })

  const collector = currentPage.createMessageComponentCollector({
    componentType: ComponentType.Button,
    filter: (i: ButtonInteraction) => i.toJSON() === previousPageButton.toJSON() || i.toJSON() === nextPageButton.toJSON(),
  })

  collector.on('collect', async (i) => {
    switch (i.customId) {
      case 'previousPage':
        --page
        break
      case 'nextPage':
        ++page
        break
      default:
        break
    }

    previousPageButton.setDisabled(page === 1)
    nextPageButton.setDisabled(page === pagesCount)

    paginatedJumps = tournament.jumps!
      .slice(((page - 1) * 10), ((page - 1) * 10) + 10)
      .map(x => ({
        name: `${x.place}. ${x.user.username}`,
        value: `${x.length}m / ${x.points}pt \nhttps://replay.dsj2.com/${x.replayCode}`,
        inline: false,
      }))
    embed.setDescription(description)
    embed.setFields([
      { name: 'Hill', value: `${hillToFlag(tournament.hill.name)}  ${tournament.hill.name}`, inline: true },
      { name: 'Code', value: tournament.code!, inline: true },
      ...paginatedJumps as EmbedField[],
    ])
    embed.setFooter({ text: `Page: ${page}/${pagesCount}` })

    await i.deferUpdate()
    await i.editReply({
      embeds: [embed],
      components: [buttons],
    })
  })

  return currentPage
}
