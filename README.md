# Dsj2Tournaments

An attempt to create Discord based bot to host custom Deluxe Ski Jump 2 mobile version tournaments.

## Architecture

The project consists of two folders: server and client. The server is written in C# and uses ASP.NET Web Api backed by PostgreSQL database. The client will use Discord.js with TypeScript, backed by Node.js server.

## Server

### Endpoints

For exact models reference, see ```/server/Models``` folder and ```/server/Controllers``` folder for errors reference.

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

**HTTP POST** ```/jump```

Posts a jump to the matching tournament.

```json
{
  "user": {
    "userId": 310443068937732108,
    "username": "Mensix#2002"
  },
  "replayCode": "acyhRgcUxCsQ",
}
```

**HTTP DELETE** ```/jump/{replayCode}```

Deletes jump by given replay code.

**HTTP GET** ```/tournament/current```

Returns current tournament(s) info.

```json
[
  {
    "createdBy": {
      "userId": 310443068937732108,
      "username": "Mensix#2002"
    },
    "settings": {
      "liveBoard": true
    },
    "hill": {
      "name": "Finland K105"
    },
    "code": "3f0173",
    "createdDate": "2023-01-29T09:53:59.821482Z",
    "startDate": "2023-01-29T09:57:11.268Z",
    "endDate": "2023-01-29T10:17:11.268Z",
    "isFinished": false
  }
]
```

**HTTP POST** ```/tournament```

Creates new tournament, returns error if dates are invalid.

```json
{
  "createdBy": {
    "userId": 310443068937732108,
    "username": "Mensix#2002"
  },
  "settings": {
    "liveBoard": true
  },
  "hill": {
    "name": "Finland K105"
  },
  "startDate": "2023-01-29T09:57:11.268Z",
  "endDate": "2023-01-29T10:17:11.268Z"
}
```

**HTTP DELETE** ```/tournament/{tournamentCode}```

Deletes tournament with given code, an user body is required. Returns nothing.

```json
{
  "userId": 310443068937732108,
  "username": "Mensix#2002"
}
```