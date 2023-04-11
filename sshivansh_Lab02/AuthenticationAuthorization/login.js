var globalVar;
$('document').ready(function(){
    $('body').on('click', '.registerBtn', RegisterUser);
    $(".loginBtn").click(LoginUser);
});

//******************************************************************************************************************
//Method Name : RegisterUser
//Parameters :  None
//Return Type : void
//Description : Helps in register the user by sending the ajax request when the user clicks register button
//******************************************************************************************************************
function RegisterUser()
{
    let userName = ($("#idUserName").val()).trim();
    let userPassword = ($("#idPassword").val()).trim();
    
    if(userName.length === 0)
    {
        $(".statusPage").html("Failed to register - The UserName field cannot be blank");
    }
    else
    {
        if(userName.length <=3)
        {
            $(".statusPage").html("The username should be at least 4 characters long");
        }
        else
        {
            if(userPassword.length === 0)
            {
                $(".statusPage").html(" Try Again! The password field cannot be left empty.");
            }
            else
            {
                if(userPassword.length < 6)            
                {
                    $("#idPassword").val(String.empty);
                    $(".statusPage").html("The password should be at least six characters long");
                }
                else
                {
                    $(".statusPage").html("");
                    var postData = {};
                    postData["action"] = "Register";
                    postData["username"] = userName;
                    postData["password"] = userPassword;
    
                    AjaxRequest("loginService.php", "post", postData, "json", RegisterSuccess, ErrorMethod);
                }
            }
        }
    }
}

//******************************************************************************************************************
//Method Name : RegisterSuccess
//Parameters :  ReturnedData - returns the data sent by the server
//Return Type : void
//Description : Helps in displaying the status message whether the user can be registered or not
//******************************************************************************************************************
function RegisterSuccess(ReturnedData)
{
    $(".statusPage").html(ReturnedData["status"]); 
}

//******************************************************************************************************************
//Method Name : LoginUser
//Parameters :  None 
//Return Type : void
//Description : Helps in sending an ajax request to the server along with the related information the user is trying to access
//******************************************************************************************************************
function LoginUser()
{
    let userName = ($("#idUserName").val()).trim();
    let userPassword = ($("#idPassword").val()).trim();
    if(userName.length === 0 || userPassword.length === 0)
    {
        $(".statusPage").html("Not a valid Username/Password, Try Again!");
    }
    else
    {
        var postData = {};
        postData["action"] = "Login";
        postData["username"] = userName;
        postData["password"] = userPassword;
        AjaxRequest("loginService.php", "post", postData, "json", LoginSuccess, ErrorMethod);
    }
}

//******************************************************************************************************************
//Method Name : LoginSuccess
//Parameters :  ReturnedData - returns the data sent by the server
//Return Type : void
//Description : Helps in displaying the status message is the login is right or not, if right - Redirect by submitting the form
//******************************************************************************************************************
function LoginSuccess(ReturnedData)
{
    $(".statusPage").html(ReturnedData["status"]);
    if(ReturnedData["loginError"])
    {
        console.log("clicking the login button when error");
    }
    else
    {
        document.getElementById("idLoginForm").submit();
        //$(".loginBtn").attr("type", "submit");
        //document.getElementById('#idLoginBtn').click();
        console.log("Submitted successfully");
    }
}

//******************************************************************************************************************
//Method Name : AjaxRequest
//Parameters :  url, method, data, dataType, successMethod, errorMethod
//Return Type : void
//Description : Helps in sending the ajax request to the server
//******************************************************************************************************************
function AjaxRequest(url, method, data, dataType, successMethod, errorMethod)
{
    var options = {};
    options["url"] = url;
    options["method"] = method;
    options["data"] = data;
    options["dataType"] = dataType;
    options["success"] = successMethod;
    options["error"] = errorMethod;

    $.ajax(options);
}

//******************************************************************************************************************
//Method Name : ErrorMethod
//Parameters :  request - XMLHttpRequest
//              status - a string describing the type of error that occurred
//              errorMessage - optional exception object
//Return Type : void
//Description : A function to be called if the request fails and outputs the errors in the console
//******************************************************************************************************************
function ErrorMethod(request, status, errorMessage) 
{
    console.log(request);
    console.log(status);
    console.log(errorMessage);
}
