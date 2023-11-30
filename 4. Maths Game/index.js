//if we click on the start/Reset
    // if we are playing
        //reload page
    // if we are not playing
        //show countdown box
        //change the button name to reset
        //reduce time by 1 sec
            //time left?
                //yes -> continue
                //no -> gameover
        //generate new Q&A


//if we click on answer box
    //if we are playing
        //correct?
            //yes
                //increase score
                //show correct box for 1 sec
                //generate new questions
            //no
                //show try again box


var startButton = document.querySelector("#idStartBtn");
var time = document.querySelector("#idTime");
var gameOverDiv = document.querySelector("#idGameOver");
var quesDiv = document.querySelector("#idQuestion");
var quesProp = {"25x60": 1500, "2+6":8, "89-45":44, "99/3":33};
var boxChoice = document.querySelectorAll(".box");
var scoreVal = document.querySelector("#idScoreVal");
var isPlaying = false;
var score = 70;
var ques = [];
var quesInd = -1;
var myQues = "";
var score;
var genQues = "";
startButton.onclick = function(){

    // if we are playing
    if(isPlaying == true) // or just keep (isPlaying)
    {
        location.reload();
    }
    else // if we are not playing
    {
        //set the score to zero
        score = 0;
        isPlaying = true;
        //show countdown box
        time.style.visibility = "visible";
    
        //change the button name to reset
        startButton.innerHTML = "Reset Game";
        var clockVal = 60;
    
        //reduce time by 1 sec
        timer = setInterval(() => {
            --clockVal;
            document.querySelector("#idTimeRem").innerHTML = clockVal;
            
        //time left?
            //yes -> continue
            //no -> gameover

            if(clockVal < 1)
            {
                clearInterval(timer);
                parOne = document.createElement('p');
                parOne.innerHTML = "Game Over!";
                parTwo = document.createElement('p');
                parTwo.innerHTML = `Your score is ${score}`;
                gameOverDiv.appendChild(parOne);
                gameOverDiv.appendChild(parTwo);
                gameOverDiv.style.display = "inline";
                isPlaying = false;
            }
        }, 1000);

        genQues = GenerateQA();
    }
}


//if we click on answer box
boxChoice.forEach(element => {
    element.onclick = function(){
        let userChoice = this.innerHTML;
        
        //if we are playing
        if(isPlaying == true)
        {   
            console.log("UserChoice: " + userChoice);
            console.log("genQues: " + quesProp[genQues])
            //correct?
            if (quesProp[genQues] == userChoice)
            {
                console.log("The answer is right");
                score+=2;
                scoreVal.innerHTML = score;
                
               
            //yes
                //increase score
                //show correct box for 1 sec
                setTimeout(()=>{
                    document.getElementById("idCorrect").classList.add("hidden");
                }, 5000)
                //generate new questions
                genQues = GenerateQA();
            }
            //no
                //show try again box

            console.log("I am playing");
        }
        else
        {
            console.log("I am not playing");
        }
    }
});


function GenerateQA()
{
    //Generate new Question
    ques = Object.keys(quesProp);//;
    quesInd = Math.floor(Math.random()*ques.length)
    myQues = ques[quesInd];
    quesDiv.innerHTML = myQues;
    return myQues;
}