STEPS TO ADD A SPRITEFONT TO YOUR GAME:
---------------------------------------

NOTES:
- You will need a new spritefont for each different size and font type of text in your game (but not colour changes)
- Font files end with the extention .ttf or .otf
- When downloading fonts, it will either come as one of these files or a .zip file you will need to extract first
- You can either find fonts on your computer itself (Google where) or downlad them from a trusted source like dafont.com

STEP 1: When your font file is located, copy IT and ONLY IT into this folder, not a .zip file or its extracted folder

STEP 2: Make a copy of the Template.spritefont file and rename it
NAMING: Use MixedCase naming and name it according to its usage, e.g. MenuFont.spritefont or LargeText.spritesont

STEP 3: Open the renamed file either in a text editor (e.g. Notepad) or right in VS Code

STEP 4: Make necessary option choices by changing the following properties in the file and clicking SAVE after

NOTE: Find the desired property opening and closing tags, e.g. <FontName> and </FontName> and change the text in between
- FontName:	Default is Arial	<-- Most challenging to get correct, must match the true name of the font
- Size:		Default is 12		<-- This is roughly how tall the text is, in pixels
- Spacing:	Default is 0		<-- The amount of pixels between each letter
- Style: This property is broken, normally you would use the words Regular, Default or Italic.
       Instead, include these words after a space following the FontName text (if the font supports it), e.g. <FontName>Arial Bold</FontName>

IMPORTANT: Chances are, you will not get the options correct the first time.  
           When you need to tweak them, follow steps 3 and 4 again and re-run the Builder program to recreate the spritefont binary file