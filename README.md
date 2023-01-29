# Dsj2Tournaments

An attempt to create Discord based bot to host custom Deluxe Ski Jump 2 mobile version tournaments.

## Architecture

The project consists of two folders: server and client. The server is written in C# and uses ASP.NET Web Api backed by PostgreSQL database. The client will use Discord.js with TypeScript, backed by Node.js server.

## Server

### Endpoints

**HTTP GET** ```/jump/{id}```

Returns scraped jump data from the Mediamond server, returns error if not found.

```json
{
  "replayCode": "acyhRgcUxCsQ",
  "hill": {
    "name": "Slovakia K110"
  },
  "player": "mask",
  "length": 127.49,
  "crash": false,
  "points": 148.6,
  "date": "2022-03-12"
}
```