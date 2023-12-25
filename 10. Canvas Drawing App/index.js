$(document).ready(function () {
    $( "#sizeSlider" ).slider({
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

    $("#sliderLabel").html( $( "#sizeSlider" ).slider( "value" ) );

    var context = $("#paint").getContext('2d');

    //draw a line
    //declaring a new path
    

});