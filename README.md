# UnityAssignment3

Assignment #03

Deadline For Submission
01/03/26, 20:00

In this assignment, you will practice using triggers, prefabs, coroutines, and audio, as introduced in recent lectures. Continue from assignment #02, and update it accordingly. 

You can use scripts from the project we’ve been building in class.

Instructions
Create a new type of enemies - enemies that can shoot at the player.
Each enemy starts in a patrol state and patrols between two points selected in the scene.


When the player enters the enemy’s detection radius:
The enemy stops patrolling and turns to look at the player (instead of chasing him like in the previous assignment). 


The enemy then waits for 2 seconds, and if the player is still within range, it shoots a bullet in their direction. Otherwise, it returns to patrolling.


After 2 additional seconds, if the player is still within range:
The enemy shoots two bullets, with a 0.5 second delay between each shot.


It then waits 1 second and returns to patrolling.
If the player is not within range at that moment, the enemy immediately returns to patrolling.

Track each enemy’s state (e.g., patrolling, shooting).

Enemy Types
Create three different shooter enemies:
One with 100 max health.


One with 200 max health.


One that shoots a strong bullet which causes 20 points of damage (instead of 10).


Distinguish enemies by color.
Distinguish bullets by color and size (the bullet can simply be a circle that moves forward).

Audio 

When an enemy shoots, play a shooting sound effect.


When a bullet hits the player:


Play a hit sound effect.


Destroy the bullet.


Bullets should also be automatically destroyed after 2 seconds if they don’t hit the player.

Animation 
Use the enemy attack animation to shoot the bullet from the end of the enemy’s hand — spawn the bullet on the exact animation frame where the hand is stretched forward.



📊 Grading Criteria

Your grade will be based on the following:
✔️ Requirements
Ensure all requirements are met.


✔️ Good Practice
Follow proper Unity workflow as demonstrated in class such as exposing to variables, getting references, separate to functions wherever you use the same block of code to maintain readability and efficiency.


✔️ Project Structure
Keep your project organized (folders, asset naming, etc.).


⚠️ In order to avoid bad practice, avoid using features or topics that have not yet been covered in class. Doing so will result in a lower grade.
Good luck! 😊
