# Charades

## Game Types
- Quick Play / Partida rápida (id: 0): Only one category is selected.
- Competitive / Competición (id: 1): Multiple categories, played in succession.
- Mash-Up (id: 2): Multiple categories mashed together in a single round.

## Scenes

- MainMenu: Shows the main menu of the game.
- CategorySelect: Where the categories played are chosen.
- Presentation: Shows an overview of the category about to be played.
- Countdown: Countdown before a round begins.
- CharadesGameplay: Scene where a round unfolds.
- RoundResults: Shows the score and prompt list from a particular round.
- FinalResults: Shows the final score for each player.
- CustomCreator: Category creator for the Charades game type.

## Sounds
Made using Chiptone.
- blip1: Category selection / Countdown
- blip2: Countdown finish
- coin1: Forward button selection
- coin2: Correct prompt
- hurt1: Return button
- hurt2: Clear categories
- hurt3: Skip prompt
- 1up1: Play button
- 1up2: Round clear
- lose1, boom1, boom2 (currently unused)

## Scripts

### SFXManager
Script in charge of playing sound effects. Made using the Singleton design pattern.

### SoundEffectManager
Used whenever a single sound effect needs to be played.

### MainMenu Scripts
- SetVersionText: Used to write the version in the bottom part of the screen.
- ScrollingBackground: Scrolls BG image.
- DefaultSliderMenu: Used to set slider values in the Options submenu.

## Bugs to fix
- Canvas are mismatched in CustomCreator.
- CustomCreator does not react properly.
- Sliders don't update properly when returning to Main Menu. This is caused due to interacting with the SFXManager, which stays loaded.
- Music and SFX values aren't loaded until entering Options
- Pause triggers a point on PC version.

- Category
- CategoryButton
- Competition
- Countdown
- Game
- GameOver
- JSONReader
- Menu
- Pause
- SceneLoader
- Score
- Timer