<?php
    session_start();
    if(!isset($_SESSION["username"]) || ($_SESSION["rolename"] == "Member") || ($_SESSION["rolename"] == "Moderator"))
    {
        header("Location: index.php");
        $_SESSION["errorMessage"] = "Sorry, you are not allowed to access the Roles Management page";
        die();
    }
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>
    <script src="role.js"></script>
    <link rel="stylesheet" href="role.css">
    <title>Role Management</title>
    <style>
        footer, h1
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <h1>Lab02 - Role Management - <?php echo $_SESSION["rolename"];?></h1> 
    <div class = "tableDiv">

    </div>

    <div class = "addRoleDiv">
        <label for="idRoleName">Role Name: &nbsp; &nbsp;</label>
        <input type="text" id="idRoleName" placeholder= "supply a rolename"><br>
        <label for="idDesc">Description :</label>
        <input type="text" id="idDesc" placeholder= "description"><br>
        <label for="idRoleValue">Role Value :</label>
        <input type="number" id="idRoleValue" placeholder= "role value" min = "1" max = "99" value="10"><br>
        <input type="button" value="Add Role" id = "idAddRole">
    </div>

    <div class ="statusDiv">

    </div>
    <div class ="deleteStatus">

    </div>

    <footer><a href="index.php">Index</a></footer>
</body>
</html>