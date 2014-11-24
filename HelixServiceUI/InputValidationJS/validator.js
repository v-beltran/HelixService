$(document).ready(function () {
    // Enter key will submit the form.
    $("input").keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $("form").submit();
        }
    });
});

// Clears the form.
function ClearForm() {
    $("form")[0].reset();
    $(".success-container").remove();
    $(".error-container").remove();
}

// Display form results.
function DisplayResults(container, value, valid) {
    if (valid) {
        $(container).append("<div class='success-container'><label>" + value + " IS VALID.</label></div>");
    }
    else {
        $(container).append("<div class='error-container'><label>" + value + " IS NOT VALID.</label></div>");
    }
}

// Validate all input fields on the form.
function IsValid() {
    // Clear previous results.
    $(".success-container").remove();
    $(".error-container").remove();

    var valid = true;

    // Validate password input.
    if (IsValidPassword($("#txtPassword").val())) {
        DisplayResults("#inputPassword", $("#txtPassword").val(), true)
    }
    else {
        DisplayResults("#inputPassword", $("#txtPassword").val(), false)
        valid = false;
    }

    // Validate ZIP code input.
    if (IsValidZipCode($("#txtZipCode").val())) {
        DisplayResults("#inputZipCode", $("#txtZipCode").val(), true)
    }
    else {
        DisplayResults("#inputZipCode", $("#txtZipCode").val(), false)
        valid = false;
    }

    // Validate telephone number input.
    if (IsValidPhoneNumber($("#txtTelephoneNo").val())) {
        DisplayResults("#inputTelephoneNo", $("#txtTelephoneNo").val(), true)
    }
    else {
        DisplayResults("#inputTelephoneNo", $("#txtTelephoneNo").val(), false)
        valid = false;
    }

    // Validate e-mail address input.
    if (IsValidEmail($("#txtEmail").val())) {
        DisplayResults("#inputEmail", $("#txtEmail").val(), true)
    }
    else {
        DisplayResults("#inputEmail", $("#txtEmail").val(), false)
        valid = false;
    }

    // Validate date of birth input.
    if (IsValidDOB($("#txtDOB").val())) {
        DisplayResults("#inputDOB", $("#txtDOB").val(), true)
    }
    else {
        DisplayResults("#inputDOB", $("#txtDOB").val(), false)
        valid = false;
    }

    // Validate date input.
    if (IsValidDate($("#txtDate").val())) {
        DisplayResults("#inputDate", $("#txtDate").val(), true)
    }
    else {
        DisplayResults("#inputDate", $("#txtDate").val(), false)
        valid = false;
    }

    // Validate standard time input.
    if (IsValidStandardTime($("#txtTimeStandard").val())) {
        DisplayResults("#inputTimeStandard", $("#txtTimeStandard").val(), true)
    }
    else {
        DisplayResults("#inputTimeStandard", $("#txtTimeStandard").val(), false)
        valid = false;
    }

    // Validate military time input.
    if (IsValidMilitaryTime($("#txtTimeMilitary").val())) {
        DisplayResults("#inputTimeMilitary", $("#txtTimeMilitary").val(), true)
    }
    else {
        DisplayResults("#inputTimeMilitary", $("#txtTimeMilitary").val(), false)
        valid = false;
    }

    // Validate number input.
    if (IsValidInteger($("#txtNumber").val())) {
        DisplayResults("#inputNumber", $("#txtNumber").val(), true)
    }
    else {
        DisplayResults("#inputNumber", $("#txtNumber").val(), false)
        valid = false;
    }

    // Validate currency input.
    if (IsValidDecimal($("#txtCurrency").val())) {
        DisplayResults("#inputCurrency", $("#txtCurrency").val(), true)
    }
    else {
        DisplayResults("#inputCurrency", $("#txtCurrency").val(), false)
        valid = false;
    }

    // Validate website url input.
    if (IsValidUrl($("#txtWebsite").val())) {
        DisplayResults("#inputWebsite", $("#txtWebsite").val(), true)
    }
    else {
        DisplayResults("#inputWebsite", $("#txtWebsite").val(), false)
        valid = false;
    }

    return valid;
}


//**************************************************************************//
//BEGIN VALIDATION METHODS
//**************************************************************************//

function IsValidPassword(value) {
    var pattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{8,32}$/;
    return pattern.test(value);
}

function IsValidZipCode(value) {
    var pattern = /^\d{5}(?:[-\s]\d{4})?$/;
    return pattern.test(value);
}

function IsValidPhoneNumber(value) {
    var pattern = /^[(]{0,1}[0-9]{3}[)]{0,1}[-\s\.]{0,1}[0-9]{3}[-\s\.]{0,1}[0-9]{4}$/;
    return pattern.test(value);
}

function IsValidEmail(value) {
    var pattern = /^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$/;
    return pattern.test(value);
}

function IsValidDOB(value) {
    var valid = false;
    if (IsValidDate(value)) {
        var year = value.split("/")[2];
        if (year >= 1900 && year <= new Date().getFullYear())
            valid = true;
    }

    return valid;
}

function IsValidDate(value) {
    var pattern = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
    var valid = false;
    if (pattern.test(value)) {
        var month = value.split("/")[0];
        var day = value.split("/")[1];
        var year = value.split("/")[2];
        var date = new Date(year, month - 1, day);
        if ((date.getMonth() + 1 == month) && (date.getDate() == day) && (date.getFullYear() == year))
            valid = true;
    }

    return valid;
}

function IsValidMilitaryTime(value) {
    var pattern = /^(?:[01][0-9]|2[0-3]):?[0-5][0-9]$/;
    return pattern.test(value);
}

function IsValidStandardTime(value) {
    var pattern = /^([1-9]|1[0-2]):([0-5]\d)\s?(AM|PM)?$/i;
    return pattern.test(value);
}

function IsValidInteger(value) {
    var pattern = /^\s*(\+|-)?\d+\s*$/;
    return pattern.test(value);
}

function IsValidDecimal(value) {
    var pattern = /^\s*(\+|-)?(\d+\.?\d*|\.\d+)$/;
    return pattern.test(value);
}

function IsValidUrl(value) {
    var pattern = /^(((ftp|http|https):\/\/)|www.)[^ "]+$/;
    return pattern.test(value);
}

//**************************************************************************//
//END VALIDATION METHODS
//**************************************************************************//

