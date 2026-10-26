# 2D-Game
3902 2D Game Design

### Team Members
Lydia Williams, Joseph Painter, Lily Scheerer & Ethan Weintz

___

# Team Seven 777
Method of Contact: Discord and SMS  
Designated Meeting Time @ 4:30 Fridays In Person or Online  

### Project Goals
*What do we value from least to greatest?*  

 1. Functionality/Grade
 2. Team Morale & Communication
 3. Code Cleanliness
 4. Equal Work Loads
 5. Compilation Time
 6. Breadth of Features

### Conflict Resoltuion
 *What are our methods?*  
 1. Discussion of conflict during meethings and compromising the matter
 2. Meeting with instructor to arbitrate

 *How do we approach specific scenarios?*  
 - If No work is done in a particular area 3 days before a deadline, someone must take
 over for that assigned area.
 -  When arguing over feature implementation, decided by group vote. Ties will be
 resolved by the will of the people (anonymous tie breaker)

### Project Game Vision
Nighttime Forest Explorem-up : The Fourest Seven  

A top down perspective game where the protagonist will explore a dark and spooky forest in search of their friends. Facing enemies like spiders, wolves, and mosquitos, the goal is to collect enough goods to make smores with the pals back at the campfire, and make it out alive...

---
# Design Philosophy
### Ideas behind code structure 

When it comes to 




For the spawning of Projectiles, the responsibility of creating them is handled by the respective projectile handlers for the player and enemies. The reason they are separated is for clarity's sake, making it not only easier to debug, but also to create 
better cohesion of the class. Each class spawns projectiles when the command is called to use a ranged weapon in the case of the player, and when specific enemy types are chasing the player after a short cooldown. As it stands, projectiles are loaded and unloaded
from a queue, meaning the oldest projectile gets removed first and is typically what hits first, and then updated until it despawns or collides with something, which is handled by a separate collision handler class. There is a flaw with this design however, 
because the player could get hit by a newer projectile and only the further one gets removed, and therefore we are looking into alternative data structures to read and write this information. The projectiles themselves contain information about what type they are, the 
direction they are travelling, and how big their hitbox is.

Collisions are handled by independent classes for each "type" of object in the game (i.e. Player, Enemies, Obstacles, Projectiles etc.) They read hitbox classes, which themselves have unique classes for each type of object with slight differences specific to their type
that constantly check if they are colliding with something else. If the hitboxes return a collision, the collision handler determines the behavior of the objects after the collision, or in some cases before they even happen to cut down on jittery behavior, mainly
in the case of obstacles that prevent movement.

---

# Game Information 

You play as [PLACEHOLDER] in a Forest Campsite trying to find [PLACEHOLDER] while collecting [PLACEHOLDER] and dodging Ghosts, Spiders, and Mosquitos

| Controls    |   Keys        |
| ----------- | ------------  |
|	W		  | Move Upward   |
|   S		  | Move Downward |
|   A         | Move Left     |
|   D		  | Move Right    |
|  Space  	  | Interact 	  |
|   Z         | Fist          |
|   1         | Stick    	  |
|   2         | Ranged Attack |
|   Y     	  | Cycle  Weapon |
|   ESC 	  | Pause 		  |
|   R         | Restart		  |
|   Q		  | Quit		  |
| (DEBUG) H   | Show Hitboxes |
| (DEBUG) E   | Take Damage   |





