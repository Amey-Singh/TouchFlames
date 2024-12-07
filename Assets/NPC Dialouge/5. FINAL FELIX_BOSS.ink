INCLUDE globals.ink
// Part 2: Prison Escape

-> Dialogue1
=== Dialogue1 ===
!#speaker:Morbius #portrait:morbius # layout:right #audio:animal_crossing_high
    "Felix, how did you manage to escape from prison?"
    Felix: #speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    * ["I'm done playing your games, Morbius. Foxhaven will be free from your tyranny."]
    determined "I'm done playing your games, Morbius. Foxhaven will be free from your tyranny."#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    -> Dialogue2
    * ["You may have taken my power, but you won't take my spirit."]
    defiant "You may have taken my power, but you won't take my spirit."#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    -> Dialogue2

===Dialogue2===
    "You were once so sure of your power, Felix. What changed?"#speaker:Morbius #portrait:morbius # layout:right #audio:animal_crossing_high
    Felix:#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    * ["True strength comes from using power for good, not domination."]
    solemn "True strength comes from using power for good, not domination."#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    -> Dialogue3
    * ["I've grown tired of being the villain in my own story."]
    determined "I've grown tired of being the villain in my own story."#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    -> Dialogue3
    
===Dialogue3===
    "And you think you can defeat me, Felix? You're just a mere mortal now."#speaker:Morbius #portrait:morbius # layout:right #audio:animal_crossing_high
    Felix:#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    * ["I may be mortal, but I have the power of redemption. And with it, I'll triumph over your darkness."]
    with conviction "I may be mortal, but I have the power of redemption. And with it, I'll triumph over your darkness."
    -> Dialogue4
    * ["Maybe I'm just a mortal, but I have the will to fight for what's right. And with my power restored, justice will prevail."]
    with determination "Maybe I'm just a mortal, but I have the will to fight for what's right. And with my power restored, justice will prevail."
    -> Dialogue4
    
===Dialogue4===
    "Is that so, Felix? What power do you speak of?"#speaker:Morbius #portrait:morbius # layout:right #audio:animal_crossing_high
    Felix:#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    * ["The power you failed to take into account, Morbius. My flames, once extinguished, now burn brighter than ever."]
    smirking "The power you failed to take into account, Morbius. My flames, once extinguished, now burn brighter than ever."
    -> Dialogue5
    * ["My flames have returned, stronger than before. And they're here to bring your darkness to an end."]
    grinning "My flames have returned, stronger than before. And they're here to bring your darkness to an end."
    -> Dialogue5
    
===Dialogue5===
    "Impossible! How did you..."#speaker:Morbius #portrait:morbius # layout:right #audio:animal_crossing_high
    #speaker:Felix #portrait:felix # layout:left #audio:celeste_high
   * ["My power lies not just in flames, Morbius, but in the choice to wield them for good."]
   confidently "My power lies not just in flames, Morbius, but in the choice to wield them for good."
   -> Dialogue6
   * ["You forgot one thing - the power of redemption. And now, it's your turn to feel its heat."]
   defiantly "You forgot one thing - the power of redemption. And now, it's your turn to feel its heat."
   -> Dialogue6
  
===Dialogue6===
    <b><color=\#FF1E35>Let's end this for once and all....</color></b>#speaker:Morbius #portrait:morbius # layout:right #audio:animal_crossing_high
-> END
   

