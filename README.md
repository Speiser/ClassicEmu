# ClassicEmu
Play with different client versions on the same realm

## Status
| Task                          | Classic (1.12.1)   | TBC (2.4.3)        | WotLK (3.3.5a)     |
|:------------------------------|:------------------:|:------------------:|:------------------:|
| Login                         | :heavy_check_mark: | :heavy_check_mark: | :heavy_check_mark: |
| Character creation            | :heavy_check_mark: | :heavy_check_mark: |                    |
| Spawn player                  | :heavy_check_mark: | :heavy_check_mark: |                    |
| Spawn creatures               | :heavy_check_mark: |                    |                    |
| Movement sync between clients | :heavy_check_mark: | :heavy_check_mark: |                    |
| Auto attacking                |                    |                    |                    |
| Spell casting                 |                    |                    |                    |
| Damage units                  |                    |                    |                    |
| Spawn items                   |                    |                    |                    |

## How to use
### Database Setup
- Option 1: docker-compose
  - run `docker-compose up`
- Option 2: local postgres instance
  - adapt connection strings in `appsettings.json` for both projects ([Auth](https://github.com/Speiser/ClassicEmu/blob/master/src/Auth/appsettings.json#L3) and [World](https://github.com/Speiser/ClassicEmu/blob/master/src/World/appsettings.json#L3))

### Run Auth and WorldServer
- run both projects via `dotnet run` or your IDE
- login with any username
    - use the username as password
    - if the user does not exist, it will be created