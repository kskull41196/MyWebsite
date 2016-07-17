function chkallClick(o) {
    var form = document.frmForm;
    for (var i = 0; i < form.elements.length; i++) {
        if (form.elements[i].type == "checkbox" && form.elements[i].name!="chkall") {
            form.elements[i].checked = document.frmForm.chkall.checked;
        }
    }
}
$(document).ready(function () {
    $("#chkall").click(function () {
        var status = this.checked;
        $("input[class='tai_c']").each(function () { this.checked = status; })
    });
    /*$('.iteCategory').change(function () {
        var idCat = $(this).find('option:selected').val();
        window.location = 'http://cu.vanphuco.com/quantri/?act=item_category&cat=' + idCat;
    });*/
});
$(window).load(function () {
    $('#page-loader').hide(200);
});