<?php
    // error_log("Testing api: ".json_encode($_GET));
    require_once 'apiDef.php';
    // error_log("Login Errory: ".json_encode($loginError));
try
{
    $API = new MyAPI($_REQUEST['request']);
    echo $API->processAPI();
}
catch(Exception $e)
{
    //error_log(json_encode(Array('error' => $e.getMessage())));
    echo json_encode(Array('error' => $e.getMessage()));
}
?>