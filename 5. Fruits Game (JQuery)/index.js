//Press Start Game
    //Already Running(Yes or no)
        //yes
            //reload
        //no
            //change button name to "reset"
            //Show the 3 lives box
            //Start Creating Random Fruits
                //Move a fruit one step down
                    // Slice
                        //make a sound
                        //animate
                        //increase score by one
                    // if too low(if left)
                        //check for lives if > 1
                            //A life minus
                        //Game Over
                        //change the button name to start
                   
                    
var isPlaying = false;
var score;
var lives;
var fruitsArr = ['apple', 'banana', 'pineapple', 'grapes', 'mango', 'orange', 'strawberry', 'watermelon'];
var step;

$(document).ready(function(){
   // if Sliced
        //make a sound
        //animate
        //increase score by one   
    $("#idFruit").mouseover(function(){
        score++;
        $("#idSliceSound")[0].play();
        $("#idScoreVal").html(score);
        clearInterval(action);
        $("#idFruit").hide("explode", 400);
        setTimeout(PlayFruits, 500);
    });

    $("#idStartBtn").click(function(){
        // If we are already playing
        if(isPlaying)
            window.location.reload();
        // If we are not playing
        else
        {
            isPlaying = true;
            $("#idLives").show();
            // or $("#idLives").css("display", "block");

            score = 0;
            lives = 3; //Initially we have three lives
            $("#idScoreVal").html(score);
            $("#idStartBtn").html("Reset Game");
            RemainingLives(lives);
            $("#idGameOver").hide();
            PlayFruits();
        }
    })
});

function RemainingLives(lives)
{
    $("#idLives").empty();
    for (let i = 0; i < lives; i++) {
        $("#idLives").append(' <img src="images/8CQbvh-LogoMakr.png"> ');
    }
}

function GenerateFruits()
{
 //Start Creating Random Fruits
 let randFruit = fruitsArr[Math.floor(Math.random()*fruitsArr.length)];
 $("#idFruit").attr("src", "images/" + randFruit + ".png");
 $("#idFruit").show();

 $("#idFruit").css({"left": Math.floor(Math.random()*700) + "px", "top": "-75px"});
 
 //Move a fruit one step down
 step = 2 + Math.floor(Math.random()*8);
}

function PlayFruits()
{
    // use a lot of memory space
    // $("#idGameArea").append(' <img src="images/apple.png"> ');

    //Start Creating Random Fruits
    GenerateFruits();
    action = setInterval(function(){
        $("#idScore").html("Score: " + score);
        $("#idFruit").css("top",  $("#idFruit").position().top + step + "px")
        // Check if too low(if left)
        if ($("#idFruit").position().top > $("#idGameArea").height())
        {
            if(lives > 1)
            {
                //A life minus 
                lives--;
                RemainingLives(lives);
                GenerateFruits();
            }
            else //Game Over
            {
                lives = 0;
                isPlaying = false
                RemainingLives(lives);
                clearInterval(action);
                $("#idGameOver").html("Game Over! </br> Your Score is: " + score);
                $("#idGameOver").show();
                $("#idStartBtn").html("Start Game");
                $("#idGameArea").empty(); //Clears the game area 
            }
        } 
    }, 10);

    // Check if too low(if left)
        //check for lives if > 1
            //A life minus
        //Game Over
        //change the button name to start
       
}

//Credits to www.logomakr.com
//www.openclipart.org
//www.pixabay.com