// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Handle new item add
function addRow(type) {

    $(".datepicker").datepicker("destroy");
    
    // Copyable rows
    let starterTemplate = $('#starter-' + type).html();
    let count = $('.box-' + type).length;

    // Needed to work with asp-for tags
    let upperCaseType = type.charAt(0).toUpperCase() + type.slice(1) + "s";

    // Needed to escape special characters
    RegExp.quote = function(str) {
        return str.replace(/([.?*+^$[\]\\(){}|-])/g, "\\$1");
    };

    let regexOne = new RegExp(RegExp.quote(upperCaseType + "_0"), "g");
    let regexTwo = new RegExp(RegExp.quote(upperCaseType + "[0]"), "g");
    let newRow = starterTemplate.replace(regexOne, upperCaseType + "_" + count)
        .replace(regexTwo, upperCaseType + "[" + count + "]");

    $('#box-' + type).append(newRow);

    // Needed to activate radio buttons, if required
    $('.ui.radio.checkbox').checkbox();

    showOrHideRemoveButton(type, true);
    $('.datepicker').removeAttr('id');
    $('.datepicker').datepicker({ dateFormat: 'yy-mm-dd' });
}


$('.ui.radio.checkbox').checkbox();

function removeRow(type) {
    let count = $('.box-' + type).length;
    let lastItem = $('.box-' + type)[count - 1].remove();

    if (count === 2) showOrHideRemoveButton(type, false);
}

function showOrHideRemoveButton(type, show) {
    show ? $('#remove-' + type).show() : $('#remove-' + type).hide();
}

$(function() {

    $('.datepicker').datepicker({ dateFormat: 'yy-mm-dd' });

    $('.ui.radio.checkboxe').checkbox();

    $('#generate').click(function() {
        let valid = $('#boxe-form')[0].checkValidity();
        if (valid) {
            let element = $(this);
            element.addClass('loading');
            setTimeout(function() {
                element.removeClass('loading');
            }, 2000);
        }
    });
});

