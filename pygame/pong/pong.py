import pygame
pygame.init()
pygame.font.init()

WIN_SIZE = WIN_WIDTH, WIN_HEIGHT = 800, 600
screen = pygame.display.set_mode(WIN_SIZE)
fps = pygame.time.Clock()
myfont = pygame.font.SysFont('Arial', 64)
score = [0, 0]

class Player(object):
    def __init__(self, color, x, y, w, h, vel):
        self.color = color
        self.vel = vel;
        self.rect = pygame.Rect(x, y, w, h)
    
    def draw(self, screen):
        pygame.draw.rect(screen, self.color, self.rect)

    def move(self, keys, player='default'):
        # Player 1 key command
        if player == 'default' or player == 'player1':
            if keys[pygame.K_w] and self.rect.top - self.vel >= 0:
                self.rect.y -= self.vel

            if keys[pygame.K_s] and self.rect.bottom - self.vel <= WIN_HEIGHT:
                self.rect.y += self.vel

        # Player 2 key command
        elif player == 'player2':
            if keys[pygame.K_UP] and self.rect.top - self.vel >= 0:
                self.rect.y -= self.vel

            if keys[pygame.K_DOWN] and self.rect.bottom - self.vel <= WIN_HEIGHT:
                self.rect.y += self.vel
        
        # Unlisted player
        else:
            print('Player move unlisted!')
            pygame.quit()

class Ball(object):
    def __init__(self, color, x, y, R, w, vel):
        self.color = color
        self.vel_x = vel
        self.vel_y = vel
        self.x = x;
        self.y = y;
        self.R = R;
        self.w = w;
    
    def draw(self, screen):
        pygame.draw.circle(screen, self.color, (self.x, self.y), self.R, self.w)
    
    def move(self, score, player1, player2):
        self.x += self.vel_x
        self.y += self.vel_y

        # Player 1 collision
        if self.x - (self.R/2) <= player1.rect.right and self.y >= player1.rect.top and self.y <= player1.rect.bottom:
            self.vel_x = -self.vel_x

        # Player 2 collision
        if self.x + (self.R/2)>= player2.rect.left and self.y >= player2.rect.top and self.y <= player2.rect.bottom:
            self.vel_x = -self.vel_x

        # Board collision & scoring
        if self.x - (self.R/2) <= 0:
            self.vel_x = -self.vel_x
            score[1] += 1
        if self.x + (self.R/2) >= WIN_WIDTH:
            self.vel_x = -self.vel_x
            score[0] += 1
        if self.y - (self.R/2) <= 0 or self.y + (self.R/2) >= WIN_HEIGHT:
            self.vel_y = -self.vel_y


player1 = Player(
    color=(255,255,255),
    x=25,
    y=(WIN_HEIGHT-100)/2,
    w=10,
    h=100,
    vel=25
)

player2 = Player(
    color=(255,255,255),
    x=WIN_WIDTH - 35,
    y=(WIN_HEIGHT-100)/2,
    w=10,
    h=100,
    vel=25
)

ball = Ball(
    color=(255,255,255),
    x=(WIN_WIDTH-10)/2,
    y=(WIN_HEIGHT-10)/2,
    R=20,
    w=0,
    vel=12.5
)

run = True
while run:
    fps.tick(30)
    keys = pygame.key.get_pressed()

    for event in pygame.event.get():
        if event.type == pygame.QUIT: run = False


    screen.fill((0,0,0))

    player1.move(keys)
    player1.draw(screen)
    player2.move(keys, player='player2')
    player2.draw(screen)
    ball.draw(screen)
    ball.move(score, player1, player2)

    # Score text
    textsurface = myfont.render(f'{score[0]} : {score[1]}', False, (255, 255, 255))
    screen.blit(textsurface,((WIN_WIDTH/2) - (textsurface.get_rect().width/2),0))

    # Game rules
    if score[0] >= 10:
        print('Player 1 WIN!')
        break
    if score[1] >= 10:
        print('Player 2 WIN!')
        break

    pygame.display.update()

pygame.quit()