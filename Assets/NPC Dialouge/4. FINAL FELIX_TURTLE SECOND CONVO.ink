INCLUDE globals.ink
-> Confrontation
// Part 3: Confrontation and Choice
=== Confrontation ===
Turtle!
    "I... I did it. I got my power back."#speaker:Felix #portrait:felix # layout:left
    "Congratulations, Felix. But tell me, now that you hold such power once more, what do you intend to do with it?"#speaker:Turtle #portrait:turttle # layout:right #audio:animal_crossing_mid
    "I've tasted power before, Turtle. And I won't let anyone take it away from me again. I'll show them all who's in control."#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    "Control, Felix? But at what cost? Consider the lives you could save if you chose a different path."#speaker:Turtle #portrait:turttle # layout:right #audio:animal_crossing_mid
    Felix:#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    * ["I've endured too much to relinquish my power now, Turtle. It's time to make them all see my true strength."]
    "I've endured too much to relinquish my power now, Turtle. It's time to make them all see my true strength."#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    -> Bad1
    * ["I've learned from my past mistakes, Turtle. This time, I'll use my power to protect and uplift those around me."]
    "I've learned from my past mistakes, Turtle. This time, I'll use my power to protect and uplift those around me."#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    -> Good1
    
=== Bad1 ===
    <b><color=\#FF1E35>"Perhaps, Felix. But remember, power unchecked can lead to ruin. Choose wisely."</color></b>#speaker:Turtle #portrait:turttle # layout:right #audio:animal_crossing_mid
    -> FinalChoice
=== Good1 ===
    "Indeed, Felix. True strength lies not in dominance, but in the ability to uplift and protect others. Embrace this path, and you'll find fulfillment beyond measure."#speaker:Turtle #portrait:turttle # layout:right #audio:animal_crossing_mid
    -> FinalChoice

=== FinalChoice ===
"I've made my choice, Turtle. And I choose to ."#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    
    // Choice 1: Proceed with Felix's desire for dominance
    *[Proceed with Felix's desire for dominance]
    "I've made my choice, Turtle. And I choose to reign supreme."#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    "Very well, Felix. But remember, power unchecked can lead to ruin. Choose wisely."#speaker:Turtle #portrait:turttle # layout:right #audio:animal_crossing_mid
    "I've heard enough, Turtle. My mind is made up. It's time for Foxhaven to feel my wrath."#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
->END
    
    // Choice 2: Persuade Felix to use his power for good
    *[Persuade Felix to use his power for good]
    "I've tasted power before, Turtle. But maybe... maybe it's time for a different approach."#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    "Wise choice, Felix. Using your power for protection rather than domination is commendable."#speaker:Turtle #portrait:turttle # layout:right #audio:animal_crossing_mid
    "You're right, Turtle. It's time for me to make things right, to face Morbius and end this once and for all."#speaker:Felix #portrait:felix # layout:left #audio:celeste_high
    
->END