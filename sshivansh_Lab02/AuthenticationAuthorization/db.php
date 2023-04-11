<?php
session_start();
$mysql_connection = null;//connection
$mysql_response = "";//getting status from the user
$mysql_status = "";
$goodToSubmit;
//Excecutable php at the top
//Then functions underneath

mysqlConnect();//creating a function

function mysqlConnect()
{
    global $mysql_connection, $mysql_response;
    //Creating a connection
    $mysql_connection = new mysqli("localhost","sshiv021_TestUser", "myPassword@123", "sshiv021_TestDb");
    
    // error_log(json_encode($_SESSION));//
    //Checking a connection
    if($mysql_connection -> connect_error)
    {
        $mysql_response = "Connect Error (". $mysql_connection -> connect_errno . ") ".$mysql_connection -> connect_error;//Error number/code and the description of error is given
        echo json_encode($mysql_response);//Turns everything into a text
        error_log(json_encode($mysql_response));
        die();
    }
    else
    {
        $userId = 100;
        $query = "SELECT username from Users";
        $query.= " WHERE `user_id` = '100'";

        if($result = mysqlQuery($query))
        {
            while($row = $result -> fetch_assoc())
            {
                $_SESSION["rootUsername"] = $row['username'];
                // myPassword@21
            }
        }
        //$_SESSION["loginError"] = true;
        error_log(json_encode($_SESSION));
        error_log("connnection successfully created");
    }
}

//Query will be probably a string
function mysqlQuery($query)//use the query to bounce off the database
{
    global $mysql_connection, $mysql_response;

    $result = false;

    if($mysql_connection == null)
    {
        $mysql_response = "No database connection estalished";
        return $result; //false
    }
    if(!($result = $mysql_connection->query($query)))
    {
        $mysql_response = "Query Error : {$mysql_connection -> errno} : {$mysql_connection->error}";
    }
    return $result;//Object
}

//Not expecting data back, only message back
function mysqlNonQuery($query)
{
    global $mysql_connection, $mysql_response;

    $result = 0;

    if($mysql_connection == null)
    {
        $mysql_response = "No database connection estalished";
        return $result; //0
    }

    if(!($result = $mysql_connection->query($query)))
    {
        $mysql_response = "Query Error : {$mysql_connection -> errno} : {$mysql_connection->error}";
        return $result;
    }

    return $mysql_connection -> affected_rows;
    /*
    An integer greater than zero indicates the number of rows affected or retrieved.
    Zero indicates that no records were updated for an UPDATE statement, no rows matched the WHERE clause in the query or that no query has yet been executed. 
    -1 indicates that the query returned an error or that mysqli_affected_rows() was called for an unbuffered SELECT query.
    */
}