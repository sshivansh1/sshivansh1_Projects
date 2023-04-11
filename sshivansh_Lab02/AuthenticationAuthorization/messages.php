<?php
require_once "db.php";
/*Response Codes
200 OK
400 Bad Request
401 Unauthorized
403 Forbidden
404 Not Found
500 Internal Server Error --Something misspelled or missing in your code
*/
    $newArray = json_decode($_REQUEST['data'], true);
    $myUsername = $newArray["username"];
    $myPassword = $newArray["password"];

    $selQuery = "Select `password` FROM Users WHERE `username` = '$myUsername'";

    if($result = mysqlQuery($selQuery))
    {
        while($row = $result -> fetch_assoc())
        {
            $dbPassword = $row["password"];
        }

        if(password_verify($myPassword, $dbPassword))
        {
        //When json_decode is used it I'll be expecting the return to be assoc array
        //but instead it'll return object std
        //Either you have to cast it using (array) in front like: 
        //$newArray = (array)(json_decode($_REQUEST['data']));
        //Or
        //set the second parameter of the function json_decode to true like:
        $newArray = json_decode($_REQUEST['data'], true);
        
        $myUser = $newArray["username"];
        $userRoleValue = $newArray["rolevalue"];
        }
        else
        {
            header("Location: ../Lab02/login.php", true, 403);
            die();
        }
    }
    else
    {
        echo "Internal Server error";
    }


?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Messages</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>
    <script src="messages.js"></script>
    <link rel="stylesheet" href="messages.css">
</head>
<body>
    <h1>Lab02 - Messages - <?php echo "$myUser - $userRoleValue";?></h1>

    <div class = "userInputDiv">
        <label for="idFilterTb">Filter: </label>
        <input type="text" id="idFilterTb" placeholder = "Supply a filter"><br>
        <input type="button" value="Search" id ="idSearchBtn"><br>
        <label for="idMessageTb">Message: </label>
        <input type="text" id="idMessageTb" placeholder = "Enter a message to share"><br>
        <input type="button" value="Send" id ="idSendBtn">
    </div>

    <div class = "tableDiv">
    
    </div>

    <div class = "statusMessage">

    </div>
    <div class = "updateDiv">

    </div>

    <footer style="text-align:center;"><a href="../Lab02/login.php">Home</a></footer>
</body>
</html>