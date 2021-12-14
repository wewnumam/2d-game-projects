function love.load()
    love.window.setMode(400, 600, {resizable = false})

    player = {}
    player.w = 25
    player.h = 40
    player.x = love.graphics.getWidth()/2 - player.w/2
    player.y = love.graphics.getHeight() - player.h
    player.v = 5
    player.dx = 0

    bullet = {}
    bullet.r = 10
    bullet.x = {love.graphics.getWidth()/2}
    bullet.y = {love.graphics.getHeight() - bullet.r}
    bullet.v = 5
    bullet.dy = {0}
    bullet.aim = {true}

    enemy = {}
    enemy.h = 20
    enemy.y = 100
    enemy.table = {
        {true, true, true, true},
        {true, true, true, true},
        {true, true, true, true}
    }
    enemy.dx = 0
    enemy.v = 1
    enemy.count = 0
    enemy.fill = 0
    enemy.empty = 0
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

    -- bullet movement
    for i = 1, tablelength(bullet.x) do
        if bullet.aim[i] then
            bullet.x[i] = player.x + player.w/2
        else
            bullet.x[i] = bullet.x[i]
        end
        if love.keyboard.isDown('space') then
            bullet.dy[i] = -bullet.v
            bullet.aim[i] = false
        end
        bullet.y[i] = bullet.y[i] + bullet.dy[i]
    end

    -- enemy movement
    enemy.dx = enemy.dx + enemy.v
    if enemy.dx > 50 then
        enemy.v = -enemy.v
    elseif enemy.dx < 0 then
        enemy.v = math.abs(enemy.v)
    end

    -- bullet hit enemy
    for i = 1,  tablelength(enemy.table) do
        local enemywidth = love.graphics.getWidth() / tablelength(enemy.table[i]) / 2
        for j = 1, tablelength(enemy.table[i]) do
            local enemyx = enemywidth * (j - 1) * 2 + enemy.dx
            local enemyy = enemy.y + enemy.h * i * 2

            for k = 1, tablelength(bullet.x) do
                if enemy.table[i][j] and bullet.x[k] > enemyx and bullet.x[k] < enemyx + enemywidth and bullet.y[k] < enemyy then
                    enemy.table[i][j] = false
                end
            end
        end
    end

    -- append bullet
    if bullet.aim[1] == false and bullet.y[1] < love.graphics.getHeight()/2 then
        table.insert(bullet.x, 1, player.x + player.w/2)
        table.insert(bullet.y, 1, love.graphics.getHeight() - bullet.r)
        table.insert(bullet.dy, 1, 0)
        table.insert(bullet.aim, 1, true)
    end

    -- keep player on the board
    if player.x < 0 then
        player.x = 0
    elseif player.x + player.w > love.graphics.getWidth() then
        player.x = love.graphics.getWidth() - player.w
    end
end

function love.draw()
    -- draw player
    love.graphics.rectangle('fill', player.x, player.y, player.w, player.h)

    -- draw bullet
    for i = 1, tablelength(bullet.x) do
        love.graphics.circle('fill', bullet.x[i], bullet.y[i], bullet.r)
    end

    -- draw enemy
    for i = 1,  tablelength(enemy.table) do
        local enemywidth = love.graphics.getWidth() / tablelength(enemy.table[i]) / 2
        for j = 1, tablelength(enemy.table[i]) do
            if enemy.table[i][j] then
                love.graphics.rectangle("fill", enemywidth * (j - 1) * 2 + enemy.dx, enemy.y + enemy.h * i * 2, enemywidth, enemy.h)
            end
        end
    end

    -- print
    local playerprint = string.format('player.x : %d \n', player.x)
    local bulletprint = string.format('bullet.x : %d \nbullet.y : %d \n  bullets : %d\n', bullet.x[1], bullet.y[1], tablelength(bullet.x))
    local enemyprint = string.format('enemy.dx : %d\n', enemy.dx)
    love.graphics.print(playerprint .. bulletprint .. enemyprint, 10, 10)
end

function tablelength(T)
    local count = 0
    for _ in pairs(T) do count = count + 1 end
    return count
end