# Unity Achtung die Kurve!

A remake of flash game [Achtung, die Kurve!](https://achtung.life/)

## About

The whole game has 2 main scenes: Main Menu and Game Board. Players select their characters and controls and then after pressing **spacebar** they go to game.
In the game each player controls a dot that leaves a colored trail behind, don't hit it! When player hits a wall or tail left by players they are eliminated and other players are granted points.
After everyone except one player dies the round ends and after pressing **spacebar** once more new round begins.

During match a variety of **Power-ups** can appear which can be picked up by players to alter the course of the round.

There's an additional scene: Settings in which there are a number of options to change to whatever you like.

The game requires at least 2 players in order to start with the max amount being 6 players.

## Controls

Pressing spacebar has a number of functionalities:
  1. When in Main Menu and at least 2 players are selected and have chosen their controls the game goes to Game Board.
  2. When on Game Board:
      - At the start of a new round allows everyone to move.
      - During the round allows to pause.
      - After the end of round restarts the scene.
      - After one player wins goes back to the Main Menu.
    
Pressing and holding escape has 2 uses:
  1. When in Main Menu after 1 second the game closes.
  2. During round after 3 seconds game goes back to Main Menu.

    
That's about it, every other control is chosen by players.

## Power-ups

Power-ups can appear of 3 types: Global, Other and Self.
1. Global power-ups affect the whole board or do some general stuff.
2. Other power-ups affect every player except for the one who collected it.
3. Self power-ups affect the player who collected it.

---

Here's a list of every power-up currently in the game:
1. Speed Boost - Boost players speed (can be either Self or Other type).
2. Slow Down - Decreases players speed (Self, Other).
3. Grow - Makes players tail and head grow (Self, Other).
4. Shrink - Shrinks players tail and head (Self, Other).
5. Maze Move - Makes players turn 90 degrees instead of typical curved turn (Self, Other).
6. Reverse Keybinds - Reverses players keybinds (left keybind is right and vice versa) (Other).
7. Phase Walk - Makes player not leave a tail behind and be able to pass through other players tails without dying (Self).
8. Player Wrap - Makes players head blink and be able to wrap around borders of the board (Self).
9. Wrapped Borders - Makes border walls blink and allows every player to wrap around them (Global).
10. Spawn Power-ups - Spawns 4 additional power-ups on the board (Global).
11. Random Power-up - Upon being picked up chooses a random power-up to activate (Global).
12. Color Chaos - Makes every player change to a random color (Global).
13. Clear Tails - Clears tails of every player (Global).
14. Camera Chaos - Makes camera do a 360 degree turn (Global).

## Screenshots

<img width="1919" height="1079" alt="Main Menu" src="https://github.com/user-attachments/assets/fcbba3bd-fb03-4286-90b2-ca6d42bce094" />

---

<img width="1919" height="1079" alt="Settings Menu" src="https://github.com/user-attachments/assets/9287a785-48f4-4537-a192-db404666405c" />

---

<img width="1919" height="1079" alt="Game Board" src="https://github.com/user-attachments/assets/35ea99f3-aac8-4c57-ae04-eaab2c4ec3ca" />
