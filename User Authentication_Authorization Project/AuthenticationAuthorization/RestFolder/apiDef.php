<?php
require_once "db.php";
class MyAPI
{
    private $method = '';     // storing the HTTP method by which the request was received
    private $endpoint = '';   // storing the sought after endpoint function in the extended class
    private $verb = '';       // storing a contextualizing piece of information to be used with the method
    private $args = Array();  // storing all of the rest of the information passed in the URI
    private $file = Null;     // storing information extracted from the "file" accompanying a PUT/DELETE method      
    private $cleanedData;

    public function __construct($request)
    {
        header("Content-Type: application/json");

        $this->parseRequest($request);

        $this->method = $_SERVER['REQUEST_METHOD'];     // Retrieve the method used to submit the AJAX request 

        switch($this->method)
        {
            case 'DELETE':
                parse_str(file_get_contents("php://input"), $this->file);
                $this->cleanedData = $this->_cleanInputs($this->file);
                break;
            case 'POST':
                parse_str(file_get_contents("php://input"), $this->file);
                $this->cleanedData = $this->_cleanInputs($_POST);
                break;
            case 'GET':
                parse_str(file_get_contents("php://input"), $this->file);
                $this->cleanedData = $this->_cleanInputs($_GET);
                break;
            case 'PUT':
                parse_str(file_get_contents("php://input"), $this->file);
                $this->cleanedData = $this->_cleanInputs($this->file);
                break;
            default:
                $this->_response('Invalid Method', 405);
                break; 
        }
    }

    private function parseRequest($request)
    {
        // Remove a trailing '/' if there is one, then separate the URI by '/' chars
        // Store the result in args member
        $this->args = explode('/', rtrim($request, '/'));   

        // Remove the the first item of the array and store it as the endpoint
        $this->endpoint = array_shift($this->args);

        // If any items remain and the first is not a numeric value, store it as the verb
        if (array_key_exists(0, $this->args)/* && !is_numeric($this->args[0])*/) {
            $this->verb = array_shift($this->args);
        }
    } 

    private function _cleanInputs($data) {

        $clean_input = Array();
        if (is_array($data)) {
            foreach ($data as $k => $v) {
                $clean_input[$k] = $this->_cleanInputs($v);
            }
        } else {
            $clean_input = trim(strip_tags($data));
        }
        return $clean_input;
    }

