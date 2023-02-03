import { SlashCommandBuilder } from '@discordjs/builders'

export default new SlashCommandBuilder()
  .setName('results')
  .setDescription('Fetches tournament result by given code.')
  .addStringOption(option =>
    option.setName('code')
      .setDescription('Tournament code')
      .setRequired(true),
  )
  .toJSON()
