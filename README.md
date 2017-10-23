# SimpleSpriteAnimator
Simple sprites animation for Unity 3d. It allow to create base classic sprites animations. Not used “Unity Animator”, its just show/hide sprites (GameObjects) in hierarchy.


## Animations types

- Loop. Its simple loop from 0 frame to end, and begin again.

- Ping pong, with allow begin from one frame (can enable/disable if need), but begin loop from other frame. 
For example: we have “run animation sprites” , from 0 to 10 - it transition from “Stay”,
From 10 to 20 - it “run loop”. So in first we play 0-10, and loop only 11-20.

- Once, with allow play other animation when current “once animation” is end (can disable/enable if need).
For example we have “attack animation”, when attack is finished its need play “idle”.


## Its support lot of animations per object.
Have DEMO scene for know how it work. 1 player have animations run, idle, attack, etc…
