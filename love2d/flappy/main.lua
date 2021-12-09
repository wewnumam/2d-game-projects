function love.load()
    world = love.physics.newWorld(0, 200, true)
    world:setCallbacks(beginContact)

    flappy = {}
    flappy.w = 40
    flappy.h = 40
    flappy.v = 25
    flappy.b = love.physics.newBody(world, love.graphics.getWidth()/2, love.graphics.getHeight()/2, "dynamic")
    flappy.s = love.physics.newRectangleShape(flappy.w, flappy.h)
    flappy.f = love.physics.newFixture(flappy.b, flappy.s)
    flappy.f:setUserData("Flappy")

    pipe = {}
    pipe.w = 60
    pipe.h = love.graphics.getHeight()
    pipe.v = 2
    local rand = math.random(-pipe.h/2, pipe.h/3)
    pipe.bt = {love.physics.newBody(world, love.graphics.getWidth(), rand, "static")}
    pipe.st = {love.physics.newRectangleShape(pipe.w, pipe.h)}
    pipe.ft = {love.physics.newFixture(pipe.bt[1], pipe.st[1])}
    pipe.bb = {love.physics.newBody(world, love.graphics.getWidth(), rand + pipe.h + 150, "static")}
    pipe.sb = {love.physics.newRectangleShape(pipe.w, pipe.h)}
    pipe.fb = {love.physics.newFixture(pipe.bb[1], pipe.sb[1])}

    ground = {}
    ground.b = love.physics.newBody(world, love.graphics.getWidth()/2, love.graphics.getHeight(), "static")
    ground.s = love.physics.newRectangleShape(love.graphics.getWidth(), 10)
    ground.f = love.physics.newFixture(ground.b, ground.s)
    ground.f:setUserData("Ground")
end

function love.update(dt)
    world:update(dt)

    -- pipe movement
    for i = 1, tablelength(pipe.bt), 1 do
        pipe.bt[i]:setX(pipe.bt[i]:getX() - pipe.v)
        pipe.bb[i]:setX(pipe.bb[i]:getX() - pipe.v)
        pipe.ft[i]:setUserData('PipeTop['.. tablelength(pipe.bt)-i+1 ..']')
        pipe.fb[i]:setUserData('PipeBottom['.. tablelength(pipe.bb)-i+1 ..']')
    end
    
    -- append pipe
    if pipe.bt[1]:getX() < love.graphics.getWidth()/2 then
        local rand = math.random(-pipe.h/2, pipe.h/3)
        table.insert(pipe.bt, 1, love.physics.newBody(world, love.graphics.getWidth(), rand, "static"))
        table.insert(pipe.st, 1, love.physics.newRectangleShape(pipe.w, pipe.h))
        table.insert(pipe.ft, 1, love.physics.newFixture(pipe.bt[1], pipe.st[1]))
        table.insert(pipe.bb, 1, love.physics.newBody(world, love.graphics.getWidth(), rand + pipe.h + 150, "static"))
        table.insert(pipe.sb, 1, love.physics.newRectangleShape(pipe.w, pipe.h))
        table.insert(pipe.fb, 1, love.physics.newFixture(pipe.bb[1], pipe.sb[1]))
    end
end

function love.draw()
    
    -- draw pipe
    for i = 1, tablelength(pipe.bt), 1 do
        love.graphics.setColor(0,1,0)
        love.graphics.polygon("fill", pipe.bt[i]:getWorldPoints(pipe.st[i]:getPoints()))
        love.graphics.polygon("fill", pipe.bb[i]:getWorldPoints(pipe.sb[i]:getPoints()))
    end
    
    -- draw ground
    love.graphics.setColor(1,1,1)
    love.graphics.polygon("fill", ground.b:getWorldPoints(ground.s:getPoints()))

    -- draw flappy
    love.graphics.setColor(1,1,0)
    love.graphics.polygon("fill", flappy.b:getWorldPoints(flappy.s:getPoints()))

    
    if not os.execute("clear") then os.execute("cls") end
    print(string.format("flappy.y : %d", flappy.b:getY()))
    print(string.format("flappy.x : %d", flappy.b:getX()))
    for i = 1, tablelength(pipe.bt) do
        print(string.format("\n  pipe.x : %s", pipe.bt[i]:getX()))
        print(string.format(" pipe.ft : %s", pipe.ft[i]:getUserData()))
        print(string.format(" pipe.yt : %s", pipe.bt[i]:getY()))
        print(string.format(" pipe.fb : %s", pipe.fb[i]:getUserData()))
        print(string.format(" pipe.yb : %s", pipe.bb[i]:getY()))
    end
end

-- collision contact
function beginContact(a, b, coll)
    love.timer.sleep(2)
    love.event.quit('restart')
end

function love.keypressed(key)
    if key == "escape" then
        love.event.quit()
    end

    -- flappy flap
    if key == "space" then
        -- flappy.b:applyForce(0, -5000)
        flappy.b:setLinearVelocity(0, 10)
        flappy.b:setY(flappy.b:getY() - flappy.v)
    end

end

function tablelength(T)
    local count = 0
    for _ in pairs(T) do count = count + 1 end
    return count
end