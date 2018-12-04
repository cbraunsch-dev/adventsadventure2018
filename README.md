# Advents Adventure 2018
This is an advents calendar game I made in the winter of 2018.

![Screenshot](https://github.com/cbraunsch-dev/adventsadventure2018/raw/master/Screenshots/_AdventsCal1.png "Screenshot")

# Setup
The game was made using Unity version 5.6.3p2. To play the game:

- Clone the project
- Open it in Unity, doing any necessary project conversion
- Open the scene 'TitleScreen'
- Press on the Play button

If you're on a Mac, you can also try to download the binary in the `Binaries` folder and run that. The binary was compiled under **Mac OS Mojave (10.14)**.

# Rules of the game
The goal of the game is to get to the end of the map within a set number of turns. The number of turns is shown in the game in the top right under **Days remaining**. In addition to having to reach the end in time, the player also has to collect enough pieces of shell to rebuild the snail's shell. These pieces are scattered throughout the game board. Some can be found by talking to the NPCs in the game while others are found by stopping on specific spaces.

![Screenshot](https://github.com/cbraunsch-dev/adventsadventure2018/raw/master/Screenshots/_AdventsCal3.png "Screenshot")

![Screenshot](https://github.com/cbraunsch-dev/adventsadventure2018/raw/master/Screenshots/_AdventsCal2.png "Screenshot")

![Screenshot](https://github.com/cbraunsch-dev/adventsadventure2018/raw/master/Screenshots/_AdventsCal4.png "Screenshot")

## Movement
Unlike traditional board games, players in this game do not move by casting a die (or a virtual die). Instead, players play a little mini game which determines how far they can move. The better players are at the mini game, the farther they can move. This mini game mechanic allows the player to choose how far he wants to move provided he is skilled enough at the mini game. The movement mini game works as follows:

- Press on the **Let's Go** button on the bottom left
- You will now see a ball orbiting a square. Pardon this very simplistic visual style but I didn't have time to beautify this. I only had 3 weeks to make this game so I had to cut corners somewhere
- In the top left corner you will see the current score

![Screenshot](https://github.com/cbraunsch-dev/adventsadventure2018/raw/master/Screenshots/Movement1.png "Screenshot")

- This score represents the amount of spaces you will move once you finish the mini game
- The goal of the mini game is to align the orbiting ball with the square. Once the ball is inside the square, hit the space bar
- The center ball will then increase in size and the orbiting ball's speed will increase
- Your score will now show **2**
- Keep trying to align the small ball with the square to increase your score

### Double Or Nothing
Once you managed to align the small ball with the square at least once, you will see a button in the bottom right called **Double Or Nothing**. 

![Screenshot](https://github.com/cbraunsch-dev/adventsadventure2018/raw/master/Screenshots/Movement2.png "Screenshot")

If you press this, you will see the score show something like: **Either X or Y** where X is double your current score and Y is always 1.

![Screenshot](https://github.com/cbraunsch-dev/adventsadventure2018/raw/master/Screenshots/Movement3.png "Screenshot")

If you now align the small ball with the square, you will earn the value X whereas if you miss, you will only earn a value of 1. You can decided to go double or nothing at any level (except for on the very first level). If you were at the level where your current score was 3, you could use double or nothing to try and get a 6. If you miss, however, you would only get a 1.