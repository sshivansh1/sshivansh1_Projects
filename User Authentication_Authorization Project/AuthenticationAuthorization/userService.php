<?php
    require_once "db.php";

    $userDataArr = array();
    $userRoleValue = $_SESSION["rolevalue"];
    $isSent = false;
    $canEdit = false;
    $roleArray = array();
/******************************************************************************************************************
/* Description : Helps in displaying the users by retreiving everything stored in the users table using ShowUsers()
******************************************************************************************************************/
    if(isset($_POST["action"]) && $_POST["action"] == "ShowUsers")
    {
        $roleArray = GetRoles();
        $userDataArr = ShowUsers();
        SendInformation();
    }

/******************************************************************************************************************
/* Description : Helps in deleting the users using the userId sent by the user, if the user have deleting permissions
******************************************************************************************************************/    
    if(isset($_POST["action"]) && $_POST["action"] == "DeleteUser")
    {
        $userId = $_POST["userId"];
        if($userId == 100)
        {
            $mysql_response = "Sorry you cannot delete the root user.";
            SendInformation();
        }
        else
        {
            DeleteUser($userId);
            SendInformation();
        }
    }

/******************************************************************************************************************
/* Description : Helps in editing the users by sending the information regarding
                 whether the user is permitted to edit (boolean type)
******************************************************************************************************************/
    if(isset($_POST["action"]) && $_POST["action"] == "EditUser")
    {
        $roleVal = $_POST["roleValue"];
        if($userRoleValue > $roleVal)
        {
            $roleArray = GetRoles();
            $mysql_response = "you can edit!";
            $canEdit = true;
            SendInformation();
        }
        else
        {
            $canEdit = false;
            $mysql_response = "Sorry you don't have permissions to edit this user";
            SendInformation();
        }

    }
/******************************************************************************************************************
/* Description : Helps in sending the prestored information by retrieving the role name information linked to the userid
                 on which the cancel request was asked
******************************************************************************************************************/    
    if(isset($_POST["action"]) && $_POST["action"] == "CancelUser")
    {
        $uId = $_POST["userId"];

        $query = "SELECT r.role_name";
        $query.= " FROM Roles r";
        $query.= " JOIN Users u";
        $query.= " ON";
        $query.= " r.role_id = u.role_id";
        $query.= " WHERE u.user_id = '$uId'";

        if($result = mysqlQuery($query))
        {
            while($row = $result -> fetch_assoc())
            {
                $roleName = $row["role_name"]; 
            }
        }
        
        echo json_encode($roleName);
    }
/******************************************************************************************************************
/* Description : Helps in adding the user with the information sent by the user, if the user have the adding permissions
                Also, stores the password as hash
******************************************************************************************************************/
    if(isset($_POST["action"]) && $_POST["action"] == "AddUser")
    {
       $myUser = $mysql_connection -> real_escape_string(strip_tags(trim(strtolower($_POST["username"]))));
       $myPassword = $mysql_connection -> real_escape_string(strip_tags(trim($_POST["password"])));
       $myRole = $mysql_connection -> real_escape_string(strip_tags(trim($_POST["role"])));//no need but still
       $myRoleName = $mysql_connection -> real_escape_string(strip_tags(trim($_POST["roleName"])));
       $hashPassword = password_hash($myPassword, PASSWORD_DEFAULT);
       $userNameArray = GetUsername();
       foreach ($userNameArray as $item) {
            $uNameArr[] = $item["username"];
        }

        if(!(in_array($myUser, $uNameArr)))
        {
            if($userRoleValue > $myRole)
            {
                $roleId = AddRole($myRoleName, $myRole);
                if($isSent)
                {
                    AddUser($myUser, $hashPassword, $roleId);
                    SendInformation();
                }
            }
            else
            {
                $mysql_response = "Sorry you cannot promote the user to $myRoleName";
                SendInformation();
            }
        }
        else
        {
            $mysql_response = "Oops, There's already a user present with the same username";
            SendInformation();
        }
    }
/******************************************************************************************************************
/* Description : Helps in updating the users' rolename and rolevalue
                 with the information sent by the user, if the user have the editing permissions
******************************************************************************************************************/
    if(isset($_POST["action"]) && $_POST["action"] == "UpdateUser")
    {
        $userId = $_POST["userId"];
        $newRoleName = $_POST["roleName"];
        $newRoleValue = $_POST["roleValue"];
        $selQuery = "SELECT r.role_id, r.role_value, r.role_name";
        $selQuery.= " from Roles r";
        $selQuery.= " JOIN Users u";
        $selQuery.= " ON r.role_id = u.role_id";
        $selQuery.= " WHERE u.user_id = '$userId'";

        if(($userId != "100"))
        {
            if($result = mysqlQuery($selQuery))
            {
                while($row = $result-> fetch_assoc())
                {
                    $roleId = $row["role_id"];
                    $roleName = $row["role_name"];
                }

                if($userRoleValue > $newRoleValue)
                {
                    $query = "Update `Roles`";
                    $query.= " Set `role_name` = '$newRoleName',";
                    $query.= " `role_value` = '$newRoleValue'";
                    $query.= " Where `role_id` = '$roleId'";

                    $numRows = mysqlNonQuery($query);
                    $mysql_response = "$numRows row Updated";
                    SendInformation();
                }
                else
                {
                    $mysql_response = "Sorry you are not allowed to promote the user to this role";
                    SendInformation();
                }
            }
            else
            {
                $mysql_response = "Query Error";
                SendInformation();
            }
        }
        else
        {
            $mysql_response = "Sorry, you cannot change the role of the root user";
            SendInformation();  
        }
    }
