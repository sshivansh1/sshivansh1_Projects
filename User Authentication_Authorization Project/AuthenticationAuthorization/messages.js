var headArray = ["Op", "MessageID", "User", "Message", "Timestamp"]; //To display the titles while creating the messages table
$(document).ready(function(){
    GETMessages();
    $("body").on("click", "#idDelBtn", DeleteMessage);
    $("body").on("click", "#idEditBtn", EditMessage);
    $("body").on("click", "#idUpdateBtn", UpdateMessage);
    $("#idSearchBtn").on("click", function()
    {
        GETMessages();
    })
    $("#idSendBtn").on("click", AddMessage);
});

//******************************************************************************************************************
//Method Name : AddMessage
//Parameters :  none
//Return Type : void
//Description : Helps in sending an ajax request with the required information to add a message to the messages table
//******************************************************************************************************************
function AddMessage()
{
    let userMessage = $("#idMessageTb").val();
    userMessage = userMessage.trim();
    var postData = {};
    postData["userMessage"] = userMessage;

    if(userMessage.length === 0)
    {
        $(".updateDiv").html("The message field cannot be empty.");
        $(".updateDiv").css("color", "red");
    }
    else
    {
        AjaxRequest("RestFolder/example", "POST", postData, "json", AddSuccessMessage, ErrorMethod);
    }
}

//******************************************************************************************************************
//Method Name : AddSuccessMessage
//Parameters :  ReturnedData - returns the data sent by the server
//Return Type : void
//Description : Helps in displaying the refreshed table after the message was added to the messages table with the updated message
//******************************************************************************************************************
function AddSuccessMessage(ReturnedData)
{
    GETMessages();
    $(".updateDiv").html(ReturnedData);
}

//******************************************************************************************************************
//Method Name : UpdateMessage
//Parameters :  none
//Return Type : void
//Description : Helps in sending an ajax request with the information to update a specific message
//******************************************************************************************************************
function UpdateMessage()
{
    let parent = $(this).parent().parent();
    let messageId = $(parent).attr("id");
    let tbValue = $($($(parent).children()[3]).children()[0]).val();
    
    var updateData = {};
    updateData["action"] = "UpdateMessages";
    updateData["messageValue"] = tbValue;
    updateData["username"] = $($(parent).children()[2]).html();
    AjaxRequest("RestFolder/example/abc/"+messageId, "PUT", updateData, "json", UpdateSuccessMessage, ErrorMethod);
}
//******************************************************************************************************************
//Method Name : UpdateSuccessMessage
//Parameters :  ReturnedData - The data sent by the server
//Return Type : void
//Description : Helps in displaying the table after the user's request to update a specific message in the table
//******************************************************************************************************************
function UpdateSuccessMessage(ReturnedData)
{
    GETMessages();
    $(".updateDiv").html(ReturnedData);
}
//******************************************************************************************************************
//Method Name : EditMessage
//Parameters :  none
//Return Type : void
//Description : Helps in changing the controls in the table so that the user can edit the messages which he is allowed to
//******************************************************************************************************************
function EditMessage() {
    $(".updateDiv").empty();
    let parent = $(this).parent().parent();
    let messageCell = $(parent).children()[3];
    let currMessage = $(messageCell).html();
    let messageId = $(parent).children()[1].innerHTML;
    
    $(messageCell).empty();

    let inputText = document.createElement("input");
    $(inputText).attr("type", "text");
    $(inputText).attr("value", currMessage);
    $(inputText).css("width", "80%");

    let updateBtn = document.createElement("input");
    $(updateBtn).attr("type", "button");
    $(updateBtn).val("Update");
    $(updateBtn).attr("id", "idUpdateBtn");

    $(messageCell).append(inputText);
    $(messageCell).append(updateBtn);
    
    $(this).val("Cancel");
    $(this).attr("id", "idCancelBtn" + messageId);

    $("body").on("click","#idCancelBtn" + messageId, function(){
        $(this).attr("id", "idEditBtn");
        $(this).val("Edit");
        
        messageCell.innerHTML = "";
        messageCell.innerHTML = currMessage;
    });
}
//******************************************************************************************************************
//Method Name : DeleteMessage
//Parameters :  none
//Return Type : void
//Description : Helps in sending an ajax request with the required information to delete a message from the messages table
//******************************************************************************************************************
function DeleteMessage()
{
    $(".updateDiv").empty();
    let parent = $(this).parent().parent();
    let messageId = $(this).parent().parent().attr("id");
    var deleteData = {};
    deleteData["action"] = "DeleteMessages";
    deleteData["username"] = $($(parent).children()[2]).html();
    
    AjaxRequest("RestFolder/example/abc/"+messageId, "DELETE", deleteData, "json", DelMessageSuccess, ErrorMethod);
}

