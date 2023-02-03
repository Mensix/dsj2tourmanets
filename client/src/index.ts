import consola from 'consola'
import { Client, GatewayIntentBits, REST, Routes } from 'discord.js'
import * as dotenv from 'dotenv'
import { resultsCommand, scheduleTournamentCommand } from './commands'
import { sendJump } from './handlers'
import { resultsInteraction, scheduleTournamentInteraction } from './interactions'
dotenv.config()

const client = new Client({ intents: [GatewayIntentBits.Guilds, GatewayIntentBits.GuildMessages, GatewayIntentBits.MessageContent] })
const rest = new REST({ version: '10' }).setToken(process.env.BOT_TOKEN as string);
(async () => {
  await rest.put(Routes.applicationGuildCommands(process.env.CLIENT_ID!, process.env.GUILD_ID!), { body: [scheduleTournamentCommand, resultsCommand] })
  consola.info('Discord commands registered.')
})()

client.on('interactionCreate', async (interaction) => {
  if (!interaction.isCommand())
    return

  if (interaction.commandName === 'schedule')
    await scheduleTournamentInteraction(client, interaction)
  else if (interaction.commandName === 'results')
    await resultsInteraction(interaction)
})

client.on('messageCreate', async (message) => {
  if (message.content.startsWith('https://replay.dsj2.com')) {
    message.delete()
    await sendJump(message)
  }
})

client.login(process.env.BOT_TOKEN)
client.on('ready', () => consola.info('Discord client ready.'))
