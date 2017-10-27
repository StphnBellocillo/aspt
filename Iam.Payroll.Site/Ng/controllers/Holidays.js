$(document).ready(function () {

    var idof = window.location.href.substr(window.location.href.lastIndexOf('/') + 1);

    //var person = {DepartmentName: $('#DepartmentName'), FirstName: $('#FirstName'), LastName: $('#LastName'), Gender: $('#Gender')};
    $.ajax({
        url: 'http://localhost:60294/api/Holidays',
        type: 'GET',
        dataType: 'json',
        //data: JSON.stringify(person),
        success: function (data, textStatus, xhr) {
            for (var i = 0; i < data.length; i++) {
                var holiday = data[i];
                $("#HideRow .Date").text(holiday.Date);
                $("#HideRow .Name").text(holiday.Name);
                $("#HideRow .TypeName").text(holiday.TypeName);
                $("#HideRow .edit").attr("href", "http://localhost:60294/Holidays/Edit/" + holiday.Id);
                $("#HideRow .details").attr("href", "http://localhost:60294/Holidays/Details/" + holiday.Id);
                $("#HideRow .delete").attr("href", "javascript:Delete(" + holiday.Id + ")");
                var template = $("#HideRow .ListRow").parent().html();
                $("#ShowRow").append(template);
            }
        },

    });

    $("#save").click(function () {
        if ($("#Date").val() == "" || $("#Name").val() == "" || $("#TypeName").val() == "") {
            alert("Opps! please filled out some forms")
        }
        else {
            $.post("http://localhost:60294/api/Holidays", { Date: $("#Date").val(), Name: $("#Name").val(), TypeName: $("#TypeName").val()}, function () {
                alert("New Hpoliday has been Added");
                window.location.assign("http://localhost:60294/Holidays/Index");

            });
        }
    });

    $.get("http://localhost:60294/api/Holidays/" + idof, function (datas) {
        $("#Date").val(datas.Date);
        $("#Name").val(datas.Name);
        $("#TypeName").val(datas.TypeName);

        $("#Date").text(datas.Date);
        $("#Name").text(datas.Name);
        $("#TypeName").text(datas.TypeName);
        $("#edit").attr("href", "http://localhost:60294/Holidays/Edit/" + datas.Id);
    });

    $("#edit").click(function () {
        $.post("http://localhost:60294/api/Holidays/" + idof, { Date: $("#Date").val(), Name: $("#Name").val(), TypeName: $("#TypeName").val()}, function () {
            alert("Holiday has been updated");
            window.location.assign("http://localhost:63804/Holidays/Index");
        });
    });

});

function Delete(id) {

    var con = confirm("Would you like to delete this record?");

    if (con == true) {
        $.ajax({
            url: "http://localhost:60294/api/PeopleApi/" + id,
            type: "DELETE",
            dataType: "json",
            data : id,
            success: function () {
                location.reload();
            }

        });
    }
   
}



