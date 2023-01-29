import consola from 'consola'
import { Client, GatewayIntentBits, REST, Routes } from 'discord.js'
import * as dotenv from 'dotenv'
import { scheduleTournamentCommand } from './commands'
import { scheduleTournamentInteraction } from './interactions'
dotenv.config()

const client = new Client({ intents: [GatewayIntentBits.Guilds, GatewayIntentBits.GuildMessages] })
const rest = new REST({ version: '10' }).setToken(process.env.BOT_TOKEN as string);
(async () => {
  await rest.put(Routes.applicationGuildCommands(process.env.CLIENT_ID!, process.env.GUILD_ID!), { body: [scheduleTournamentCommand] })
})()

client.on('interactionCreate', async (interaction) => {
  if (!interaction.isCommand())
    return

  if (interaction.commandName === 'schedule')
    await scheduleTournamentInteraction(client, interaction)
})

client.login(process.env.BOT_TOKEN)
client.on('ready', () => consola.info('Discord client ready.'))
