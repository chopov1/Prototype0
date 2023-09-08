
# 9/6/23

The prototype I set out to make was a simple combat simulator. I wanted to explore the elements that go into creating a 3D, top down, melee combat system.

I started by looking for some assets I could use. I found a knight character, and a pack of animations off of Mixamo, which included idle, running, and attack animations. 

After a few hours of getting unreal engine setup with git as my source control, and importing the mixamo assets, I have decided I am way over my head with the time scope of 2 weeks I set for myself. I am much less familiar with unreal c++ than unity, and with the time I have I think I would be putting myself through alot of pain. 

I got a basic top down character controller setup in unity, as well as a mixamo character with walking and idle animations. 

# 9/7/23

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

## Bugs

attack animation does not play consistantly on key press.