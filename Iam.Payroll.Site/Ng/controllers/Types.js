$(document).ready(function () {

    var idof = window.location.href.substr(window.location.href.lastIndexOf('/') + 1);

    //var person = {DepartmentName: $('#DepartmentName'), FirstName: $('#FirstName'), LastName: $('#LastName'), Gender: $('#Gender')};
    $.ajax({
        url: 'http://localhost:60294/api/Types',
        type: 'GET',
        dataType: 'json',
        //data: JSON.stringify(person),
        success: function (data, textStatus, xhr) {
            for (var i = 0; i < data.length; i++) {
                var type = data[i];
                $("#HideRow .Name").text(type.Name);
                $("#HideRow .Differential").text(type.Differential);
                $("#HideRow .edit").attr("href", "http://localhost:60294/api/Types/Edit/" + holiday.Id);
                $("#HideRow .details").attr("href", "http://localhost:60294/api/Types/Details/" + holiday.Id);
                $("#HideRow .delete").attr("href", "javascript:Delete(" + person.Id + ")");
                var template = $("#HideRow .ListRow").parent().html();
                $("#ShowRow").append(template);
            }
        },

    });

    $("#save").click(function () {
        if ($("#Name").val() == "", $("#Differential").val() == "") {
            alert("Opps! please filled out some forms")
        }
        else {
            $.post("http://localhost:60294/api/Types", { Name: $("#Name").val(), Differential: $("#Differential").val() }, function () {
                alert("New Type has been Added");
                window.location.assign("http://localhost:60294/Types/Index");

            });
        }
    });

    $.get("http://localhost:60294/api/Types/" + idof, function (datas) {
        $("#Name").val(datas.Name);
        $("#Differential").text(datas.Differential);
        $("#edit").attr("href", "http://localhost:60294/Types/Edit/" + datas.Id);
    });

    $("#edit").click(function () {
        $.post("http://localhost:60294/api/Types/" + idof, { Name: $("#Name").val(), Differential: $("#Differential").val() }, function () {
            alert("Types has been updated");
            window.location.assign("http://localhost:63804/Types/Index");
        });
    });

});

function Delete(id) {

    var con = confirm("Would you like to delete this record?");

    if (con == true) {
        $.ajax({
            url: "http://localhost:60294/api/Departments/" + id,
            type: "DELETE",
            dataType: "json",
            data: id,
            success: function () {
                location.reload();
            }

        });
    }

}



