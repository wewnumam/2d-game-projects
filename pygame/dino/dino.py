import pygame
import time
import os
pygame.init()

WIN_SIZE = WIN_WIDTH, WIN_HEIGHT = 800, 600
screen = pygame.display.set_mode(WIN_SIZE)
fps = pygame.time.Clock()
score = 0

ground_W = WIN_WIDTH
ground_h = 20
ground_x = 0
ground_y = WIN_HEIGHT - ground_h

dino_w = 40
dino_h = 40
dino_x = (WIN_WIDTH / 2) - (dino_w / 2)
dino_y = ground_y
dino_v = 8
dino_m = 1
dino_jump = False
dino_delay = 0

cactus_w = 20
cactus_h = 75
cactus_v = 10
cactus_y = WIN_HEIGHT - cactus_h
cactus_x = [WIN_WIDTH - cactus_w]


run = True
while run:
    screen.fill((0,0,0))
    fps.tick(30)

    # cactus movement
    for i in range(len(cactus_x)):
        cactus_x[i] -= cactus_v
    
    # append cactus
    if cactus_x[len(cactus_x)-1] < WIN_WIDTH / 2:
        cactus_x.append(WIN_WIDTH)

    keys = pygame.key.get_pressed()

    # app close and keyup event
    for event in pygame.event.get():
        if event.type == pygame.QUIT: 
            run = False
            status = 'QUIT'
    
    # quit shortcut
    if keys[pygame.K_q] or keys[pygame.K_ESCAPE]:
        run = False

    # dino movement
    if keys[pygame.K_SPACE]:
        dino_jump = True

    if dino_jump:
        # Calculate force (F). F = 0.5 * mass * velocity^2.
        if dino_v > 0:
            F = ( 0.5 * dino_m * (dino_v*dino_v) )
        else:
            F = -( 0.5 * dino_m * (dino_v*dino_v) )

        # Change position
        dino_y = dino_y - F

        # Change velocity
        dino_v = dino_v - 1

        # If ground is reached, reset variables.
        if dino_y >= ground_y:
            dino_y = ground_y
            dino_jump = False
            dino_v = 8 
        

    # hit the ground
    if dino_y + dino_h > ground_y:
        dino_y = ground_y - dino_h

    # hit the cactus
    if (dino_x + dino_w >= cactus_x[len(cactus_x)-1] and 
        dino_x <= cactus_x[len(cactus_x)-1] + cactus_w and 
        dino_y >= cactus_y):
        run = False
    
    # draw cactus
    for i in range(len(cactus_x)):
        pygame.draw.rect(screen, (0,255,0), pygame.Rect(cactus_x[i], cactus_y, cactus_w, cactus_h))
        
    # draw dino
    dino_rect = pygame.Rect(dino_x, dino_y, dino_w, dino_h)
    pygame.draw.rect(screen, (255,255,0), dino_rect)

    # draw ground
    pygame.draw.rect(screen, (255, 255, 255), pygame.Rect(ground_x, ground_y, ground_W, ground_h))

    os.system('clear')
    print(f'  dino_x : {dino_x}')
    print(f'  dino_y : {dino_y}')
    print(f'cactus_x : {cactus_x[len(cactus_x)-1]}')
    print(f'cactus_y : {cactus_y}')


    pygame.display.update()