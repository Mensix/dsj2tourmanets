import { SlashCommandBuilder } from 'discord.js'

export default new SlashCommandBuilder()
  .setName('delete')
  .setDescription('Deletes a tournament by given code.')
  .addStringOption(option =>
    option.setName('code')
      .setDescription('Tournament code')
      .setRequired(true),
  )
  .toJSON()
