function love.load()
    love.window.setMode(800, 600, {resizable=false})
    fps = 2
    delta = 0
    score = 0

    snake = {}
    snake.w = 20
    snake.h = 20
    snake.x = 0
    snake.y = 0
    snake.v = 20
    snake.dx = 0 
    snake.dy = 0
    tail = {}
    tail.x = {}
    tail.y = {}

    apple = {}
    apple.x = 0
    apple.y = 0
    apple.r = 10
    
    -- board for apple random position
    board = {}
    board.x = {}
    board.y = {}
    
    for i = 0, love.graphics.getWidth(), 20 do
        table.insert(board.x, i)
    end
    
    for i = 0, love.graphics.getHeight(), 20 do
        table.insert(board.y, i)
    end
end

function love.update(dt)
    delta = dt
    
    -- nake movement, manipulate by love.keypressed()
    snake.x = snake.x + snake.dx
    snake.y = snake.y + snake.dy

    -- fps setting
    if dt < 1/fps then
        love.timer.sleep((1/fps - dt) / 2)
    end

    -- stay on the board
    if snake.x > love.graphics.getWidth() then
        snake.x = 0
    elseif snake.x < 0 then
        snake.x = love.graphics.getWidth()
    elseif snake.y > love.graphics.getHeight() then
        snake.y = 0
    elseif snake.y < 0 then
        snake.y = love.graphics.getHeight()
    end

    -- snake eats apple
    if snake.x == apple.x and snake.y == apple.y then
        score = score + 1
        apple.x = board.x[math.random(0, #board.x - 1)]
        apple.y = board.y[math.random(0, #board.y - 1)]
    end

    -- tail
    table.insert(tail.x, snake.x)
    table.insert(tail.y, snake.y)
    if tablelength(tail.x) > score then
        table.remove(tail.x, 1)
        table.remove(tail.y, 1)
    end

    -- snake bites his tail
    for i = 1, score, 1 do
        if i < score and tail.x[score] == tail.x[i] and tail.y[score] == tail.y[i] then
            love.event.quit('restart')
        end
    end
end

function love.draw()
    -- draw snake
    love.graphics.setColor(1, 1, 0)
    love.graphics.rectangle("fill", snake.x, snake.y, snake.w, snake.h)

    -- draw tail
    for i = 1, score, 1 do
        if i == score then
            love.graphics.setColor(1, 1, 0)
        else
            love.graphics.setColor(0, 0, 1)
        end
        love.graphics.rectangle("fill", tail.x[i], tail.y[i], snake.w, snake.h)
    end

    -- draw apple
    love.graphics.setColor(1, 0, 0)
    love.graphics.circle("fill", apple.x + apple.r, apple.y + apple.r, apple.r)

    -- draw score
    love.graphics.setColor(1, 1, 1)
    love.graphics.print(tostring(score), love.graphics.getWidth()/2, 0)

    if not os.execute("clear") then
        os.execute("cls")
    end
    print(string.format("    FPS : %d", love.timer.getFPS()))
    print(string.format("  Delta : %f", delta))
    print(string.format("  Sleep : %f", 1 / fps - delta))
    print(string.format("  Score : %d\n", score))
    print(string.format(" AppleX : %s\t AppleY : %s", apple.x, apple.y))
    print(string.format(" SnakeX : %s\t SnakeY : %s", snake.x, snake.y))
    print(string.format("SnakeDX : %s\tSnakeDY : %s\n", snake.dx, snake.dy))
    print(string.format("  TailX : %s", dump(tail.x)))
    print(string.format("  TailY : %s\n\n", dump(tail.y)))
end

function love.keypressed(key)
    if key == "escape" then
       love.event.quit()
    elseif key == 'right' then
        if snake.dx == 0 then
            snake.dx = snake.v
        end
        snake.dy = 0
    elseif key == 'left' then
        if snake.dx == 0 then
            snake.dx = -snake.v
        end
        snake.dy = 0
    elseif key == 'down' then
        snake.dx = 0
        if snake.dy == 0 then
            snake.dy = snake.v
        end
    elseif key == 'up' then
        snake.dx = 0
        if snake.dy == 0 then
            snake.dy = -snake.v
        end
    end
 end

 function dump(o)
    if type(o) == 'table' then
       local s = '{ \n'
       for k,v in pairs(o) do
          if type(k) ~= 'number' then k = '"'..k..'"' end
          s = s .. '['..k..'] = ' .. dump(v) .. ',\n'
       end
       return s .. '\n}'
    else
       return tostring(o)
    end
 end

 function tablelength(T)
    local count = 0
    for _ in pairs(T) do count = count + 1 end
    return count
  end