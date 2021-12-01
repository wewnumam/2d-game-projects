function love.load()
    ball = {}
    ball.x = 300
    ball.y = 200
    ball.vel = {}
    ball.vel.x = 5
    ball.vel.y = 5
    ball.radius = 50

    mouse = {}
    mouse.x = 0
    mouse.y = 0
end

function love.update(dt)
    -- update mouse position
    mouse.x, mouse.y = love.mouse.getPosition()

    local a, b, c = pythagoras(ball, mouse)
    ball.x = ball.x + (1 / (love.graphics.getWidth() / a)) * ball.vel.x
    ball.y = ball.y + (1 / (love.graphics.getHeight() / b)) * ball.vel.y
end

function love.draw()
    io.write(string.format(" ball.x : %s\t ball.y : %s\n", math.floor(ball.x), math.floor(ball.y)))
    io.write(string.format("mouse.x : %s\tmouse.y : %s\n\n", mouse.x, mouse.y))
    love.graphics.circle("fill", ball.x, ball.y, ball.radius)
end

function pythagoras(ball, mouse)
    a = mouse.x - ball.x
    b = mouse.y - ball.y
    c = math.sqrt(a^2 + b^2)
    return a, b, c
end