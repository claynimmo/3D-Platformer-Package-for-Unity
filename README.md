
# 3D Platformer Basics

This package includes a standard character controller, with a health and respawn system. It also includes several different obstacle types.

## Dependencies
This package requires the new input systems to be installed and enabled in the project.

A SettingsData static class is also required to control sound effect volumes, and is used for many of the other packages. It is included in this package, but it may need to be deleted or updated when adding additional packages.

## Player

The player is composed of several scripts.

- CameraMove: third person camera controller, that automatically adjusts its distance to remain in front of walls.

- Movement: handles the general movement of the player. Is coupled to the camera move script to handle directional controls.

- MovementVisuals: coupled to the movement script to activate animation states based on what is happening in the movement script. An empty animation controller is setup, where you can add your own animations to the nodes, and even add more nodes for more movement types such as dashes.

- PlayerHealth: handles the health of the player, containing resistances, regeneration, invinsibility frames, and calls for the player's death in the DeathState script.

- DeathState: handles the player death, including respawning. It includes scaffolding for UI integration.

## Utilities

This package includes some utilities to improve quality of life.

- BatchedUpdate / BatchedUpdateManager: apply the batched update base class to any component that relies on any update function, where there are large quantities that can be batched together increasing performance. The batched update manager calls these update functions through its own. Currently, this is implemented in the gravity zone component, since in the game I extracted the code from it was used extensively.

- AutoDestruct: destroy an object after a set amount of seconds. Used to cleanup created prefabs, such as effects that need to be coupled to dynamic objects.

- MouseManager: manages the cursor lock state and visibility, which is a requirement for any game.

- ExitManager: script to pause the game and unlock the cursor, which is necessary for fullscreen games, where the user can get stuck in the window. This is coupled to the MouseManager script.

A prefab containing all of these scripts is provided.

## Interfaces

There are two interfaces that can be applied to your custom classes to interface with the package.

- IHealth: apply this interface to everything that can take damage, such as breakable walls, enemies, bosses etc. This interface is applied to the PlayerHealth script

- IDead: apply this to any script that requires an action when the player is killed, such as reseting boss fights, loading assets, changing music, or even scripted scene transitions.

- IButton: apply this to any script that can be controlled by a button, such as for puzzles.

## Obstacles

Several obstacle types are included in this package to help with designing levels.

### Buttons

There are currently two types of buttons:
 - standard buttons, that activate once when touched and reset after a time.
 - pressure buttons, that remian active while the player is standing on it, and resets immediately when the player steps off it.

Add more button types by inheriting from the Button base class.

The buttons can perform a primary or secondary function from the IButton interface method. For example, the primary function for barriers is enabling and disabling it, and the secondary function is to enable or disable its alternating functionality.

### Traps

The traps are obstacles that call the TakeDamage function in the IHealth interface. There are two types currently, one for particle collisions and another for triggers.

For the particle collisions, put the script on the same gameobject as the particle system. Ensure the particle system sends collision messages, and limit the layermask to only contain the necessary objects, such as a Player or Enemy layer to minimize the performance cost of the GetComponent on every collision. For further optimization, try to limit collisions by extracting the collision particles from the display, which is what I use for my attack combos where the attacks only create one particle.

For the trigger, simply apply it the gameobject of choice and include a collider that is marked as a trigger.

### Zones

Zones are areas that cause a continous effect to objects that are inside of them.

Currently, the only zone is a gravity zone, that applies a force to all rigidbodies that come into contact with it. This zone must be connected to a BatchedUpdateManager to function, since its update calls are batched.

### Platforms

Several types of platforms are included in the package. Platforms that have specific button interactions will be described.

#### Ghost Platform
The GhostPlatformZone script is applied to a gameobject with a visible transparent material and a trigger collider. This platform spawns a smaller platform whenever the player collides with the ghost zone, so the player needs to continuously jump to cross it, while simply walking will make them fall through. This does not have any interactions with buttons. A prefab example is provided.

#### Invisible Objects
The InvisibleObjectManager script does nothing other than making the invisible objects visible on the primary action of a button press, by enabling renderer components from an array serialized in the editor. It has no secondary action. The invisible objects should initialy have their renderer components disabled.

#### Jump Pad
Launch the player upwards vertically, similar to if they have jumped. The jump pad instantiates an effect that is parented under the player. An example prefab is provided.

#### Barricade
Barricades are usefull for blocking of sections or having timed platform challenges. It has an alternating function, where it automatically enables and disables periodically over time through a coroutine. This alternating pattern is enabled and disabled through the secondary function of a button. The primary button function simply toggles the barrier.

#### Temporary Platform
The TempPlatform script removes a platform shortly after the player lands on it, after which it will re-enable after a delay. It swaps the active material after being touched as a visual warning for when it will disable. It has no interactions with buttons. There is a sample prefab provided.

### Moving Platforms

To save on performance, the moving platforms can be controlled using the MoveObstacleTrigger script, where it turns on when the player enters the collider, and disables the movement when the player leaves. This only updates the paused boolean in the moving obstacle scripts, so this can be ignored.

#### Rotating Platform
Rotates an object in a direction vector, multiplied by a scalar speed. The primary action of the button reverses the direction. If you intend to parent the player to moving platforms to make them move with it, you need to be carefull with the scale of the rotating platform, since it can distort the player model.

#### Standard Moving Platform
Platform that moves at a constant speed between a list of postions, set as gameobjects to make visualising the movement in the scene easier. It has no interaction with buttons. If the move towards causes too much jitter, it can be replaced with any lerp function, but this requires additional code to ensure the platform moves at a constant speed independant to the distance to the next object.