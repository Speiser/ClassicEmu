# ClassicEmu
ClassicEmu is an Open Source Server for World of Warcraft (1.12.1) written in Python.  
This is a "proof of concept" project.  
For specifications and other helpful resources check the `/doc` folder.

## Status
LoginServer working  
WorldServer WIP

## Dependencies
+ Python 3.6

## Usage
```
git clone https://github.com/Speiser/ClassicEmu.git
cd ClassicEmu
python main.py login
python main.py world
```
+ Change realmlist.wtf to `set realmlist 127.0.0.1`
+ Start the game
+ Login in (Username has to be equal to the password!)

## Auth Server
![authserver](https://github.com/Speiser/ClassicEmu/raw/master/doc/auth.png)
