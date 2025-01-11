var ManagementScripts = function () {
    //constant
    var constantDataTable = function () {
        $('#tblConstant').dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
            },
            "bServerSide": true,
            "sAjaxSource":   "/Management/GetConstantDataTable",
            "bProcessing": true,
            "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
            "aaSorting": [[0, 'desc']],
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "ConstantId", "value": $("#txtConstantSearch").attr("data-id") },
                            { "name": "ParentId", "value": $("#ddlParentConstant").val() });
            },
            "bStateSave": false,
            "aoColumns": [
                { "sType": "html", "sWidth": '5%', "mDataProp": "Id"},
                { "sType": "html", "sWidth": '45%', "mDataProp": "Name", "bSortable": false },
                { "sType": "html", "sWidth": '45%', "mDataProp": "ParentName", "bSortable": false },
                { "sType": "html", "sWidth": '5%', "mDataProp": "Id" }
            ],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.ParentName === null) {
                    $('td:eq(2)', nRow).html("<u>ثابت رئيسي</u>");
                }
                    $('td:eq(3)', nRow).html('<div class="btn-group">' +
                        '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                        '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                        '</a>' +
                        ' <ul class="dropdown-menu pull-right">' +
                        '<li>' +
                        '<a href="javascript:;" class="lnk btnUpdateConstant" data-id="' + aData.Id + '"><i class="fa fa-edit fa-fw"></i> ' + Messages.edit + '</a>' +
                        ' </li>' +
                        ' <li>' +
                        '<a href="javascript:;" class="lnk btnDeleteConstant" data-id ="' + aData.Id + '"><i class="fa fa-trash fa-fw"></i> ' + Messages.delete + '</a>' +
                        ' </li>' +
                        '</ul>' +
                        ' </div>');
                
                $(nRow).dblclick(function () {
                    updateConstantModel($(this).find(".btnUpdateConstant").attr("data-id"), $("#basicModal"));
                });
            },
            "fnDrawCallback": function (oSettings) {
                getUpdateConstantModal();
                deleteConstant();
            },
            "bFilter": false
            //"sPaginationType": "bootstrap"
        });
    };
    var constantDataTableUpdateWithReSort = function () {
        var oTable = $('#tblConstant').dataTable();
        oTable.fnDraw(false);
    };
    var constantDataTableUpdate = function () {
        var oTable = $('#tblConstant').dataTable();
        oTable.fnDraw(false);
    };
    var constantSearch = function () {
        $("#btnSearch").off('click').click(function () {
            constantDataTableUpdateWithReSort();
        });
    };
    var resetConstantDataTable = function () {
        $("#btnClearForm").off("click").click(function () {
            $("#txtConstantSearch").removeAttr("data-id");
            //$("#ddlParentConstant").val("0");
            //$("#txtConstantSearch").val("");
            gsResetInsertForm("SearchForm");
            constantDataTableUpdateWithReSort();
        });
    };
    var getInsertConstantModal = function () {
        var bsModal = $("#basicModal");
        $("#btnGetInsertConstantModal").off('click').click(function () {
            bsModal.html('');
            setTimeout(function () {
                bsModal.load( '/Management/InsertConstantModal', '', function () {
                    bsModal.modal('show'); resetbooststrapSelect();
                    validateInsertConstantForm();
                    handleBootstrapSelect();
                });
            }, 10);
        });
    };
    var getUpdateConstantModal = function () {
        var bsModal = $("#basicModal");
        $(".btnUpdateConstant").off('click').click(function () {
            var id = $(this).attr("data-id");
            bsModal.html('');
            setTimeout(function () {
                bsModal.load( '/Management/UpdateConstantModal?id=' + id, '', function () {
                    bsModal.modal('show'); resetbooststrapSelect();
                    validateUpdateConstantForm();
                    handleBootstrapSelect();
                });
            }, 100);
        });
    };
    var updateConstantModel = function (id, bsModal) {
        bsModal.html('');
        setTimeout(function () {
            bsModal.load( '/Management/UpdateConstantModal?id=' + id, '', function () {
                bsModal.modal('show');
                resetbooststrapSelect();
                validateUpdateConstantForm();
                handleBootstrapSelect();
            });
        }, 100);
    }
    var validateInsertConstantForm = function () {
        // for more info visit the official plugin documentation: 
        // http://docs.jquery.com/Plugins/Validation

        var form2 = $('#InsertConstantForm');
        var error2 = $('.alert-danger', form2);
        var success2 = $('.alert-success', form2);

        form2.validate({
            ignoreTitle: true,
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                Name: {
                    minlength: 3,
                    maxlength: 50,
                    required: true
                },
                Comment: {
                    minlength: 3,
                    maxlength: 500
                },
                ParentId: {
                    required: true
                },
                Icon: {
                    maxlength: 50
                }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit 
                success2.hide();
                var errors = validator.numberOfInvalids();
                var messageDetails = '';
                if (errors) {
                    $.each(validator.errorList, function () {
                        messageDetails += '<li><u><b>' + this.element.attributes["data-field"].nodeValue + "</b></u> : " + this.message + '</li>';
                    });
                    var message;
                    if (errors === 1) {
                        message = '' + Messages.errorInField + '';
                    }
                    else if (errors > 1 && errors < 11) {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    } else {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    }
                    message += '<button class="close" data-close="alert"></button><br><ul>' + messageDetails + '</ul>';
                    error2.html(message);



                    error2.show();
                } else {
                    error2.hide();
                }
            },
            errorPlacement: function (error, element) { // render error placement for each input type

            },
            highlight: function (element) { // hightlight error inputs
                $(element).closest('.formElement').addClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').removeClass('has-success');
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.formElement').removeClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').addClass('has-success');
            },
            success: function (label) {
                label.closest('.formElement').removeClass('has-error').addClass('has-success'); // set success class to the control group
            },
            submitHandler: function (form) {
                gsDisablSubmitButton(form2); success2.show();
                error2.hide();
                insertConstant();
            }
        });
    };
    var validateUpdateConstantForm = function () {
        // for more info visit the official plugin documentation: 
        // http://docs.jquery.com/Plugins/Validation

        var form2 = $('#UpdateConstantForm');
        var error2 = $('.alert-danger', form2);
        var success2 = $('.alert-success', form2);

        form2.validate({
            ignoreTitle: true,
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                Id: {
                    required: true
                },
                Name: {
                    minlength: 3,
                    maxlength: 50,
                    required: true
                },
                Comment: {
                    minlength: 3,
                    maxlength: 500
                },
                ParentId: {
                    required: true
                },
                Icon: {
                    maxlength: 50
                }
                
            },
            invalidHandler: function (event, validator) { //display error alert on form submit 
                success2.hide();
                var errors = validator.numberOfInvalids();
                var messageDetails = '';
                if (errors) {
                    $.each(validator.errorList, function () {
                        messageDetails += '<li><u><b>' + this.element.attributes["data-field"].nodeValue + "</b></u> : " + this.message + '</li>';
                    });
                    var message;
                    if (errors === 1) {
                        message = '' + Messages.errorInField + '';
                    }
                    else if (errors > 1 && errors < 11) {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    } else {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    }
                    message += '<button class="close" data-close="alert"></button><br><ul>' + messageDetails + '</ul>';
                    error2.html(message);
                    error2.show();
                } else {
                    error2.hide();
                }
            },
            errorPlacement: function (error, element) { // render error placement for each input type

            },
            highlight: function (element) { // hightlight error inputs
                $(element).closest('.formElement').addClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').removeClass('has-success');
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.formElement').removeClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').addClass('has-success');
            },
            success: function (label) {
                label.closest('.formElement').removeClass('has-error').addClass('has-success'); // set success class to the control group
            },
            submitHandler: function (form) {
                gsDisablSubmitButton(form2); success2.show();
                error2.hide();
                updateConstant();
            }
        });
    };
    var insertConstant = function () {
        var form = $("#InsertConstantForm");
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
                    gsResetInsertForm("InsertConstantForm");
                    constantDataTableUpdateWithReSort();
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
    };
    var updateConstant = function () {
        var form = $("#UpdateConstantForm");
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
                    constantDataTableUpdate();
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
    };
    var deleteConstant = function () {
        $(".btnDeleteConstant").off('click').click(function () {
            var id = $(this).attr('data-Id');
            gsConfirm('' + Messages.deleteConfirm + '', function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        cache: false,
                        url:   '/Management/DeleteConstant',
                        dataType: "JSON",
                        data: { 'id': id },
                        success: function (data) {
                            if (data.cStatus === "success") {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                                constantDataTableUpdateWithReSort();

                            } else {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                            }

                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            gsNotifyMsg('' + Messages.noResultFound + '', "error");
                            gsEnableSubmitButton(form);
                        }
                    });
                }
            });
        });
    };
    var constantSearchAutoComplete = function () {
        if (!$('#txtConstantSearch').hasClass('tt-input')) {
            var constant = new Bloodhound({
                datumTokenizer: function (d) { return d.tokens; }, 
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                remote: {
                    url:   '/Management/ConstantSearchAutoComplete/%QUERY',
                    wildcard: '%QUERY'
                }
            });

            constant.initialize();

            $('#txtConstantSearch').typeahead({
                hint: true,
                highlight: true,
                minLength: 1,
                dir: true
            }, {
                name: 'search-constant',
                displayKey: 'Name',
                source: constant.ttAdapter(),
                    limit: 20,
                    dir: true,
                templates: {
                    empty: [
                        '<div class="empty-message">',
                        '' + Messages.noResultFound + '',
                        '</div>'
                    ].join('\n'),
                    suggestion: Handlebars.compile([
                       '<div class="media">',
                        '<div class="pull-left">',
                        '<div class="media-object">',
                        '</div>',
                        '</div>',
                        '<div class="media-body">',
                        ' ',
                        ' <h5 class="media-heading">{{Name}}</h5>',
                        '</div>',
                        '</div>',
                    ].join(''))
                },

            }).on('typeahead:selected', function ($e, datum) {
                $("#txtConstantSearch").attr('data-id', datum.Id);
            });
        }
    };
    //Pages
    var pagesDataTable = function () {
        $('#tblPages').dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
            },
            "bServerSide": true,
            "sAjaxSource":  "/Management/GetPagesDataTable",
            "bProcessing": true,
            "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
            "aaSorting": [[4, 'desc']],
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "PageId", "value": $("#txtPagesSearch").attr("data-id") },
                            { "name": "ParentId", "value": $("#ddlParentPage").val() });
            },
            "bStateSave": true,
            "aoColumns": [
                { "sType": "html", "sWidth": '25%', "mDataProp": "Name", "bSortable": false },
                { "sType": "html", "sWidth": '25%', "mDataProp": "Link", "bSortable": false },
                { "sType": "html", "sWidth": '25%', "mDataProp": "ParentName", "bSortable": false },
                { "sType": "html", "sWidth": '10%', "mDataProp": "IsActive", "bSortable": false, "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '5%', "mDataProp": "Id", "sClass": "tdCenter" }
            ],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.ParentName === null) {
                    $('td:eq(2)', nRow).html("<u class='font-green-meadow'>Parent Page</u>");
                }
                if (aData.IsActive === true) {
                    $('td:eq(3)', nRow).html("<span class='font-green-meadow fa fa-fw fa-check-circle-o fa-lg'></span>");
                } else {
                    $('td:eq(3)', nRow).html("<span class='font-red-thunderbird fa fa-fw fa-times-circle-o fa-lg'></span>");
                }
                    $('td:eq(4)', nRow).html('<div class="btn-group">' +
                        '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                        '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                        '</a>' +
                        ' <ul class="dropdown-menu pull-right">' +
                        '<li>' +
                        '<a href="javascript:;" class="lnk btnUpdatePage" data-id="' + aData.Id + '"><i class="fa fa-edit fa-fw"></i> ' + Messages.edit + '</a>' +
                        ' </li>' +
                        ' <li>' +
                        '<a href="javascript:;" class="lnk btnDeletePage" data-id ="' + aData.Id + '"><i class="fa fa-trash fa-fw"></i> ' + Messages.delete + '</a>' +
                        ' </li>' +
                        '</ul>' +
                        ' </div>');
                $(nRow).dblclick(function () {
                    updatePageModel($(this).find(".btnUpdatePage").attr("data-id"), $("#basicModal"));
                });
            },
            "fnDrawCallback": function (oSettings) {
                getUpdatePagesModal();
                deletePage();
            },
            "bFilter": false
            //"sPaginationType": "bootstrap"
        });
    };
    var pagesDataTableUpdateWithReSort = function () {
        var oTable = $('#tblPages').dataTable();
        oTable.fnDraw(false);
    };
    var pagesDataTableUpdate = function () {
        var oTable = $('#tblPages').dataTable();
        oTable.fnDraw(false);
    };
    var pagesSearch = function () {
        $("#btnSearch").off('click').click(function () {
            pagesDataTableUpdateWithReSort();
        });
    };
    var resetPagesDataTable = function () {
        $("#btnClearForm").off("click").click(function () {
            $("#txtPagesSearch").removeAttr("data-id");
            //$("#txtPagesSearch").val("");
            //$("#ddlParentPage").val("0");
            gsResetInsertForm("SearchForm");
            pagesDataTableUpdateWithReSort();
        });
    };
    var getInsertPagesModal = function () {
        var bsModal = $("#basicModal");
        $("#btnGetInsertPagesModal").off('click').click(function () {
            bsModal.html('');
            setTimeout(function () {
                bsModal.load( '/Management/InsertPagesModal', '', function () {
                    bsModal.modal('show'); resetbooststrapSelect();
                    validateInsertPagesForm();
                    handleBootstrapSelect();
                });
            }, 10);
        });
    };
    var getUpdatePagesModal = function () {
        var bsModal = $("#basicModal");
        $(".btnUpdatePage").off('click').click(function () {
            var id = $(this).attr("data-id");
            bsModal.html('');
            setTimeout(function () {
                bsModal.load( '/Management/UpdatePagesModal?id=' + id, '', function () {
                    bsModal.modal('show'); resetbooststrapSelect();
                    validateUpdatePagesForm();
                    handleBootstrapSelect();
                });
            }, 100);
        });
    };
    var updatePageModel = function (id, bsModal) {
        bsModal.html('');
        setTimeout(function () {
            bsModal.load( '/Management/UpdatePagesModal?id=' + id, '', function () {
                bsModal.modal('show'); resetbooststrapSelect();
                validateUpdatePagesForm();
                handleBootstrapSelect();
            });
        }, 100);
    }
    var validateInsertPagesForm = function () {
        // for more info visit the official plugin documentation: 
        // http://docs.jquery.com/Plugins/Validation

        var form2 = $('#InsertPagesForm');
        var error2 = $('.alert-danger', form2);
        var success2 = $('.alert-success', form2);

        form2.validate({
            ignoreTitle: true,
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                Name: {
                    minlength: 3,
                    maxlength: 50,
                    required: true
                },
                ParentId: {
                    required: true
                },
                TypeId: {
                    required: true
                }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit 
                success2.hide();
                var errors = validator.numberOfInvalids();
                var messageDetails = '';
                if (errors) {
                    $.each(validator.errorList, function () {
                        messageDetails += '<li><u><b>' + this.element.attributes["data-field"].nodeValue + "</b></u> : " + this.message + '</li>';
                    });
                    var message;
                    if (errors === 1) {
                        message = '' + Messages.errorInField + '';
                    }
                    else if (errors > 1 && errors < 11) {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    } else {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    }
                    message += '<button class="close" data-close="alert"></button><br><ul>' + messageDetails + '</ul>';
                    error2.html(message);
                    error2.show();
                } else {
                    error2.hide();
                }
            },
            errorPlacement: function (error, element) { // render error placement for each input type

            },
            highlight: function (element) { // hightlight error inputs
                $(element).closest('.formElement').addClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').removeClass('has-success');
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.formElement').removeClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').addClass('has-success');
            },
            success: function (label) {
                label.closest('.formElement').removeClass('has-error').addClass('has-success'); // set success class to the control group
            },
            submitHandler: function (form) {
                gsDisablSubmitButton(form2); success2.show();
                error2.hide();
                insertPages();
            }
        });
    };
    var validateUpdatePagesForm = function () {
        // for more info visit the official plugin documentation: 
        // http://docs.jquery.com/Plugins/Validation

        var form2 = $('#UpdatePagesForm');
        var error2 = $('.alert-danger', form2);
        var success2 = $('.alert-success', form2);

        form2.validate({
            ignoreTitle: true,
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                Id: {
                    required: true
                },
                Name: {
                    minlength: 3,
                    maxlength: 50,
                    required: true
                },
                ParentId: {
                    required: true
                },
                TypeId: {
                    required: true
                }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit 
                success2.hide();
                var errors = validator.numberOfInvalids();
                var messageDetails = '';
                if (errors) {
                    $.each(validator.errorList, function () {
                        messageDetails += '<li><u><b>' + this.element.attributes["data-field"].nodeValue + "</b></u> : " + this.message + '</li>';
                    });
                    var message;
                    if (errors === 1) {
                        message = '' + Messages.errorInField + '';
                    }
                    else if (errors > 1 && errors < 11) {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    } else {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    }
                    message += '<button class="close" data-close="alert"></button><br><ul>' + messageDetails + '</ul>';
                    error2.html(message);
                    error2.show();
                } else {
                    error2.hide();
                }
            },
            errorPlacement: function (error, element) { // render error placement for each input type

            },
            highlight: function (element) { // hightlight error inputs
                $(element).closest('.formElement').addClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').removeClass('has-success');
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.formElement').removeClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').addClass('has-success');
            },
            success: function (label) {
                label.closest('.formElement').removeClass('has-error').addClass('has-success'); // set success class to the control group
            },
            submitHandler: function (form) {
                gsDisablSubmitButton(form2); success2.show();
                error2.hide();
                updatePages();
            }
        });
    };
    var insertPages = function () {
        var form = $("#InsertPagesForm");
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
                    gsResetInsertForm("InsertPagesForm");
                    pagesDataTableUpdateWithReSort();
                } else if (data.cStatus === "notValid") {
                    notValidOperations(data.cMsg);
                }
                else {
                    gsNotifyMsg(data.cMsg, data.cStatus);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                gsNotifyMsg('' + Messages.noResultFound + '', "error");
                gsEnableSubmitButton(form);
            }
        });
    };
    var updatePages = function () {
        var form = $("#UpdatePagesForm");
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
                    pagesDataTableUpdate();
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
    };
    var pagesSearchAutoComplete = function () {
        if (!$('#txtPagesSearch').hasClass('tt-input')) {
            var pages = new Bloodhound({
                datumTokenizer: function (d) { return d.tokens; },
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                remote: {
                    url:   '/Management/PagesSearchAutoComplete/%QUERY',
                    wildcard: '%QUERY'
                }
            });

            pages.initialize();

            $('#txtPagesSearch').typeahead({
                hint: true,
                highlight: true,
                minLength: 1,
                
            }, {
                name: 'search-Pages',
                displayKey: 'Name',
                source: pages.ttAdapter(),
                limit: 30,
                templates: {
                    empty: [
                        '<div class="empty-message">',
                        '' + Messages.noResultFound + '',
                        '</div>'
                    ].join('\n'),
                    suggestion: Handlebars.compile([
                       '<div class="media">',
                        '<div class="pull-left">',
                        '<div class="media-object">',
                        '</div>',
                        '</div>',
                        '<div class="media-body">',
                        ' ',
                        ' <h5 class="media-heading">{{Name}}</h5>',
                        '</div>',
                        '</div>',
                    ].join(''))
                },

            }).on('typeahead:selected', function ($e, datum) {
                $("#txtPagesSearch").attr('data-id', datum.Id);
            });
        }
    };
    var deletePage = function () {
        $(".btnDeletePage").off('click').click(function () {
            var id = $(this).attr('data-Id');
            gsConfirm('' + Messages.deleteConfirm + '', function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        cache: false,
                        url: '/Management/DeletePage',
                        dataType: "JSON",
                        data: { 'id': id },
                        success: function (data) {
                            if (data.cStatus === "success") {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                                pagesDataTableUpdateWithReSort();

                            } else {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                            }

                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            gsNotifyMsg('' + Messages.noResultFound + '', "error");
                            gsEnableSubmitButton(form);
                        }
                    });
                }
            });
        });
    };



    //UserPermission
    var getUserTypePermission = function() {

        $("#ddlUserType").off("change").on("change", function () {
            $("#ddlUserType").selectpicker('refresh');
            if ($(this).val() > 0) {
                if ($(this).val() === "1") {
                    $("input[type=checkbox]").attr("checked", "checked");
                    $("input[type=checkbox]").prop("checked", "true");
                } else {
                    $.ajax({
                        type: "GET",
                        cache: false,
                        url:   "/Management/GetUserTypePermission/" + $(this).val(),
                        dataType: "JSON",
                        success: function(data) {
                            if (data.cResult === "OK") {
                                $("input[type=checkbox]").removeAttr("checked");
                                $.each(data.lstPages, function(index, item) {
                                    $(".cb_" + item.Id).attr("checked", "checked");
                                    $(".cb_" + item.Id).prop("checked", "true");
                                });
                            } else {
                                $("input[type=checkbox]").removeAttr("checked");
                                $("input[type=checkbox]").removeProp("checked");
                            }

                        },
                        error: function(xhr, ajaxOptions, thrownError) {
                        }
                    });
                }
            }

        });
    };
    var saveUserTypePermission = function () {
        $("#btnSavePermission").off("click").click(function () {
            var pages = "";
            $(this).closest(".portlet").find("[Type='checkbox']:checked").each(function () {
                if (parseInt($(this).val()) > 0) {
                    pages = pages + $(this).val() + ",";
                }
            });
            var userType = $("#ddlUserType").val();
            $.ajax({
                type: "POST",
                cache: false,
                dataType: "json",
                url: "/Management/SaveUserPermission",
                data: { 'userTypeId': userType, 'pages': pages },
                success: function (data) {
                    if (data.cStatus === "success") {
                        gsNotifyMsg(data.cMsg, data.cStatus);
                    } else if (data.cStatus === "notValid") {
                        gsNotifyMsg(data.cMsg, data.cStatus);
                    }
                    else {
                        gsNotifyMsg(data.cMsg, data.cStatus);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    gsNotifyMsg('' + Messages.noResultFound + '', "error");
                }
            });
        });
    };


    //Users
    var userDataTable = function () {
        $('#tblUsers').dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
            },
            "bServerSide": true,
            "sAjaxSource":  "/Management/GetUserDataTable",
            "bProcessing": true,
            "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
            "aaSorting": [[7, 'asc']],
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "Name", "value": $("#txtUserSearch").val() },
                            { "name": "UserTypeId", "value": $("#ddlUserType").val() });
            },
            "bStateSave": true,
            "aoColumns": [
                { "sType": "html", "sWidth": '10%', "mDataProp": "Avatar", "bSortable": false, "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '20%', "mDataProp": "Name", "bSortable": false },
                { "sType": "html", "sWidth": '20%', "mDataProp": "Email", "bSortable": false },
                { "sType": "html", "sWidth": '15%', "mDataProp": "Mobile", "bSortable": false },
                { "sType": "html", "sWidth": '10%', "mDataProp": "Gender", "bSortable": false },
                { "sType": "html", "sWidth": '15%', "mDataProp": "UserTypeName", "bSortable": false },
                { "sType": "html", "sWidth": '10%', "mDataProp": "IsActive", "bSortable": false, "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '5%', "mDataProp": "Id", "sClass": "tdCenter" }
            ],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.IsActive === true) {
                    $('td:eq(6)', nRow).html("<span class='font-green-meadow fa fa-fw fa-check-circle-o fa-lg'></span>");
                } else {
                    $('td:eq(6)', nRow).html("<span class='font-red-thunderbird fa fa-fw fa-times-circle-o fa-lg'></span>");
                }
                if (aData.Gender.trim() === "M") {
                    $('td:eq(4)', nRow).html("<span>" + Messages.M+"</span>");
                } else {
                    $('td:eq(4)', nRow).html("<span>" + Messages.F +"</span>");
                }
                    $('td:eq(7)', nRow).html('<div class="btn-group">' +
                        '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                        '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                        '</a>' +
                        ' <ul class="dropdown-menu pull-right">' +
                        '<li>' +
                        '<a href="javascript:;" class="lnk btnUpdateUser" data-id="' + aData.Id + '"><i class="fa fa-edit fa-fw"></i> ' + Messages.edit + '</a>' +
                        ' </li>' +
                        ' <li>' +
                        '<a href="javascript:;" class="lnk btnDeleteUser" data-id ="' + aData.Id + '"><i class="fa fa-trash fa-fw"></i> ' + Messages.delete + '</a>' +
                        ' </li>' +
                        '</ul>' +
                        ' </div>');
                    $('td:eq(0)', nRow).html('<img  style="width: 50px;" alt="" src="/Content/UploadedFile/Account/Avatar/Thumbnail/' + aData.Avatar + ' " onError="this.onerror=null;this.src=\'/Content/UploadedFile/Account/Avatar/NoImage.png\';"/>');
                
                $(nRow).dblclick(function () {
                    updateUserModel($(this).find(".btnUpdateUser").attr("data-id"), $("#basicModal"));
                });
            },
            "fnDrawCallback": function (oSettings) {
                // getInsertUserAccountModal();
                getUpdateUserModal();
                deleteUserAccount();
            },
            "bFilter": false
            //"sPaginationType": "bootstrap"
        });
    };
    var userDataTableUpdateWithReSort = function () {
        var oTable = $('#tblUsers').dataTable();
        oTable.fnDraw(false);
    };
    var userDataTableUpdate = function () {
        var oTable = $('#tblUsers').dataTable();
        oTable.fnDraw(false);
    };
    var getInsertUserAccountModal = function () {
        var bsModal = $("#basicModal");
        $("#btnGetInsertUserModal").off('click').click(function () {
            bsModal.html('');
            setTimeout(function () {
                bsModal.load( '/Management/InsertUsersModal', '', function () {
                    bsModal.modal('show');
                    handleBootstrapSelect();
                    resetbooststrapSelect();
                    validateInsertUserForm();
                    uploadImg();
                });
            }, 10);
        });
    };
    var validateInsertUserForm = function () {
        // for more info visit the official plugin documentation: 
        // http://docs.jquery.com/Plugins/Validation

        var form2 = $('#InsertUserForm');
        var error2 = $('.alert-danger', form2);
        var success2 = $('.alert-success', form2);

        form2.validate({
            ignoreTitle: true,
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                Name: {
                    minlength: 3,
                    maxlength: 50,
                    required: true
                },
                Pass: {
                    minlength: 8,
                    required: true
                },
                ConfirmPassword: {
                    minlength: 8,
                    equalTo : "#tbPass"
                },
                Email: {
                    required: true,
                    email: true
                },
                UserTypeId: {
                    required: true
                },
                Mobile: {
                    required: true
                },
                Gender: {
                    required: true
                },
                CurrencyId: {
                    required: true
                }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit 
                success2.hide();
                var errors = validator.numberOfInvalids();
                var messageDetails = '';
                if (errors) {
                    $.each(validator.errorList, function () {
                        messageDetails += '<li><u><b>' + this.element.attributes["data-field"].nodeValue + "</b></u> : " + this.message + '</li>';
                    });
                    var message;
                    if (errors === 1) {
                        message = '' + Messages.errorInField + '';
                    }
                    else if (errors > 1 && errors < 11) {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    } else {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    }
                    message += '<button class="close" data-close="alert"></button><br><ul>' + messageDetails + '</ul>';
                    error2.html(message);
                    error2.show();
                } else {
                    error2.hide();
                }
            },
            errorPlacement: function (error, element) { // render error placement for each input type

            },
            highlight: function (element) { // hightlight error inputs
                $(element).closest('.formElement').addClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').removeClass('has-success');
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.formElement').removeClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').addClass('has-success');
            },
            success: function (label) {
                label.closest('.formElement').removeClass('has-error').addClass('has-success'); // set success class to the control group
            },
            submitHandler: function (form) {
                gsDisablSubmitButton(form2); success2.show();
                error2.hide();
                insertUserAccount();
            }
        });
    };
    var insertUserAccount = function () {
        var form = $("#InsertUserForm");
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
                    gsResetInsertForm("InsertUserForm");
                    userDataTableUpdateWithReSort();
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
    };
    var getUpdateUserModal = function () {
        var bsModal = $("#basicModal");
        $(".btnUpdateUser").off('click').click(function () {
            var id = $(this).attr("data-id");
            bsModal.html('');
            setTimeout(function () {
                bsModal.load( '/Management/UpdateUsersModal?id=' + id, '', function () {
                    bsModal.modal('show');
                    resetbooststrapSelect();
                    validateUpdateUserForm();
                    uploadImg();
                });
            }, 100);
        });
    };
    var updateUserModel = function (id, bsModal) {
        bsModal.html('');
        setTimeout(function () {
            bsModal.load( '/Management/UpdateUsersModal?id=' + id, '', function () {
                bsModal.modal('show'); resetbooststrapSelect();
                validateUpdateUserForm();
                uploadImg();
                handleBootstrapSelect();
            });
        }, 100);
    }
    var uploadImg = function () {
        var flag = true;
        $("#imgUploadUser").off('change').change(function () {
            if (flag === true) {
                flag = false;
                var my_file = this.files[0];
                var size = 0;
                if (my_file !== undefined)
                    size = parseInt(this.files[0].size);
                if (size !== undefined)
                    size = size / 1024;
                var file = $(this).val();
                var extension = file.substr((file.lastIndexOf('.') + 1)).toLowerCase();
                var type = false;
                if (extension === 'jpg' || extension === 'jpeg' || extension === 'png' || extension === 'gif' || extension === 'bmp')
                    type = true;
                if (size <= 2024 && type === true) {
                    //$('.error-message-image').slideUp(500);
                    var fd = new FormData();

                    fd.append("choose-file", my_file);
                    $.ajax({
                        url: '/Management/UploadUserImg',
                        type: 'POST',
                        data: fd,
                        cache: false,
                        contentType: false,
                        processData: false,
                        dataType: "json",
                        xhr: function () {

                            var xhr = new window.XMLHttpRequest();
                            xhr.upload.addEventListener('progress', function (e) {
                                // THIS IS ONLY RUNS ONCE!!!
                                if (e.lengthComputable) {
                                    //    $('#progressbar').css('width', (e.loaded / e.total) * 100 + '%');
                                    console.log((e.loaded / e.total) * 100);
                                }
                            }, false);
                            return xhr;

                        },
                        beforeSend: function () {
                        },
                        success: function (data) {
                            flag = true;
                            //var obj = JSON.parse(data);
                            $("#imgUploadUser").attr("value", data.Filename);
                            $("#hdImage").val(data.Filename);
                        }
                    });
                } else {
                    flag = true;
                    var message = "";
                    if (size > 2024) {
                        message = "حجم الصورة غير مقبول";
                    }
                    else if (type === false && size > 0) {
                        message = "الرجاء اختيار صورة بصيغة (JPG, PNG)";
                    } else {
                        message = "الرجاء اختيار صورة";
                    }
                    gsNotifyMsg(message, "error");
                }
            }
        });
    };
    var userSearch = function () {
        $("#btnSearch").off('click').click(function () {
            userDataTableUpdate();
        });
    };
    var userreset = function () {
        $("#btnClearForm").off("click").click(function () {
            $("#txtUserSearch").val("");
            //$("#txtUserSearch").removeAttr("data-id");
            gsResetInsertForm("SearchForm");
            userDataTableUpdateWithReSort();
        });
    };
    var validateUpdateUserForm = function () {
        // for more info visit the official plugin documentation: 
        // http://docs.jquery.com/Plugins/Validation

        var form2 = $('#UpdateUsersForm');
        var error2 = $('.alert-danger', form2);
        var success2 = $('.alert-success', form2);

        form2.validate({
            ignoreTitle: true,
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                Name: {
                    minlength: 3,
                    maxlength: 50,
                    required: true
                },
                Pass: {
                    minlength: 8
                },
                ConfirmPassword: {
                    minlength: 8,
                    equalTo: "#tbPass"
                },
                Email: {
                    required: true,
                    email : true
                },
                UserTypeId: {
                    required: true
                },
                Mobile: {
                    required: true
                },
                CurrencyId: {
                    required: true
                }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit 
                success2.hide();
                var errors = validator.numberOfInvalids();
                var messageDetails = '';
                if (errors) {
                    $.each(validator.errorList, function () {
                        messageDetails += '<li><u><b>' + this.element.attributes["data-field"].nodeValue + "</b></u> : " + this.message + '</li>';
                    });
                    var message;
                    if (errors === 1) {
                        message = '' + Messages.errorInField + '';
                    }
                    else if (errors > 1 && errors < 11) {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    } else {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    }
                    message += '<button class="close" data-close="alert"></button><br><ul>' + messageDetails + '</ul>';
                    error2.html(message);
                    error2.show();
                } else {
                    error2.hide();
                }
            },
            errorPlacement: function (error, element) { // render error placement for each input type

            },
            highlight: function (element) { // hightlight error inputs
                $(element).closest('.formElement').addClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').removeClass('has-success');
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.formElement').removeClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').addClass('has-success');
            },
            success: function (label) {
                label.closest('.formElement').removeClass('has-error').addClass('has-success'); // set success class to the control group
            },
            submitHandler: function (form) {
                gsDisablSubmitButton(form2); success2.show();
                error2.hide();
                updateUserAccount();
            }
        });
    };
    var updateUserAccount = function () {
        var form = $("#UpdateUsersForm");
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
                    userDataTableUpdate();
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
    };
    var deleteUserAccount = function () {
        $(".btnDeleteUser").off('click').click(function () {
            var id = $(this).attr('data-Id');
            gsConfirm('' + Messages.deleteConfirm + '', function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        cache: false,
                        url:   '/Management/DeleteUserAccount',
                        dataType: "JSON",
                        data: { 'id': id },
                        success: function (data) {
                            if (data.cStatus === "success") {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                                userDataTableUpdateWithReSort();

                            } else {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                            }

                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            gsNotifyMsg('' + Messages.noResultFound + '', "error");
                            gsEnableSubmitButton(form);
                        }
                    });
                }
            });
        });
    };


    //UserTypes

    var showUserTypePartial = function () {
        var $modalUserTypeAdd = $("#UserTypeModel");
        $("#btnAddUserType").off('click').click(function () {
            $modalUserTypeAdd.html('');
            setTimeout(function () {
                $modalUserTypeAdd.load( '/Management/UserTypePartial', '', function () {
                    $modalUserTypeAdd.modal('show');
                    addEditUserTypeForm();
                });

            }, 500);

        });
    };
    var getUpdateUserType = function () {
        var $modalUserTypeAdd = $("#UserTypeModel");
        $(".btnUpdate").off('click').click(function () {
            var id = $(this).attr("data-id");
            $modalUserTypeAdd.html('');
            setTimeout(function () {
                $modalUserTypeAdd.load( '/Management/UserTypePartial?id=' + id, '', function () {
                    $modalUserTypeAdd.modal('show');
                    addEditUserTypeForm();
                });
            }, 100);
        });
    };
    var updateUserTypeModel = function (id, bsModal) {
        bsModal.html('');
        setTimeout(function () {
            bsModal.load( '/Management/UserTypePartial?id=' + id, '', function () {
                bsModal.modal('show');
                addEditUserTypeForm();
            });
        }, 100);
    }
    var deleteUserType = function () {
        $(".btnDelete").off('click').click(function () {
            var id = $(this).attr('data-Id');
            gsConfirm('' + Messages.deleteConfirm + '', function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        cache: false,
                        url: "/Management/DeleteUserType",
                        dataType: "JSON",
                        data: { 'id': id },
                        success: function (data) {
                            if (data.cStatus === "success") {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                                userTypeListDataTable();

                            } else {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                            }

                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            gsNotifyMsg('' + Messages.noResultFound + '', "error");
                            gsEnableSubmitButton(form);
                        }
                    });
                }
            });
        });
    };
    var addEditUserTypeForm = function () {
        var form2 = $("#AddUserType");
        var error2 = $(".alert-danger", form2);
        var success2 = $(".alert-success", form2);

        form2.validate({
            ignoreTitle: true,
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "ignore",
            rules: {
                Name: {
                    minlength: 2,
                    required: true
                }

            },
            //messages: {
            //    tbChainName: {
            //        required: "Please insert User Type",
            //        minlength: "User Type must be more than 2 chars"
            //    }
            //},

            invalidHandler: function (event, validator) { //display error alert on form submit 
                success2.hide();
                var errors = validator.numberOfInvalids();
                var messageDetails = '';
                if (errors) {
                    $.each(validator.errorList, function () {
                        messageDetails += '<li><u><b>' + this.element.attributes["data-field"].nodeValue + "</b></u> : " + this.message + '</li>';
                    });
                    var message;
                    if (errors === 1) {
                        message = '' + Messages.errorInField + '';
                    }
                    else if (errors > 1 && errors < 11) {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    } else {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    }
                    message += '<button class="close" data-close="alert"></button><br><ul>' + messageDetails + '</ul>';
                    error2.html(message);



                    error2.show();
                } else {
                    error2.hide();
                }
            },
            errorPlacement: function (error, element) { // render error placement for each input type

            },
            highlight: function (element) { // hightlight error inputs
                $(element).closest('.formElement').addClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').removeClass('has-success');
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.formElement').removeClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').addClass('has-success');
            },
            success: function (label) {
                label.closest('.formElement').removeClass('has-error').addClass('has-success'); // set success class to the control group
            },

            submitHandler: function (form) {
                gsDisablSubmitButton(form2); success2.show();
                error2.hide();
                addEditUserType();
            }
        });

    };
    var addEditUserType = function () {
        var form = $("#AddUserType");
        var postData = $(form).serializeArray();
        var formUrl = $(form).attr("action");

        $.ajax({
            type: "POST",
            cache: false,
            url: formUrl,
            dataType: "JSON",
            data: postData,
            success: function (data) {
                gsEnableSubmitButton(form);
                if (data.cStatus === "success") {
                    completedSuccessfuly(data.cMsg);
                    userTypeListDataTable();
                } else {
                    notValidOperations(data.cMsg);
                }
            },
            error: function () {
                gsNotifyMsg('' + Messages.noResultFound + '', "error");
                gsEnableSubmitButton(form);
            }
        });
    };
    var userTypeList = function () {
        $("#tblUserType").dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
            },
            "bServerSide": true,
            "sAjaxSource": "/Management/UserTypeTableAjax",
            "bProcessing": true,
            "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
            "aaSorting": [[1, 'asc']],
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "Name", "value": $("#txtUserTypeSearch").val() });
            },
            //"bStateSave": true,
            "aoColumns": [
                { "sType": "html", "sWidth": "95%", "mDataProp": "Name" },
                { "sType": "html", "sWidth": "5%", "mDataProp": "Id" }
            ],
            "aoColumnDefs": [],

            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.Id > 1) {
                    $('td:eq(1)', nRow).html('<div class="btn-group">' +
                        '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                        '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                        '</a>' +
                        ' <ul class="dropdown-menu pull-right">' +
                        '<li>' +
                        '<a href="javascript:;" class="lnk btnUpdate" data-id="' +
                        aData.Id +
                        '"><i class="fa fa-edit fa-fw"></i> ' +
                        Messages.edit +
                        '</a>' +
                        ' </li>' +
                        ' <li>' +
                        '<a href="javascript:;" class="lnk btnDelete" data-id ="' +
                        aData.Id +
                        '"><i class="fa fa-trash fa-fw"></i>' +
                        Messages.delete +
                        '</a>' +
                        ' </li>' +
                        '</ul>' +
                        ' </div>');
                    $(nRow).dblclick(function () {
                        updateUserTypeModel($(this).find(".btnUpdate").attr("data-id"), $("#UserTypeModel"));
                    });
                } else {
                    $('td:eq(1)', nRow).html('');
                }
            },

            "fnDrawCallback": function (oSettings) {
                showUserTypePartial();
                getUpdateUserType();
                deleteUserType();

            },
            "bFilter": false,
            //"sPaginationType": "bootstrap"
        });
    }
    var userTypeListDataTable = function () {
        var oTable = $('#tblUserType').dataTable();
        oTable.fnDraw(false);
    };
    var userTypeSearch = function () {
        $("#btnSearch").off('click').click(function () {
            userTypeListDataTable();
        });
    };
    var userTypeReset = function () {
        $("#btnClearForm").off("click").click(function () {
            $("#txtUserTypeSearch").val("");
            userTypeListDataTable();
        });
    };

    //global setting 

    var showGSettingPartial = function () {
        var $bsModal = $("#basicModal");
        $("#btnAddGSetting").off('click').click(function () {
            $bsModal.html('');
            setTimeout(function () {
                $bsModal.load( '/Management/InsertGlobalSettingModel', '', function () {
                    $bsModal.modal('show'); resetbooststrapSelect();
                    insertGSettingForm();
                });

            }, 500);

        });
    };
    var getUpdateGSetting = function () {
        var $bsModal = $("#basicModal");
        $(".btnUpdate").off('click').click(function () {
            var id = $(this).attr("data-id");
            $bsModal.html('');
            setTimeout(function () {
                $bsModal.load( '/Management/UpdateGlobalSettingModel?id=' + id, '', function () {
                    $bsModal.modal('show'); resetbooststrapSelect();
                    updateGSettingForm();
                });
            }, 100);
        });
    };
    var updateGlobalSettingModel = function (id, bsModal) {
        bsModal.html('');
        setTimeout(function () {
            bsModal.load( '/Management/UpdateGlobalSettingModel?id=' + id, '', function () {
                bsModal.modal('show');
                updateGSettingForm();
            });
        }, 100);
    }
    var deleteGSetting = function () {
        $(".btnDelete").off('click').click(function () {
            var id = $(this).attr('data-Id');
            gsConfirm('' + Messages.deleteConfirm + '', function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        cache: false,
                        url:   '/Management/DeleteGlobalSetting',
                        dataType: "JSON",
                        data: { 'id': id },
                        success: function (data) {
                            if (data.cStatus === "success") {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                                gSettingUpdateDataTable();

                            } else {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                            }

                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            gsNotifyMsg('' + Messages.noResultFound + '', "error");
                            gsEnableSubmitButton(form);
                        }
                    });
                }
            });
        });
    };
    var insertGSettingForm = function () {
        var form2 = $("#InsertGlobalSettingForm");
        var error2 = $(".alert-danger", form2);
        var success2 = $(".alert-success", form2);

        form2.validate({
            ignoreTitle: true,
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "ignore",
            rules: {
                Name: {
                    minlength: 2,
                    required: true
                }

            },
            //messages: {
            //    tbChainName: {
            //        required: "Please insert User Type",
            //        minlength: "User Type must be more than 2 chars"
            //    }
            //},

            invalidHandler: function (event, validator) { //display error alert on form submit 
                success2.hide();
                var errors = validator.numberOfInvalids();
                var messageDetails = '';
                if (errors) {
                    $.each(validator.errorList, function () {
                        messageDetails += '<li><u><b>' + this.element.attributes["data-field"].nodeValue + "</b></u> : " + this.message + '</li>';
                    });
                    var message;
                    if (errors === 1) {
                        message = '' + Messages.errorInField + '';
                    }
                    else if (errors > 1 && errors < 11) {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    } else {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    }
                    message += '<button class="close" data-close="alert"></button><br><ul>' + messageDetails + '</ul>';
                    error2.html(message);



                    error2.show();
                } else {
                    error2.hide();
                }
            },
            errorPlacement: function (error, element) { // render error placement for each input type

            },
            highlight: function (element) { // hightlight error inputs
                $(element).closest('.formElement').addClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').removeClass('has-success');
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.formElement').removeClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').addClass('has-success');
            },
            success: function (label) {
                label.closest('.formElement').removeClass('has-error').addClass('has-success'); // set success class to the control group
            },

            submitHandler: function (form) {
                gsDisablSubmitButton(form2); success2.show();
                error2.hide();
                insertGSetting();
            }
        });

    };
    var insertGSetting = function () {
        var form = $("#InsertGlobalSettingForm");
        var postData = $(form).serializeArray();
        var formUrl = $(form).attr("action");

        $.ajax({
            type: "POST",
            cache: false,
            url: formUrl,
            dataType: "JSON",
            data: postData,
            success: function (data) {
                gsEnableSubmitButton(form);
                if (data.cStatus === "success") {
                    completedSuccessfuly(data.cMsg);
                    gsResetInsertForm("InsertGlobalSettingForm");
                    gSettingUpdateDataTable();
                } else if (data.cStatus === "notValid") {
                    notValidOperations(data.cMsg);
                }
                else {
                    notValidOperations(data.cMsg);
                }
            },
            error: function () {
                notValidOperations(Messages.noResultFound);
                //gsNotifyMsg('' + Messages.noResultFound + '', "error");
                gsEnableSubmitButton(form);
            }
        });
    };
    var updateGSettingForm = function () {
        var form2 = $("#UpdateGlobalSettingForm");
        var error2 = $(".alert-danger", form2);
        var success2 = $(".alert-success", form2);

        form2.validate({
            ignoreTitle: true,
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "ignore",
            rules: {
                Name: {
                    minlength: 2,
                    required: true
                }

            },
            //messages: {
            //    tbChainName: {
            //        required: "Please insert User Type",
            //        minlength: "User Type must be more than 2 chars"
            //    }
            //},

            invalidHandler: function (event, validator) { //display error alert on form submit 
                success2.hide();
                var errors = validator.numberOfInvalids();
                var messageDetails = '';
                if (errors) {
                    $.each(validator.errorList, function () {
                        messageDetails += '<li><u><b>' + this.element.attributes["data-field"].nodeValue + "</b></u> : " + this.message + '</li>';
                    });
                    var message;
                    if (errors === 1) {
                        message = '' + Messages.errorInField + '';
                    }
                    else if (errors > 1 && errors < 11) {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    } else {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    }
                    message += '<button class="close" data-close="alert"></button><br><ul>' + messageDetails + '</ul>';
                    error2.html(message);



                    error2.show();
                } else {
                    error2.hide();
                }
            },
            errorPlacement: function (error, element) { // render error placement for each input type

            },
            highlight: function (element) { // hightlight error inputs
                $(element).closest('.formElement').addClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').removeClass('has-success');
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.formElement').removeClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').addClass('has-success');
            },
            success: function (label) {
                label.closest('.formElement').removeClass('has-error').addClass('has-success'); // set success class to the control group
            },

            submitHandler: function (form) {
                gsDisablSubmitButton(form2); success2.show();
                error2.hide();
                updateGSetting();
            }
        });

    };
    var updateGSetting = function () {
        var form = $("#UpdateGlobalSettingForm");
        var postData = $(form).serializeArray();
        var formUrl = $(form).attr("action");

        $.ajax({
            type: "POST",
            cache: false,
            url: formUrl,
            dataType: "JSON",
            data: postData,
            success: function (data) {
                gsEnableSubmitButton(form);
                if (data.cStatus === "success") {
                    completedSuccessfuly(data.cMsg);
                    gSettingUpdateDataTable();
                } else if (data.cStatus === "notValid") {
                    notValidOperations(data.cMsg);
                }
                else {
                    notValidOperations(data.cMsg);
                }
            },
            error: function () {
                notValidOperations(Messages.noResultFound);
                //gsNotifyMsg('' + Messages.noResultFound + '', "error");
                gsEnableSubmitButton(form);
            }
        });
    };
    var gSettingDataTable = function () {
        $("#tblGlobalSetting").dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
            },
            "bServerSide": true,
            "sAjaxSource":  "/Management/GlobalSettingTableAjax",
            "bProcessing": true,
            "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
            "aaSorting": [[0, 'asc']],
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "Name", "value": $("#txtSearch").val() });
            },
            //"bStateSave": true,
            "aoColumns": [
                { "sType": "html", "sWidth": "50%", "mDataProp": "Name" },
                { "sType": "html", "sWidth": "45%", "mDataProp": "value" },
                { "sType": "html", "sWidth": "5%", "mDataProp": "ConId" }
            ],
            "aoColumnDefs": [],

            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $('td:eq(2)', nRow).html('<div class="btn-group">' +
                        '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                        '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                        '</a>' +
                        ' <ul class="dropdown-menu pull-right">' +
                        '<li>' +
                        '<a href="javascript:;" class="lnk btnUpdate" data-id="' + aData.ConId + '"><i class="fa fa-edit fa-fw"></i> ' + Messages.edit + '</a>' +
                        ' </li>' +
                        ' <li>' +
                        '<a href="javascript:;" class="lnk btnDelete" data-id ="' + aData.ConId + '"><i class="fa fa-trash fa-fw"></i>' + Messages.delete + '</a>' +
                        ' </li>' +
                        '</ul>' +
                        ' </div>');
                $(nRow).dblclick(function () {
                    updateGlobalSettingModel($(this).find(".btnUpdate").attr("data-id"), $("#basicModal"));
                });
            },

            "fnDrawCallback": function (oSettings) {
                showGSettingPartial();
                getUpdateGSetting();
                deleteGSetting();

            },
            "bFilter": false,
            //"sPaginationType": "bootstrap"
        });
    }
    var gSettingUpdateDataTable = function () {
        var oTable = $('#tblGlobalSetting').dataTable();
        oTable.fnDraw(false);
    };
    var gSettingSearch = function () {
        $("#btnSearch").off('click').click(function () {
            gSettingUpdateDataTable();
        });
    };
    var gSettingRest = function () {
        $("#btnClearForm").off("click").click(function () {
            $("#txtSearch").val("");
            gSettingUpdateDataTable();
        });
    };

    //ErrorsRepository

    var errorsLogsDataTable = function () {
        $('#tblErrorsLogs').dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
            },
            "bServerSide": true,
            "sAjaxSource":  "/Management/GetErrorsLogsDataTable",
            "bProcessing": true,
            "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
            "aaSorting": [[9, 'desc']],

            "bStateSave": false,
            "aoColumns": [
                { "sType": "html", "sWidth": '5%', "mDataProp": "IP", "bSortable": false },
                { "sType": "html", "sWidth": '10%', "mDataProp": "UserAgent", "bSortable": false },
                { "sType": "html", "sWidth": '10%', "mDataProp": "Browser", "bSortable": false },
                { "sType": "html", "sWidth": '10%', "mDataProp": "ErrorMessage", "bSortable": false },
                { "sType": "html", "sWidth": '10%', "mDataProp": "Link", "bSortable": false },
                { "sType": "html", "sWidth": '10%', "mDataProp": "ErrorDate", "bSortable": false },
                { "sType": "html", "sWidth": '10%', "mDataProp": "IsSolved", "bSortable": false },
                { "sType": "html", "sWidth": '10%', "mDataProp": "IsAjax", "bSortable": false },
                { "sType": "html", "sWidth": '10%', "mDataProp": "PostedData", "bSortable": false },
                { "sType": "html", "sWidth": '5%', "mDataProp": "Id" }
            ],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $('td:eq(9)', nRow).html('<div class="btn-group">' +
                        '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                        '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                        '</a>' +
                        ' <ul class="dropdown-menu pull-right">' +
                        ' <li>' +
                        '<a href="javascript:;" class="lnk btnDeleteLogs" data-id ="' + aData.Id + '"><i class="fa fa-trash fa-fw"></i> ' + Messages.delete + '</a>' +
                        ' </li>' +
                        '</ul>' +
                        ' </div>');
            },
            "fnDrawCallback": function (oSettings) {
                deleteErrorLogs();
            },
            "bFilter": false
            //"sPaginationType": "bootstrap"
        });
    };
    var errorsLogsDataTableUpdate = function () {
        var oTable = $('#tblErrorsLogs').dataTable();
        oTable.fnDraw(false);
    };
    var deleteErrorLogs = function () {
        $(".btnDeleteLogs").off('click').click(function () {
            var id = $(this).attr('data-Id');
            gsConfirm('' + Messages.deleteConfirm + '', function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        cache: false,
                        url: '/Management/DeleteErrorLog',
                        dataType: "JSON",
                        data: { 'id': id },
                        success: function (data) {
                            if (data.cStatus === "success") {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                                errorsLogsDataTableUpdate();

                            } else {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                            }

                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            gsNotifyMsg('' + Messages.noResultFound + '', "error");
                            gsEnableSubmitButton(form);
                        }
                    });
                }
            });
        });
    };

    //Profile
    var getProfileModal = function () {
    var bsModal = $("#basicModal");
    $("#btnMyProfile").off('click').click(function () {
        bsModal.html('');
        setTimeout(function () {
            bsModal.load( '/Management/MyProfileModal', '', function () {
                bsModal.modal('show');
                validateUserProfileForm();
                uploadImg();
            });
        }, 100);
    });
    };
    var validateUserProfileForm = function () {
        // for more info visit the official plugin documentation: 
        // http://docs.jquery.com/Plugins/Validation

        var form2 = $("#MyProfileModel");
        var error2 = $('.alert-danger', form2);
        var success2 = $('.alert-success', form2);

        form2.validate({
            ignoreTitle: true,
            errorElement: 'span', //default input error message container
            errorClass: 'help-block help-block-error', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            rules: {
                Name: {
                    minlength: 3,
                    maxlength: 50,
                    required: true
                },
               Email: {
                    required: true
                },
                Mobile: {
                    required: true
                }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit 
                success2.hide();
                var errors = validator.numberOfInvalids();
                var messageDetails = '';
                if (errors) {
                    $.each(validator.errorList, function () {
                        messageDetails += '<li><u><b>' + this.element.attributes["data-field"].nodeValue + "</b></u> : " + this.message + '</li>';
                    });
                    var message;
                    if (errors === 1) {
                        message = '' + Messages.errorInField + '';
                    }
                    else if (errors > 1 && errors < 11) {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    } else {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    }
                    message += '<button class="close" data-close="alert"></button><br><ul>' + messageDetails + '</ul>';
                    error2.html(message);
                    error2.show();
                } else {
                    error2.hide();
                }
            },
            errorPlacement: function (error, element) { // render error placement for each input type

            },
            highlight: function (element) { // hightlight error inputs
                $(element).closest('.formElement').addClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').removeClass('has-success');
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.formElement').removeClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').addClass('has-success');
            },
            success: function (label) {
                label.closest('.formElement').removeClass('has-error').addClass('has-success'); // set success class to the control group
            },
            submitHandler: function (form) {
                gsDisablSubmitButton(form2); success2.show();
                error2.hide();
                updateUserProfile();
            }
        });
    };
    var updateUserProfile = function () {
        var form = $("#MyProfileModel");
        var postData = $(form).serializeArray();
        var formUrl = $(form).attr("action");
        postData.push({ name: "Avatar", value: $(".imgUserlogo").attr("imgname") });
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
                    userDataTableUpdate();
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
    };

    var getChangePasswordModal = function () {
        var bsModal = $("#basicModal");
        $("#btnChangePass").off('click').click(function () {
            bsModal.html('');
            setTimeout(function () {
                bsModal.load( '/Management/ChangePasswordModal', '', function () {
                    bsModal.modal('show');
                    updatePasswordForm();
                    
                });
            }, 100);
        });
    };
    var updatePasswordForm = function () {
        var form2 = $("#UpdatePassForm");
        var error2 = $(".alert-danger", form2);
        var success2 = $(".alert-success", form2);

        form2.validate({ignoreTitle: true,
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "ignore",
            rules: {
                CurrentPassword: {
                    minlength: 2,
                    required: true
                },
                NewPassword: {
                    minlength: 2,
                    required: true
                },
                ConfirmPassword: {
                    minlength: 2,
                    required: true,
                    equalTo: "#tbNewPassword"
                },

            },
            invalidHandler: function (event, validator) { //display error alert on form submit 
                success2.hide();
                var errors = validator.numberOfInvalids();
                var messageDetails = '';
                if (errors) {
                    $.each(validator.errorList, function () {
                        messageDetails += '<li><u><b>' + this.element.attributes["data-field"].nodeValue + "</b></u> : " + this.message + '</li>';
                    });
                    var message;
                    if (errors === 1) {
                        message = '' + Messages.errorInField + '';
                    }
                    else if (errors > 1 && errors < 11) {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    } else {
                        message = '' + Messages.there + ' ' + errors + ' ' + Messages.errorInField1 + '';
                    }
                    message += '<button class="close" data-close="alert"></button><br><ul>' + messageDetails + '</ul>';
                    error2.html(message);



                    error2.show();
                } else {
                    error2.hide();
                }
            },
            errorPlacement: function (error, element) { // render error placement for each input type

            },
            highlight: function (element) { // hightlight error inputs
                $(element).closest('.formElement').addClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').removeClass('has-success');
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.formElement').removeClass('has-error'); // set error class to the control group
                $(element).closest('.formElement').addClass('has-success');
            },
            success: function (label) {
                label.closest('.formElement').removeClass('has-error').addClass('has-success'); // set success class to the control group
            },

            submitHandler: function (form) {
                gsDisablSubmitButton(form2);success2.show();
                error2.hide();
                updatePassword();
            }
        });

    };
    var tryNo = 0;
    var updatePassword = function () {
        tryNo++;
        var form = $("#UpdatePassForm");
        var postData = $(form).serializeArray();
        var formUrl = $(form).attr("action");
        postData.push({ name: "tryNo", value: tryNo });
        $.ajax({
            type: "POST",
            cache: false,
            url: formUrl,
            dataType: "JSON",
            data: postData,
            success: function (data) {
                gsEnableSubmitButton(form);
                if (data.cStatus === "success") {
                    completedSuccessfuly(data.cMsg);
                    gSettingUpdateDataTable();
                } else if (data.cStatus === "notValid") {
                    notValidOperations(data.cMsg);
                }
                if (data.isRedirect) {
                    notValidOperations(data.cMsg);
                    window.setTimeout(function () {window.location.href = data.redirectUrl;}, 1500);

                    
                }
            },
            error: function () {
                notValidOperations(Messages.noResultFound);
                //gsNotifyMsg('' + Messages.noResultFound + '', "error");
                gsEnableSubmitButton(form);
            }
        });
    };
    return {
        initConstant: function () {
            constantDataTable();
            resetConstantDataTable();
            constantSearch();
            getInsertConstantModal();
            constantSearchAutoComplete();
            handleBootstrapSelect();
        },
        initPages: function () {
            pagesDataTable();
            resetPagesDataTable();
            pagesSearch();
            getInsertPagesModal();
            pagesSearchAutoComplete();
            handleBootstrapSelect();
        },
        initUserPermission: function () {
            handleBootstrapSelect();
            getUserTypePermission();
            saveUserTypePermission();
            $(".cbMP").click(function () {
                var parent = $(this);
                var pName = $(parent).attr("child");
                if ($(parent).prop("checked")) {
                    $(pName).find("input[type=checkbox]").prop("checked", "true");
                    $(pName).find("input[type=checkbox]").attr("checked", "checked");
                } else {
                    $(pName).find("input[type=checkbox]").removeProp("checked");
                    $(pName).find("input[type=checkbox]").removeAttr("checked");
                }
            });
            $(".cbML").click(function () {
                var parent = $(this);
                var pName = $(parent).attr("child");
                if ($(parent).prop("checked")) {
                    $(pName).find("input[type=checkbox]").prop("checked", "true");
                    $(pName).find("input[type=checkbox]").attr("checked", "checked");
                } else {
                    $(pName).find("input[type=checkbox]").removeProp("checked");
                    $(pName).find("input[type=checkbox]").removeAttr("checked");
                }
                var father = $(this).closest(".panel").find(".cbMP");
                if ($(this).prop("checked")) {
                    if (!$(father).prop("checked")) {
                        $(father).prop("checked", "true");
                        $(father).attr("checked", "checked");
                    }
                }

            });
            $(".cbChiled").off('click').click(function () {
                var father = $(this).closest(".mainList").find(".cbML");
                var grandFather = $(father).closest(".panel").find(".cbMP");
                if ($(this).prop("checked")) {
                    if (!$(father).prop("checked")) {
                        $(father).prop("checked", "true");
                        $(father).attr("checked", "checked");
                    }
                    if (!$(grandFather).prop("checked")) {
                        $(grandFather).prop("checked", "true");
                        $(grandFather).attr("checked", "checked");
                    }
                }
            });
        },
        initUsers: function () {
            handleBootstrapSelect();
            userDataTable();
            getInsertUserAccountModal();
            userSearch();
            userreset();
        },
        initUserTypes: function () {
            userTypeList();
            userTypeSearch();
            userTypeReset();

        },
        initGlobalSetting: function () {
            gSettingDataTable();
            //showGSettingPartial();
            gSettingSearch();
            gSettingRest();
        },
        initMyProfile: function () {
            getProfileModal();
            getChangePasswordModal();
        },
        initErrorsLogs: function () {
            errorsLogsDataTable();
        },
        initAccountTypes: function () {
            accountTypeList();
            accountTypeSearch();
            accountTypereset();

        },
        initAccounts: function () {
            accountsDataTable();
            resetAccountsDataTable();
            accountsSearch();
            getInsertAccountsModal();
            accountsSearchAutoComplete();
            handleBootstrapSelect();
        }
    };
}();