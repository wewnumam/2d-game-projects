const canvas = document.getElementById('canvas');
const ctx = canvas.getContext('2d');
const scoreDisplay = document.querySelector('#score span');
let game = setInterval(update, 250);

const snakeW = 20;
const snakeH = 20;
const snakeV = 20;
let snakeX = 0;
let snakeY = 0;
let snakeDX = 0;
let snakeDY = 0;
let tailX = [];
let tailY = [];

const appleW = 20;
const appleH = 20;
let appleX = 0;
let appleY = 0;

let score = 0;
let boardX = [];
let boardY = [];
for (let i = 0; i < canvas.width; i += appleW) boardX.push(i);
for (let i = 0; i < canvas.height; i += appleW) boardY.push(i);

function update() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    // Snake movement, manipulate by input()
    snakeX += snakeDX;
    snakeY += snakeDY;

    // Stay on the board
    if (snakeX > canvas.width) snakeX = 0;
    if (snakeX < 0) snakeX = canvas.width;
    if (snakeY > canvas.height) snakeY = 0;
    if (snakeY < 0) snakeY = canvas.height;
    
    // Snake eats apple
    if (snakeX == appleX && snakeY == appleY) {
        score++;
        appleX = boardX[Math.floor(Math.random() * boardX.length)];
        appleY = boardY[Math.floor(Math.random() * boardY.length)];
    }

    // Tail
    tailX.unshift(snakeX);
    tailY.unshift(snakeY);
    if (tailX.length > score) {
        tailX.pop();
        tailY.pop();
    }

    // Snake bites his tail
    for (let i = 0; i < score; i++) {
        if (i > 2 && snakeX == tailX[i] && snakeY == tailY[i]) {
            clearInterval(game);
            score += ' GAME OVER'
            break;
        }
    }
    
    // Draw snake
    ctx.fillStyle = 'gold';
    ctx.fillRect(snakeX, snakeY, snakeW, snakeH);

    // Draw tail
    for (let i = 0; i < tailX.length; i++) {
        if (i > 0) {
            ctx.fillStyle = 'blue';
        } else {
            ctx.fillStyle = 'gold';
        }
        ctx.fillRect(tailX[i], tailY[i], snakeW, snakeH);
    }

    // Draw apple
    ctx.fillStyle = 'red';
    ctx.fillRect(appleX, appleY, appleW, appleH);

    // Draw score
    scoreDisplay.innerHTML = score;
}

function input(e) {
    switch (e.key) {
        case 'ArrowLeft':
            if (snakeDX == 0) snakeDX = -snakeV;
            snakeDY = 0;
            break;
        case 'ArrowRight':
            if (snakeDX == 0) snakeDX = snakeV;
            snakeDY = 0;
            break;
        case 'ArrowUp':
            snakeDX = 0;
            if (snakeDY == 0) snakeDY = -snakeV;
            break;
        case 'ArrowDown':
            snakeDX = 0;
            if (snakeDY == 0) snakeDY = snakeV;
            break;
        case 'Escape':
            clearInterval(game);
            break;
        case 'q':
            clearInterval(game);
            break;
    }
}

document.addEventListener('keydown', input);