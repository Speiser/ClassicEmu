# Authentication Process
The goal of this document is to describe the authentication process.

## Overview
- [x] Client connects to Authentication Server (AS)
- [x] Client sends `ClientLogonChallenge`
- [x] AS responds with `ServerLogonChallenge`
- [x] Client sends `ClientLogonProof`
- [x] AS responds with `ServerLogonProof`
- [x] Client is authenticated
- [x] Client sends `ClientRealmList`
- [x] AS responds with `ServerRealmList`
- [x] Client chooses a realm and connects to World Server (WS)
- [ ] WS responds with `ServerAuthenticationChallenge`
- [ ] Client sends `ClientAuthenticationSession`
- [ ] WS requests `ClientSessionInfo` from AS
- [ ] AS responds with `ClientSessionInfo`
- [ ] Client is authenticated on WS (and his realm)


## Servers
- [x] Authentication Server [Implementation](https://github.com/Speiser/ClassicEmu/blob/master/classicemu/auth/logonserver.py)
- [ ] World Server [Implementation](https://github.com/Speiser/ClassicEmu/blob/master/classicemu/world/worldserver.py)

## Messages
### ClientLogonChallenge
This message starts the authentication process and is sent after the client initiates his connection to the authentication server. This message contains the username (important) and some details about the client and its environment. [Implementation](https://github.com/Speiser/ClassicEmu/blob/master/classicemu/auth/clientlogonchallenge.py)

#### Struct
```
| type    | name        | bytes | used* | info                           |
|---------|-------------|-------|-------|--------------------------------|
| uint8   | cmd         |     1 | false | opcode (0)                     |
| uint8   | error       |     1 | false | ?                              |
| uint16  | size        |     2 | false | len(packet) - 4                |
| uint8   | gamename[4] |     4 | false | WoW\0                          |
| uint8   | version1    |     1 | false | Major version (1 for 1.2.3)    |
| uint8   | version2    |     1 | false | Minor version (2 for 1.2.3)    |
| uint8   | version3    |     1 | false | Patch version (3 for 1.2.3)    |
| uint16  | build       |     2 | false | Build number                   |
| uint8   | platform[4] |     4 | false | Platform reversed (eg "68x\0") |
| uint8   | os[4]       |     4 | false | OS reversed (eg "niW\0")       |
| uint8   | country[4]  |     4 | false | Locale reversed (eg "SUne")    |
| uint32  | timezone    |     4 | false | ?                              |
| uint32  | ip          |     4 | false | Client´s IP in binary          |
| uint8   | I_len       |     1 | true  | len(I)                         |
| uint8   | I[50] (SRP) |    50 | true  | Username                       |
```
`*` currently used in ClassicEmu  
`(SRP)` value needed for [SRP6](http://srp.stanford.edu/design.html) ([Implementation](https://github.com/Speiser/ClassicEmu/blob/master/classicemu/crypto/srp6.py))

### ServerLogonChallenge
This message is the response to the `ClientLogonChallenge`. [Implementation](https://github.com/Speiser/ClassicEmu/blob/master/classicemu/auth/serverlogonchallenge.py)

#### Struct
```
| type    | name        | bytes | info                |
|---------|-------------|-------|---------------------|
| uint8   | cmd         |     1 | opcode (0)          |
| uint8   | error       |     1 | errorcode           |
| uint8   | unk2        |     1 | ?                   |
| uint8   | B     (SRP) |    32 | Server public value |
| uint8   | g_len       |     1 | len(g)              |
| uint8   | g     (SRP) |     1 | 7                   |
| uint8   | N_len       |     1 | len(N)              |
| uint8   | N     (SRP) |    32 | Big prime           |
| uint8   | s     (SRP) |    32 | salt                |
| uint8   | unk3        |    16 | ?                   |
| uint8   | unk4        |     1 | ?                   |
```

### ClientLogonProof
### ServerLogonProof
### ClientRealmList
### ServerRealmList
### ServerAuthenticationChallenge
### ClientAuthenticationSession
