# Idle-Game

This is a Unity game project I created as a portfolio piece. 

This game demo showcases some of the core features of an idle clicker game similar to Adventure Capitalist. 

  - The player can purchase factories (bottle cap, shoe, furniture etc) and these factories will generate money. The money can then be used to buy and upgrade more factories.
  - The player continues to earn money while away and receives those offline earnings when returning to the game. This is done by saving and loading the player’s data. 

The code is architected using Scriptable Objects and Events. I created a MVC type relationship between the factory's logic code and the factory’s UI using Scriptable Objects. The SO holds the factory’s values and when the logic code makes a change to the values the SO sends an OnValueChanged event and the UI is automatically updated.

Prefab Variants were used to create the factories. They all inherit from one parent prefab. 

I used the SOLID principles as a guide while writing my code

