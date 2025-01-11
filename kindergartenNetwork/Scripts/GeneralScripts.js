var lst_ra = [];

var GeneralFunction = function () {

    return {
        initGeneralFunction: function () {
        }
    };
}();
var handelTimePicker = function () {
    jQuery().timepicker && ($(".timepicker-default").timepicker({
        autoclose: true,
        showSeconds: true,
        minuteStep: 1
    }), $(".timepicker-no-seconds").timepicker({
        autoclose: true,
        minuteStep: 5
    }), $(".timepicker-24").timepicker({
        autoclose: true,
        minuteStep: 5,
        showSeconds: false,
        showMeridian: false
    }), $(".timepicker").parent(".input-group").on("click", ".input-group-btn", function (t) {
        t.preventDefault(), $(this).parent(".input-group").find(".timepicker").timepicker("showWidget");
    }), $(document).scroll(function () {
        $("#form_modal4 .timepicker-default, #form_modal4 .timepicker-no-seconds, #form_modal4 .timepicker-24").timepicker("place");
    }));
};
var handleDatePickers = function () {
    if (jQuery().datepicker) {
        $('.date-picker').datepicker({
            rtl: App.isRTL(),
            orientation: "left",
            autoclose: true,
            format: 'dd-mm-yyyy'
        });
    }
};
var handelTooltips = function () {
    $(".form-control").each(function () {
        $(this).tooltip({
            html: true,
            title: '<span style="font-family: \'GESSTwoLight-Light\'">' + $(this).attr("data-field") + '</span>'
        });
    });
};
var gsNotifyMsg = function (msg, status) {
    //toastr[status](msg);
    //toastr.options = {
    //    "closeButton": true,
    //    "debug": false,
    //    "newestOnTop": false,
    //    "progressBar": true,
    //    "positionClass": "toast-top-right",
    //    "preventDuplicates": false,
    //    "onclick": null,
    //    "showDuration": "300",
    //    "hideDuration": "1000",
    //    "timeOut": "5000",
    //    "extendedTimeOut": "1000",
    //    "showEasing": "swing",
    //    "hideEasing": "linear",
    //    "showMethod": "fadeIn",
    //    "hideMethod": "fadeOut"
    //}
    var theme = "ruby";
    if (status === "success") {
        theme = "lime";
    }
    var t = {
        theme: theme,
            sticky: false,
        verticalEdge: 'left',
        horizontalEdge: 'top'
        },
        n = $(this);
    //"" !== $.trim('Alert') && (t.heading = $.trim('Alert')),
        t.sticky || (t.life = 3000), $.notific8("zindex", 11500),
        $.notific8($.trim(msg), t), n.attr("disabled", "disabled"), setTimeout(function () {
            n.removeAttr("disabled");
        }, 1e3);
};
var gsConfirm = function (message, item) {
    bootbox.confirm({
        message: message,
        callback: function (result) { item(result); },
        buttons: {
            confirm: {
                label: Messages.yes,
                className: "red btn-outline"
            },
            cancel: {
                label: Messages.cancel,
                className: 'dark btn-outline'
            }
        }
    });
};
var gsAlert = function (message) {
    bootbox.alert({
        message: message,
        buttons: {
            ok: {
                label: Messages.ok,
                className: "blue btn-outline"
            }
        }
    });
};
var gsReloadDataTable = function () {
    $(".reload").each(function () {
        $(this).off("click").click(function () {
            var x = $(this).closest(".portlet").find("table").attr("id");
            var oTable = $('#' + x + '').dataTable();
            oTable.fnDraw();
        });
    });
};
var handleBootstrapSelect = function () {
    $('.selectpicker').selectpicker({
        size: 7
    });
};
var completedSuccessfuly = function (msg) {
    $(".alert-success").html(msg);
    $(".alert-danger").css("display", "none");
    $(".alert-success").css("display", "block");
    setTimeout(function () {
        $(".alert-success").css("display", "none");
    }, 3000);

};
var completedSuccessfulyforvoucher = function (msg, form) {
    $(".alert-success", form).html(msg);
    $(".alert-danger", form).css("display", "none");
    $(".alert-success", form).css("display", "block");
    setTimeout(function () {
        $(".alert-success", form).css("display", "none");
    }, 3000);

};
var notValidOperations = function (msg) {
    $(".alert-danger").html(msg);
    $(".alert-danger").css("display", "block");
    $(".alert-success").css("display", "none");
};
var formNotValidOperations = function (msg, form) {
    $(".alert-danger", form).html(msg);
    $(".alert-danger", form).css("display", "block");
    $(".alert-success", form).css("display", "none");
};
var lang = $('html').attr('data-lang');
var resetbooststrapSelect = function () {
    $('#btnClearForm').click(function () {
        $('.selectpicker').selectpicker('render');
    });
    $('[type=reset]').click(function () {
        setTimeout(function () {
            $('.selectpicker').selectpicker('render');
 
        });
    });
}
var gsResetInsertForm = function (form) {
    document.getElementById(form).reset();
    $('.selectpicker').selectpicker('render');
    $("#" + form + " input[type='hidden']:not(.dontReset)").val(0);
};
var gsEnableSubmitButton = function(form) {
    setTimeout(function() {
        $(form).find("button[Type='submit']").removeAttr("disabled");
    }, 2000);
};
var gsDisablSubmitButton = function(form) {
    $(form).find("button[Type='submit']").attr("disabled", "true");
};
var gsShowModal=function(btn) {
    $("*").off("keydown").keydown(function (e) {
        //debugger;
        if ($(".modal.fade.in").length == 0) {
            if (e.which == 107) {
                $(btn).click();
                gsAutoFocus();
                return false;
            }
        } else {
            if (e.which == 107) {
                e.preventDefault();
                var form = $(".modal.fade.in form").attr("id");
                gsResetInsertForm(form);
                return false;
            }
            else if (e.which == 13) {
                e.preventDefault();
                if (!$(e.target).is("select,option,a,.disableEnter,.bs-searchbox") &&
                    !$(e.target.parentNode).is("select,option,a,.disableEnter,.bs-searchbox")) {
                    var form = $(".modal.fade.in form").attr("id");
                    gsSubmitForm("#" + form);
                    return false;
                }
            }
        }
    });
}

