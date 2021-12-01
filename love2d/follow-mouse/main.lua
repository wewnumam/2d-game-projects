function love.load()
    ball = {}
    ball.x = 300
    ball.y = 200
    ball.vel = {}
    ball.vel.x = 3
    ball.vel.y = 3
    ball.radius = 50

    mouse = {}
    mouse.x = 0
    mouse.y = 0
end

function love.update(dt)
    -- update mouse position
    mouse.x, mouse.y = love.mouse.getPosition()

    -- make ball position closer to mouse position
    if ball.x < mouse.x and ball.y < mouse.y then
        ball.x = ball.x + ball.vel.x
        ball.y = ball.y + ball.vel.y
    elseif ball.x < mouse.x and ball.y > mouse.y then
        ball.x = ball.x + ball.vel.x
        ball.y = ball.y - ball.vel.y
    elseif ball.x > mouse.x and ball.y < mouse.y then
        ball.x = ball.x - ball.vel.x
        ball.y = ball.y + ball.vel.y
    elseif ball.x > mouse.x and ball.y > mouse.y then
        ball.x = ball.x - ball.vel.x
        ball.y = ball.y - ball.vel.y
    elseif ball.x == mouse.x and ball.y < mouse.y then
        ball.y = ball.y + ball.vel.y
    elseif ball.x == mouse.x and ball.y > mouse.y then
        ball.y = ball.y - ball.vel.y
    elseif ball.x < mouse.x and ball.y == mouse.y then
        ball.x = ball.x + ball.vel.x
    elseif ball.x > mouse.x and ball.y == mouse.y then
        ball.x = ball.x - ball.vel.x
    end
end

function love.draw()
    io.write(string.format("x: %s\ty: %s\n", mouse.x, mouse.y))
    love.graphics.circle("fill", ball.x, ball.y, ball.radius)
end