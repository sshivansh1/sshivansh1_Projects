var myParent;
$(document).ready(function(){
    ShowRoles();
    $("#idAddRole").click(AddRoles);
    $("body").on("click", "#idDelBtn", DeleteRoles);
    $("body").on("click", "#idEditBtn", EditRoles);
    $("body").on("click", "#idCancelBtn", CancelRoles);
    $("body").on("click", "#idUpdateBtn", UpdateRoles);
});

//******************************************************************************************************************
//Method Name : UpdateRoles
//Parameters :  none
//Return Type : void
//Description : Helps in sending an ajax request with the required information to update the roles
//******************************************************************************************************************
function UpdateRoles()
{
    let myRow = $(this).parent().parent();
    let roleId = $(myRow).attr("id");
    let newRoleName = $($(myRow).children()[2]).children().val().trim();
    let newRoleDesc = $($(myRow).children()[3]).children().val().trim();
    let newRoleValue = $($(myRow).children()[4]).children().val().trim();

    if(newRoleName.length === 0)
    {
        $(".deleteStatus").html("The Role Name field cannot be empty, Try Again");
    }
    else
    {
        if(newRoleValue.length === 0)
        {
            $(".deleteStatus").html("The Role value cannot be empty, Try Again");
        }
        else
        {
            if(newRoleDesc.length === 0)
            {
                $($(myRow).children()[3]).children().val("N/A"); 
            }

            if((newRoleValue >= 1 && newRoleValue <=99) && newRoleValue.length > 0)
            {
                var postData = {};
                postData["action"] = "UpdateRole";  
                postData["roleid"] = roleId;
                postData["rolename"] = newRoleName;
                postData["roledesc"] = newRoleDesc;
                postData["rolevalue"] = newRoleValue;

                AjaxRequest("roleService.php", "post", postData, "json", UpdateRoleSuccess, ErrorMethod);
            }
        }
    }
}

//******************************************************************************************************************
//Method Name : UpdateRoleSuccess
//Parameters :  ReturnedData - returns the data sent by the server
//Return Type : void
//Description : Helps in displaying the refreshed table depending on whether the update was successful or not
//******************************************************************************************************************
function UpdateRoleSuccess(ReturnedData)
{
   $(".deleteStatus").html(ReturnedData["status"]);
   ShowRoles();
}

//******************************************************************************************************************
//Method Name : CancelRoles
//Parameters :  none
//Return Type : void
//Description : Helps in sending an ajax request which helps in getting the stored infomation in the tables 
//******************************************************************************************************************
function CancelRoles()
{
    myParent = this.parentElement.parentElement;
    let roleId = myParent.children[1];
    let postData = {};
    postData["action"] = "CancelRole";
    postData["roleId"] = roleId.innerHTML;

    AjaxRequest("roleService.php", "post", postData, "json", CancelRoleSuccess, ErrorMethod);
}

//******************************************************************************************************************
//Method Name : CancelRoleSuccess
//Parameters :  ReturnedData - The data sent by the server
//Return Type : void
//Description : It will just rollback to the original values without changing the table
//******************************************************************************************************************
function CancelRoleSuccess(ReturnedData)
{
    let UpdateBtn = myParent.children[0].children[0];
    let CancelBtn = myParent.children[0].children[1];
    $(UpdateBtn).val("Edit");
    $(UpdateBtn).attr("id", "idEditBtn");
    $(CancelBtn).val("Delete");
    $(CancelBtn).attr("id", "idDelBtn");
    myParent.children[2].innerHTML = ReturnedData[0]["role_name"];
    myParent.children[3].innerHTML = ReturnedData[0]["desc"];
    myParent.children[4].innerHTML = ReturnedData[0]["role_value"];
}

//******************************************************************************************************************
//Method Name : EditRoles
//Parameters :  none
//Return Type : void
//Description : Helps in sending an ajax request with the required information to edit the roles
//******************************************************************************************************************
function EditRoles() {
    $(".deleteStatus").empty();
    myParent = this.parentElement.parentElement;
    let roleVal = myParent.children[4].innerHTML;
    var postData = {};
    postData["action"] = "EditRoles";
    postData["rolevalue"] = roleVal;
    AjaxRequest("roleService.php", "post", postData, "json", EditRoleSuccess, ErrorMethod);
}

