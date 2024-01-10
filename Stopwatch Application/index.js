$(document).ready(function(){
    var myTimer = 0;
    var timeCounter = 0;
    var lapCounter = 0;
    var timeMin, timeSec, timeCentiSec, lapMin, lapSec, lapCentiSec;
    var isMode = false;
    var lapNum = 0;
    HideShowButtons("#startBtn", "#lapBtn");
    //Click Start
    $("#startBtn").click(function (e) { 
        isMode = true;
        //Will change the button to 'stop'
        HideShowButtons("#stopBtn", "#lapBtn");
        //Will start_interval the stopwatch timer
        //and the lap timer
        StartTimer();
    });

    //Click Stop
    //stop_interval for the stopwatch timer
    //and the lap timer
    //will change the button to 'resume' and 'reset'
    $("#stopBtn").click(function (e){
        clearInterval(myTimer);
        HideShowButtons("#resumeBtn", "#resetBtn");
    }); 

    //Click Resume
        //starts from the current time for stopwatch timer
        //and the lap timer
        //changes the button name to 'stop'
    $("#resumeBtn").click(function (e){
        //Will change the button to 'stop'
        HideShowButtons("#stopBtn", "#lapBtn");
        //Will start_interval the stopwatch timer
        //and the lap timer
        StartTimer();
    }); 

    //Click Lap
        //saves time for the lap and lapwatch timer
        //clear_interval and then starts again for the lap timer
        $("#lapBtn").click(function (e){
            if(isMode == true){
               clearInterval(myTimer);
               lapCounter = 0;
               ShowLaps();
               StartTimer();
            }
        });


    //Click Reset
        //will clear all the timers and clean the laptime saved div
        //Button is changed to 'lap' and 'start'
        //all flags must be cleared
    $("#resetBtn").click(function (e){
        location.reload();
        isMode = false;
    });

    $("#resetBtn").click(function (e){
        //Will change the button to 'stop'
        HideShowButtons("#resetBtn", "#lapBtn");
        //Will start_interval the stopwatch timer
        //and the lap timer
        StartTimer();
    }); 

    function StartTimer() {
        myTimer = setInterval(() => {
            timeCounter++;
            if(timeCounter == 100*60*100)
                timeCounter = 0;
            if(lapCounter == 100*60*100)
                lapCounter = 0;
            lapCounter++;
            UpdateTime();
        }, 10);
    }
    
    function UpdateTime(){
        // This calculates the total number of minutes
        timeMin = Math.floor(timeCounter/(6000));
        // Takes the remaining milliseconds after calculating minutes
        timeSec = Math.floor((timeCounter%6000)/100);
        // Takes the remaining 
        timeCentiSec = (timeCounter%6000)%100;

        // This calculates the total number of minutes
        lapMin = Math.floor(lapCounter/(6000));
        // Takes the remaining milliseconds after calculating minutes
        lapSec = Math.floor((lapCounter%6000)/100);
        // Takes the remaining 
        lapCentiSec = (lapCounter%6000)%100;

        $("#timeMinute").html(Format(timeMin));
        $("#timeSecond").html(Format(timeSec));
        $("#timeCentisecond").html(Format(timeCentiSec));
        $("#lapMinute").html(Format(lapMin));
        $("#lapSecond").html(Format(lapSec));
        $("#lapCentisecond").html(Format(lapCentiSec));
    }

    function HideShowButtons(leftBtn, rightBtn)
    {
        $(".control").hide();
        $(leftBtn).show();
        $(rightBtn).show();
    }

    function ShowLaps()
    {
        lapNum++;
        var myLapDetails = 
        `<div class = "lap">
            <div class="laptimetitle">Lap${lapNum}</div>` + 
            `<div class="laptimeinfo">${Format(timeMin)}:${Format(timeSec)}:${Format(timeCentiSec)}</div>
        </div>`;
        $(myLapDetails+"</br>").prependTo("#laps");
    }
    
    function Format(number) {
        if(number < 10)
           return '0'+number;
        else
            return number;
    }
});