/******************************************************************************************************************
* Method Name : ShowUsers
* Parameters :  none
* Return Type : void
/* Description : Helps in displaying the users by retreiving everything in the users table and the role assigned to it
******************************************************************************************************************/
    function ShowUsers()
    {
        $userArray = array();
        $query = "SELECT `user_id`, `username`, `password`, `role_name`";
        $query.= " FROM `Users` u";
        $query.= " JOIN `Roles` r";
        $query.= " ON";
        $query.= " u.role_id = r.role_id";
        $query.= " ORDER BY `username`";

        if($result = mysqlQuery($query))
        {
            while($row = $result -> fetch_assoc())
            {
                $userArray[] = $row; 
            }
        }
        return $userArray;
    }
/******************************************************************************************************************
* Method Name : DeleteUser
* Parameters :  $userId - the id of user that is requested to be deleted
* Return Type : void
/* Description : Helps in deleting the users using the userId sent by the user, if the user have deleting permissions
******************************************************************************************************************/
    function DeleteUser($userId)
    {
        global $mysql_response, $userRoleValue;
        $query = "DELETE FROM Users";
        $query.= " WHERE `user_id` = '$userId'";    

        $roleQuery = "SELECT r.role_value, r.role_id FROM `Roles` r";
        $roleQuery.= " JOIN `Users` u on";
        $roleQuery.= " r.role_id = u.role_id";
        $roleQuery.= " WHERE `user_id` = '$userId'";

        if($result = mysqlQuery($roleQuery))
        {
            while($row = $result -> fetch_assoc())
            {
                $currRoleValue = $row["role_value"];
                $currRoleId = $row["role_id"];
            }
            if($userRoleValue > $currRoleValue)
            {
                $numRows = mysqlNonQuery($query);
                $mysql_response = "Number of rows deleted : ".$numRows;
                $canDeleteRole = true;
                if($canDeleteRole)
                {
                    $roleDelQuery = "Delete FROM Roles";
                    $roleDelQuery.= " WHERE role_id = '$currRoleId'";

                    mysqlNonQuery($roleDelQuery);
                    $canDeleteRole = false;
                }
            }    
            else
            {
                $mysql_response = "You don't have the priviliges to delete this user";
            }
        }
    }
/******************************************************************************************************************
* Method Name : AddUser
* Parameters :  $userName - the username of the user
                $userPassword - the password provided by the user
                $roleId - the roleId that you want it to link to
* Return Type : void
/* Description : Helps in adding the users into the Users table with the specified information sent by the user, 
                if the user have the adding permissions
******************************************************************************************************************/
    function AddUser($userName, $userPassword, $roleId)
    {
        global $isSent, $mysql_connection, $mysql_response;
        
        $query = "INSERT INTO Users";
        $query.= " (`username`, `password`, `role_id`) VALUES";
        $query.= " ('$userName', '$userPassword', '$roleId')";

        $numRows = mysqlNonQuery($query);
        $isSent = false;
        $mysql_response = "User Added Successfully";
    }
/******************************************************************************************************************
* Method Name : AddRole
* Parameters :  $roleName - the name of the role
                $roleValue - the value of that particular role 
* Return Type : the last id that was inserted
/* Description : Helps in adding the roles with the information sent by the user, if the user have the adding permissions
******************************************************************************************************************/
    function AddRole($roleName, $roleValue)
    {
        global $isSent, $mysql_connection;
        
        $query = "INSERT INTO Roles";
        $query.= " (`role_name`, `desc`, `role_value`) VALUES";
        $query.= " ('$roleName', 'N/A', '$roleValue')";
        $numRows = mysqlNonQuery($query);
        $isSent = true;
        return $mysql_connection -> insert_id;
    }

/******************************************************************************************************************
* Method Name : GetUsername
* Parameters :  none
* Return Type : $uNameArr - an array which has all the usernames stored in the tables
* Description : Helps in getting all the username from users so that they can checked if already present 
******************************************************************************************************************/
    function GetUsername()
    {
        $uNameArr = array();
        $query = "SELECT username FROM Users";
        if($result = mysqlQuery($query))
        {
            while($row = $result -> fetch_assoc())
            {
                $uNameArr[] = $row;
            }
        }
        return $uNameArr;
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
        global $userDataArr, $mysql_response, $canEdit, $roleArray;
        $infoArray = array();
        $infoArray["data"] = $userDataArr;
        $infoArray["status"] = $mysql_response;
        $infoArray["canEdit"] = $canEdit;
        $infoArray["roleArray"] = $roleArray;
        echo json_encode($infoArray);
    }
/******************************************************************************************************************
* Method Name : GetRoles
* Parameters :  none
* Return Type : $roleArray - An array consisting of objects with role_name and role_value as the properties 
* Description : Helps in retrieving all the role name and values while refreshing the page
******************************************************************************************************************/  
function GetRoles()
{
    $query = "SELECT DISTINCT role_name, role_value from Roles";

    $roleArray = array();
    if($result = mysqlQuery($query))
    {
        while($row = $result -> fetch_assoc())
        {
            $roleArray[] = $row;
        }
    }   
    return $roleArray;
}
?>