//******************************************************************************************************************
//Method Name : EditRolesSuccess
//Parameters :  ReturnedData - The data sent by the server
//Return Type : void
//Description : Helps in changing the elements of the row which further helps in updating the data with that particular
//id whose information is requested to be updated
//******************************************************************************************************************
function EditRoleSuccess(ReturnedData)
{
    if(ReturnedData["canEdit"])
    {
       let editBtn = myParent.children[0].children[0];
       let delBtn = myParent.children[0].children[1];
       let roleName = myParent.children[2];
       let roleDesc = myParent.children[3];
       let roleValue = myParent.children[4];
       currRoleValue = roleValue.innerHTML;
       currRoleDesc = roleDesc.innerHTML;
       currRoleName = roleName.innerHTML;

       $(roleName).empty();
       $(roleDesc).empty();
       $(roleValue).empty();

       let descTb = document.createElement('input');
       $(descTb).attr("type", "text");
       $(descTb).attr("placeholder", "description");
       $(descTb).attr("value", currRoleDesc);
       $(descTb).attr("style", "width: 95%");
       $(roleDesc).append(descTb);

       let roleNameTb = document.createElement('input');
       $(roleNameTb).attr("type", "text");
       $(roleNameTb).attr("placeholder", "rolename");
       $(roleNameTb).attr("value", currRoleName);
       $(roleName).append(roleNameTb);

       let roleValueTb = document.createElement('input');
       $(roleValueTb).attr("type", "number");
       $(roleValueTb).attr("min", "1");
       $(roleValueTb).attr("max", "99");    
       $(roleValueTb).attr("value", currRoleValue);
       $(roleValue).append(roleValueTb);   

       $(editBtn).val("Update");
       $(editBtn).attr("id", "idUpdateBtn");
       $(delBtn).val("Cancel");
       $(delBtn).attr("id", "idCancelBtn");
    }
    else
    {
        $(".deleteStatus").html(ReturnedData["status"]);
    }
}

//******************************************************************************************************************
//Method Name : DeleteRoles
//Parameters :  none
//Return Type : void
//Description : Helps in sending an ajax request with the required information to delete the roles
//******************************************************************************************************************
function DeleteRoles()
{
    $(".statusDiv").empty();
    var postData = {};
    postData["action"] = "DeleteRoles";
    postData["roleId"] = this.parentElement.parentElement.getAttribute("id");

    AjaxRequest("roleService.php", "post", postData, "json", DeleteSuccess, ErrorMethod);
}

//******************************************************************************************************************
//Method Name : DeleteSuccess
//Parameters :  ReturnedData - The data we got from the server
//Return Type : void
//Description :  Helps in displaying the status message and a refreshed table depending 
// on whether a new role was successfully deleted from the table
//******************************************************************************************************************
function DeleteSuccess(ReturnedData)
{
    console.log(ReturnedData["status"]);
    $(".deleteStatus").html(ReturnedData["status"]);
    ShowRoles();
}

//******************************************************************************************************************
//Method Name : AddRoles
//Parameters :  none
//Return Type : void
//Description : Helps in sending an ajax request with the required information to add the roles
//******************************************************************************************************************
function AddRoles()
{
    $(".deleteStatus").empty();
    let roleName = $("#idRoleName").val().trim();
    let roleDesc = $("#idDesc").val().trim();
    let roleValue = $("#idRoleValue").val().trim();

    if(roleName.length === 0)
    {
        $(".statusDiv").html("Please assign a role name to the user");
    }
    else
    {
        if(roleDesc.length === 0)
        {
            $("#idDesc").val("Not Defined");
            roleDesc = "Not Defined";
        }
            if(roleValue.length > 0 &&   (roleValue >= 1 && roleValue < 99))
            {
                var postData = {};
                postData["action"] = "AddRoles";
                postData["roleName"] = roleName;
                postData["roleDesc"] = roleDesc;
                postData["roleValue"]= roleValue;
                console.log(roleDesc);
                AjaxRequest("roleService.php", "post", postData, "json", AddRolesSuccess, ErrorMethod);
            }
            else
            {
                $(".statusDiv").html("Not a valid value");
                $("#idRoleValue").val("");  
            }
    }
}

//******************************************************************************************************************
//Method Name : AddRolesSuccess
//Parameters :  ReturnedData - The data sent by the server
//Return Type : void
//Description : Helps in displaying the status message and a refreshed table depending 
// on whether a new role is added to the table
//******************************************************************************************************************
function AddRolesSuccess(ReturnedData)
{
    $(".statusDiv").html(ReturnedData["status"]);
    ShowRoles();
}

//******************************************************************************************************************
//Method Name : ShowRoles
//Parameters :  none
//Return Type : void
//Description : Helps in sending an ajax request which helps in gettting role information which will be used to display
//******************************************************************************************************************
function ShowRoles()
{
    var postData = {};
    postData["action"] = "ShowRoles";

    AjaxRequest("roleService.php", "post", postData, "json", ShowSuccess, ErrorMethod);
}

//******************************************************************************************************************
//Method Name : ShowSuccess
//Parameters :  ReturnedData - The data sent by the server
//Return Type : void
//Description : Helps in creating a roles table using the information sent by the server and displays relevant messages
//******************************************************************************************************************
function ShowSuccess(ReturnedData)
{

    console.log(ReturnedData);
    $(".tableDiv").empty();
    let table = document.createElement('table');
    let trHead = document.createElement('tr');
    var count = 0;
    let opHead = document.createElement('th');
    opHead.innerHTML = "action";
    trHead.append(opHead);
    for(const iterator in ReturnedData["dataArray"][0])
    {
        let th = document.createElement('th');
        th.innerHTML = iterator;
        trHead.append(th);  
    }

    table.append(trHead);       
    for (const iterator of ReturnedData["dataArray"]) {
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
        //console.log(iterator);
        for(const element in iterator)
        {
            let td = document.createElement('td');
            td.innerHTML = iterator[element];
            tr.append(td);
        }
        $(tr).attr("id", iterator['role_id']);
        table.append(tr);
        ++count;
    }
    $(".tableDiv").append(table);
    $(".showUserStatus").html("Retrieved " + count+ " user records.");
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
