function love.load()
    player = {}
    player.w = 150
    player.h = 25
    player.x = love.graphics.getWidth()/2 - player.w/2
    player.y = love.graphics.getHeight() - player.h
    player.v = 7
    player.dx = 0
    player.lives = 3

    ball = {}
    ball.r = 15
    ball.x = love.graphics.getWidth()/2
    ball.y = love.graphics.getHeight()/2
    ball.v = 5
    ball.dx = ball.v / 4
    ball.dy = ball.v

    pad = {}
    pad.h = 25
    pad.y = 100
    pad.table = {
        {true, true, true, true, true, true, true, true},
        {true, true, true, true, true, true, true, true},
        {true, true, true, true, true, true, true, true}
    }
    pad.count = 0
    pad.fill = 0
    pad.empty = 0
end

function love.update(dt)
    -- player movement
    player.x = player.x + player.dx
    if love.keyboard.isDown('right') then
        player.dx = player.v
    elseif love.keyboard.isDown('left') then
        player.dx = -player.v
    else
        player.dx = 0
    end

    -- ball movement
    ball.x = ball.x + ball.dx
    ball.y = ball.y + ball.dy

    -- keep player stay on the board
    if player.x < 0 then
        player.x = 0
    elseif player.x + player.w > love.graphics.getWidth() then
        player.x = love.graphics.getWidth() - player.w
    end

    -- keep ball stay on the board
    if ball.x - ball.r < 0 then
        ball.dx = ball.v
    elseif ball.y - ball.r < 0 then
        ball.dy = ball.v
    elseif ball.x + ball.r > love.graphics.getWidth() then
        ball.dx = -ball.v
    elseif ball.y + ball.r > love.graphics.getHeight() then
        ball.dy = -ball.v

        -- if player miss the ball decrease player lives
        player.lives = player.lives - 1
    end

    -- ball collide with player
    if ball.x - ball.r >= player.x and ball.x + ball.r <= player.x + player.w and ball.y + ball.r >= player.y then
        ball.dy = -ball.v
    end

    for i = 1,  tablelength(pad.table) do
        pad.count = tablelength(pad.table) * tablelength(pad.table[i])
        local padwidth = love.graphics.getWidth() / tablelength(pad.table[i])

        for j = 1, tablelength(pad.table[i]) do
            local padx = padwidth * (j - 1)
            
            -- ball collide with pad
            if pad.table[i][j] and ball.x - ball.r > padx and ball.x + ball.r < padx + padwidth and ball.y - ball.r < pad.y + pad.h * i then
                pad.table[i][j] = false
                pad.empty = pad.empty + 1

                if ball.dy == ball.v then
                    ball.dy = -ball.v
                elseif ball.dy == -ball.v then
                    ball.dy = ball.v
                end
            end
        end
    end
    
    pad.fill = pad.count - pad.empty

    -- game over
    if player.lives == 0 then
        love.event.quit('restart')
    end
end

function love.draw()
    -- draw player
    love.graphics.rectangle("fill", player.x, player.y, player.w, player.h)
    
    -- draw ball
    love.graphics.circle("fill", ball.x, ball.y, ball.r)
    
    -- draw pad
    for i = 1,  tablelength(pad.table) do
        local padwidth = love.graphics.getWidth() / tablelength(pad.table[i])
        for j = 1, tablelength(pad.table[i]) do
            if pad.table[i][j] then
                love.graphics.rectangle("fill", padwidth * (j - 1), pad.y + pad.h * i, padwidth, pad.h)
            end
        end
    end
    
    -- print
    local playerprint = string.format('player.lives : %d\n\tplayer.x : %d\n', player.lives, player.x)
    local ballprint = string.format('\t\tball.x : %d \n\t\tball.y : %d\n', ball.x, ball.y)
    local padprint = string.format('\t  pad.fill : %d \npad.emtpy : %d', pad.fill, pad.empty)
    love.graphics.print(playerprint .. ballprint .. padprint, 10, 10)
end

function tablelength(T)
    local count = 0
    for _ in pairs(T) do count = count + 1 end
    return count
end