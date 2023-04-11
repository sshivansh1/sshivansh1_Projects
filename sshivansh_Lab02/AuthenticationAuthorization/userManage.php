<?php
    session_start();
    if(!isset($_SESSION["username"]) || ($_SESSION["rolename"] == "Member"))
    {
        header("Location: login.php");
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
    <script src="user.js"></script>
    <link rel="stylesheet" href="user.css">
    <title>User Management</title>
</head>
<body>
    <h1>Lab02 - User Management - <?php echo $_SESSION["rolename"] ?></h1>

    <div class = "addUserData">
        <label for="idUser">Username: <input type="text" name="" id="idUser" style="text-transform:lowercase"></label> <br>
        <label for="idPassword">Password: <input type="text" name="" id="idPassword"></label> <br>
        <label for="idRole">Role:
             <select name="roleSelect" id="idRole">
                <!-- <option disabled hidden selected>Choose the Role name</option>
                <option value="25" name = "Moderator">Moderator</option>
                <option value="50" name = "Administrator">Administrator</option>
                <option value="10" name = "Member">Member</option> -->
            </select>
        </label><br>
        <button id = "idAddUser">Add User</button><br><br>
        <span class = "addUserStatus">

        </span>
    </div>

    <div class = "showUserData">
    </div>
        
    <div class = "showUserStatus">
    </div>
    <div class = "deleteStatus"></div>

    <footer><a href="index.php">Index</a></footer>
</body>
</html>