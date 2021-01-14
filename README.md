# SharpChess

A command line application written in C# for playing Chess.

## Screenshots

![Screen Shot 2021-01-12 at 7 40 57 PM](https://user-images.githubusercontent.com/30376211/104391931-01eb4b80-550f-11eb-9ac6-aea20bd3e262.png)

Figure 1: Main menu

![Screen Shot 2021-01-12 at 7 53 57 PM](https://user-images.githubusercontent.com/30376211/104392279-c2712f00-550f-11eb-87f3-2b1f084a84f0.png)

Figure 2: Chess match rendered using default theme

![Screen Shot 2021-01-12 at 7 55 57 PM](https://user-images.githubusercontent.com/30376211/104392458-17ad4080-5510-11eb-8609-7a3b487d33d4.png)

Figure 3: Chess match rendered using unicode piece theme

![Screen Shot 2021-01-12 at 7 56 57 PM](https://user-images.githubusercontent.com/30376211/104392569-59d68200-5510-11eb-8171-ed30e8a06116.png)

Figure 4: Chess match start menu

## How to Run

## Functionality & MVP

### Controls

The control scheme is described below. Type any of the following characters to send input to the application.

* `w` - Move cursor up.
* `d` - Move cursor right.
* `s` - Move cursor down.
* `a` - Move cursor left.
* space - Enter/select element currently highlighted by cursor.
* `q` - Opens the match menu when pressed during a match. The match menu can be used to save a match in the middle of a game.
* `t` - Toggles the board theme when pressed in the middle of a match.
* `f` - Flips the board about the horizontal axis passing between rows 4 and 5.
* `u` - Undo the previous turn when pressed during a match. This will decrement the displayed turn number, return the piece moved in the last turn to its previous location, move the cursor to the previous destination, and toggle the current user. This command does nothing on turn 1. After loading a saved game, you cannot undo turns that were played before saving.
* `r` - Redo an undone turn. Reverses all changes from the last undo command. Note: The redo cache will be cleared if you undo a command, then set a new space prior to redo.
