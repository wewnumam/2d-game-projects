function love.load()
    love.window.setMode(600, 600, {resizable = false})
    board = {}
    board.image = love.graphics.newImage('board.png')
    board.mark = '*'
    board.table = {
        {board.mark, board.mark, board.mark},
        {board.mark, board.mark, board.mark},
        {board.mark, board.mark, board.mark}
    }
 
    me = {}
    me.mark = 'X'
    me.image = love.graphics.newImage('me.png')
    me.move = true

    comp = {}
    comp.mark = 'O'
    comp.image = love.graphics.newImage('comp.png')
    comp.move = false
        
    mouse = {}
    mouse.x = 0
    mouse.y = 0

    status = 'GAME'
end

function love.update(dt)
    -- update mouse position
    mouse.x, mouse.y = love.mouse.getPosition()

    -- comp input
    local compx, compy = compinput()

    -- prevent comp overrides unfillable cell
    if comp.move then
        if board.table[compy][compx] == board.mark then
            board.table[compy][compx] = comp.mark
            comp.move = false
            me.move = true
        else
            comp.move = true
        end
    end

    -- winner check
    if winnercheck(me.mark) then
        status = 'HUMAN WIN'
        love.event.quit()
    end

    if winnercheck(comp.mark) then
        status = 'COMPUTER WIN'
        love.event.quit()
    end

    -- draw check
    local empty = 0
    for i = 1, tablelength(board.table) do
        for j = 1, tablelength(board.table[i]) do
            if board.table[j][i] == board.mark then
                empty = empty + 1
            end
        end
    end
    if empty == 0 then
        status = 'DRAW'
        love.event.quit()
    end
end

function love.draw()
    -- draw board
    love.graphics.setColor(1,1,1)
    love.graphics.draw(board.image, 1, 1)
    
    -- draw player
    for i = 1, tablelength(board.table) do
        for j = 1, tablelength(board.table[i]) do
            -- draw me
            if board.table[i][j] == me.mark then
                love.graphics.draw(me.image, (j - 1) * 200, (i - 1) * 200)
            end

            -- draw comp
            if board.table[i][j] == comp.mark then
                love.graphics.draw(comp.image, (j - 1) * 200, (i - 1) * 200)
            end
        end
    end

    -- console log
    if not os.execute("clear") then os.execute("cls") end
    
    print('    me : '.. tostring(me.move))
    print('  comp : '.. tostring(comp.move))
    print('status : '.. status)

    for i = 1, tablelength(board.table) do
        for j = 1, tablelength(board.table[i]) do
            io.write(string.format("%s ", board.table[i][j] ))
        end
        print()
    end
    print()

    love.graphics.setColor(1,0,0)
    love.graphics.print(string.format('X: %s\nY: %s', mouse.x, mouse.y))
end

function love.mousepressed(x, y, button, istouch, presses)
    if button == 1 then
        -- me input
        local mex, mey = meinput(x, y)

        -- prevent me overrides unfillable cell
        if me.move then
            if board.table[mey][mex] == board.mark then
                board.table[mey][mex] = me.mark
                me.move = false
                comp.move = true
            else
                me.move = true
            end
        end
    end
end

function winnercheck(playermark)
    for i = 1, tablelength(board.table) do
        -- checking horizontal line
        if board.table[i][1] == playermark and board.table[i][2] == playermark and board.table[i][3] == playermark then
            return true
        end

        -- checking vertical line
        if board.table[1][i] == playermark and board.table[2][i] == playermark and board.table[3][i] == playermark then
            return true
        end

        -- checking diagonal line
        if (board.table[1][1] == playermark and board.table[2][2] == playermark and board.table[3][3] == playermark) or (board.table[1][3] == playermark and board.table[2][2] == playermark and board.table[3][1] == playermark) then
            return true
        end
    end

    return false
end

function meinput(x, y)
    -- will return x, y
    if x < 200 and y < 200 then        
        return 1, 1
    elseif x > 200 and x < 400 and y < 200 then
        return 2, 1
    elseif x > 400 and y < 200 then
        return 3, 1
    elseif x < 200 and y > 200 and y < 400 then
        return 1, 2
    elseif x > 200 and x < 400 and y > 200 and y < 400 then
        return 2, 2
    elseif x > 400 and y > 200 and y < 400 then
        return 3, 2
    elseif x < 200 and y > 400 then
        return 1, 3
    elseif x > 200 and x < 400 and y > 400 then
        return 2, 3
    elseif x > 400 and y > 400 then
        return 3, 3
    end
end

function compinput()
    x = math.random(1, 3)
    y = math.random(1, 3)
    return x, y
end

function tablelength(T)
    local count = 0
    for _ in pairs(T) do count = count + 1 end
    return count
end