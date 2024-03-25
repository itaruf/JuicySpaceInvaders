## Juicy Space Invaders
![Build Passing](https://img.shields.io/badge/build-passing-brightgreen)
![Version](https://img.shields.io/badge/version-2.0.0-blue)
[![Itch.io](https://img.shields.io/badge/download-itch.io-%23e3326d)](https://wiloux.itch.io/juicy-space-invader)
[![Youtube](https://img.shields.io/badge/demo-youtube-%23db1818)](https://www.youtube.com/watch?v=c2318l2lzzI)

## 👾 Creepy Invaders - 2D survival action game

- Based on the famous Space Invaders game, but reskined and enhanced to be as creepy and pressuringas possible, play as a survivor that repels waves of enemies within a limited field of vision and a weapon that can be jammed.

<!-- Table of Contents -->
## :notebook_with_decorative_cover: Table of contents
- [About the project](#star-about-the-project)
  * [Screenshots](#camera-screenshots)
  * [Tech stack](#space_invader-tech-stack)
  * [Highlights](#star-highlights)
  * [Features](#dart-features)
  * [Challenges encountered](#zap-challenges-encountered)
 
<!-- About the Project -->
## :star: About the project

 <!-- Screenshots -->
### :camera: Screenshots

<div align="center"> 
  <img width="500px" src="https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExcWN3bWZmcDhtazlwM2R3dGRsN2xkbms2eXh2M21xZXZ6czVsNnpubiZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/fZmvchxhdMRq8MzCO8/giphy.gif">
</div>

<!-- TechStack -->
### :space_invader: Tech stack

  - **Programming language**: C#.
  - **Game engine**: Unity.
  - **IDE**: Visual Studio Community 2019.
  - **Version control**: Git.

### :star: Highlights 
- Incoming

### :dart: Features
<details id="projectDescription" open>
  <summary id="summaryText">...</summary>

  <ol style="text-align: justify;">
    <li><h4>Gameplay overview</h4>
      <ul>
        <li>The player stands behind a barricade (that can be destroyed by the enemies) with two batteries at each
side of it to reload their flashlight. The latter highlights the enemies to better shoot at them before they
can reach the barricade. </li>
        <li>The player must get rid of all the waves to win the game.</li>
      </ul>
  </li>
    
  <li><h4>Movements</h4></li>
    <ul>
      <li>The player can only move horizontally, while staying behind the barricade.</li> 
      <li>Waves of enemies move horizontally, and on reaching the sides, advance further towards the player. They also gain in speed each time they advance.</li> 
      <li>Overtime, enemies become harder to aim at, and the player then needs to preshot bullets to destroy them, while still dodging their attacks.</li> 
    </ul>
  </li>

 <li><h4>Player’s gun weapon</h4></li>
  <ul>
    <li>The player can fire bullets with his gun and in the direction they are facing. One bullet is fired at a time, and each increases the weapon jam bar.</li> 
    <li>If the player spams bullets, the gun can be jammed. When that happens, they have to unjam it through a simple smash mechanic. To completely avoid that, they can also wait for the jam bar to decrease overtime.</li> 
  </ul>
</li>
  
<li><h4>Player’s flashlight</h4></li>
  <ul>
    <li>The player keeps a flashlight at all time, highlighting anything in front of them in a cone. To mimic real-life lightning, obstacles block the flashlight's light.</li> 
    <li>The flashlight has a limited battery, which causes the vision cone to decrease in size overtime. However, the player can recharge it thanks to batterie supplies located on the sides.</li> 
  </ul>
 </li>
  
  <br>
  <div align="center"> 
    <img src="https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExa2QzbDdsZ2I3a2Rtdjk3c242MzBobmFiNGIzYnA5cDJuM3piZWVrciZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/BIhyPrxxPkZtVqccB3/giphy.gif" style="display: block; margin: auto;" width="500" />
  </div>
    
  <li><h4>Enemy waves spawner</h4></li>
    <ul>
      <li>Enemies spawn in waves after a certain amount of time, regardless if the previous wave is cleared or not.</li> 
      <li>The spawner decides the spawn location considering the amount of enemies within, to keep it fair for the player.</li> 
    </ul>
  </li>
  
  <li><h4>Enemies' attacks</h4></li>
    <ul>
      <li>Enemies fire attacks towards the player and the barricade, which they can destroy after a certain amount of damages done.</li>
      <li>These attacks deal damages to the player when hit too, that momentarily stop the action-flow to allow the player to take cover.</li>
    </ul>
  </li>

  <li><h4>The barricade</h4></li>
    <ul>
      <li>Covering the player from the enemies' attacks, some areas of the barricade can be destroyed after a few hit. The player can use and preserve them efficiently by taking cover behind the undestructible obstacles spread across the barricade</li>
    </ul>
  </li>
  
  <li><h4>Dynamic events responses</h4></li>
    <ul>
      <li>To maximize the juicyness (theme of the game), different events are triggered by actions in-game.</li> 
      <li>From SFX / VFX triggering on the characters attacks and hits, glitching animation played specifically for the player when hit, to spawning new waves of enemies when the previous one has been destroyed, these events allow for a dynamic and immersive environment.</li> 
    </ul>
  </li>
    
<br>
  <div align="center"> 
    <img src= "https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExZDNnMHlyMXdta25va2pidXRlc2QyYWI2a25jOWRsZXYxanc1NmRociZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/rK4SeEKCb5ZskjGbCC/giphy.gif" style="display: block; margin: auto;" width="500" />
  </div>
</div>

</ol>

### :zap: Challenges encountered
- Since Space Invaders is a classic but basic game, the intent here was to make the game as juicy
and immersive as possible by focusing on the effects and by adding simple game mechanics
without loosing the spirit of a Space Invaders game.
