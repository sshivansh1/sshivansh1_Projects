<?php
    require_once "db.php";
    $roleArray = array();
    $currRoleValue = $_SESSION["rolevalue"];
    $canEdit = false;

/******************************************************************************************************************
/* Description : Helps in displaying the roles by retreiving everything in the roles table
******************************************************************************************************************/
    if(isset($_POST["action"]) && $_POST["action"] == "ShowRoles")
    {
        $query = "SELECT `role_id`, `role_name`, `desc`, `role_value` FROM Roles";
        if($result = mysqlQuery($query))
        {
            while($row = $result->fetch_assoc())
            {
                $roleArray[] = $row;
            }
        }
        SendInformation();
    }

/******************************************************************************************************************
/* Description : Helps in adding the roles with the information sent by the user, if the user have the adding permissions
******************************************************************************************************************/
    if(isset($_POST["action"]) && $_POST["action"] == "AddRoles")
    {
        $roleName = $mysql_connection -> real_escape_string(strip_tags(trim($_POST["roleName"])));
        $roleValue = $mysql_connection -> real_escape_string(strip_tags(trim($_POST["roleValue"])));
        $roleDesc = $mysql_connection -> real_escape_string(strip_tags(trim($_POST["roleDesc"])));

        $newRoleArray = GetRoles();
        foreach ($newRoleArray as $item) {
            $newRoleNameArray[] = $item["role_name"];
            $newRoleValueArray[] = $item["role_value"];
        }
        if(!in_array($roleName, $newRoleNameArray))
        {
            if(!in_array($roleValue, $newRoleValueArray))
            {
                $query = "INSERT INTO Roles";
                $query.= " (`role_name`, `desc`, `role_value`) VALUES";
                $query.= " ('$roleName', '$roleDesc', '$roleValue')";

                if($currRoleValue > $roleValue)
                {
                    $numRows = mysqlNonQuery($query);
                    $mysql_response = "$numRows row Updated";
                    SendInformation();
                }
                else
                {
                    $mysql_response = "You cannot change the value of the role to $roleValue";
                    SendInformation();
                }
            }
            else
            {
                $mysql_response = "There's already a $roleName present with a value of $roleValue";
                SendInformation();
            }
        }
        else
        {
            $mysql_response = "There's already a $roleName present";
            SendInformation();
        }
    }

/******************************************************************************************************************
/* Description : Helps in deleting the roles using the roleId sent by the user, if the user have deleting permissions
******************************************************************************************************************/
    if(isset($_POST["action"]) && $_POST["action"] == "DeleteRoles")
    {
        $roleId = $_POST["roleId"];
        if($roleId == 1)
        {
            $mysql_response = "Sorry you cannot delete the root role.";
            SendInformation();
        }
        else
        {
            DeleteRole($roleId);
            SendInformation();
        }
    }

/******************************************************************************************************************
/* Description : Helps in editing the roles by sending the information regarding
                 whether the user is permitted to edit the role (boolean type)
******************************************************************************************************************/
    if(isset($_POST["action"]) && $_POST["action"] == "EditRoles")
    {
        $roleValue = $_POST["rolevalue"];

        if($currRoleValue > $roleValue)
        {
            $canEdit = true;
            SendInformation();
        }
        else
        {
            $canEdit = false;
            $mysql_response = "Sorry, you don't have the permission to edit this role";
            SendInformation();
        }
    }

/******************************************************************************************************************
/* Description : Helps in sending the prestored information of the role on which the cancel request was asked
******************************************************************************************************************/
    if(isset($_POST["action"]) && $_POST["action"] == "CancelRole")
    {
        $roleId = $_POST["roleId"];

        $query = "SELECT `role_name`, `desc`, `role_value`";
        $query.= " FROM Roles";
        $query.= " WHERE `role_id` = '$roleId'";
        
        $roleArray = array();
        if($result = mysqlQuery($query))
        {
            while($row = $result -> fetch_assoc())
            {
                $roleArray[] = $row;
            }

            echo json_encode($roleArray);
        }
    }

/******************************************************************************************************************
/* Description : Helps in updating the roles with the information sent by the user, if the user have deleting permissions
******************************************************************************************************************/
    if(isset($_POST["action"]) && $_POST["action"] == "UpdateRole")
    {
        $roleName = $mysql_connection -> real_escape_string(strip_tags(trim($_POST["rolename"])));
        $roleDesc = $mysql_connection -> real_escape_string(strip_tags(trim($_POST["roledesc"])));
        $roleValue = $mysql_connection -> real_escape_string(strip_tags(trim($_POST["rolevalue"])));
        $roleId = $_POST["roleid"];
        if($currRoleValue > $roleValue)
        {
            $query = "Update Roles";
            $query.= " SET `role_name` = '$roleName',";
            $query.= "  `desc` = '$roleDesc',";
            $query.= " `role_value` = '$roleValue'";
            $query.= " WHERE `role_id` = '$roleId';";

            $numRows = mysqlNonQuery($query);
            $mysql_response = "Rows Updated : $numRows";
            SendInformation();
        }
        else
        {
            $mysql_response = "Sorry you are not authorized to promote the role's value to $roleValue";
            SendInformation();
        }
    }

/******************************************************************************************************************
* Method Name : DeleteRole
* Parameters :  $roleId - the role id that the user wants to remove
* Return Type : void
* Description : Helps in deleting the roles as well as user linked to that role, if the user have deleting permission
******************************************************************************************************************/
    function DeleteRole($roleId)
    {
        global $currRoleValue, $mysql_response;
        $query = "DELETE FROM Roles";
        $query.= " WHERE `role_id` = '$roleId'";

        $userQuery = "DELETE FROM Users";
        $userQuery.= " WHERE `role_id` = '$roleId'";

        $selQuery = "Select role_value FROM Roles";
        $selQuery.= " WHERE `role_id` = '$roleId'";

        if($result = mysqlQuery($selQuery))
        {
            while($row = $result -> fetch_assoc())
            {
                $roleValue = $row["role_value"];
            }
            if($currRoleValue > $roleValue)
            {
                mysqlNonQuery($userQuery);
                $canDelete = true;

                if($canDelete)
                {
                    $numRows = mysqlNonQuery($query);
                    $canDelete = false;
                    $mysql_response = "Rows Deleted: $numRows";
                }
            }
            else
            {
                $mysql_response = "You don't have the permission to delete this user";
            }
        }
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
        global $roleArray, $mysql_response, $canEdit;

        $infoArray = array();
        $infoArray["dataArray"] = $roleArray;
        $infoArray["status"] = $mysql_response;
        $infoArray["canEdit"] = $canEdit;

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

