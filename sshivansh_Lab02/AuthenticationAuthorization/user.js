var userHeadArr = ["Op", "userID", "UserName", "Hashed Password", "Change Role"];

var myParent;
$(document).ready(function(){
    ShowUsers();
    $("body").on("click", "#idAddUser", AddUser);
    $("body").on("click", "#idDelBtn", DeleteUser);
    $("body").on("click", "#idEditBtn", EditUser);
    $("body").on("click", "#idUpdateBtn", UpdateUser);
    $("body").on("click", "#idCancelBtn", CancelUser);
});

//******************************************************************************************************************
//Method Name : UpdateUser
//Parameters :  None
//Return Type : void
//Description : Helps in sending an ajax request to the server along with the role and user information required to update the tables
//******************************************************************************************************************
function UpdateUser() {
    let myRow = $(this).parent().parent();
    let newRole = $($(myRow).children()[4]).children();
    var postData = {};
    postData["action"] = "UpdateUser";
    postData["userId"] = $(myRow).attr("id");
    postData["roleName"] = $(newRole).children("option:selected").attr("name");
    postData["roleValue"] = $(newRole).children("option:selected").attr("value");
    AjaxRequest("userService.php", "post", postData, "json", UpdateSuccess, ErrorMethod);
}

//******************************************************************************************************************
//Method Name : UpdateSuccess
//Parameters :  ReturnedData - returns the data sent by the server
//Return Type : void
//Description : Helps in displaying the status message as well as showing the updated users if the update request was successful
//******************************************************************************************************************
function UpdateSuccess(ReturnedData) {
    $(".deleteStatus").html(ReturnedData["status"]);
    ShowUsers();
}

//******************************************************************************************************************
//Method Name : ShowUsers
//Parameters :  None
//Return Type : void
//Description : Helps in sending an ajax request to the server which helps in getting all the userinfo from the database
//******************************************************************************************************************
function ShowUsers()
{
    $(".showUserData").empty();
    var postData = {};
    postData["action"] = "ShowUsers";
    AjaxRequest("userService.php", "post", postData, "json", ShowSuccess, ErrorMethod);
}


//******************************************************************************************************************
//Method Name : ShowSuccess
//Parameters :  ReturnedData - returns the data sent by the server
//Return Type : void
//Description : Helps in displaying the data retrieved in form of table and number of records as status message
//******************************************************************************************************************
function ShowSuccess(ReturnedData) 
{
    let roleData = ReturnedData["roleArray"];
    $("#idRole").empty();
    for (const row of roleData) {
        if(row["role_value"] !== "100")
        {
            let option = document.createElement('option');
            $(option).attr("value", row["role_value"]);
            $(option).attr("name", row["role_name"]);
            $(option).html(row["role_name"]);
            $("#idRole").append(option);
        }
    }

    let userData = ReturnedData['data'];
    let table = document.createElement('table');
    let rowHead = document.createElement('tr');
    var count = 0;
    for(const iterator of userHeadArr)
    {
        let tdHead = document.createElement('th');
        tdHead.innerHTML = iterator;
        rowHead.append(tdHead);
    }
    table.append(rowHead);
    for(const iterator of userData)
    {
        
        let tr = document.createElement('tr');
        let tdBtn = document.createElement('td');
        let delBtn = document.createElement('input');
        let editBtn = document.createElement('input');
        $(delBtn).attr("type", "button");
        $(delBtn).attr("value", "Delete");
        $(delBtn).attr("id", "idDelBtn");
        $(editBtn).attr("type", "button");
        $(editBtn).attr("value", "Edit");
        $(editBtn).attr("id", "idEditBtn");
        tdBtn.append(editBtn);
        tdBtn.append(delBtn);
        tr.append(tdBtn);
        for(const item in iterator)
        {
            let td = document.createElement('td');
            td.innerHTML = iterator[item];
            tr.append(td);
        }
        $(tr).attr("id", iterator["user_id"]);
        table.append(tr);
        ++count;
    }
    $(".showUserData").append(table);
    $(".showUserStatus").html("Retrieved " + count+ " user records.");
}

//******************************************************************************************************************
//Method Name : DeleteUser
//Parameters :  None
//Return Type : void
//Description : Helps in sending an ajax request to the server so that user with the desired userId can be deleted
//******************************************************************************************************************
function DeleteUser()
{
    var postData = {};
    postData["action"] = "DeleteUser";
    postData["userId"] = this.parentElement.parentElement.getAttribute("id");
    AjaxRequest("userService.php", "post", postData, "json", DeleteSuccess, ErrorMethod);
}


//******************************************************************************************************************
//Method Name : DeleteSuccess
//Parameters :  ReturnedData - returns the data sent by the server
//Return Type : void
//Description : Helps in displaying the status message and the refreshed user table if any rows are deleted
//******************************************************************************************************************
function DeleteSuccess(ReturnedData)
{
    $(".deleteStatus").html(ReturnedData["status"]);
    ShowUsers();
}