//******************************************************************************************************************
//Method Name : DelMessageSuccess
//Parameters :  ReturnedData - The data we got from the server
//Return Type : void
//Description : Helps in displaying the refreshed table depending on whether a new message was successfully deleted from the table
//******************************************************************************************************************
function DelMessageSuccess(ReturnedData)
{
    GETMessages();
    $(".updateDiv").html(ReturnedData);
}
//******************************************************************************************************************
//Method Name : GETMessages
//Parameters :  none
//Return Type : void
//Description : Helps in sending an ajax request with the required information along with the url to get the all the messages from the message table
//              depending on whether the filter was applied or not
//******************************************************************************************************************
function GETMessages()
{
    let filterValue = $("#idFilterTb").val();
    var getData = {};
    getData["action"] = "GetMessages";
    AjaxRequest("RestFolder/example/"+filterValue, "GET", getData, "json", GetMessageSuccess, ErrorMethod);
}
//******************************************************************************************************************
//Method Name : GetMessageSuccess
//Parameters :  ReturnedData - The data sent by the server
//Return Type : void
//Description : Helps in creating a message table using the information sent by the server and it displays the number of rows retrieved
//******************************************************************************************************************
function GetMessageSuccess(ReturnedData)
{
    if(Array.isArray(ReturnedData))
    {
        $(".tableDiv").empty();
        let table = document.createElement('table');
        let headRow = document.createElement('tr');
        for(let i = 0; i < headArray.length; ++i)
        {
            let th = document.createElement('th');
            $(th).html(headArray[i]);
            $(headRow).append(th);
        }
        table.append(headRow);
        let rowCount = 0;
        for (const iterator of ReturnedData) {
            ++rowCount;
            let row = document.createElement('tr');
            let tdBtn = document.createElement('td');
            let editBtn = document.createElement('input');
            $(editBtn).attr("value", "Edit");
            $(editBtn).attr("type", "button");
            $(editBtn).attr("id", "idEditBtn");
            let delBtn = document.createElement('input');
            $(delBtn).attr("value", "Delete");
            $(delBtn).attr("type", "button");
            $(delBtn).attr("id", "idDelBtn");
            $(row).attr("id", iterator["MessageID"]);
            tdBtn.append(delBtn);
            tdBtn.append(editBtn);

            row.append(tdBtn);
            for(const item in iterator)
            {
                let td = document.createElement('td');
                td.innerHTML = iterator[item];
                $(row).append(td);

            }
            table.append(row);
        }
        $(".statusMessage").html(rowCount + "row(s) retrieved");
        $(".tableDiv").append(table);
    }
    else
    {
        myMessageArr = ReturnedData.split(" ");
        myFilter = myMessageArr[myMessageArr.length - 1];
        $(".tableDiv").html(ReturnedData);
        $(".tableDiv").css("text-align", "center");
        $(".tableDiv").css("color", "red");
        
        // if (document.querySelector(".tableDiv").innerHTML.indexOf(myFilter) != -1) { 
        //     $(".tableDiv").css("color", "green");
        //     let newStr = ReturnedData.replace(myFilter, );
        // }
    }
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
    console.log(errorMessage);
    console.log(status);
    console.log(request);
    $(".updateDiv").html(errorMessage);
}

//******************************************************************************************************************
//Method Name : AjaxRequest
//Parameters :  url, method, data, dataType, successMethod, errorMethod
//Return Type : void
//Description : Helps in sending the ajax request to the server
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