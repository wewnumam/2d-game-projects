import os
import random

# winner rule
def check(board, player):
    for i in range(3):
        # checking horizontal line
        if board[i][0] == player and board[i][1] == player and board[i][2] == player:
            return True

        # checking vertical line
        if board[0][i] == player and board[1][i] == player and board[2][i] == player:
            return True

    # checking diagonal line
    if (board[0][0] == player and board[1][1] == player and board[2][2] == player) or (board[0][2] == player and board[1][1] == player and board[2][0] == player):
        return True

def player_input():
    y = int(input('input y (0/1/2): '))
    x = int(input('input x (0/1/2): '))
    return y, x

def comp_generate():
    y = random.randint(0, 2)
    x = random.randint(0, 2)
    return y, x

empty = '*'
me = 'X'
comp = 'O'

board = [
    [empty,empty,empty],
    [empty,empty,empty],
    [empty,empty,empty]
]

while (True):
    os.system('cls' if os.name == 'nt' else 'clear')
    
    # draw board
    i = 0
    print('')
    print('     | x0 | x1 | x2 |')
    print('-----+----+----+----+')
    for y in board:
        print(f'  y{i} |', end='')
        for x in y:
            print(f' {x}  ', end='|')
        i+=1
        print('\n-----+----+----+----+')

    if check(board, comp):
        print('YOU LOSE!')
        break
    
    if check(board, me):
        print('YOU WON!')
        break

    me_y, me_x = player_input()
    comp_y, comp_x = comp_generate()

    # prevent me overrides unfillable cell
    while(True):
        if board[me_y][me_x] == empty and board[me_y][me_x] != comp and board[me_y][me_x] != me:
            board[me_y][me_x] = me
            break
        me_y, me_x = player_input()

    # prevent comp overrides unfillable cell
    while(True):
        if board[comp_y][comp_x] == empty and board[comp_y][comp_x] != me and board[comp_y][comp_x] != comp:
            board[comp_y][comp_x] = comp
            break
        comp_y, comp_x = comp_generate()


    

