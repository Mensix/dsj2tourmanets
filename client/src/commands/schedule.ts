import { SlashCommandBuilder } from '@discordjs/builders'

export default new SlashCommandBuilder()
  .setName('schedule')
  .setDescription('Schedules tournament on the given hill using given code.')
  .addStringOption(option =>
    option.setName('hill')
      .setDescription('Hill, where tournament will take a place.')
      .setRequired(true),
  )
  .addStringOption(option =>
    option.setName('start_date')
      .setDescription('Start date of tournament.'),
  )
  .addStringOption(option =>
    option.setName('end_date')
      .setDescription('End date of tournament.')
  )
  .addBooleanOption(option =>
    option.setName('live_board')
      .setDescription('Toggles tournament live scoreboard.'),
  )
  .toJSON()
