---------------------Helpful information about the level builder-----------------------------
where to find text file: \bin\Windows\x86\Debug
general notes:
	*****MAKES USES OF HARD CODED VALUES BASED ON SCREEN HEIGHT BEING 480****
	-all numbers can be floats, but floor and stair commands will parse as ints
	-comments can be written by starting with "// " (the space needs to be there)
	-1 line per sprite/command
	-case in-sensitive
	-individual sprites follow the general pattern of TYPE X_POSITION Y_POSITION SUB-TYPE

TYPE item:
	-follows general pattern
	-sub-types: 
		super = mushroom
		1up = 1up mushroom
		fire = fire flower
		star = invincibility star
		small_pipe = warp pipe size 1
		medium_pipe = warp pipe size 2
		large_pipe = warp pipe size 3
		castle = end level castle
		flag_pole = end level flag pole
		flag = end level flag

TYPE enemy:
	-follows general pattern
	-spawn in their default state
	-sub-types are fairly self explanatory
	-sub-types:
		goomba
		greenKoopa
		redKoopa

TYPE block:
	-uses general pattern, but requires a 4th argument.  This allows items to be hidden
	inside the blocks.  Can be either 'null' for no item, or any of the item sub-types 
	can be used.  (N.B. this will try and fit things like a whole castle inside a 16x16 block)
	-sub-types are fairly self explanatory
	-sub-types:
		brick
		question
		floor
		stair
		hidden
		used

TYPE mario:
	-uses general pattern, but does not have any sub-types at this time

command notes:
	For ease of use/sanity, some key words will produce more than 1 sprite

	floor command:
	-follows pattern of floor START END
	-will produce two, stacked rows of floor blocks along the bottom of the screen with the left-most
		edge being at START and the right edge being at END
	-currently no error checking on start, end bounds
	-uses integer division to round down to nearest factor of 16
	-let END encompass the whole last block you wish to see
	 ex: to fill the floor on the default screen use 'floor 0 800' NOT 'floor 0 784'

	 stairs_up command:
	 -follows pattern stairs_up START HEIGHT REPEAT
	 -START = x value of the upper left corner of the first block in the stair (y value = 432)
	 -HEIGHT = desired final height of the staircase
		--value must be >= 1 or else nothing will draw
			ex: stairs_up 100 1 0 will be a single block at (100,432)
	-REPEAT = the number of times the final height will be repeated
	 -builds ascending staircase, always increasing 1 step at a time
	
	stairs_down command:
	-follows the same pattern as stairs_up