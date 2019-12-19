# GE1-Assignment-GenaMaze
###A Unity Project that uses Procedural Generation to create a Maze that the user can then attempt to escape with First Person Gameplay

The Goal of this project is to create a program in Unity that will allow the Procedural Genertion of 3D mazes, a scenario where the user can watch the maze being built before their eyes, and then actually step into and explore the brand new maze in a First Person Perspective.
As I plan to make use of Procedural Generation in a similar context for my Final Year Project, this seemed like the perfect test run to get familiar with Procedural Generaction concepts, and experience the ups and downs of their implementation. 
Procedurally Generated mazes are hardly a new concept, and based on my research on the topic thus far I've found what appears to be the perfect method for their implementation in Unity. 
An algorithm known as the "Hunt and Kill" Algorithm achieves the goal of Procedural Maze Generation, by first instansiating a grid (For example: a 10x10 Grid), and then "Hunting through the Grid by choosing a starting point and then knocking down either the North, South, East or West wall in a given cell in the grid, in order to traverse a path from the start point. The hunting ends when the path taken by the Algorithm leads it to loop back on itself through ground already covered, causing the algoritgm to reset to the top of the grid and move downwards until it reaches the first and highest available point, whereby it will commence hunting once again. 
The repetition of this process allows for the rapid generation of complex mazes, which are guaranteed to be different each time. 
A good resource to learn more about the Algorithm can be found [here](https://weblog.jamisbuck.org/2011/1/24/maze-generation-hunt-and-kill-algorithm). 
Once I have the Maze Generation up and running in its most simplistic form, the goal will be to make it look as aesthetically pleasing as possible both while the maze is being generated, and while it is being explored. But the frills will come later, the first step is to create a Maze Generator. Or a GenaMaze.. 

The end result of the project was for the most part satisfactory, and what I achieved in the end was very close to exactly matching what I had planned. I managed to implement the Hunt And Kill Algorithm in Unity to successfully work on a Default Grid of any size, meaning a maze of any (realistic) size can be generated without issue. The Largest I generated was a 50x50 maze which looked fantastic, however the Framrate got a little choppy at that size. I also managed to incorporate varying wall types to make the maze look less sparse and allow the player to keep track of where they are in the maze. Although I didn't build the FPSController used in the game myself, it functioned perfectly with my mazes once implemented, and I added a birds eye view of the maze to the bottom left corner of the players perspective.

What I would say I am most proud of with this Assignment was the implementation of the Algorithm in Unity, when the Version I used as a reference/guide was a two-dimensional Python Version. The concept of the Grid with individual maze cells was extremely tricky to conceptualize, and it took a lot of trial and error before the walls of the would-be maze were even generating properly, not to mention the concept of traversing the maze and knocking down walls to make paths. 

Unfortunately not everything was able to be implemented as I would have liked, but not for lack of trying. In particular, I am disappointed that I could not manage to get the CoRoutines in the HuntAndKillMaze class to effectively stop for a second with the destruction of each new wall, which would have shown the user the maze being generated step by step. After the Maze was being generated and the bulk of the project was done, I turned my focus to being entirely on this aspect of the program, and spent far more time than I care to admit trying and failing to implement Coroutines and Invoke() methods to get the maze to pause at each step of its generation. 
Eventually I had to move on to keep the project progressing, but this would be my primary focus if I was to continue development.

Other Ideas I would implement for continued work on this project include my idea for a moveable door. Four Seperate Cube GameObjects would make up the wall that the door is encased in, with a gap placed in the center. In the gap is the door
which is a Seperate GameObject and can stay fixed at one point and swivel on its axis (maybe use LERP for a smooth transition animation) to another point. The space in the Wall can now be entered because the Rigidbody for the Door has moved out of the way, allowing it to be moved through. This would be expensive to do for many doors, so you would have to limit the amount of doors with low probability of occurence, and in the case of the exit of the maze, you could make it so only one gets generated in the level and once found it contains a trigger that escapes the maze and ends the game.

Another idea came from my reading of the Unity Documentation on Random Seeds.

"You might set your own seed, for example, when you generate a game level procedurally. You can use randomly-chosen elements to make the scene look arbitrary and natural but set the seed to a preset value 
before generating. This will make sure that the same "random" pattern is produced each time the game is played. 
This can often be an effective way to reduce a game's storage requirements - you can generate as many levels as you like procedurally and store each one using nothing more than an integer seed value."
- From the Unity Random Seed Documentation

Could be used to Save levels and replay particularly hard mazes. This way you could play easy, medium and hard mazes, and keep them saved for players to try, in addition to the actual generation aspect where they make an entirely new maze.

What resources were useful for you (websites, tutorials, assets etc)

The three main resources I used to bring the project to fruition were as follows: 

[James Bucks Webpage "Buckblog"](http://weblog.jamisbuck.org/2011/1/24/maze-generation-hunt-and-kill-algorithm), which has a page centered on the implementation of the HuntAndKill algorithm to create mazes.

[This Tutorial from CatlikeCoding](https://catlikecoding.com/unity/tutorials/maze/) which focuses on the creation of mazes in unity, albeit using different mathods to the ones I eventually implemented. However this resource helped a lot with visualising the creation of the maze in unity, and helped me understand how GameObjects could be used properly. It also offered me some helpful design suggestions for making the maze look better and gave me the idea for a BirdsEye view of the maze being used as a map for the player.

[And lastly the Unity Documentation Page](https://docs.unity3d.com/530/Documentation/ScriptReference/Random-seed.html), because whenever I had a syntax or implementation question that needed to be answered, this reference was the bible and helped me at every step of the projects creation (Except for the Coroutines :( ).

[Video of the Project](https://www.youtube.com/watch?v=9CgCLJuqgMo&feature=youtu.be)