//******************************************************************************************************************
//Method Name : EditUser
//Parameters :  None
//Return Type : void
//Description : Helps in sending an ajax request to the server along with the role information required to edit the user
//******************************************************************************************************************
function EditUser()
{
    $(".deleteStatus").empty();
    myParent = this.parentElement.parentElement;
    let myRow = $(this).parent().parent();
    let myRole = $(myRow).children()[4];
    let roleValue;
    if(myRole.innerHTML == "Moderator")
    {
        roleValue = 25;
    }
    else if(myRole.innerHTML == "Member")
    {
        roleValue = 10;
    }
    else if(myRole.innerHTML == "Administrator")
    {
        roleValue = 50;
    }
    else if(myRole.innerHTML == "Root")
    {
        roleValue = 100;
    }
    var postData = {};
    postData["action"] = "EditUser";
    postData["roleValue"] = roleValue;

    AjaxRequest("userService.php", "post", postData, "json", EditSuccess, ErrorMethod);
}


//******************************************************************************************************************
//Method Name : EditSuccess
//Parameters :  ReturnedData - returns the data sent by the server
//Return Type : void
//Description : Helps in editing the desired row if the user have the permissions
//******************************************************************************************************************
function EditSuccess(ReturnedData)
{
    
    if(ReturnedData["canEdit"])
    {
    btnCell = myParent.children[0];
    roleCell = myParent.children[4];
    selRole = myParent.children[4].innerHTML;
    roleCell.innerHTML = "";
    $(btnCell).children("#idEditBtn").val("Update");
    $(btnCell).children("#idEditBtn").attr("id", "idUpdateBtn");
    // $(btnCell).children("#idEditBtn").siblings().val("Cancle");
    $(btnCell).children("#idDelBtn").val("Cancel");
    $(btnCell).children("#idDelBtn").attr("id", "idCancelBtn");

    
     let select = document.createElement('select');
     for (const element of ReturnedData["roleArray"]) {
        if(element["role_value"] !== "100")
        {
            let option = document.createElement('option');
            if(selRole == element["role_name"])
            {
                option.setAttribute("selected", "selected");
            }   
            option.innerHTML = element["role_name"];
            option.setAttribute("value", element["role_value"]);
            option.setAttribute("name", element["role_name"]);
            select.append(option);
        }
     }
     $(roleCell).append(select);
    }
    else
    {
        $(".deleteStatus").html(ReturnedData["status"]);
    }
}

//******************************************************************************************************************
//Method Name : CancelUser
//Parameters :  None
//Return Type : void
//Description : Helps in sending an ajax request to the server along with the userId which the user was trying to edit
//******************************************************************************************************************
function CancelUser()
{
    myParent = this.parentElement.parentElement;
    let uId = $(myParent).attr("id");
    let postData = {};
    postData["action"] = "CancelUser";
    postData["userId"] = uId;

    AjaxRequest("userService.php", "post", postData, "json", CancelSuccess, ErrorMethod);
}

//******************************************************************************************************************
//Method Name : CancelSuccess
//Parameters :  ReturnedData - returns the data sent by the server
//Return Type : void
//Description : This method helps in changing the buttons back to edit and delete and rolling the rolevalue back to the original
//******************************************************************************************************************
function CancelSuccess(ReturnedData)
{
    myParent.children[0].children[0].value = "Edit";
    myParent.children[0].children[0].setAttribute("id", "idEditBtn");
    myParent.children[0].children[1].value = "Delete";
    myParent.children[0].children[1].setAttribute("id", "idDelBtn");
    myParent.children[4].innerHTML = ReturnedData;
}

//******************************************************************************************************************
//Method Name : AddUser
//Parameters :  None
//Return Type : void
//Description : Helps in sending an ajax request to the server along with the information 
//******************************************************************************************************************
function AddUser() 
{
    var postData = {};
    userVal = ($("#idUser").val()).trim();
    passVal = ($("#idPassword").val()).trim();
    roleVal = $("#idRole").val();
    postData["action"] = "AddUser";
    postData["username"] = $("#idUser").val();
    postData["password"] = $("#idPassword").val();
    postData["role"] = $("#idRole").val();
    postData["roleName"] = $("#idRole").children("option:selected").attr("name");
    $(".deleteStatus").empty();
    if(userVal.length === 0)
    {
        $(".addUserStatus").html("This is not a valid username");
    }
    else if(passVal.length < 6)
    {
        $(".addUserStatus").html("Password should be at least six characters.");
    }
    else if(roleVal == null)
    {
        $(".addUserStatus").html("Please assign a role to the user.");
    }
    else
    {
        $(".addUserStatus").html("");
        AjaxRequest("userService.php", "post", postData, "json", AddUserSuccess, ErrorMethod);
    }
}


//******************************************************************************************************************
//Method Name : AddUserSuccess
//Parameters :  ReturnedData - returns the data sent by the server
//Return Type : void
//Description : Helps in displaying the status as well as dipslaying the tables with updated users
//******************************************************************************************************************
function AddUserSuccess(ReturnedData) 
{
    $(".addUserStatus").html(ReturnedData["status"]);
    ShowUsers();
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

//******************************************************************************************************************
//Method Name : AjaxRequest
//Parameters :  url, method, data, dataType, successMethod, errorMethod
//Return Type : void
//Description : Helps in sending the ajax request to the server
//******************************************************************************************************************
function AjaxRequest(url, method, data, dataType, successMethod, errorMethod) {
    var options = {};
    options["url"] = url;
    options["method"] = method;
    options["data"] = data;
    options["dataType"] = dataType;
    options["success"] = successMethod;
    options["error"] = errorMethod;

    $.ajax(options);
}