//var preventEnter = function () {
//    $("*").off("keydown").keydown(function (e) {
//        if (e.which == 13) {
//            e.preventDefault();
//            if (!$(e.target).is("select,option,a,.disableEnter,.bs-searchbox") &&
//                !$(e.target.parentNode).is("select,option,a,.disableEnter,.bs-searchbox")) {
//                var form = $("form").attr("id");
//                gsSubmitForm("#" + form);
//                return false;
//            }
//        }
//    });
//}

var getVideoId = function (url) {
    if (url.indexOf('v=') !== -1) {
        var videoId = url.split('v=')[1];
        if (videoId.indexOf('&') !== -1) {
            videoId = videoId.substring(0, v);
        }
        return videoId;
    } else
        return url.split('/').pop();
};
var convertToEmbed = function (url) {
    url = url.replace('youtu.be', 'www.youtube.com/embed');
    return url.replace("watch?v=", 'embed/');
}

var getViewContactUsModal = function () {
    var bsModal = $("#basicModal");
    $(".btnViewContactUs").off('click').click(function () {
        var id = $(this).attr("data-id");
        var _this = this;
        bsModal.html('');
        setTimeout(function () {
            bsModal.load('/ControlPanel/ViewContactUs?id=' + id, '', function () {
                bsModal.modal('show');
                replyContactUs();
                if ($(_this).attr("data-isRead") === "true")
                    return false;
                else {
                    $(".page-header").find(".btnViewContactUs[data-id='" + id + "']").remove().remove();
                    $(".page-header").find(".msgCounter")
                        .text(parseInt($(".page-header").find(".msgCounter").last().text()) - 1);
                    contactUsDataTableUpdate();
                }
            });
        }, 100);
    });
};
var replyContactUs = function () {
    $('#ReplyContactUsForm').submit(function () {
        var form = this;
        var postData = $(form).serializeArray();
        var formUrl = $(form).attr("action");
        $.ajax({
            type: "POST",
            cache: false,
            url: formUrl,
            data: postData,
            dataType: "json",
            success: function (data) {
                gsEnableSubmitButton(form);
                if (data.cStatus === "success") {
                    completedSuccessfuly(data.cMsg);
                    contactUsDataTableUpdate();
                    $(".scroll-to-top").click();
                } else if (data.cStatus === "notValid") {
                    notValidOperations(data.cMsg);
                }
                else {
                    notValidOperations(data.cMsg);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                gsNotifyMsg('' + Messages.noResultFound + '', "error");
                gsEnableSubmitButton(form);
            }
        });
    });
}
var contactUsDataTable = function () {
    $('#tblContactUs').dataTable({
        "language": {
            "url": "../Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
        },
        "bServerSide": true,
        "sAjaxSource": "/ControlPanel/getContactUsDataTable",
        "bProcessing": true,
        "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
        "aaSorting": [[7, 'asc']],
        //"fnServerParams": function (aoData) {
        //    aoData.push({ "name": "Id", "value": $("#txtContactUsSearch").attr("data-Id") },
        //        { "name": "Type", "value": $("#ddlType").val() });
        //},
        "bStateSave": true,
        "aoColumns": [
            { "sType": "html", "sWidth": '10%', "mDataProp": "Name", "sClass": "tdCenter" },
            { "sType": "html", "sWidth": '15%', "mDataProp": "Email" },
            { "sType": "html", "sWidth": '20%', "mDataProp": "Subject" },
            { "sType": "html", "sWidth": '20%', "mDataProp": "Message" },
            { "sType": "html", "sWidth": '15%', "mDataProp": "InsertedDate", "bSortable": false },
            { "sType": "html", "sWidth": '10%', "mDataProp": "IsAnswered" },
            { "sType": "html", "sWidth": '10%', "mDataProp": "IsRead" },
            { "sType": "html", "sWidth": '5%', "mDataProp": "Id", "sClass": "tdCenter" }
        ],
        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {

            if (true) {
                $('td:eq(7)', nRow).html('<div class="btn-group">' +
                    '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                    '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                    '</a>' +
                    ' <ul class="dropdown-menu pull-right">' +
                    '<li>' +
                    '<a href="javascript:;" class="lnk btnViewContactUs" data-id="' + aData.Id + '" data-isRead="' + aData.IsRead + '"><i class="fa fa-edit fa-fw"></i> ' + Messages.view + '</a>' +
                    ' </li>' +
                    ' <li>' +
                    '<a href="javascript:;" class="lnk btnDeleteContactUs" data-id ="' + aData.Id + '"><i class="fa fa-trash fa-fw"></i> ' + Messages.delete + '</a>' +
                    ' </li>' +
                    ' </li>' +
                    '</ul>' +
                    ' </div>');
            }
            if (aData.IsAnswered === true) {
                $('td:eq(5)', nRow).html("<span class='font-green-meadow fa fa-fw fa-check-circle-o fa-lg'></span>");
            } else {
                $('td:eq(5)', nRow).html("<span class='font-red-thunderbird fa fa-fw fa-times-circle-o fa-lg'></span>");
            }

            if (aData.IsRead === true) {
                $('td:eq(6)', nRow).html("<span class='font-green-meadow fa fa-fw fa-check-circle-o fa-lg'></span>");
            } else {
                $('td:eq(6)', nRow).html("<span class='font-red-thunderbird fa fa-fw fa-times-circle-o fa-lg'></span>");
            }
            $(nRow).dblclick(function () {
                viewContactUsModel($(this).find(".btnViewContactUs").attr("data-id"), $("#basicModal"), $(".btnViewContactUs"));
            });
        },
        "fnDrawCallback": function (oSettings) {
            getViewContactUsModal();
            deleteContactUs();
        },
        "bFilter": false
        //"sPaginationType": "bootstrap"
    });
};
var contactUsDataTableUpdate = function () {
    var oTable = $('#tblContactUs').dataTable();
    oTable.fnDraw(false);
};
var viewContactUsModel = function (id, bsModal, btn) {
    bsModal.html('');
    setTimeout(function () {
        bsModal.load('/ControlPanel/ViewContactUs?id=' + id, '', function () {
            bsModal.modal('show');
            replyContactUs();
            if (btn.attr("data-isRead") === "true")
                return false;
            else {
                $(".page-header").find("li[data-id='" + id + "']").remove().remove();
                $(".page-header").find(".msgCounter")
                    .text(parseInt($(".page-header").find(".msgCounter").last().text()) - 1);
                contactUsDataTableUpdate();
            }
        });
    }, 100);
}
var deleteContactUs = function () {
    $(".btnDeleteContactUs").off('click').click(function () {
        var id = $(this).attr('data-Id');
        gsConfirm('' + Messages.deleteConfirm + '', function (result) {
            if (result) {
                $.ajax({
                    type: "POST",
                    cache: false,
                    url: '/ControlPanel/DeleteContactUs',
                    dataType: "JSON",
                    data: { 'id': id },
                    success: function (data) {
                        if (data.cStatus === "success") {
                            gsNotifyMsg(data.cMsg, data.cStatus);
                            contactUsDataTableUpdate();

                        } else {
                            gsNotifyMsg(data.cMsg, data.cStatus);
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        gsNotifyMsg('' + Messages.noResultFound + '', "error");
                    }
                });
            }
        });
    });
};

var getViewCommentModal = function () {
    var bsModal = $("#basicModal");
    $(".btnViewComment").off('click').click(function () {
        var id = $(this).attr("data-id");
        bsModal.html('');
        setTimeout(function () {
            bsModal.load('/ControlPanel/ViewComment?id=' + id, '', function () {
                bsModal.modal('show');
                approveComment();
            });
        }, 100);
    });
};
var commentsDataTable = function () {
    $('#tblComments').dataTable({
        "language": {
            "url": "../Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
        },
        "bServerSide": true,
        "sAjaxSource": "/ControlPanel/getCommentsDataTable",
        "bProcessing": true,
        "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
        "aaSorting": [[4, 'desc']],
        "fnServerParams": function (aoData) {
            aoData.push(
                { "name": "Name", "value": $("#txtNameSearch").val() },
                { "name": "Email", "value": $("#txtEmailSearch").val() });
        },
        "bStateSave": false,
        "aoColumns": [
            { "sType": "html", "sWidth": '20%', "mDataProp": "Title", "sClass": "tdCenter", "bSortable": false },
            { "sType": "html", "sWidth": '10%', "mDataProp": "Name", "sClass": "tdCenter" },
            { "sType": "html", "sWidth": '10%', "mDataProp": "Email" },
            { "sType": "html", "sWidth": '30%', "mDataProp": "Comment" },
            { "sType": "html", "sWidth": '15%', "mDataProp": "Date" },
            { "sType": "html", "sWidth": '10%', "mDataProp": "IsApproved" },
            { "sType": "html", "sWidth": '5%', "mDataProp": "Id", "sClass": "tdCenter", "bSortable": false }
        ],
        "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {

            if (true) {
                var html = '<div class="btn-group">' +
                    '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                    '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                    '</a>' +
                    ' <ul class="dropdown-menu pull-right">' +
                    '<li>' +
                    '<a href="javascript:;" class="lnk btnViewComment" data-id="' + aData.Id + '" data-isRead="' + aData.IsRead + '"><i class="fa fa-edit fa-fw"></i> ' + Messages.view + '</a>' +
                    ' </li>' +
                    ' <li>' +
                    '<a href="javascript:;" class="lnk btnDeleteComment" data-id ="' + aData.Id + '"><i class="fa fa-trash fa-fw"></i> ' + Messages.delete + '</a>' +
                    ' </li>';
                if (!aData.IsApproved) {
                    html += ' <li>' +
                        '<a href="javascript:;" class="lnk btnApproveComment" data-id ="' + aData.Id + '"><i class="fa fa-check fa-fw"></i> ' + Messages.approve + '</a>' +
                        ' </li>';
                }
                html += '</ul>' + ' </div>';
                $('td:eq(6)', nRow).html(html);
            }
            if (aData.IsApproved) {
                $('td:eq(5)', nRow).html("<span class='font-green-meadow fa fa-fw fa-check-circle-o fa-lg'></span>");
            } else {
                $('td:eq(5)', nRow).html("<span class='font-red-thunderbird fa fa-fw fa-times-circle-o fa-lg'></span>");
            }

            $(nRow).dblclick(function () {
                viewCommentModel($(this).find(".btnViewComment").attr("data-id"), $("#basicModal"), $(".btnViewComment"));
            });
        },
        "fnDrawCallback": function (oSettings) {
            getViewCommentModal();
            approveComment();
            deleteComment();
        },
        "bFilter": false
        //"sPaginationType": "bootstrap"
    });
};
var commentsDataTableUpdate = function () {
    var oTable = $('#tblComments').dataTable();
    oTable.fnDraw(false);
};
var commentsSearch = function () {
    $("#btnSearch").off('click').click(function () {
        commentsDataTableUpdate();
    });
};
var resetCommentsDataTable = function () {
    $("#btnClearForm").off("click").click(function () {
        gsResetInsertForm("SearchForm");
        commentsDataTableUpdate();
    });
};
var viewCommentModel = function (id, bsModal, btn) {
    bsModal.html('');
    setTimeout(function () {
        bsModal.load('/ControlPanel/ViewComment?id=' + id, '', function () {
            bsModal.modal('show');
            approveComment();
        });
    }, 100);
}
var deleteComment = function () {
    $(".btnDeleteComment").off('click').click(function () {
        var id = $(this).attr('data-Id');
        gsConfirm('' + Messages.deleteConfirm + '', function (result) {
            if (result) {
                $.ajax({
                    type: "POST",
                    cache: false,
                    url: '/ControlPanel/DeleteComment',
                    dataType: "JSON",
                    data: { 'id': id },
                    success: function (data) {
                        if (data.cStatus === "success") {
                            gsNotifyMsg(data.cMsg, data.cStatus);
                            commentsDataTableUpdate();

                        } else {
                            gsNotifyMsg(data.cMsg, data.cStatus);
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        gsNotifyMsg('' + Messages.noResultFound + '', "error");
                    }
                });
            }
        });
    });
};
var approveComment = function () {
    $(".btnApproveComment").off('click').click(function () {
        var id = $(this).attr('data-Id');
        gsConfirm('' + Messages.approveConfirm + '', function (result) {
            if (result) {
                $.ajax({
                    type: "POST",
                    cache: false,
                    url: '/ControlPanel/ApproveComment',
                    dataType: "JSON",
                    data: { 'id': id },
                    success: function (data) {
                        if (data.cStatus === "success") {
                            gsNotifyMsg(data.cMsg, data.cStatus);
                            $(".page-header").find(".btnViewComment[data-id='" + id + "']").remove().remove();
                            $(".page-header").find(".commentsCounter")
                                .text(parseInt($(".page-header").find(".commentsCounter").last().text()) - 1);
                            commentsDataTableUpdate();

                        } else {
                            gsNotifyMsg(data.cMsg, data.cStatus);
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        gsNotifyMsg('' + Messages.noResultFound + '', "error");
                    }
                });
            }
        });
    });
};

var gsAutoFocus=function(btn) {
    $('.modal').on('shown.bs.modal',
        function() {
            $(".modal input[type='text']")[0].focus();
            $('button[type="reset"]').off('click').click(function () {
                debugger;
                $(".modal input[type='text']")[0].focus();
            });
        });
}
var gsSubmitForm=function(form) {
    $(form).find("button[type='submit']").click();
}
var gsRedirect = function (url) {
    $("*").off("keydown").keydown(function (e) {
        if (!$(e.target).hasClass("form-control") && e.which == 107)
            window.location.href = url;
    });
}
var BlockForm = function (form) {
    App.blockUI({
        target: form,
        animate: !0
    }), window.setTimeout(function () {
        App.unblockUI(form);
    }, 2e3);
}
var FormNotifyMsg = function (msg, status, form) {
    $(form).find('.alert').remove();
    div = '<div class="alert alert-' + status + ' alert-dismissible fade show" role="alert">' +
        '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
        '<span aria-hidden="true">&times;</span>' +
        '</button>' + msg +
        '</div>';
    $(form).prepend(div);
    //if (status == 'success')
    $(form).find('.alert').fadeTo(2000, 500);
};
var isValid = function (form) {
    _isvalid = false;
    if ($(form).find('input.is-invalid').length === 0 && $(form).find('button[type="submit"].disabled').length === 0)
        _isvalid = true;
    return _isvalid;
};
var validate = function () {
    $.each(lst_ra, function (index, item) {
        $('*[name$="' + item + '"]').attr('required', 'required');
    });
}
var NotifyMsg = function (msg) {
    var t = {
        theme: 'lemon',
        sticky: false,
        horizontalEdge: 'right',
        verticalEdge: 'top'
    },
    n = $(this);
    "" !== $.trim('Alert') && (t.heading = $.trim('Alert')), t.sticky || (t.life = 10000), $.notific8("zindex", 11500),
        $.notific8($.trim(msg), t), n.attr("disabled", "disabled"), setTimeout(function() {
                n.removeAttr("disabled"); }, 1e3);
};
var Confirm = function (title, text, type) {
    /*bootbox.confirm({
        message: message,
        callback: function (result) { item(result); },
        buttons: {
            confirm: {
                label: Messages.yes,
                className: "red btn-outline"
            },
            cancel: {
                label: Messages.cancel,
                className: 'dark btn-outline'
            }
        }
    });*/
    return swal({
        title: title,
        text: text,
        type: type,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: Messages.delete
    });
};
var ConfirmYN = function (message, item) {
    bootbox.confirm({
        message: message,
        callback: function (result) { item(result); },
        buttons: {
            confirm: {
                label: Messages.yes,
                className: "red btn-outline"
            },
            cancel: {
                label: Messages.no,
                className: 'dark btn-outline'
            }
        }
    });
};
var ReloadDataTable = function () {
    $(".reload").each(function () {
        $(this).off("click").click(function () {
            var x = $(this).closest(".portlet").find("table").attr("id");
            var oTable = $('#' + x + '').dataTable();
            oTable.fnDraw();
        });
    });
};
var completedSuccessfulyMultiform = function (msg, form) {
    $(".alert-success", form).html(msg);
    $(".alert-danger", form).css("display", "none");
    $(".alert-success", form).css("display", "block");
    setTimeout(function () {
        $(".alert-success", form).css("display", "none");
    }, 3000);
};
var notValidOperationsMultiform = function (msg, form) {
    $(".alert-danger", form).html(msg);
    $(".alert-danger", form).css("display", "block");
    $(".alert-success", form).css("display", "none");
};
var ResetInsertForm = function (form) {
    document.getElementById(form).reset();
    //$('.selectpicker').selectpicker('render');
};
var EnableSubmitButton = function (form) {
    setTimeout(function () {
        $(form).find("button[Type='submit']").removeAttr("disabled");
    }, 2000);
};
var DisablSubmitButton = function (form) {
    $(form).find("button[Type='submit']").attr("disabled", "true");
};

var ConvertJsDateTimeToReadable = function (jsDate) {
    if (jsDate !== null) {
        var date = new Date(parseInt(jsDate.substr(6)));
        var sMonth = date.getMonth() + 1;
        return date.getDate() + "-" + sMonth + "-" + date.getFullYear() + "  " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    } else
        return false;
};
var handleDatePickersRangeOpenFrom = function (from, to) {
    var days = 1;
    var d = new Date();

    $(from).datepicker({
        rtl: App.isRTL(),
        orientation: "left",
        autoclose: true,
        format: 'dd-mm-yyyy'
    }).on('changeDate', function () {
        $(to).datepicker('remove');
        //debugger;
        var diff = new Date($(from).datepicker('getDate') - d) / (1000 * 60 * 60 * 24);
        days = 1 + Math.round(diff);

        //var toDate = new Date($('#tbFromDate').datepicker('getDate') + days);
        //days = 1;

        var toDate = $(from).datepicker('getDate');
        toDate.setDate(toDate.getDate() + days);
        var dd = toDate.getDate();
        var mm = toDate.getMonth() + 1;
        $(to).val((dd > 9 ? '' : '0') + dd + '-' + (mm > 9 ? '' : '0') + mm + '-' + toDate.getFullYear());

        $(to).datepicker({
            rtl: App.isRTL(),
            orientation: "left",
            autoclose: true,
            format: 'dd-mm-yyyy',
            startDate: '+' + -5 + 'd'
        });
    });

    $(to).datepicker({
        rtl: App.isRTL(),
        orientation: "left",
        autoclose: true,
        format: 'dd-mm-yyyy',
        startDate: '+1d'
    });
}

var dropifyUpload = function (fileUploader, url) {
    flag = true;
    $(fileUploader).off('change').change(function () {
        if (flag === true) {
            flag = false;
            my_file = this.files[0];
            fd = new FormData();
            fd.append("choose-file", my_file);
            $.ajax({
                url: url,
                type: 'POST',
                data: fd,
                //cache: false,
                contentType: false,
                processData: false,
                dataType: "json",
                xhr: function () {
                    xhr = new window.XMLHttpRequest();
                    xhr.upload.addEventListener('progress', function (e) {
                        // THIS IS ONLY RUNS ONCE!!!
                        if (e.lengthComputable) {
                            //    $('#progressbar').css('width', (e.loaded / e.total) * 100 + '%');
                            //console.log((e.loaded / e.total) * 100);
                        }
                    }, false);
                    return xhr;
                },
                beforeSend: function () {
                },
                success: function (data) {
                    flag = true;
                    //debugger;
                    // obj = JSON.parse(data);
                    if ($(fileUploader).closest('.dropify-wrapper').find('span.dropify-render img').length > 0)
                        $(fileUploader).closest('.dropify-wrapper').find('span.dropify-render img').attr("src", data.links[0]);
                    else
                        $(fileUploader).closest('.dropify-wrapper').find('span.dropify-render').html("<img src='" + data.links[0] + "'/>");

                    $(fileUploader).closest('.dropify-wrapper').find('span.dropify-render').parent().show();
                    $(fileUploader).attr("imgName", data.clstFilenames[0]);
                }
            });
        };
    });
}

var handleStatus = function () {
    $('.cb:not(status)').off('change').change(function () {
        debugger;
        if ($(this).prop('checked')) {
            $(this).val("true");
        } else {
            $(this).val("false");
        }
    });
    $('.cb.status').off('change').change(function () {
        debugger;
        if ($(this).prop('checked')) {
            $(this).val(2);
            $(this).next().val(2);
        } else {
            $(this).val(3);
            $(this).next().val(3);
        }
    });
}

var getQueryStrings = function () {
    var assoc = {};
    var decode = function (s) { return decodeURIComponent(s.replace(/\+/g, " ")); };
    var queryString = location.search.substring(1).toLowerCase();
    var keyValues = queryString.split('&');

    for (var i in keyValues) {
        var key = keyValues[i].split('=');
        if (key.length > 1) {
            assoc[decode(key[0])] = decode(key[1]);
        }
    }

    return assoc;
}

var AddEditProfile = function () {
    $("#myProfile").off("click").click(function () {
        var modal = $("#AddEditProfileModel");
        console.log(modal.data("id"));
        var link = '/setting/AddEditProfileModel/';
        modal.html('');
        setTimeout(function () {
            modal.load(link, '', function () {
                AddEditProfileForm();
                modal.modal('show');
                dropifyUpload($("#imgUpload"), "/Setting/Upload");
            });
        }, 100);
    });
}

AddEditProfileForm = function () {
    form = $("#AddEditForm");
    $(form).off('submit').submit(function () {
        DisablSubmitButton(form);
        if (isValid(form)) {
            postData = $(form).serializeArray();
            formUrl = $(form).attr("action");
            postData.push({ name: "Avatar", value: $("#imgUpload").attr("imgName") });
            $.ajax({
                type: "POST",
                cache: false,
                url: formUrl,
                data: postData,
                dataType: "json",
                success: function (data) {
                    EnableSubmitButton(form);
                    if (data.cStatus === "success") {
                        FormNotifyMsg(data.cMessage, data.cStatus, form);
                        ResetInsertForm(form.attr('id'));
                        //updateTable();
                    } else {
                        FormNotifyMsg(data.cMessage, data.cStatus, form);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    FormNotifyMsg('' + Messages.noResultFound + '', "warning", form);
                    EnableSubmitButton(form);
                }
            });
        }
    });
};


var handleDatePickersRange = function (start, from, to) {
    days = 1;
    d = new Date();

    $(from).datepicker({
        rtl: App.isRTL(),
        orientation: "left",
        autoclose: true,
        format: 'dd-mm-yyyy',
        startDate: start.length > 0 ? start + 'd' : ''
    }).on('changeDate', function () {
        $(to).datepicker('remove');
        //debugger;
        if ($(from).datepicker('getDate') > d) {
            diff = new Date($(from).datepicker('getDate') - d) / (1000 * 60 * 60 * 24);

            //diff = new Date($(from).datepicker('getDate') - d) / (1000 * 60 * 60 * 24);

            //if (parseInt(diff.toFixed(2).toString().split(".")[1]) > 5)
            //    days = 2 + Math.round(diff);
            //else
            //    days = 1 + Math.round(diff);

            days = 1 + Math.round(diff);

            toDate = new Date();
            toDate.setDate(toDate.getDate() + days);
            dd = toDate.getDate();
            mm = toDate.getMonth() + 1;
            $(to).val((dd > 9 ? '' : '0') + dd + '-' + (mm > 9 ? '' : '0') + mm + '-' + toDate.getFullYear());

            $(to).datepicker({
                rtl: App.isRTL(),
                orientation: "left",
                autoclose: true,
                format: 'dd-mm-yyyy',
                startDate: '+' + days + 'd'
            });
        } else {
            debugger;
            diff = new Date(d - $(from).datepicker('getDate')) / (1000 * 60 * 60 * 24);
            days = Math.round(diff) - 2;

            toDate = new Date();
            toDate.setDate(toDate.getDate() - days);
            dd = toDate.getDate();
            mm = toDate.getMonth() + 1;
            $(to).val((dd > 9 ? '' : '0') + dd + '-' + (mm > 9 ? '' : '0') + mm + '-' + toDate.getFullYear());

            $(to).datepicker({
                rtl: App.isRTL(),
                orientation: "left",
                autoclose: true,
                format: 'dd-mm-yyyy',
                startDate: '-' + days + 'd'
            });
        }
        //debugger;
        _fromPicker = $(from).datepicker('getDate');
        _from = _fromPicker.getDate() + "/" + (_fromPicker.getMonth() + 1) + "/" + _fromPicker.getFullYear();
        _toPicker = $(to).datepicker('getDate');
        _to = _toPicker.getDate() + "/" + (_toPicker.getMonth() + 1) + "/" + _toPicker.getFullYear();
        //console.log(_from);
        //console.log(_to);
        if (_from === _to) {
            toDate = new Date(_toPicker.getFullYear(), (_toPicker.getMonth()), (_toPicker.getDate() + 1));
            dd = toDate.getDate();
            mm = toDate.getMonth() + 1;
            $(to).val((dd > 9 ? '' : '0') + dd + '-' + (mm > 9 ? '' : '0') + mm + '-' + toDate.getFullYear());
            $(to).datepicker({
                rtl: App.isRTL(),
                orientation: "left",
                autoclose: true,
                format: 'dd-mm-yyyy',
                startDate: '-' + 1 + 'd'
            });
        }
    });

    $(to).datepicker({
        rtl: App.isRTL(),
        orientation: "left",
        autoclose: true,
        format: 'dd-mm-yyyy',
        startDate: '+1d'
    });
}

var menuSearch=function() {
    $("#txtSidebarSearch").off("keyup").keyup(function () {
        var filter = $(this).val().trim().toUpperCase();
        $.each($("ul.page-sidebar-menu li.nav-item"),function(index, item) {
            var a = $(item).find('a').text().trim().toUpperCase();
            if (a.indexOf(filter) > -1) {
                $(item).css("display", "block");
                $(item).closest('ul').css("display", "block");
                $(item).closest('ul.sub-menu').addClass("open");
                $(item).closest('ul.sub-menu').closest("li.nav-item").addClass("open");
            } else {
                $(item).css("display", "none");
                $(item).closest('ul.sub-menu').removeClass("open");
                $(item).closest('ul.sub-menu').closest("li.nav-item").removeClass("open");
            }
        });
    });
}

var drawAmChart = function (div, data, label, value, horizontalGap, labelRotation) {
    AmCharts.makeChart(div, {
        "type": "serial",
        "theme": "light",
        "dataProvider": data,
        "valueAxes": [{
            //"position": "right",
            "gridColor": "#FFFFFF",
            "gridAlpha": 0.2,
            "dashLength": 0
        }],
        "gridAboveGraphs": true,
        //"rtl": true,
        "startDuration": 1,
        "graphs": [{
            "balloonText": "[[category]]: <b>[[value]]</b>",
            "fillAlphas": 0.9,
            "lineAlpha": 0.2,
            "type": "column",
            "valueField": value
        }],
        "chartCursor": {
            "categoryBalloonEnabled": false,
            "cursorAlpha": 0,
            "zoomable": false
        },
        "categoryField": label,
        "categoryAxis": {
            "gridPosition": "start",
            "gridAlpha": 0,
            "tickPosition": "start",
            "tickLength": 20,
            "labelRotation": labelRotation,
            "minHorizontalGap": horizontalGap
        },
        "export": {
            "enabled": false
        }

    });

}
var drawAmChartDoubleCol = function (div, data, catField, label1, value1, label2, value2, horizontalGap, labelRotation) {
    AmCharts.makeChart(div, {
        "type": "serial",
        "theme": "light",
        "categoryField": catField,
        //"rotate": true,
        "startDuration": 1,
        "categoryAxis": {
            "gridPosition": "start",
            "position": "left",
            "labelRotation": labelRotation,
            "minHorizontalGap": horizontalGap
        },
        "trendLines": [],
        "graphs": [
            {
                "balloonText": ""+label1+":[[value]]",
                "fillAlphas": 0.9,
                "id": "AmGraph-1",
                "lineAlpha": 0.2,
                "title": label1,
                "type": "column",
                "valueField": value1
            },
            {
                "balloonText": "" + label2 +":[[value]]",
                "fillAlphas": 0.8,
                "id": "AmGraph-2",
                "lineAlpha": 0.2,
                "title": label2,
                "type": "column",
                "valueField": value2
            }
        ],
        "guides": [],
        "valueAxes": [
            {
                "id": "ValueAxis-1",
                "position": "left",
                "axisAlpha": 0
            }
        ],
        "allLabels": [],
        "balloon": {},
        "titles": [],
        "dataProvider": data,
        "export": {
            "enabled": false
        }

    });

}


var gsDelete = function(url, id) {
    if (id > 0) {
        $.ajax({
            type: "POST",
            cache: false,
            url: url,
            dataType: "JSON",
            async: false,
            data: { 'id': id },
            success: function(data) {
                if (data.cStatus === "success") {
                    gsNotifyMsg(data.cMsg, data.cStatus);
                } else {
                    gsNotifyMsg(data.cMsg, data.cStatus);
                }
            },
            error: function(xhr, ajaxOptions, thrownError) {
                gsNotifyMsg('' + Messages.noResultFound + '', "error");
            }
        });
    }
}
var gsPrint = function(url) {
    var printWindow = window.open(url, 'Print', 'left=200, top=100, width=950, height=500, toolbar=0, resizable=0');
    printWindow.addEventListener('load', function () {
        printWindow.print();
        printWindow.close();
    }, true);
}


/*************Hot Keys***************/
// print => f1
var hk_shortcuts = function () {
    $(document).bind('keydown', 'f1', function () {
        var model = $("#shortcutsModel");
        if (!model.isVisible) {
            model.modal('show');
        }
        return false;
    });
}
// print => Ctrl+p
var hk_print=function(btn) {
    $(document).bind('keydown', 'Ctrl+p', function () {
        if ($(btn).first().length > 0)
            $(btn).first().click();
    });
}
// cancel => Shift+delete
var hk_cancel = function (btn) {
    $(document).bind('keydown', 'Shift+del', function () {
        if ($(btn).first().length > 0)
            $(btn).first().click();
    });
}
// save => Ctrl+enter
var hk_save = function (btn) {
    $(document).bind('keydown', 'Ctrl+return', function () {
        if ($(btn).first().length > 0)
            $(btn).first().click();
    });
}
// edit => Ctrl+Shift
var hk_edit = function (btn) {
    $(document).bind('keydown', 'Ctrl+Shift', function () {
        if ($(btn).first().length > 0)
            $(btn).first().click();
    });
}
// close widnow => Esc
var hk_esc = function () {
    $(document).bind('keydown', 'Esc', function () {
        if ($('.modal-backdrop.fade.in').length === 0) {
            var url = window.location.pathname.toLowerCase();
            var breadcrumb = $('.page-breadcrumb li a');
            $.each(breadcrumb,
                function(ind, item) {
                    if (url.includes($(item).attr('href').toLowerCase())) {
                        var p = $(item).closest('li').prev('li').find('a').attr('href');
                        if (p == 'javascript:;')
                            p = '/Home';
                        window.location.href = p;
                    }
                });
        }
    });
}
// first => Ctrl+up , prev => Ctrl+right , next => Ctrl+left, last => Ctrl+down
var hk_pageination = function (first,prev,next,last) {
    $(document).bind('keydown', 'Ctrl+up', function () {
        if ($(first).first().length > 0) {
            var link = $(first).first().attr("href");
            window.location = link;
        }
    });
    $(document).bind('keydown', 'Ctrl+right', function () {
        if ($(prev).first().length > 0) {
            var link = $(prev).first().attr("href");
            window.location = link;
        }
    });
    $(document).bind('keydown', 'Ctrl+left', function () {
        if ($(next).first().length > 0) {
            var link = $(next).first().attr("href");
            window.location = link;
        }
    });
    $(document).bind('keydown', 'Ctrl+down', function () {
        if ($(last).first().length > 0) {
            var link = $(last).first().attr("href");
            window.location = link;
        }
    });
}
var hk_dt_pageination = function (first, prev, next, last) {
    $(document).bind('keydown', 'Ctrl+up', function () {
        if ($(first).first().length > 0) {
            $(first).first().click();
        }
    });
    $(document).bind('keydown', 'Ctrl+right', function () {
        if ($(prev).first().length > 0) {
            $(prev).first().click();
        }
    });
    $(document).bind('keydown', 'Ctrl+left', function () {
        if ($(next).first().length > 0) {
            $(next).first().click();
        }
    });
    $(document).bind('keydown', 'Ctrl+down', function () {
        if ($(last).first().length > 0) {
            $(last).first().click();
        }
    });
}
// transfare => f5, reverse
var hk_transfare = function (btn) {
    $(document).bind('keydown', 'f5', function () {
        if ($(btn).first().length > 0)
            $(btn).first().click();
    });
}

var hk_itemTransactions = function (func) {
    $("tr").off("keydown").keydown(function (e) {
        if (e.which == 120) {
            var itemNo = $(e.target).closest("tr").find(".item").data("id");
            if (itemNo)
                func(itemNo);
            return false;
        }
    });
}