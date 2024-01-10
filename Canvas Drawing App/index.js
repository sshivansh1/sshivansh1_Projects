$(document).ready(function () {
    $("#sizeSlider").slider({
        range: "min",
        min: 5,
        max: 50,
        value: 30,
        slide: function( event, ui ) {
          $( "#sliderLabel" ).html( ui.value );
          $("#brushSize").height(ui.value);
          $("#brushSize").width(ui.value);
        }
    });

    $("#sliderLabel").html($("#sizeSlider").slider("value"));

    //getting the context of the canvas area
    var context = document.getElementById("paint").getContext('2d');

    //get the canvas container
    var canvasContainer = $("#canvasContainer");

    //is painting or erasing
    var isPaintErase = false;

    //current performing mode
    var paintMode = "paint";
    
    //mouse position
    var mouse = {x: 0, y: 0};
    
    if(localStorage.getItem("imgCanvas") != null){
      var img = new Image();
      img.onload = function(){
        context.drawImage(img, 0, 0);
      }
      //retrieving the previously saved image
      img.src = localStorage.getItem("imgCanvas");
    }


    //setting some attributes of the lines 
    //that will be used for drawing
    context.lineWidth = 30;
    context.lineJoin = "round";
    context.lineCap = "round";
    
    //mouse down inside the container
    canvasContainer.mousedown(function(event) { 
      isPaintErase = true;
      context.beginPath();
      mouse.x = event.pageX - this.offsetLeft;
      mouse.y = event.pageY - this.offsetTop;
      context.moveTo(mouse.x, mouse.y);
    });

    canvasContainer.mousemove(function (event) {
      mouse.x = event.pageX - this.offsetLeft;
      mouse.y = event.pageY - this.offsetTop;
      if(isPaintErase){
        if(paintMode == "paint")
        {
          //get color input
          context.strokeStyle = $("#paintColor").val();
          context.lineWidth = $("#sizeSlider").slider( "value" );
        }
        else{
          //white color
          context.strokeStyle = "white"
        }
        context.lineTo(mouse.x, mouse.y);
        context.stroke();
      }
    });

    canvasContainer.mouseup(function (event) {
      isPaintErase = false;
    });
    canvasContainer.mouseleave(function (event) {
      isPaintErase = false;
    });

    //erase button functionality
    $("#erase").click(function (e) { 
      if(paintMode == "paint")
        paintMode = "erase";
      else
        paintMode = "paint";

      $(this).toggleClass("eraseMode");
    });

    //clicking on reset button
    $("#reset").click(function (e) { 
      context.clearRect(0, 0, document.getElementById("paint").width, document.getElementById("paint").height);
      paintMode = "paint";
      $(this).removeClass("eraseMode");
    });

    //save button
    $("#save").click(function (e) { 
      console.log("helllo");
      if(typeof(localStorage) != null){
        //first parameter -- variable 
        //second paramter -- value 
        localStorage.setItem("imgCanvas", document.getElementById("paint").toDataURL());
        
        // Just to get an idea of the graphical representation
        // window.alert(localStorage.getItem("imgCanvas"));    
      }
      else{
        window.alert("Your browser does not support local storgae!")
      }
    });


    $("#sizeSlider").slider({
      range: "min",
      min: 5,
      max: 50,
      value: 30,
      slide: function( event, ui ) {
        $( "#sliderLabel" ).html( ui.value );
        $("#brushSize").height(ui.value);
        $("#brushSize").width(ui.value);
        context.lineWidth = ui.value;
      }
  });
  $("#paintColor").change(function (e) { 
    $("#brushSize").css("background-color", $("#paintColor").val())
  });
});