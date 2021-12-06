import pygame
import random
import os
pygame.init()

WIN_SIZE = WIN_WIDTH, WIN_HEIGHT = 800, 600
screen = pygame.display.set_mode(WIN_SIZE)
fps = pygame.time.Clock()
score = 0

flappy_w = 40
flappy_h = 40
flappy_x = (WIN_WIDTH / 2) - (flappy_w / 2)
flappy_y = 0
flappy_g = 5
flappy_v = flappy_g * 10
flappy_angle = 0
flap = False

pipe_w = 60
pipe_h = WIN_HEIGHT
pipe_v = 3
pipe_y = 0
pipe_x = [WIN_WIDTH - pipe_w]
pipe_hole_h = pipe_h / 4
pipe_hole_t = [random.randint(0, WIN_HEIGHT - pipe_hole_h)]
pipe_hole_b = [int(pipe_hole_t[0] + pipe_hole_h)]

run = True
while run:
    screen.fill((0,0,0))
    fps.tick(30)

    # pipe movement
    for i in range(len(pipe_x)):
        pipe_x[i] -= pipe_v
    
    # append pipe
    if pipe_x[len(pipe_x)-1] < WIN_WIDTH / 2:
        pipe_x.append(WIN_WIDTH)
        rand = random.randint(0, WIN_HEIGHT - pipe_hole_h)
        pipe_hole_t.append(rand)
        pipe_hole_b.append(int(rand + pipe_hole_h))

    keys = pygame.key.get_pressed()

    # app close and keyup event
    for event in pygame.event.get():
        if event.type == pygame.QUIT: 
            run = False
            status = 'QUIT'
        elif event.type == pygame.KEYUP:
            flap = True
        else:
            flap = False   
    
    # quit shortcut
    if keys[pygame.K_q] or keys[pygame.K_ESCAPE]:
        run = False

    # flap movement
    if flap and keys[pygame.K_SPACE]:
        flappy_y -= flappy_v
        flappy_angle = 90
    else:
        flappy_y += flappy_g
        flappy_angle = 0
    
    # hit the pipe
    for i in range(len(pipe_x)):
        if (
            flappy_x + flappy_w > pipe_x[i] and 
            flappy_x + flappy_w < pipe_x[i] + pipe_w
        ) and (
            flappy_y < pipe_hole_t[i] or 
            flappy_y > pipe_hole_b[i]
        ):
            run = False
    
    # draw pipe
    for i in range(len(pipe_x)):
        pygame.draw.rect(screen, (0,255,0), pygame.Rect(pipe_x[i], pipe_y, pipe_w, pipe_h))

        pygame.draw.rect(screen, (0,0,0), pygame.Rect(pipe_x[i], pipe_hole_t[i], pipe_w, pipe_hole_h))
        
    # draw flappy
    flappy_rect = pygame.Rect(flappy_x, flappy_y, flappy_w, flappy_h)
    pygame.draw.rect(screen, (255,255,0), flappy_rect)

    os.system('clear')
    print(f'   flappy_x : {flappy_x}')
    print(f'   flappy_y : {flappy_y}')
    print(f'     pipe_x : {pipe_x}')
    print(f'     pipe_y : {pipe_y}')
    print(f'pipe_hole_t : {pipe_hole_t}')
    print(f'pipe_hole_b : {pipe_hole_b}\n')


    pygame.display.update()