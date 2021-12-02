import pygame
import random
import os
pygame.init()

WIN_SIZE = WIN_WIDTH, WIN_HEIGHT = 800, 600
screen = pygame.display.set_mode(WIN_SIZE)
fps = pygame.time.Clock()
score = 0
status = 'RUN'

snake_x = 0
snake_y = 0
snake_w = 20
snake_h = 20
snake_vel = 5
snake_x_move = 0
snake_y_move = 0
tail = 0
tail_x = []
tail_y = []

apple_x = 0
apple_y = 0
apple_w = 20
apple_h = 20

x = []
y = []
i = 0
while i < WIN_WIDTH:
    x.append(i)
    i += snake_w
i = 0
while i < WIN_HEIGHT:
    y.append(i)
    i += snake_h

run = True
while run:
    screen.fill((0,0,0))
    fps.tick(snake_vel)

    # app close
    for event in pygame.event.get():
        if event.type == pygame.QUIT: 
            run = False
            status = 'QUIT'
        
    keys = pygame.key.get_pressed()
    
    # quit shortcut
    if keys[pygame.K_q] or keys[pygame.K_ESCAPE]:
        run = False
        status = 'QUIT'

    # snake movement
    if keys[pygame.K_RIGHT]:
        if snake_x_move == 0:
            snake_x_move = snake_w
        snake_y_move = 0
    elif keys[pygame.K_LEFT]:
        if snake_x_move == 0:
            snake_x_move = -snake_w
        snake_y_move = 0
    elif keys[pygame.K_DOWN]:
        snake_x_move = 0
        if snake_y_move == 0:
            snake_y_move = snake_h
    elif keys[pygame.K_UP]:
        snake_x_move = 0
        if snake_y_move == 0:
            snake_y_move = -snake_h
    else:
        pass
    
    snake_x += snake_x_move
    snake_y += snake_y_move

    # stay on the board
    if snake_x < 0:
        snake_x = WIN_WIDTH
    if snake_x > WIN_WIDTH:
        snake_x = 0
    if snake_y < 0:
        snake_y = WIN_HEIGHT
    if snake_y > WIN_HEIGHT:
        snake_y = 0

    # snake eats apple
    if snake_x == apple_x and snake_y == apple_y:
        apple_x = random.choice(x)
        apple_y = random.choice(y)
        score += 1

    # tail
    tail_x.insert(0, snake_x)
    tail_y.insert(0, snake_y)
    if len(tail_x) > score:
        tail_x.pop()
        tail_y.pop()
    
    # snake bites his tail
    for t in range(score):
        if t > 2 and snake_x == tail_x[t] and snake_y == tail_y[t]:
            run = False
            status = 'BITES'

    # draw snake
    pygame.draw.rect(screen, (255,255,255), pygame.Rect(snake_x, snake_y, snake_w, snake_h))
    for t in range(score):
        # head & tail color
        snake_color = (0, 0, 255)
        if t == 0: snake_color = (0,255,0)
        pygame.draw.rect(screen, snake_color, pygame.Rect(tail_x[t], tail_y[t], snake_w, snake_h))

    # draw apple
    pygame.draw.rect(screen, (255,0,0), pygame.Rect(apple_x, apple_y, apple_w, apple_h))

    # draw score
    font = pygame.font.Font(pygame.font.get_default_font(), 24)
    text_surface = font.render(str(score), True, (255,255,255))
    screen.blit(text_surface,((WIN_WIDTH/2) - (text_surface.get_rect().width/2),0))

    pygame.display.update()

    if run: os.system('clear')
    print(f'snakex : {snake_x}\t snakey : {snake_y}')
    print(f' movex : {snake_x_move}\t  movey : {snake_y_move}')
    print(f'applex : {apple_x}\t appley : {apple_y}')
    print(f' score : {score}\t status : {status}')
    print(f' tailx : {tail_x}')
    print(f' taily : {tail_y}\n')
    