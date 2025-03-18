## ~~Lerp left and right movement~~
- ~~make ring hitboxes really forgiving, but movement logic is still instant~~
## calculate map ring size
- every current and future map part should still all be some standard unit (was 1)
- *I think ramps and hills should still work with the old placement system*
## Make long map again!
- much longer than old map
- rethink sizes and speeds, keep it difficult but possible on high speeds
- individual cubes (ground tiles) should be much larger, to make ramps more of a chunky and visible target
## add Hill, height variation (dumb)
- hill object that "sets" new ground height when on that lane
- height is calculated by each hill monobehavior on start()
- vertical hitbox, also with horizontal part (to hit all lanes) that sends its height and lane to player so it knows where "ground" should be on what lane (set on enter, clear on exit)
- ie player snaps to that height
## add jump
- can be air controlled completely
- uses ground height given by hills to know when to stop
	- if below target ground height, cannot move that direction
	- let them move there if they're jumping upward though
		- maybe add check to make sure the height of the jump will make it to the target height
## Add ramps (dumb)
- includes quantized speed system
- same hitbox system as hills, including height
	- now includes length (also calculated)
- up ramp that sets speed to -1 on any contact (red)
- down ramp that sets speed to +1 on any contact (green)
- ignore lerping up and down for now, just snap height to like half Height lol
## Add boost pad
- speed +1, but only up to say 3
## Add timer + round system
- includes game hud for current time and lap number
	- "Lap 1/3   01:02.017"
- gamestart countdown in later milestone, timer just starts counting immediately for now
- lap counter based on rotation of map ring (with checkpoint at halfway for funsies)
	- this includes new "% complete" float for map ring, effectively "z distance" for player
- end screen is just victory screen for now
	- shows final time too
## Make Ramps skippable
- ie you can jump over them to not get their effect
- check if player height is at (or below) height of current ground object (hill or ramp)
	- tiny bit of extra room above height, just to make sure (dial it in to feel ok)

=== ==^MVP??^==  ===
## Add Ring collection
- includes ring energy hud (remove old hud, ring # and health don't matter anymore)
- remove health system, but turn rings into float
- collecting a ring increases energy value, up to certain max
- ring energy decays linearly
## Calculate deceleration
- ring power prevents it, just inside the slowdown function, check if ring power is on I guess
- being on the ground brings speed down over time, touching even once starts inevitable deceleration to next value
	- being in the air when speed power runs out maintains current exact speed
	- and landing starts deceleration UNLESS you bunny hop (only once allowed though, next ground touch is guaranteed)
- In other words, jumping once cancels deceleration, then jumping again maintains the speed (single bunnyhop), then touching the ground always resumes deceleration. 
	- first jump can be overwritten as a speed gain/loss jump, and none of this applies if energy is nonzero. Still track it, but don't apply it if there's energy.
## Add tricks
- jumping off down ramp immediately adds 1 speed
- landing on down ramp immediately adds 1 speed
- landing on up ramp immediately removes 1 speed...
## Add ramp vertical lerping
- ramps calculate start and end points (map ring rotation values, player distance) based on ramp's calculated length
- all ramps are linear, so calculate Current ground height based on single lerp for every "onContinueInside" or whatever that trigger function event thing is called. I guess this is on player monobehavior, calling the ramp's function
## Uhhh camera shit
- consider "static" ground height, current player height, maybe max player height if calculated
- do I use cinemachine... idk....
## Add ramp horizontal lerping??
- while grounded, speed goes from one value at the start to the end value at the end
## reconsider ramp size being 1 unit...
- it would be too small to easily use if it's only 1 unit long... maybe divide it up somehow to make it longer?
- damn ring gimmick...