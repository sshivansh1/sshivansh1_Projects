<?php
    require_once "db.php";
    $isSent = false;
    $roleId;
    $loginError = true;
    //It ensures whether the username that's tyring to register is already present in the system
    //It also stores the password into the database as a hash using the PASSWORD_DEFAULT Algorithm
    //Also, validates everything before proceeding to the RegisterUser function to finally register the user
    if(isset($_POST["action"]) && $_POST["action"] == "Register")
    {
        $myUserName = $mysql_connection -> real_escape_string(strip_tags(trim(strtolower($_POST["username"]))));
        $myPassword = $mysql_connection -> real_escape_string(strip_tags(trim($_POST["password"])));
        $myHashPassword = password_hash($myPassword, PASSWORD_DEFAULT);
        $userNameArray = GetUsername();
        if(!(in_array($myUserName, $userNameArray)))
        {
            if(isset($myUserName) && isset($myPassword))
            {
               // error_log("The actual password is $myPassword and hashed password is $myHashPassword");
               $roleId = InsertRoles();
               if($isSent)
                {
                    RegisterUser($myUserName, $myHashPassword, $roleId);
                    SendInformation();
                }
            }
            else
            {
                $mysql_response = "UserName/Password field cannot be empty";
                SendInformation();
            }
        }
        else
        {
            $mysql_response = "There's already a user registered with the same Username.";
            SendInformation();
        }
    }

    //Gets all the information stored in the user and checks it against the information provided by the user
    //Uses password_verify() to check the user provided string password
    //When the password is verified the rolename, rolevalue and username is sent to the session
    if(isset($_POST["action"]) && $_POST["action"] == "Login")
    {
        $myUserName = $mysql_connection -> real_escape_string(strip_tags(trim(strtolower($_POST["username"]))));
        $myPassword = $mysql_connection -> real_escape_string(strip_tags(trim($_POST["password"])));

        $hashPassword = GetUserPassword($myUserName); //The password is retrieved whose username matches with the data in the database

        $userNameArray = GetUsername();
        if(in_array($myUserName, $userNameArray))
        {
            if(password_verify($myPassword, $hashPassword)) //Checks the user entered password with the hash retrieved
            {
                $selQuery = "SELECT `role_name`, `role_value`";
                $selQuery.= " FROM `Roles` r";
                $selQuery.= " JOIN `Users` u";
                $selQuery.= " ON";
                $selQuery.= " r.role_id = u.role_id";
                $selQuery.= " WHERE `username` = '$myUserName'";

                if($result = mysqlQuery($selQuery))
                {
                    while($row = $result -> fetch_assoc())
                    {
                        $roleName = $row['role_name'];
                        $roleValue = $row['role_value'];
                    }
                }
                $_SESSION["username"] = $myUserName;

                if($_SESSION["rootUsername"] == $_SESSION["username"] && $roleName == "Root")
                {
                    $_SESSION["rolename"] = "Root";
                    $_SESSION["rolevalue"] = 100;
                }
                else if($roleName == "Administrator")
                {
                    $_SESSION["rolename"] = "Administrator";
                    $_SESSION["rolevalue"] = 50;
                }
                else if($roleName == "Moderator")
                {
                    $_SESSION["rolename"] = "Moderator";
                    $_SESSION["rolevalue"] = 25;
                }
                else if($roleName == "Member")
                {
                    $_SESSION["rolename"] = "Member";
                    $_SESSION["rolevalue"] = 10;
                }
                else
                {
                    $_SESSION["rolename"] = $roleName;
                    $_SESSION["rolevalue"] = $roleValue;
                }
                $_SESSION["loginError"] = false;
                $mysql_response = "Logging in...";
                $loginError = false;
                SendInformation();
            }
            else
            {
                $mysql_response = "Wrong Username/Password";
                $_SESSION["loginError"] = true;
                $loginError = true;
                SendInformation();
            }
        }
        else
        {
            $mysql_response = "Wrong Username/Password";
            $_SESSION["loginError"] = true;
            $loginError = true;
            SendInformation();
        }
    }

/******************************************************************************************************************
* Method Name : GetUserPassword
* Parameters :  $uName - username
* Return Type : $uPassword - password
* Description :  Helps in returning the retrieved password of the specified username which is used to verify
******************************************************************************************************************/
    function GetUserPassword($uName)
    {
        $query = "SELECT `password` from Users";
        $query.= " WHERE `username` = '$uName'";

        $uPassword;
        if($result = mysqlQuery($query))
        {
            while($row = $result -> fetch_assoc())
            {
                $uPassword = $row['password'];
            }
        }
        
        return $uPassword;
    }

/******************************************************************************************************************
* Method Name : InsertRoles
* Parameters :  None
* Return Type : the last id that was inserted
* Description : Helps in inserting the role values into the table
******************************************************************************************************************/
    function InsertRoles()
    {
        global $isSent, $mysql_connection;

        $query = "INSERT INTO Roles";
        $query.= " (`role_value`) VALUES ('10')";

        $numRows = mysqlNonQuery($query);
        $isSent = true;
        return $mysql_connection -> insert_id;
    }

/******************************************************************************************************************
* Method Name : RegisterUser
* Parameters :  $uName - username
*               $uPass - password
* Return Type : void
* Description : Helps in inserting the username and password into the users table using the query.
******************************************************************************************************************/
    function RegisterUser($uName, $uPass, $roleId)
    {
        global $mysql_connection, $mysql_response, $myDataArray, $isSent, $roleId, $uNameArr;
            $query = "INSERT INTO Users";
            $query.= " (username, password, role_id) VALUES";
            $query.= " ('$uName', '$uPass', '$roleId');";

        $numRows = mysqlNonQuery($query);
        
        $isSent = false;
        $mysql_response = "User Registered Successfully";
    }

/******************************************************************************************************************
* Method Name : SendInformation
* Parameters :  None
* Return Type : void
* Description : Calling it from multiple points where an appropriate message has to be sent to the client
*               to tell him if the operation is successful or not
******************************************************************************************************************/
    function SendInformation()
    {
        global $mysql_response, $loginError, $myDataArray;

        $infoArray = array();
        $infoArray['status'] = $mysql_response;
        $infoArray['loginError'] = $loginError;
        $infoArray['myDataArray'] = $myDataArray; 
        echo json_encode($infoArray);
    }

/******************************************************************************************************************
* Method Name : GetUsername
* Parameters :  none
* Return Type : $uNameArr - an array which has all the usernames stored in the tables
* Description : Helps in getting all the username from users so that they can checked if already present 
*               while registering/logging in.
******************************************************************************************************************/
    function GetUsername()
    {
        $uNameArr = array();
        $query = "SELECT username FROM Users";
        if($result = mysqlQuery($query))
        {
            while($row = $result -> fetch_assoc())
            {
                $uNameArr[] = $row["username"];
            }
        }
        return $uNameArr;
    }
?>