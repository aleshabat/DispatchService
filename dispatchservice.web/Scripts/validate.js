function boostrapHighlight(element, errorClass, validClass) {
    $(element).closest(".form-group").removeClass("has-success");
    $(element).closest(".form-group").addClass("has-error");
    $(element).trigger('highlated');
};

function boostrapUnhighlight(element, errorClass, validClass) {
    $(element).closest(".form-group").removeClass("has-error");
    $(element).closest(".form-group").addClass("has-success");
    $(element).trigger('unhighlated');
};

$.validator.setDefaults({
    ignore: "",
    highlight: function (element, errorClass, validClass) {
        boostrapHighlight(element, errorClass, validClass);
    },
    unhighlight: function (element, errorClass, validClass) {
        boostrapUnhighlight(element, errorClass, validClass);
    }
});