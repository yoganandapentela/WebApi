    var selectedRow = null;
    displaydata();
    function noRecordsFound() {
        if ($("#employeeList tbody").find("tr").length == 0) {
            $("#norecords").text("NO RECORDS FOUND");
        }
        else {
            $("#norecords").text("");

        }
    }
    noRecordsFound();
    
    //Main function...
    function formsubmit() {
        if (validate()) {
             var formdata = fun1();
            if (selectedRow == null) {
                $("#norecords").text('');
               
                insertnewrecord(formdata);

            }
            else {
                updatenewrecord(formdata);
            }
            //resetform();
        }

    }

    function fun1() {
        formdata = {};
        formdata["Id"] = $("#Id").val();
        formdata["Name"] = $("#Name").val();
        formdata["Age"] = $("#Age").val();

        return formdata;
    }
//function to insert a new record
    function insertnewrecord(formdata) {
        $.ajax({
            url: '/api/Values',
            contentType: 'application/json ;charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(formdata),
            type: 'POST',
            success: function (result) {
                //alert(result);
                displaydata();
            },
            error: function () {
                alert("no data inserted");
            }

        }
        )


        resetform();

    }

//function to update record
    function updatenewrecord(formdata) {
        let id = formdata.Id;
        $.ajax({
            url: '/api/Values/'+id,
            contentType: 'application/json ;charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(formdata),
            type: 'PUT',
            success: function (result) {
                //alert(result);
                displaydata();
            },
            error: function () {
                alert("no data inserted");
            }

        }
        )
        $("#submit").prop("value", "submit");

        resetform();

    }
    //function to display data
    function displaydata() {
        $.ajax({
            url: '/api/Values',
            contentType: 'application/json ;charset=utf-8',
            dataType: 'json',
            type: 'GET',
            success: function (result) {
                if (result) {
                    $("#employeeList tbody").html('');
                    var row = '';
                    for (let i = 0; i < result.length; i++) {
                        row = row + '<tr>' + '<td>' + result[i].Id + '</td>' +
                            '<td>' + result[i].Name + '</td>' +
                            '<td>' + result[i].Age + '</td>' +
                            '<td><button onClick="onEdit(this)">Edit</button><button onClick="onDelete(' + result[i].Id + ')">Delete</button></td>' + '</tr>';

                    }
                    if (row != '') {
                        $("#employeeList tbody").append(row);

                    }
                }
            },
            error: function () {
                alert("no data inserted");
            }

        }
        )
    }
    
    //function to delete
    function onDelete(id) {
        $.ajax({
            url: '/api/Values/' + id,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'DELETE',
            success: function () {
                alert("deleted");
                noRecordsFound();

                displaydata();
                
            },
            error: function () {
                alert("data not deleted");
            }


        })
    }

//function to edit
    function onEdit(td) {
        selectedRow = $(td).closest("tr"); // Get the closest table row (tr) to the clicked button
        $("#Id").val(selectedRow.find("td:eq(0)").text());
        $("#Name").val(selectedRow.find("td:eq(1)").text());
        $("#Age").val(selectedRow.find("td:eq(2)").text());
        $("#submit").prop("value", "Update");


    }

//function to reset
    function resetform() {
        $("#Id").val("");
        $("#Name").val("");
        $("#Age").val("");
        selectedRow = null;
    }
//function to serach
    $("#searchbar").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#employeeList tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
//function for validation
    function validate() {
        return true;
    }


    