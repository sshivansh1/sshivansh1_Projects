<?php
    session_start();//resumes the session here
    //If there is no username set to the session to redirects it to the login page
    if(!isset($_SESSION["username"]))
    {
        header("Location: login.php");
        die();
    }
    else
    {
        $myUser = $_SESSION["username"];//Stores the username to display
        $myRoleValue = $_SESSION["rolevalue"];//Stores the username to display
        $myData = $_REQUEST["data"];

        $myDataArray = json_decode($myData, true);
        $myPassword = $myDataArray["password"];

        // $infoArray = array("username" => $myUser, "rolevalue" => $myRoleValue);

        $infoArray["username"] = $myUser;
        $infoArray["rolevalue"] = $myRoleValue;
        $infoArray["password"] = $myPassword;
        error_log("index array ".json_encode($infoArray));

        $myLink = 'https://thor.cnt.sast.ca/~sshiv021/draxyBoi/Labby/Lab02/messages.php?data='.json_encode($infoArray);
    }

    if(isset($_SESSION["errorMessage"]))
    {
        $errorMessage = $_SESSION["errorMessage"];
    }
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Index</title>
    <link rel="stylesheet" href="index.css">
</head>

<body>
    <h1>Lab02 - Index Page</h1>
    <div class = "divLinks">
        <?php
            if($_SESSION["rootUsername"] == $myUser || $_SESSION["rolename"] == "Root")
            {
                echo "<a class = 'userManage' href='userManage.php'>User Management</a> ";
                echo "<a class = 'roleManage' href='roleManage.php'>Role Management</a> ";
                echo "<a class = 'messageManage' href= $myLink >Messages</a> <br>";

            }
            else if($_SESSION["rolename"] == "Administrator")
            {
                echo "<a class = 'userManage' href='userManage.php'>User Management</a> <br>";
                echo "<a class = 'roleManage' href='roleManage.php'>Role Management</a> <br>";
                echo "<a class = 'messageManage' href=$myLink>Messages</a> <br>";

            }
            else if($_SESSION["rolename"] == "Moderator")
            {
                echo "<a class = 'userManage' href='userManage.php'>User Management</a> <br>";
                echo "<a class = 'messageManage' href=$myLink>Messages</a> <br>";
            }
            else
            {
                echo "<a class = 'messageManage' href=$myLink>Messages</a> <br>";
            }

        ?>
        <form action="login.php" method="post">
        <input type="submit" value="Logout" name = "newSubmit" class = "logoutBtn">
        </form>
    </div>
    <div class = "statusDiv">
        <?php 
            echo " Page Status: Welcome $myUser :) <br>";
        ?>
    </div>
    <div class = "errorStatus"><?php echo $errorMessage;?></div>
</body>
</html>