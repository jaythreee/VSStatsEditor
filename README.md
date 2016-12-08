# VSStatsEditor
VS Stats Editor v1.1 by j3
Last updated December 10, 2016
=========================

INTRODUCTION
------------

This program is a simple enemy stat editor for Vagrant Story.  It decompiles the game's ZONE files using FASM, then uses Valendian's VSTools program to make sense of most of the game's data.  VS Stats Editor just parses these outputs files and allows the user to easily view and edit basic enemy stats.  Once the stats have been edited and saved to files they are recompiled into ZONE files which can be put back into the rom using CDMage.

ABOUT
-----

I've been into hard hacks for my favorite games the past few years.  I wanted to play Vagrant Story recently, but couldn't find mods of any kind, so I figured I'd make a simple one by just beefing up enemy stats.  Valendian's tools allowed me to do this, but I wanted an easier way to edit all enemies in the game simultaneously.  So I made this GUI which includes a way to just apply a stat multiplier to all enemies.  Unfortunately I got busy right after making this and never ended up getting to really play through the game again.  

The VS modding community is almost non-existent, which makes me sad.  I feel like there's a good amount that could be done with this game.  I always felt the original was too easy.  You didn't REALLY have to understand the complex weapon/armor system to beat the game.  The link at the bottom of this is a great resource for information about VS for modding purposes.  To sum up what I've found, many aspects of the game have been reverse engineered.  Valendian even implemented a new scene.  However, there are still several things unknown and not many people actively working on it.  My hope is that this will help someone out and make it easier to get something going.  I highly encourage anyone to add to this program :).

CURRENT FUNCTIONALITY
---------------------

Using Valendian's VSTools and FASM, VS Stats Editor provides an interface to:
-Decompile ZND files
-Edit basic enemy stats, equipment, and drop rates
-Multiply stats of all enemies by a given amount
-Save and recompile these changes into ZND files

The following enemy stats can be viewed and edited using this program:
-HP
-MP
-Running speed
-Strength
-Intelligence
-Agility
-Equipment and drops rates

NOTE: THE ABOVE IS ABOUT VS STATS EDITOR, NOT VALENDIAN'S VSTOOLS.  VSTOOLS IS CAPABLE OF EDITING ALL OF THE ABOVE AND MUCH MORE.

EXAMPLE USE
-----------

MAKE SURE TO MAINTAIN THE DIRECTORY STRUCTURE FROM THE ZIP

1) When opening VS Stats Editor, the zones located in the Zones directory will be loaded.  Select a zone to edit.

2) When a zone is selected, all enemies contained in the zone will load into the enemy selection box.  Select an enemy to edit.

3) When an enemy is selected, the stats, equipment, and drop rates are loaded.  Edit desired stats, equipment, and drop rates.

4) Click save each time an enemy is edited.

5) When finished editing enemies in multiple zones, click Recompile ZNDs.  Now the enemy edits that were made in the .ASM files are in the appropriate ZND files.

6) Open your Vagrant Story image using CDMage. 
    a. Select M2 Track
    b. Now each zone edited must be imported to the image.  Unfortunately this must be done one zone at a time by right clicking on the zone, select import, then select your edited zone in the VS Stats Editor Zones directory.

CREDITS, CONTACT, ETC
------------------
Valendian:
	Without VSTools this wouldn't be possible.  He did the hard work and responded to several questions I had when making this program.  If you have any in depth questions, he would be the best person to ask :).

I currently do not have future work planned for this just due to lack of time.  It would be nice to add the ability to easily edit the enemy's equipment though.  It would also be useful to automate importing the ZND files back into the rom.  I tried several program, but due to the encoding of the rom only CDMage actually works.  CDMage does not have a way to import multiple files at once.  This could be done writing a custom program to read/edit the VS image or by automating CDMage with a script.  

I encourage anyone interested to check out the github and add to it!  I'm pretty busy these days, but feel free to contact me  with any questions :).

Contact: jthreee3@gmail.com
Github: https://github.com/jaythreee/VSStatsEditor
Valendian's VSTools: http://ffhacktics.com/smf/index.php?topic=8494.0
VS modding info: http://datacrystal.romhacking.net/wiki/Category:Vagrant_Story

Happy VS'ing :)
j3
