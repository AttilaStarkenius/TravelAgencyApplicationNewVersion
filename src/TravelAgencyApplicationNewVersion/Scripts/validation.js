function validateForm() {
    var x = document.getElementById('datepicker').value;

    if (x > document.getElementById('arrivaltime').value) {
        alert("Departure date must be earlier or same as the arrival date!");
        return false;
    }

    //var departurecityid = '@Html.IdFor( o => o.DepartureCity )';
    //var departurecitydropdown = $("#" + departurecityid);
    //var departurecityvalue = departurecitydropdown.val();

    //var arrivalcityid = '@Html.IdFor( o => o.ArrivalCity )';
    //var arrivalcitydropdown = $("#" + arrivalcityid);
    //var arrivalcityvalue = arrivalcitydropdown.val();

    var departurecity = $("#ddlDepartureCity").val();
    var arrivalcity = $("#ddlArrivalCity").val();

    if (departurecity == arrivalcity)
    {
        alert("Departure city cannot be the same as the arrival city!");
        return false;
    }



}
    



function validateEdit() {
    var x = document.getElementById('SeatId').value;

    if (!x) {
        alert("SeatId must be filled out!");
        return false;
    }
}