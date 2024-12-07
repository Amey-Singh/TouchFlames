# The script of the game goes in this file.

# Declare characters used by this game. The color argument colorizes the
# name of the character.

define s = Character("Siyokoy")


# The game starts here.

label start:
    ###################################
    ## Do not remove this portion
    show border onlayer UI 
    ###################################

    # Show a background. This uses a placeholder by default, but you can
    # add a file (named either "bg room.png" or "bg room.jpg") to the
    # images directory to show it.

    scene bg room

    # These display lines of dialogue.

    s "So you want to use this GUI?"

    # This shows a character sprite. A placeholder is used, but you can
    # replace it by adding a file named "eileen happy.png" to the images
    # directory.

    show sprite1 with dissolve

    s "Good for you! Because I've provided it for free."

    show sprite1 at left with moveinright

    s "I don't mind if you use it for commercial or non-commercial projects as long as you credit me in the game itself."

    s "However, do NOT resell any portion of this GUI as your own."

    s "Anyway, if you {i}are{/i} going to use it in  a commercial project, please consider tipping my kofi."

    show sprite1 at center with moveinleft

    $ cinematic = True

    s "I'm literally just a college student with no income."

    s "Right now we've entered the UI's cinematic mode."

    s "If you want to use this, add \"$ cinematic = True\" to your script turn it on."

    s "And if you don't want to use it anymore..."

    $ cinematic = False

    s "Just change True to False."

    show sprite1 at left with moveinright

    menu:
        s "This is what menu choices look like if you add text."
        
        "test choice 1":
            pass
        "test choice 2":
            pass
        "test choice 3":
            pass
        "test choice 4":
            pass
        "test choice 5":
            pass

    s "And this is what they look like without it."
    
    menu:
        "test choice 1":
            pass
        "test choice 2":
            pass
        "test choice 3":
            pass
        "test choice 4":
            pass
        "test choice 5":
            pass

    
    s "That's it for this portion."

    s "Try screenshotting and opening up the game menu by clicking the sun below."

    s "Ciao!"

    # This ends the game.

    return
