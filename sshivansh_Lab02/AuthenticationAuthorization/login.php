<?php
 require_once "db.php";
 
 //When user wants to logout, it destroys and unsets all the variables and we go back to the login page
if(isset($_POST["newSubmit"]) && $_POST["newSubmit"] == "Logout")
 {
    session_unset();
    session_destroy();
    header("Location: index.php");
    die();
 }

 //While logging in if everything is good, it uses the header to redirect the page to index
if(($_SESSION["loginError"]) == false && isset($_SESSION["username"]))
{
    $option = array();
    $option["username"] = $_POST["UserName"];
    $option["password"] = $_POST["Password"];
    header("Location: https://thor.cnt.sast.ca/~sshiv021/draxyBoi/Labby/Lab02/index.php?data=".json_encode($option));
}

?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>
    <script src="login.js"></script>
    <link rel="stylesheet" href="login.css">
    <title>Lab02</title>
</head>
<body>
    <h1>Lab02 - Register / Login</h1>

    <div class = "userInfo">
       <form action="login.php" method="post" id = "idLoginForm">
        <span class = "userName">
            UserName: <input type="text" name="UserName" id="idUserName" placeholder = "Supply a username" style="text-transform:lowercase">
        </span><br>
        <span class = "password">
            Password: <input type="text" name="Password" id="idPassword" placeholder = "supply your password">
        </span><br>
        <span><input type="button" value="Register" class = "registerBtn"></span><br>
        <span><input type="button" name = "newSubmit" value="Login" class = "loginBtn" id = "idLoginBtn"></span><br>
        </form>
    <span class = "statusPage"></span>
    </div>

    <footer>
        Shivansh &#169;2022 <br>
        <script>document.write("Last Modified: " + document.lastModified);</script>
    </footer>
</body>
</html>