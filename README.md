# Synology Mobile Photo Organizer

## Use Case

You are using DS Photo to backup mobile photos to a single folder -
but would like those organized into dated folders.

This currently works as a one-time execution to sort photos. Time could be
spent to look at a "always running" container - which needs to regularly
logout and re-login at configured intervals, perhaps. 

This could be used for non-photos, too. Nothing in here is photo specific, but
I'm not sure what use case that is.

## Running

> TODO: provide Docker run instructions without need to clone this repository.

## Configuring

### Docker

Using environment variables, you can override critical details.

```yml
version: "3.6"

services:
  server:
    container_name: synology-mobile-photo-organizer
    image: ghcr.io/mitch-b/synology-mobile-photo-organizer:latest
    restart: always
    environment:
      - TZ=America/Chicago
      - SYNOLOGYCONNECTION__HOST=http://dsm-url.local:5000
      - SYNOLOGYCONNECTION__USERNAME=your-synology-automation-account
      - SYNOLOGYCONNECTION__PASSWORD=ABCDEFG1234567
      - ORGANIZEINFO__MOBILEUPLOADPATH=/photo/mobile
      - ORGANIZEINFO__DESTINATIONPATH=/photo/<year>/<month>/<day>
```

### appsettings.json

You can override/update details in the JSON settings.

```json
{
  "SynologyConnection": {
    "Host": "http://dsm-url.local:5000",
    "Username": "your-synology-automation-account",
    "Password": "ABCDEFG1234567"
  },
  "OrganizeInfo": {
    "MobileUploadPath": "/photo/mobile",
    "DestinationPath": "/photo/<year>/<month>/<day>"
  }
}
```

## Path Variables

I wanted date-organized folder structure, so the configuration `DestinationPath`
supports these tokens:

* `<year>` - derived from the modified date of the file/photo
* `<month>` - derived from the modified date of the file/photo
* `<day>` - derived from the modified date of the file/photo

It would be trivial if you wanted your albums to be `<year>/<month>`
only to modify the configuration accordingly. 