    //The end point which helps in performing all the requested functions by the user
    //depending on the type of the request requested by the user
    //and the information sent along with it
    private function example()
    {
        global $mysql_connection, $mysql_response;

        //Used to retrieve all the messages based on whether the filter was supplied to it or not
        //Returns an array which is further used to display the information to the client
        if ($this->method == 'GET') 
        {
            $verbValue = $mysql_connection -> real_escape_string(strip_tags(trim($this->verb)));

            if(strlen($verbValue) > 0)
            {
                $query = "SELECT m.MessageID, u.username, m.Message, m.Timestamp";
                $query.= " FROM Messages m";
                $query.= " JOIN Users u";
                $query.= " ON u.user_id = m.user_id";
                $query.= " WHERE u.username like '%$verbValue%'";
                $query.= " OR m.Message like '%$verbValue%'";
            }
            else
            {
                $query = "SELECT m.MessageID, u.username, m.Message, m.Timestamp";
                $query.= " FROM Messages m";
                $query.= " JOIN Users u";
                $query.= " ON u.user_id = m.user_id";
            }
            $newArray = array();
            if($result = mysqlQuery($query))
            {
                while($row = $result -> fetch_assoc())
                {
                    $newArray[] = $row;   
                }

                if(mysqli_num_rows($result) == 0)
                {
                    return "Nothing retrieved for the filter value $verbValue";
                }
                else
                {
                    return $newArray;
                }
            }
            else
            {
                return $mysql_response;
            }
        }

        //using this for insert
        //Helps in inserting a new message in the messages table when a POST request was requested by the user
        else if ($this->method == 'POST')
        {
            $url = $_SERVER["HTTP_REFERER"];
            $url_components = parse_url($url);
            parse_str($url_components['query'], $params);
            $newArray = (array)(json_decode($params["data"]));
            $logRoleValue = $newArray["rolevalue"];
            $logUsername = $newArray["username"];

            $retQuery = "SELECT `user_id` FROM Users WHERE `username` = '$logUsername'";

            if($result = mysqlQuery($retQuery))
            {
                while($row = $result -> fetch_assoc())
                {
                    $logUserId = $row["user_id"];
                }

                $cleanedData = $this->cleanedData;
                $userMessage = $mysql_connection -> real_escape_string(strip_tags(trim($cleanedData["userMessage"])));
                
                $query = "INSERT INTO Messages";
                $query.= " (`Message`, `user_id`) VALUES ('$userMessage', '$logUserId')";
                
                if(!$numrows = mysqlNonQuery($query))
                {
                    return $mysql_response;
                }

                return "$numrows row(s) added successfully";
            }
            else
            {
                return "Internal Server Error";
            }
        } 

        //using this for updating the message
        //Helps in updating a message in the messages table when a PUT request is requested by the user
        else if ($this->method == 'PUT'){
            // error_log("testing Args: ".json_encode($this->args));
            // error_log("testing endPoint: ".json_encode($this->endpoint));
            // error_log("testing Verb: ".json_encode($this->verb));
            // error_log("testing cleanedData: ".json_encode($this->cleanedData));
            // error_log("testing file: ".json_encode($this->file));
            // error_log("testing post: ".json_encode($_POST));
            // error_log("testing get: ".json_encode($_GET));
            // error_log("the update request is : ".json_encode($_REQUEST));

            //Helps to get a peice of the information sent along with the url
            $url = $_SERVER["HTTP_REFERER"]; 
            $url_components = parse_url($url);
            parse_str($url_components['query'], $params);
            $newArray = (array)(json_decode($params["data"]));
            $logRoleValue = $newArray["rolevalue"];
            $logUsername = $newArray["username"];
            
            $cleanedData = $this->cleanedData;
            $action = $cleanedData["action"];
            $rMessageId = $this->args;
            $myMessageId = intval($rMessageId[0]);
            $messageValue = $cleanedData["messageValue"];
            $username = $cleanedData["username"];
            $selQuery = "SELECT `role_id` from Users";
            $selQuery.= " WHERE `username` = '$username'";

            if($result = mysqlQuery($selQuery))
            {
                while($row = $result -> fetch_assoc())
                {
                    $roleId = $row["role_id"];
                }

                $rValQuery = "SELECT `role_value` FROM Roles";
                $rValQuery.= " WHERE `role_id` = '$roleId'";

                $newResult = mysqlQuery($rValQuery);

                while($row = $newResult -> fetch_assoc())
                {
                    $roleValue = $row["role_value"];
                }
                if($logRoleValue == $roleValue)
                {

                    $query = "UPDATE Messages";
                    $query.= " SET `Message` = '$messageValue'";
                    $query.= " WHERE `MessageID` = '$myMessageId'";

                    if(!$numrows = mysqlNonQuery($query))
                    {
                        return $mysql_response;
                    }
                    return "$numrows row(s) updated successfully";
                }
                else
                {
                    return "Sorry you are not authorized to update this user's message";
                }
            }
            else
            {
                return "Internal server error";
            }
        } 
        //using this condition for deleting the message
        //Helps in deleting a message in the messages table when a DELETE request is requested by the user
        else if ($this->method == 'DELETE')
        {
            //Helps to get a peice of the information sent along with the url
            $url = $_SERVER["HTTP_REFERER"];
            $url_components = parse_url($url);
            parse_str($url_components['query'], $params);
            $newArray = (array)(json_decode($params["data"]));
            $logRoleValue = $newArray["rolevalue"];

            $cleanedData = $this->cleanedData;
            $rMessageId = $this->args;
            $myMessageId = intval($rMessageId[0]);
            $username = $cleanedData["username"];

            $selQuery = "SELECT `role_id` from Users";
            $selQuery.= " WHERE `username` = '$username'";

            if($result = mysqlQuery($selQuery))
            {
                while($row = $result -> fetch_assoc())
                {
                    $roleId = $row["role_id"];
                }
                
                $rValQuery = "SELECT `role_value` FROM Roles";
                $rValQuery.= " WHERE `role_id` = '$roleId'";

                $newResult = mysqlQuery($rValQuery);

                while($row = $newResult -> fetch_assoc())
                {
                    $roleValue = $row["role_value"];
                }

                //Only if the current logged in user has the right authorization
                if($logRoleValue >= $roleValue)
                {
                    if(is_numeric($myMessageId))
                    {
                        $query = "DELETE FROM Messages";
                        $query.= " WHERE `MessageID` = '$myMessageId'";
                        $numRows = mysqlNonQuery($query);
                        return $numRows." row(s) deleted successfully";
                    }
                    else
                    {
                        return "The message Id should be numeric";
                    }
                }
                else
                {
                    return "Sorry you are not authorized to delete the user's message";
                }
            }
            else
            {
                return "Internal Server error";
            }
        }
        else
        {
            return "Invalid Request Type!";
        }
    }

    public function processAPI() {
        if (method_exists($this, $this->endpoint)) {
            return $this->_response($this->{$this->endpoint}($this->args));
        }
        return $this->_response("No Endpoint: $this->endpoint", 404);
    }
    
    private function _response($data, $status = 200) {
        header("HTTP/1.1 " . $status . " " . $this->_requestStatus($status));
        return json_encode($data);
    }

    private function _requestStatus($code) {
        $status = array(  
            200 => 'OK',
            404 => 'Not Found',   
            405 => 'Method Not Allowed',
            500 => 'Internal Server Error',
        ); 
        return ($status[$code])?$status[$code]:$status[500]; 
    }  
}
?>
