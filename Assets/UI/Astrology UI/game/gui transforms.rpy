## Do not remove this. This has all of the animations used in the UI.

transform quickMenu_hover:
    
    on hover:
        ease 0.075 yoffset -10
    on idle:
        ease 0.075 yoffset 0

transform choice_enter:

    on show:
        alpha 0.0 yoffset 200
        ease 1.0 alpha 1.0 yoffset 0

    on hide:
        ease 1.0 alpha 0.0 yoffset -200

transform choice_hover:

    on hover:
        ease 0.25 zoom 1.05 xalign 0.5

    on idle:
        ease 0.25 zoom 1.0 xalign 0.5

transform menu_enter:

    on show:
        yoffset -1000
        ease 1.0 yoffset 0

    on hide:
        yoffset 0
        ease 1.0 yoffset -1000

transform return_hover:

    on hover:
        ease 0.25 zoom 1.01 xalign 0.5 alpha 1.0

    on idle:
        ease 0.25 zoom 1.0 xalign 0.5 

transform clockwise:

    rotate 0 xanchor 0.5 yanchor 0.5
    linear 30 rotate 360 xanchor 0.5 yanchor 0.5
    repeat

transform counterClockwise:

    rotate 0 xanchor 0.5 yanchor 0.5
    linear 30 rotate -360 xanchor 0.5 yanchor 0.5
    repeat

transform comet1:
    rotate -50 xanchor 0.5 yanchor 0.5 alpha 0.7
    linear 5 rotate -200 xanchor 0.5 yanchor 0.5 alpha 1.0
    repeat
transform comet2:
    rotate 0 xanchor 0.5 yanchor 0.5
    linear 10 rotate -360 xanchor 0.5 yanchor 0.5
    repeat

transform starlight1:
    ease 0.5 alpha 0.0
    ease 1.5 alpha 1.0
    repeat

transform starlight2:
    ease 2.0 alpha 0.0
    ease 0.5 alpha 1.0
    repeat

transform starlight3:
    ease 1.5 alpha 0.0
    ease 1.0 alpha 1.0
    ease 1.5 alpha 0.0
    repeat

transform rayTurn:

    parallel:
        rotate 0 xanchor 0.5 yanchor 0.5
        linear 30 rotate -180 xanchor 0.5 yanchor 0.5
        repeat
    parallel:
        ease 3.0 alpha 0.0
        ease 3.0 alpha 0.5
        repeat

transform confirmRight:
    on show:
        xalign 0.5
        ease 0.75 xalign 0.75
    on hide:
        xalign 0.75
        ease 0.75 xalign 0.5 alpha 0.0

transform confirmLeft:
    on show:
        xalign 0.5
        ease 0.75 xalign 0.25
    on hide:
        xalign 0.25
        ease 0.75 xalign 0.5 alpha 0.0

transform confirmDissolve:
    on show:
        alpha 0.0
        ease 1.0 alpha 1.0
    on hide:
        alpha 1.0
        ease 1.0 alpha 0.0
        