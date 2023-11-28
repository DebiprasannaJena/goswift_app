
function pageLoad() { showProgress(); }
$(document).ready(function () { showProgress(); });
function showProgress() {
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
        $.blockUI({ message: $('#divBlock'), css: { border: 'none', background: 'transparent'} });
    });
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        $.unblockUI();
    });
}