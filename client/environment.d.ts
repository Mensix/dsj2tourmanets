declare global {
    namespace NodeJS {
        interface ProcessEnv {
            APPLICATION_ID: string;
            GUILD_ID: string;
            BOT_TOKEN: string;
            BASE_URL: string;
            WORKING_CHANNEL: string;
        }
    }
}

export { };
