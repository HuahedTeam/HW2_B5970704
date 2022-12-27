$(document).ready(function () {
    getEmployees();
});
var isUpdateable = false;
function getEmployees() {
    $.ajax({
        url: '/Employees/GetEmployees/',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var rows = '';
            $.each(data, function (x, item) {
                rows += "<tr>"
                rows += "<td>" + item.ID + "</td>"
                rows += "<td>" + item.Name + "</td>"
                rows += "<td>" + item.Position + "</td>"
                rows += "<td>" + item.Address + "</td>"
                rows += "<td><button type='button' id='btnEdit' class='btn btndefault' onclick='return getEmployeeById(" + item.ID + ")'>Edit</button> <button type='button' id = 'btnDelete' class='btn btndanger' onclick = 'return deleteEmployeeById(" + item.ID + ")' > Delete</button ></td > "
                rows += "</tr>";
                $("#listEmployee").html(rows);
            });
        },
        error: function (err) {
            alert("Error: " + err.responseText);
        }
    });
}
function getEmployeeById(id) {
    $("#title").text("Employee Detail");
    $.ajax({
        url: '/Employees/Get/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $("#ID").val(data.ID);
            $("#Name").val(data.Name);
            $("#Position").val(data.Position);
            $("#Address").val(data.Address);
            isUpdateable = true;
            $("#empModal").modal('show');
        },
        error: function (err) {
            alert("Error: " + err.responseText);
        }
    });
}
$("#btnSave").click(function (e) {
    var data = {
        ID: $("#ID").val(),
        Name: $("#Name").val(),
        Position: $("#Position").val(),
        Address: $("#Address").val()
    }
    if (!isUpdateable) {
        $.ajax({
            url: '/Employees/Create/',
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                getEmployees();
                $("#empModal").modal('hide');
                clear();
            },
            error: function (err) {
                alert("Error: " + err.responseText);
            }
        })
    }
    else {
        $.ajax({
            url: '/Employees/Update/',
            type: 'POST',
            dataType: 'json',
            data: data,
            success: function (data) {
                getEmployees();
                isUpdateable = false;
                $("#empModal").modal('hide');
                clear();
            },
            error: function (err) {
                alert("Error: " + err.responseText);
            }
        })
    }
});
function deleteEmployeeById(id) {
    $("#confirmModal #title").text("Delete Employee");
    $("#confirmModal").modal('show');
    $("#confirmModal #btnOK").click(function (e) {
        $.ajax({
            url: "/Employees/Delete/" + id,
            type: "POST",
            dataType: 'json',
            success: function (data) {
                getEmployees();
                $("#confirmModal").modal('hide');
            },
            error: function (err) {
                alert("Error: " + err.responseText);
            }
        });
        e.preventDefault();
    });
}
$("#btnCreate").click(function () {
    $("#title").text("Create New");
})
$("#btnClose").click(function () {
    clear();
});
function clear() {
    $("#ID").val("");
    $("#Name").val("");
    $("#Position").val("");
    $("#Address").val("");
}