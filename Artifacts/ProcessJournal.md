
# 9/6/23 - Engine 

The prototype I set out to make was a simple combat simulator. I wanted to explore the elements that go into creating a 3D, top down, melee combat system.

I started by looking for some assets I could use. I found a knight character, and a pack of animations off of Mixamo, which included idle, running, and attack animations. 

After a few hours of getting unreal engine setup with git as my source control, and importing the mixamo assets, I have decided I am way over my head with the time scope of 2 weeks I set for myself. I am much less familiar with unreal c++ than unity, and with the time I have I think I would be putting myself through alot of pain. 

I got a basic top down character controller setup in unity, as well as a mixamo character with walking and idle animations. 

# 9/7/23 - Colliders

I dont like how I was handling movement animation, currently it is just a bool.
I setup a blend tree, after spending half an hour learning about them, that creates a blend between the walk and run animation based on the speed the player is going. 

I tried importing a sword model I downloaded, which apparently did not include textures. It took me awhile to figure this out. I dont understand textures and materials as well as I thought I did.

After an hour more I found a sword fbx with seperate texture png files. I dropped these into unity and recruited the help of a modeling friend to help me align the textures with the correct parameters on the material. 

I got to work getting an attack animation to play on a key press. I spent a good 3 hours  learning about animation layers and avatars and avatar masks.

I have now reached a point where I have a design decision to make. 
* Attach a collider to the sword model, and have that determine hits landed on enemies. However this now couples my attack radius to the animation. 
* Activate a colldier infront of the player that covers the attack area I want. This decouples my damage area from my animation. 

As I am not an animator and I am working with free assets, Im leaning towards the second option. I plan to create a sword slash effect using shader graph, that creates a disc-like shape following the sword. I want to see if I am able to achieve this effect and how it feels. I think I will let this determine which method I take for colliders. My thought is the effect will give more of a visual cue as to where the player can deal damage rather then an invisable box if I go the second route.

I am starting my journey into vfx shader graph

# 9/10/23
I succesffully implemented the sword fx on the player. I did not choose to use VFX graph, as that needs either HDRP or URP package which I didnt feel was needed. I was able to achieve an effect with unitys regular particle system.

I also re-worked the attack state system. I realized using anim notify events was a much more accurate and performant way to change the state of the weapon. 

I've decided I need to get enemys in the game to make a decision about the colliders. I also find that the FX of the sword doesnt feel the most responsive as if you run while attacking it leaves a large streak behind, instead of highlighting the movement of the sword. I may need to find a more baked way to create this effect.

# 9/19/23

I worked on combat values. I found that animation speed made a large impact on the feel of the combat. The faster the animation the quicker reactions and more accurate percision the player had to have. I tested values till I found a balance that felt fair to me. If it was too slow I found it boring, but if it was too fast I found it felt unfair and so in turn, uninteresting.   

### 1.5x enemy attack animation speed
![Alt text](Unity_6sGASpsNHg-1.gif)

### 1x enemy attack animation speed
![Alt text](Unity_HucOnpQGtk.gif)

I added a currentspeed variable for the enemys and gave the decceleration and acceleration like I did with the player, originally just to get the walking animation working. But I found these two variables impacted gameplay alot, giving a buffer from which to strike after an attack. I think a nice rhythm was struck which you can see in the animations. A slower acceleration helped achieve this as it rewarded being aggressive. Running away from the fight is now punishing as the faster the enemy is going the longer it takes them to slow down and so they end up sliding a bit closer when attacking. 

I am worried this may feel unfair to a player who is unaware of this due to the way spawning is handled with the enemies being offscreen. I worry it is not clear enough that the enemy killed the player because it was moving faster, as it starts accelerating to its maxspeed offscreen so the player never sees. I am unsure how to fix this issue but I wonder if acceleration and decceleration for an enemy that is not necessarily associated with speed, like a big troll. This might translate better with a real world example that is known to have acceleration adn decelleration like a car. 