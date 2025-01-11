var ControlPanelScripts = function () {
    /****************/
    var closeModal = function () {
        $(".closeModal").off("click").click(function () {
            $(".contentDiv").find(".portle-body").html('');
            $(".contentDiv").hide();
            $(".tableDiv").show();

        });
    };
    var uploadImg = function (uploader, input, isHdr) {
        var flag = true;
        $(uploader).off('change').change(function () {
            if (flag === true) {
                flag = false;
                var myFile = this.files[0];
                var size = 0;
                if (myFile !== undefined)
                    size = parseInt(this.files[0].size);
                if (size !== undefined)
                    size = size / 5120;
                var file = $(this).val();
                var extension = file.substr((file.lastIndexOf('.') + 1)).toLowerCase();
                var type = false;
                if (extension === 'jpg' || extension === 'jpeg' || extension === 'png' || extension === 'gif' || extension === 'bmp')
                    type = true;
                if (size <= 5120 && type === true) {
                    //$('.error-message-image').slideUp(500);
                    var fd = new FormData();

                    fd.append("choose-file", myFile);
                    $.ajax({
                        url: '/ControlPanel/UploadNewsImg',
                        type: 'POST',
                        data: fd,
                        cache: false,
                        contentType: false,
                        processData: false,
                        dataType: "json",
                        xhr: function () {

                            var xhr = new window.XMLHttpRequest();
                            xhr.upload.addEventListener('progress',
                                function (e) {
                                    // THIS IS ONLY RUNS ONCE!!!
                                    if (e.lengthComputable) {
                                        //    $('#progressbar').css('width', (e.loaded / e.total) * 100 + '%');
                                        console.log((e.loaded / e.total) * 100);
                                    }
                                },
                                false);
                            return xhr;

                        },
                        beforeSend: function () {
                        },
                        success: function (data) {
                            flag = true;
                            $(input).val(data.Filename);
                            if (isHdr)
                                $(uploader).closest('.fileinput').find('.fileinput-filename').text(data.Filename);
                        }
                    });
                } else {
                    flag = true;
                    if (size > 5120) {
                        gsNotifyMsg("حجم الصورة غير مقبول", "error");
                    } else if (type === false && size > 0) {
                        gsNotifyMsg("الرجاء اختيار صورة بصيغة صحيحة", "error");
                    } else {
                        gsNotifyMsg("الرجاء اختيار صورة", "error");
                    }
                }
            };
        });
    };
    var uploadAlbums = function () {
        var flag = true;
        $("#imgUploadUser").off('change').change(function () {
            if (flag === true) {
                flag = false;
                var myFile = this.files[0];
                var size = 0;
                if (myFile !== undefined)
                    size = parseInt(this.files[0].size);
                if (size !== undefined)
                    size = size / 5120;
                var file = $(this).val();
                var extension = file.substr((file.lastIndexOf('.') + 1)).toLowerCase();
                var type = false;
                if (extension === 'jpg' || extension === 'jpeg' || extension === 'png' || extension === 'gif' || extension === 'bmp')
                    type = true;
                if (size <= 2024 && type === true) {
                    var fd = new FormData();
                    fd.append("choose-file", myFile);
                    $.ajax({
                        url: '/ControlPanel/UploadAlbumImg',
                        type: 'POST',
                        data: fd,
                        cache: false,
                        contentType: false,
                        processData: false,
                        dataType: "json",
                        xhr: function () {

                            var xhr = new window.XMLHttpRequest();
                            xhr.upload.addEventListener('progress',
                                function (e) {
                                    // THIS IS ONLY RUNS ONCE!!!
                                    if (e.lengthComputable) {
                                        //    $('#progressbar').css('width', (e.loaded / e.total) * 100 + '%');
                                        console.log((e.loaded / e.total) * 100);
                                    }
                                },
                                false);
                            return xhr;

                        },
                        beforeSend: function () {
                        },
                        success: function (data) {
                            flag = true;
                            $("#hdImage").val(data.Filename);
                        }
                    });
                } else {
                    flag = true;
                    if (size > 2024) {
                        gsNotifyMsg("حجم الصورة غير مقبول", "error");
                    } else if (type === false && size > 0) {
                        gsNotifyMsg("الرجاء اختيار صورة بصيغة صحيحة", "error");
                    } else {
                        gsNotifyMsg("الرجاء اختيار صورة", "error");
                    }
                }
            };
        });
    };
    var uploadFile = function () {
        var flag = true;
        $("#fileUpload").off('change').change(function () {
            if (flag === true) {
                flag = false;
                var my_file = this.files[0];
                var size = 0;
                if (my_file !== undefined)
                    size = parseInt(this.files[0].size);
                if (size !== undefined)
                    size = size / 5120;
                var file = $(this).val();
                var extension = file.substr((file.lastIndexOf('.') + 1)).toLowerCase();
                //var type = false;
                //if (extension == 'jpg' || extension == 'jpeg' || extension == 'png' || extension == 'gif' || extension == 'bmp')
                type = true;
                if (size <= 51200 && type === true) {
                    //$('.error-message-image').slideUp(500);
                    var fd = new FormData();

                    fd.append("choose-file", my_file);
                    $.ajax({
                        url: '/ControlPanel/UpLoadFile',
                        type: 'POST',
                        data: fd,
                        cache: false,
                        contentType: false,
                        processData: false,
                        dataType: "json",
                        xhr: function () {

                            var xhr = new window.XMLHttpRequest();
                            xhr.upload.addEventListener('progress',
                                function (e) {
                                    // THIS IS ONLY RUNS ONCE!!!
                                    if (e.lengthComputable) {
                                        //    $('#progressbar').css('width', (e.loaded / e.total) * 100 + '%');
                                        console.log((e.loaded / e.total) * 100);
                                    }
                                },
                                false);
                            return xhr;

                        },
                        beforeSend: function () {
                        },
                        success: function (data) {
                            flag = true;
                            $("#hdFileName").val(data.Filename);
                        }
                    });
                } else {
                    flag = true;
                    var message = "";
                    if (size > 51200) {
                        message = "حجم الصورة غير مقبول";
                    }
                    //else if (type == false && size > 0) {
                    //    message = "الرجاء اختيار صورة بصيغة (JPG, PNG)";
                    //} 
                    else {
                        message = "الرجاء اختيار صورة";
                    }
                    gsNotifyMsg(message, "error");
                }
            };
        });
    };
    var uploadAvatar = function () {
        var flag = true;
        $("#hdAvatar").off('change').change(function () {
            if (flag === true) {
                flag = false;
                var my_file = this.files[0];
                var size = 0;
                if (my_file !== undefined)
                    size = parseInt(this.files[0].size);
                if (size !== undefined)
                    size = size / 5120;
                var file = $(this).val();
                var extension = file.substr((file.lastIndexOf('.') + 1)).toLowerCase();
                var type = false;
                if (extension === 'jpg' ||
                    extension === 'jpeg' ||
                    extension === 'png' ||
                    extension === 'gif' ||
                    extension === 'bmp')
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
                            xhr.upload.addEventListener('progress',
                                function (e) {
                                    // THIS IS ONLY RUNS ONCE!!!
                                    if (e.lengthComputable) {
                                        //    $('#progressbar').css('width', (e.loaded / e.total) * 100 + '%');
                                        console.log((e.loaded / e.total) * 100);
                                    }
                                },
                                false);
                            return xhr;

                        },
                        beforeSend: function () {
                        },
                        success: function (data) {
                            flag = true;
                            $("#hdImage").attr("value", data.Filename);
                            $("#imgAvatar").attr("src", "/Content/UploadedFile/Account/Avatar/Thumbnail/" + data.Filename);
                            
                        }
                    });
                } else {
                    flag = true;
                    if (size > 2024) {
                        gsNotifyMsg("حجم الصورة غير مقبول", "error");
                    } else if (type === false && size > 0) {
                        gsNotifyMsg("الرجاء اختيار صورة بصيغة صحيحة", "error");
                    } else {
                        gsNotifyMsg("الرجاء اختيار صورة", "error");
                    }
                }
            };
        });
    };

    /* ***** News ******* */
    function fnHideCol(iCol, tblname) {
        /* Get the DataTables object again - this is not a recreation, just a get of the object */
        var oTable = $("#" + tblname).dataTable();

        var bVis = oTable.fnSettings().aoColumns[iCol].bVisible;
        oTable.fnSetColumnVis(iCol, false);
    }
    function fnShowCol(iCol, tblname) {
        /* Get the DataTables object again - this is not a recreation, just a get of the object */
        var oTable = $("#" + tblname).dataTable();

        var bVis = oTable.fnSettings().aoColumns[iCol].bVisible;
        oTable.fnSetColumnVis(iCol, true);
    }
    var newsDataTable = function (isArticle) {
        $('#tblNews').dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
            },
            "bServerSide": true,
            "sAjaxSource": "/ControlPanel/GetNewsDataTable",
            "bProcessing": true,
            "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
            "aaSorting": [[6, 'asc']],
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "NewsSearch", "value": $("#txtNewsSearch").val() });
                aoData.push({ "name": "Category", "value": $("#ddlCategories").val() });
                aoData.push({ "name": "isArticle", "value": isArticle });
                aoData.push({ "name": "InsertedBy", "value": $("#ddlUsers").val() });
                aoData.push({ "name": "FromDate", "value": $("#tbFromDate").val() });
                aoData.push({ "name": "ToDate", "value": $("#tbToDate").val() });
            },
            "bStateSave": true,
            "aoColumns": [
                { "sType": "html", "sWidth": '10%', "mDataProp": "Image", "bSortable": false, "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '33%', "mDataProp": "Title", "bSortable": false, "sClass": "titlejustify" },
                { "sType": "html", "sWidth": '20%', "mDataProp": "NameAr", "bSortable": false },
                { "sType": "html", "sWidth": '15%', "mDataProp": "PublishDate", "bSortable": false },
                { "sType": "html", "sWidth": '10%', "mDataProp": "InsertedByName", "bSortable": false },
                { "sType": "html", "sWidth": '7%', "mDataProp": "IsActive", "bSortable": false, "sClass": "tdCenter" },
                //{ "sType": "html", "sWidth": '8%', "mDataProp": "LangId", "bSortable": false, "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '5%', "mDataProp": "Id", "sClass": "tdCenter" }
            ],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                var a = 0;
                if (isArticle)
                    a = 1;
                    $('td:eq(0)', nRow).html('<img  style="width: 100px;" alt="" src="/Content/UploadedFile/News/Thumbnail/' + aData.Image + ' " onError="this.onerror=null;this.src=\'/Content/UploadedFile/Account/Avatar/NoImage.png\';"/>');
                
                if (aData.IsActive === true) {
                    $('td:eq(' + (4 + a) +')', nRow).html("<span class='font-green-meadow fa fa-fw fa-check-circle-o fa-lg'></span>");
                } else {
                    $('td:eq(' + (4 + a) +')', nRow).html("<span class='font-red-thunderbird fa fa-fw fa-times-circle-o fa-lg'></span>");
                }
                $('td:eq(' + (5 + a) + ')', nRow).html('<div class="btn-group">' +
                    '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                    '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                    '</a>' +
                    ' <ul class="dropdown-menu pull-right">' +
                    '<li>' +
                    '<a href="javascript:;" class="lnk btnSaveNews"data-categoryId="' + aData.CategoryId + '" data-id="' + aData.Id + '"><i class="fa fa-edit fa-fw"></i> ' + Messages.edit + '</a>' +
                    ' </li>' +
                    ' <li>' +
                    '<a href="javascript:;" class="lnk btnDeleteNews" data-id ="' + aData.Id + '"><i class="fa fa-trash fa-fw"></i> ' + Messages.delete + '</a>' +
                    ' </li>' +
                    '</ul>' +
                    ' </div>');
                $(nRow).dblclick(function () {
                    saveNewsModal($(this).find(".btnSaveNews").attr("data-id"), $("#ddlCategories").val(), $("#SaveModal"));
                });
            },
            "fnDrawCallback": function (oSettings) {
                getSaveNewsModal();
                deleteNews();
            },
            "bFilter": false
            //"sPaginationType": "bootstrap"
        });
    };
    var newsDataTableUpdate = function () {
        var oTable = $('#tblNews').dataTable();
        oTable.fnDraw(false);
    };
    var handleSummernote = function () {
        $('.tbSummerNote').summernote({
            callbacks: {
                onImageUpload: function (files) {
                    that = $(this);
                    data = new FormData();
                    data.append("file", files[0]);
                    $.ajax({
                        data: data,
                        type: "POST",
                        url: '/ControlPanel/UploadNewsImg',
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (data) {
                            $(that).summernote('insertImage', "/Content/UploadedFile/News/Large/" + data.Filename);
                        }
                    });
                }
            },
            fontNames: ['Arial', 'Arial Black', 'Comic Sans MS', 'Courier New'],
            dialogsInBody: true,
            dialogsFade: false,
            toolbar: [
                ['style', ['style']],
                ['font', ['bold', 'italic', 'underline', 'clear']],
                ['fontsize', ['fontsize']],

                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']],
                ['table', ['table']],
                ['insert', ['link', 'picture', 'video', 'hr']],
                ['view', ['fullscreen', 'codeview']],
                ['help', ['help']],
            ],
            height: 300
        });
        //API:
        //var sHTML = $('#summernote_1').code(); // get code
        //$('#summernote_1').destroy(); // destroy
    }
    var saveNewsModal = function (id, categoryId, bsModal) {
        bsModal.html('');
        $(".page-content").LoadingOverlay("show");
        setTimeout(function () {
            bsModal.load('/ControlPanel/SaveNewsModal?id=' + id + '&categoryId=' + categoryId, '', function () {
                $(".contentDiv").show();
                $(".tableDiv").hide();
                resetbooststrapSelect();
                handleBootstrapSelect();
                handleSummernote();
                handleDatePickers();
                closeModal();
                saveNews();
                uploadImg($("#imgUploadUser"), $("#hdImage"));
                $('#tbPublishDate').datetimepicker({
                    format: 'YYYY-MM-DD HH:mm'
                });
                $("#tbNewsKeyWords").tagsinput();
                $(".page-content").LoadingOverlay("hide", true);
            });
        }, 100);
    }
    var getSaveNewsModal = function () {
        var bsModal = $("#SaveModal");
        $(".btnSaveNews").off('click').click(function () {
            var id = $(this).attr("data-id");
            var categoryId = $("#ddlCategories").val();
            bsModal.html('');
            $(".page-content").LoadingOverlay("show");
            setTimeout(function () {
                bsModal.load('/ControlPanel/SaveNewsModal?id=' + id + '&categoryId=' + categoryId, '', function () {
                    $(".contentDiv").show();
                    $(".tableDiv").hide();
                    resetbooststrapSelect();
                    handleBootstrapSelect();
                    handleSummernote();
                    handleDatePickers();
                    closeModal();
                    saveNews();
                    uploadImg($("#imgUploadUser"), $("#hdImage"));
                    $('#tbPublishDate').datetimepicker({ format: 'YYYY-MM-DD HH:mm' });
                    $("#tbNewsKeyWords").tagsinput();
                    $(".page-content").LoadingOverlay("hide", true);
                });
            }, 100);
        });
    };
    var saveNews = function () {
        $('#SaveNews').on("submit", function (event) {
            var form = this;
            gsDisablSubmitButton(form);
            var postData = $(form).serializeArray();
            var formUrl = $(form).attr("action");
            postData.push({ name: "Keywords", value: $("#tbNewsKeyWords").val() });
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
                        newsDataTableUpdate();
                        $("#newsId").val(data.id);
                        $(".scroll-to-top").click();
                    } else if (data.cStatus === "notValid") {
                        notValidOperations(data.cMsg);
                        $(".scroll-to-top").click();
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
    };

    var deleteNews = function () {
        $(".btnDeleteNews").off('click').click(function () {
            var id = $(this).attr('data-Id');
            gsConfirm('' + Messages.deleteConfirm + '', function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        cache: false,
                        url: '/ControlPanel/DeleteNews',
                        dataType: "JSON",
                        data: { 'id': id },
                        success: function (data) {
                            if (data.cStatus === "success") {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                                newsDataTableUpdate();

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
    var newsSearchAutoComplete = function () {
        //if (!$('#txtNewsSearch').hasClass('tt-input')) {
        //    var catId = $("#ddlCategories").val();
        //    var news = new Bloodhound({
        //        datumTokenizer: function (d) { return d.tokens; },
        //        queryTokenizer: Bloodhound.tokenizers.whitespace,
        //        remote: {
        //            url: '/ControlPanel/SearchAutoCompleteNews',
        //            replace: function (url, uriEncodedQuery) {
        //                return url + "/" + uriEncodedQuery + "?categoryId=" + catId;
        //            }
        //        }
        //    });
        //    news.initialize();

        //    $('#txtNewsSearch').typeahead({
        //        hint: true,
        //        highlight: true,
        //        minLength: 1,
        //        dir: true
        //    }, {
        //            name: 'search-news',
        //            displayKey: 'Title',
        //            source: news.ttAdapter(),
        //            limit: 20,
        //            dir: true,
        //            templates: {
        //                empty: [
        //                    '<div class="empty-message">',
        //                    '' + Messages.noResultFound + '',
        //                    '</div>'
        //                ].join('\n'),
        //                suggestion: Handlebars.compile([
        //                    '<div class="media">',
        //                    '<div class="pull-left">',
        //                    '<div class="media-object">',
        //                    '</div>',
        //                    '</div>',
        //                    '<div class="media-body">',
        //                    ' ',
        //                    ' <h5 class="media-heading">{{Title}} </h5>',
        //                    '</div>',
        //                    '</div>',
        //                ].join(''))
        //            },

        //        }).on('typeahead:selected', function ($e, datum) {
        //            $("#txtNewsSearch").attr('data-id', datum.Id);
        //        });
        //}
    };
    var newsSearch = function () {
        $("#btnSearch").off('click').click(function () {
            newsDataTableUpdate();
        });
    };
    var resetNewsDataTable = function () {
        $("#btnClearForm").off("click").click(function () {
            $("#txtNewsSearch").removeAttr("data-id");
            $("#txtNewsSearch").val("");
            gsResetInsertForm("SearchForm");
            newsDataTableUpdate();
        });
    };
    /********Category**********/
    var categoriesDataTable = function () {
        $('#tblCategories').dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
            },
            "paging": false,
            "info": false,
            "bServerSide": true,
            "sAjaxSource": "/ControlPanel/getCategoriesDataTable",
            "bProcessing": true,
            "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
            "aaSorting": [[1, 'asc']],
            //"fnServerParams": function (aoData) {
            //    aoData.push({ "name": "Name", "value": $("#txtNewsSearch").val() },
            //        { "name": "NewsTypeId", "value": $("#ddlNewsType").val() });
            //},
            "bStateSave": true,
            "aoColumns": [
                { "sType": "html", "sWidth": '90%', "mDataProp": "NameAr", "bSortable": false, "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '10%', "mDataProp": "Id", "sClass": "tdCenter" }
            ],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $('td:eq(1)', nRow).html('<div class="btn-group">' +
                        '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                        '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                        '</a>' +
                        ' <ul class="dropdown-menu pull-right">' +
                        '<li>' +
                        '<a href="javascript:;" class="lnk btnSaveCategory" data-id="' + aData.Id + '"><i class="fa fa-edit fa-fw"></i> ' + Messages.edit + '</a>' +
                        ' </li>' +
                        ' <li>' +
                        '<a href="javascript:;" class="lnk btnDeleteCategory" data-id ="' + aData.Id + '"><i class="fa fa-trash fa-fw"></i> ' + Messages.delete + '</a>' +
                        ' </li>' +
                        '</ul>' +
                        ' </div>');
                $(nRow).dblclick(function () {
                    saveCategoryModel($(this).find(".btnSaveCategory").attr("data-id"), $("#basicModal"));
                });
            },
            "fnDrawCallback": function (oSettings) {
                getSaveCategoryModal();
                deleteCategory();
            },
            "bFilter": false
            //"sPaginationType": "bootstrap"
        });
    };
    var categoriesDataTableUpdate = function () {
        var oTable = $('#tblCategories').dataTable();
        oTable.fnDraw(false);
    };
    var saveCategoryModel = function (id, bsModal) {
        bsModal.html('');
        setTimeout(function () {
            bsModal.load('/ControlPanel/SaveCategoryModal?id=' + id, '', function () {
                bsModal.modal('show');
                resetbooststrapSelect();
                handleBootstrapSelect();
                saveCategory();
            });
        }, 100);
    }
    var getSaveCategoryModal = function () {
        var bsModal = $("#basicModal");
        $(".btnSaveCategory").off('click').click(function () {
            var id = $(this).attr("data-id");
            bsModal.html('');
            setTimeout(function () {
                bsModal.load('/ControlPanel/SaveCategoryModal?id=' + id, '', function () {
                    bsModal.modal('show');
                    resetbooststrapSelect();
                    handleBootstrapSelect();
                    saveCategory();
                });
            }, 100);
        });
    };
    var saveCategory = function () {
        $('#SaveCategoryForm').on("submit", function (event) {
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
                        categoriesDataTableUpdate();
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
    };
    var deleteCategory = function () {
        $(".btnDeleteCategory").off('click').click(function () {
            var id = $(this).attr('data-Id');
            gsConfirm('' + Messages.deleteConfirm + '', function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        cache: false,
                        url: '/ControlPanel/DeleteCategory',
                        dataType: "JSON",
                        data: { 'id': id },
                        success: function (data) {
                            if (data.cStatus === "success") {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                                categoriesDataTableUpdate();

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
    /***********/
    var attachmentsDataTable = function () {
        $('#tblAttachments').dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
            },
            "bServerSide": true,
            "sAjaxSource": "/ControlPanel/getEducationalResourcesDataTable",
            "bProcessing": true,
            "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
            "aaSorting": [[6, 'asc']],
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "Id", "value": $("#txtAttachmentSearch").attr("data-Id") },
                    { "name": "Type", "value": $("#ddlType").val() });
            },
            "bStateSave": true,
            "aoColumns": [
                { "sType": "html", "sWidth": '5%', "mDataProp": "Icon", "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '20%', "mDataProp": "FileTitle" },
                { "sType": "html", "sWidth": '10%', "mDataProp": "TypeName" },
                { "sType": "html", "sWidth": '20%', "mDataProp": "FileDescription" },
                { "sType": "html", "sWidth": '20%', "mDataProp": "InsertedDate", "bSortable": false },
                { "sType": "html", "sWidth": '20%', "mDataProp": "UserName" },
                { "sType": "html", "sWidth": '5%', "mDataProp": "Id", "sClass": "tdCenter" }
            ],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $('td:eq(6)', nRow).html('<div class="btn-group">' +
                        '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                        '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                        '</a>' +
                        ' <ul class="dropdown-menu pull-right">' +
                        '<li>' +
                        '<a href="javascript:;" class="lnk btnSaveAttachment" data-id="' + aData.Id + '"><i class="fa fa-edit fa-fw"></i> ' + Messages.edit + '</a>' +
                        ' </li>' +
                        ' <li>' +
                        '<a href="javascript:;" class="lnk btnDeleteAttachment" data-id ="' + aData.Id + '"><i class="fa fa-trash fa-fw"></i> ' + Messages.delete + '</a>' +
                        ' </li>' +
                        ' </li>' +
                        ' <li>' +
                        '<a href="/Content/UploadedFile/Attachments/' + aData.FilePath + '" download="' + aData.FileTitle + '" "target="_blank"  class="lnk" data-id ="' + aData.Id + '"><i class="fa fa-download fa-fw"></i> تحميل</a>' +
                        ' </li>' +
                        '</ul>' +
                        ' </div>');
                    $('td:eq(0)', nRow).html('<i style="font-size:40px;line-height: 40px" class="' + aData.Icon + '"><i/>');
                    //$('td:eq(1)', nRow).html('<span>' + aData.NameAr + '-' + aData.NameEn + '</span>');
                
                $(nRow).dblclick(function () {
                    saveAttachmentModel($(this).find(".btnSaveAttachment").attr("data-id"), $("#basicModal"));

                });
            },
            "fnDrawCallback": function (oSettings) {
                getSaveAttachmentModal();
                deleteAttachment();
            },
            "bFilter": false
            //"sPaginationType": "bootstrap"
        });
    };
    var attachmentsDataTableUpdate = function () {
        var oTable = $('#tblAttachments').dataTable();
        oTable.fnDraw(false);
    };
    var saveAttachmentModel = function (id, bsModal) {
        bsModal.html('');
        setTimeout(function () {
            bsModal.load('/ControlPanel/SaveEducationalResourceModal?id=' + id, '', function () {
                bsModal.modal('show');
                resetbooststrapSelect();
                handleBootstrapSelect();
                saveAttachment();
                uploadFile();
                uploadAlbums();
            });
        }, 100);
    }
    var getSaveAttachmentModal = function () {
        var bsModal = $("#basicModal");
        $(".btnSaveAttachment").off('click').click(function () {
            var id = $(this).attr("data-id");
            bsModal.html('');
            setTimeout(function () {
                bsModal.load('/ControlPanel/SaveEducationalResourceModal?id=' + id, '', function () {
                    bsModal.modal('show');
                    resetbooststrapSelect();
                    handleBootstrapSelect();
                    saveAttachment();
                    uploadFile();
                    uploadAlbums();
                });
            }, 100);
        });
    };
    var saveAttachment = function () {
        $('#SaveAttachmentForm').submit(function () {
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
                        attachmentsDataTableUpdate();

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
    var deleteAttachment = function () {
        $(".btnDeleteAttachment").off('click').click(function () {
            var id = $(this).attr('data-Id');
            gsConfirm('' + Messages.deleteConfirm + '', function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        cache: false,
                        url: '/ControlPanel/DeleteAttachment',
                        dataType: "JSON",
                        data: { 'id': id },
                        success: function (data) {
                            if (data.cStatus === "success") {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                                attachmentsDataTableUpdate();

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
    var attachmentSearchAutoComplete = function () {
        if (!$('#txtAttachmentSearch').hasClass('tt-input')) {
            var attachment = new Bloodhound({
                datumTokenizer: function (d) { return d.tokens; },
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                remote: {
                    url: '/ControlPanel/SearchAutoCompleteEducationalResource/%QUERY',
                    wildcard: '%QUERY'
                }
            });
            attachment.initialize();

            $('#txtAttachmentSearch').typeahead({
                hint: true,
                highlight: true,
                minLength: 1,
                dir: true
            }, {
                    name: 'search-attachment',
                    displayKey: 'FileTitle',
                    source: attachment.ttAdapter(),
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
                            ' <h5 class="media-heading">{{FileTitle}} </h5>',
                            '</div>',
                            '</div>',
                        ].join(''))
                    },

                }).on('typeahead:selected', function ($e, datum) {
                    $("#txtAttachmentSearch").attr('data-id', datum.Id);
                });
        }
    };
    var attachmentSearch = function () {
        $("#btnSearch").off('click').click(function () {
            attachmentsDataTableUpdate();
        });
    };
    var resetAttachmentsDataTable = function () {
        $("#btnClearForm").off("click").click(function () {
            $("#txtAttachmentSearch").removeAttr("data-id");
            gsResetInsertForm("SearchForm");
            attachmentsDataTableUpdate();
        });
    };
    /************/
    var staticPagesDataTable = function () {
        $('#tblStaticPages').dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
            },
            "bServerSide": true,
            "sAjaxSource": "/ControlPanel/getStaticPagesDataTable",
            "bProcessing": true,
            "dom": '<"bottom"t><"clear">',
            "aaSorting": [[3, 'asc']],
            //"fnServerParams": function (aoData) {
            //    aoData.push({ "name": "Name", "value": $("#txtNewsSearch").val() },
            //        { "name": "NewsTypeId", "value": $("#ddlNewsType").val() });
            //},
            "bStateSave": true,
            "aoColumns": [
                { "sType": "html", "sWidth": '15%', "mDataProp": "Image", "bSortable": false, "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '35%', "mDataProp": "PageNameAr", "bSortable": false, "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '35%', "mDataProp": "PageNameEn", "bSortable": false },
                { "sType": "html", "sWidth": '5%', "mDataProp": "Id", "sClass": "tdCenter" }
            ],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $('td:eq(3)', nRow).html('<div class="btn-group">' +
                        '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                        '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                        '</a>' +
                        ' <ul class="dropdown-menu pull-right">' +
                        '<li>' +
                        '<a href="javascript:;" class="lnk btnSaveStaticPage" data-id="' + aData.Id + '"><i class="fa fa-edit fa-fw"></i> ' + Messages.edit + '</a>' +
                        ' </li>' +
                        ' <li>' +
                        '<a href="javascript:;" class="lnk btnDeletebtnSaveStatic" data-id ="' + aData.Id + '"><i class="fa fa-trash fa-fw"></i> ' + Messages.delete + '</a>' +
                        ' </li>' +
                        '</ul>' +
                        ' </div>');
                    $('td:eq(0)', nRow).html('<img  style="width: 200px;" alt="" src="/Content/UploadedFile/News/Thumbnail/' + aData.Image + ' " onError="this.onerror=null;this.src=\'/Content/UploadedFile/Account/Avatar/NoImage.png\';"/>');
                
                $(nRow).dblclick(function () {
                    saveStaticPagesModel($(this).find(".btnSaveStaticPage").attr("data-id"), $("#SaveModal"));

                });
            },
            "fnDrawCallback": function (oSettings) {
                getSaveStaticPagesModal();
                //deleteNewsAccount();
            },
            "bFilter": false
            //"sPaginationType": "bootstrap"
        });
    };
    var staticPagesDataTableUpdate = function () {
        var oTable = $('#tblStaticPages').dataTable();
        oTable.fnDraw(false);
    };
    var saveStaticPagesModel = function (id, bsModal) {
        bsModal.html('');
        $(".page-content").LoadingOverlay("show");
        setTimeout(function () {
            bsModal.load('/ControlPanel/SaveStaticPageModal?id=' + id, '', function () {
                $(".contentDiv").show();
                $(".tableDiv").hide();
                handleSummernote();
                closeModal();
                uploadImg($("#imgUploadUser"), $("#hdImage"));
                $(".page-content").LoadingOverlay("hide", true);
                saveStaticPages();
            });
        }, 100);
    }
    var getSaveStaticPagesModal = function () {
        var bsModal = $("#SaveModal");
        $(".btnSaveStaticPage").off('click').click(function () {
            var id = $(this).attr("data-id");
            bsModal.html('');
            $(".page-content").LoadingOverlay("show");
            setTimeout(function () {
                bsModal.load('/ControlPanel/SaveStaticPageModal?id=' + id, '', function () {
                    $(".contentDiv").show();
                    $(".tableDiv").hide();
                    handleSummernote();
                    closeModal();
                    uploadImg($("#imgUploadUser"), $("#hdImage"));
                    $(".page-content").LoadingOverlay("hide", true);
                    saveStaticPages();
                });
            }, 100);
        });
    };
    var saveStaticPages = function () {
        $('#SaveStaticPageForm').submit(function () {
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
                        staticPagesDataTableUpdate();
                        $(".scroll-to-top").click();
                    } else if (data.cStatus === "notValid") {
                        notValidOperations(data.cMsg);
                        $(".scroll-to-top").click();
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
    /********************/
    var intiDropZone = function () {
        $("#dropZoneImages").dropzone({
            maxFilesize: 700,
            acceptedFiles: 'image/*',
            sending: function (file, xhr, data) {
                data.append('caption', $("#tbmCaption").val());
            },
            queuecomplete: function (file, response) {
                gsNotifyMsg("تم رفع الصور بنجاح", "success");
                mediaDataTableUpdate();
            }

        });
    };
    var mediaDataTable = function () {
        $('#tblMedia').dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
            },
            "bServerSide": true,
            "sAjaxSource": "/ControlPanel/GetMediasDataTable",
            "bProcessing": true,
            "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
            "aaSorting": [[3, 'desc']],
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "TypeId", "value": $("#ddlTypes").val() });
            },
            "bStateSave": true,
            "aoColumns": [
                { "sType": "html", "sWidth": '25%', "mDataProp": "FilePath", "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '25%', "mDataProp": "MediaTypeName", "bSortable": false },
                { "sType": "html", "sWidth": '45%', "mDataProp": "Caption" },
                { "sType": "html", "sWidth": '5%', "mDataProp": "Id", "sClass": "tdCenter" }
            ],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $('td:eq(3)', nRow).html('<div class="btn-group">' +
                        '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                        '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                        '</a>' +
                        ' <ul class="dropdown-menu pull-right">' +
                        '<li>' +
                        '<a href="javascript:;" class="lnk btnSaveMedia" data-id="' + aData.Id + '"><i class="fa fa-edit fa-fw"></i> ' + Messages.edit + '</a>' +
                        ' </li>' +
                        ' <li>' +
                        '<a href="javascript:;" class="lnk btnDeleteMedia" data-id ="' + aData.Id + '"><i class="fa fa-trash fa-fw"></i> ' + Messages.delete + '</a>' +
                        ' </li>' +
                        '</ul>' +
                        ' </div>');

                    //$('td:eq(1)', nRow).html('<span>' + aData.MediaTypeAr + '-' + aData.MediaTypeEn + '</span>');
                    //$('td:eq(3)', nRow).html('<span>' + aData.AlbumAr + '-' + aData.AlbumEn + '</span>');
                if (aData.MediaType === 11) {
                    $('td:eq(0)', nRow).html('<a class="colorBox" href="/Content/UploadedFile/Albums/Large/' + aData.FilePath + ' "><img  style="width: 150px;" alt="" src="/Content/UploadedFile/Albums/Thumbnail/' + aData.FilePath + ' " onError="this.onerror=null;this.src=\'/Content/UploadedFile/Albums/Thumbinal/NoImage.png\';"/></a>');
                }
                else if (aData.MediaType === 12) {
                    $('td:eq(0)', nRow).html('<a class="colorBoxVideoSound " href="' + convertToEmbed(aData.ExternalLink) + ' "><img  style="width: 150px;" alt="" src="https://img.youtube.com/vi/' + getVideoId(aData.ExternalLink) + '/0.jpg" onError="this.onerror=null;this.src=\'/Content/UploadedFile/Albums/Thumbinal/NoImage.png\';"/></a>');

                } else if (aData.MediaType === 13) {
                    $('td:eq(0)', nRow).html('<a class="colorBoxVideoSound" href="' + aData.ExternalLink + ' "><img  style="width: 150px;" alt="" src="https://pmcvariety.files.wordpress.com/2015/08/soundcloud-logo.jpg" onError="this.onerror=null;this.src=\'/Content/UploadedFile/Albums/Thumbinal/NoImage.png\';"/></a>');
                }
                $(nRow).dblclick(function () {
                    saveMediaModel($(this).find(".btnSaveMedia").attr("data-id"), $("#basicModal"));
                });
            },
            "fnDrawCallback": function (oSettings) {
                getSaveMediaModal();
                deleteMedia();
                $(".colorBox").colorbox({ photo: true });
                $(".colorBoxVideoSound").colorbox({ iframe: true, innerWidth: 640, innerHeight: 390 });

            },
            "bFilter": false
            //"sPaginationType": "bootstrap"
        });
    };
    var mediaDataTableUpdate = function () {
        var oTable = $('#tblMedia').dataTable();
        oTable.fnDraw(false);
    };
    var mediaDataTableUpdateWithReSort = function () {
        var oTable = $('#tblMedia').dataTable();
        oTable.fnDraw(true);
    };
    var saveMediaModel = function (id, bsModal) {
        bsModal.html('');
        setTimeout(function () {
            bsModal.load('/ControlPanel/SaveMediaModal?id=' + id, '', function () {
                bsModal.modal('show');
                resetbooststrapSelect();
                handleBootstrapSelect();
                saveMedia();
                uploadAlbums();
                intiDropZone();

                handelChangeType();
            });
        }, 100);
    }
    var getSaveMediaModal = function () {
        var bsModal = $("#basicModal");
        $(".btnSaveMedia").off('click').click(function () {
            var id = $(this).attr("data-id");
            bsModal.html('');
            setTimeout(function () {
                bsModal.load('/ControlPanel/SaveMediaModal?id=' + id, '', function () {
                    bsModal.modal('show');
                    resetbooststrapSelect();
                    handleBootstrapSelect();
                    saveMedia();
                    uploadAlbums();
                    intiDropZone();
                    handelChangeType();
                });
            }, 100);
        });
    };
    var saveMedia = function () {
        $('#SaveMediaForm').submit(function () {
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
                        mediaDataTableUpdate();


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
    var deleteMedia = function () {
        $(".btnDeleteMedia").off('click').click(function () {
            var id = $(this).attr('data-Id');
            gsConfirm('' + Messages.deleteConfirm + '', function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        cache: false,
                        url: '/ControlPanel/DeleteMedia',
                        dataType: "JSON",
                        data: { 'id': id },
                        success: function (data) {
                            if (data.cStatus === "success") {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                                mediaDataTableUpdate();

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
    var handelChangeType = function () {
        $("select.ddlMediaType").on("change", function () {
            var vl = $(this).val();
            if (vl === "11") {
                $(".divExternalLink").hide();
                $(this).closest(".row").find(".imageDiv").show();
                $(this).closest(".mediaDiv").removeClass("col-md-12").addClass("col-md-7");
            }
            else {
                $(".divExternalLink").show();
                $(this).closest(".row").find(".imageDiv").hide();
                $(this).closest(".mediaDiv").removeClass("col-md-7").addClass("col-md-12");
            }
        });
    };
    /**********************/
    var saveSn = function () {
        $('#btnAdd').off('click').click(function () {
            var lst = [];
            $.each($(".social"), function (indexh, cb) {

                var Social = {
                    Id: $(cb).attr('SID'),
                    Link: $(cb).val()

                }
                lst.push(Social);
            });

            var str = JSON.stringify(lst, null, 2);
            $.ajax({
                type: 'post',
                dataType: 'json',
                url: '/ControlPanel/UpdateSocial',
                data: { 'str': str },
                success: function (data) {
                    if ($('.jquery-notific8-notification').length > 0) {
                        $('.jquery-notific8-notification').remove();
                    }
                    gsNotifyMsg(data.cMsg, data.cStatus);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if ($('.jquery-notific8-notification').length > 0) {
                        $('.jquery-notific8-notification').remove();
                    }
                    gsNotifyMsg('حدث خطأ ما! , الرجاء المحاولة ثانية', 'error');
                }
            });


        });
    }
    var savePagesHdr = function () {
        $('#btnAdd').off('click').click(function () {
            var lst = [];
            $.each($(".pagehdr"), function (indexh, cb) {

                var pageHdr = {
                    Id: $(cb).attr('SID'),
                    Image: $(cb).val()

                }
                lst.push(pageHdr);
            });

            var str = JSON.stringify(lst, null, 2);
            $.ajax({
                type: 'post',
                dataType: 'json',
                url: '/ControlPanel/UpdatePagesHdr',
                data: { 'str': str },
                success: function (data) {
                    gsNotifyMsg(data.cMsg, data.cStatus);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    gsNotifyMsg('حدث خطأ ما! , الرجاء المحاولة ثانية', 'error');
                }
            });


        });
    }
    var saveSd = function () {
        $('#btnAdd').off('click').click(function () {
            var lst = [];
            $.each($(".static"), function (indexh, cb) {

                var static = {
                    Id: $(cb).attr('SID'),
                    Title: $(cb).find('.txtTitle').val(),
                    Data: $(cb).find('.txtData').val(),
                    Status: $(cb).find('.cpStatus').prop("checked"),
                    Icon: $(cb).find('.txtIcon').val()

                }
                lst.push(static);
            });

            var str = JSON.stringify(lst, null, 2);
            $.ajax({
                type: 'post',
                dataType: 'json',
                url: '/ControlPanel/UpdateStaticData',
                data: { 'str': str },
                success: function (data) {
                    if ($('.jquery-notific8-notification').length > 0) {
                        $('.jquery-notific8-notification').remove();
                    }
                    gsNotifyMsg(data.cMsg, data.cStatus);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if ($('.jquery-notific8-notification').length > 0) {
                        $('.jquery-notific8-notification').remove();
                    }
                    gsNotifyMsg('حدث خطأ ما! , الرجاء المحاولة ثانية', 'error');
                }
            });


        });
    }

    var appSettingsDataTable = function () {
        $('#tblAppSettings').dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json" 
            },
            "bServerSide": true,
            "sAjaxSource": "/ControlPanel/getAppSettingsDataTable",
            "bProcessing": true,
            "dom": '<"clear">',
            "aaSorting": [[2, 'asc']],
            //"fnServerParams": function (aoData) {
            //    aoData.push({ "name": "Id", "value": $("#txtAppSettingsSearch").attr("data-Id") },
            //        { "name": "Type", "value": $("#ddlType").val() });
            //},
            "bStateSave": true,
            "aoColumns": [
                { "sType": "html", "sWidth": '10%', "mDataProp": "KeyName", "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '15%', "mDataProp": "ValueName" },
                { "sType": "html", "sWidth": '5%', "mDataProp": "ConKey", "sClass": "tdCenter" }
            ],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $('td:eq(2)', nRow).html('<div class="btn-group">' +
                        '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                        '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                        '</a>' +
                        ' <ul class="dropdown-menu pull-right">' +
                        '<li>' +
                        '<a href="javascript:;" class="lnk btnViewAppSettings" data-id="' + aData.ConKey + '"><i class="fa fa-edit fa-fw"></i> ' + Messages.edit + '</a>' +
                        ' </li>' +
                        ' </li>' +
                        '</ul>' +
                        ' </div>');
                    $('td:eq(0)', nRow).html(joinString(aData.KeyNameAr, aData.KeyNameEn));
                    $('td:eq(1)', nRow).html(joinString(aData.ValueNameAr, aData.ValueNameEn));
                $(nRow).dblclick(function () {
                    saveAppSettingsModel($(this).find(".btnViewAppSettings").attr("data-id"), $("#basicModal"));
                });
            },
            "fnDrawCallback": function (oSettings) {
                getSaveAppSettingsModal();
            },
            "bFilter": false
            //"sPaginationType": "bootstrap"
        });
    };
    var appSettingsDataTableUpdate = function () {
        var oTable = $('#tblAppSettings').dataTable();
        oTable.fnDraw(false);
    };
    var saveAppSettingsModel = function (id, bsModal) {
        bsModal.html('');
        setTimeout(function () {
            bsModal.load('/ControlPanel/SaveAppSettingsModal?id=' + id, '', function () {
                bsModal.modal('show');
                saveAppSettings();
                handleBootstrapSelect();

            });
        }, 100);
    }
    var getSaveAppSettingsModal = function () {
        var bsModal = $("#basicModal");
        $(".btnViewAppSettings").off('click').click(function () {
            var id = $(this).attr("data-id");
            bsModal.html('');
            setTimeout(function () {
                bsModal.load('/ControlPanel/SaveAppSettingsModal?id=' + id, '', function () {
                    bsModal.modal('show');
                    saveAppSettings();
                    handleBootstrapSelect();

                });
            }, 100);
        });
    };
    var saveAppSettings = function () {
        $('#SaveAppSettingsForm').submit(function () {
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
                        appSettingsDataTableUpdate();
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
    /***************/
    var teamMembersDataTable = function () {
        $('#tblTeamMembers').dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
            },
            "bServerSide": true,
            "sAjaxSource": "/ControlPanel/GetTeamMembersDataTable",
            "bProcessing": true,
            "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
            "aaSorting": [[3, 'asc']],
            "fnServerParams": function (aoData) {
                aoData.push({ "name": "Name", "value": $("#txtSearch").val() });
            },
            "bStateSave": true,
            "aoColumns": [
                { "sType": "html", "sWidth": '10%', "mDataProp": "Avatar", "bSortable": false, "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '55%', "mDataProp": "Name", "bSortable": false },
                { "sType": "html", "sWidth": '30%', "mDataProp": "JobTitle", "bSortable": false },
                { "sType": "html", "sWidth": '5%', "mDataProp": "Id", "sClass": "tdCenter" }
            ],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                $('td:eq(0)', nRow).html('<img  style="width: 100px;" alt="" src="/Content/UploadedFile/Account/Avatar/Thumbnail/' + aData.Avatar + ' " onError="this.onerror=null;this.src=\'/Content/UploadedFile/Account/Avatar/NoImage.png\';"/>');

                $('td:eq(3)', nRow).html('<div class="btn-group">' +
                    '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                    '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                    '</a>' +
                    ' <ul class="dropdown-menu pull-right">' +
                    '<li>' +
                    '<a href="javascript:;" class="lnk btnSave" data-id="' + aData.Id + '"><i class="fa fa-edit fa-fw"></i> ' + Messages.edit + '</a>' +
                    ' </li>' +
                    ' <li>' +
                    '<a href="javascript:;" class="lnk btnDelete" data-id ="' + aData.Id + '"><i class="fa fa-trash fa-fw"></i> ' + Messages.delete + '</a>' +
                    ' </li>' +
                    '</ul>' +
                    ' </div>');
                $(nRow).dblclick(function () {
                    saveTeamMemberModal($(this).find(".btnSave").attr("data-id"), $("#SaveModal"));
                });
            },
            "fnDrawCallback": function (oSettings) {
                getSaveTeamMemberModal();
                deleteTeamMember();
            },
            "bFilter": false
            //"sPaginationType": "bootstrap"
        });
    };
    var teamMembersDataTableUpdate = function () {
        var oTable = $('#tblTeamMembers').dataTable();
        oTable.fnDraw(false);
    };
    var saveTeamMemberModal = function (id, bsModal) {
        bsModal.html('');
        $(".page-content").LoadingOverlay("show");
        setTimeout(function () {
            bsModal.load('/ControlPanel/SaveTeamMemberModal?id=' + id , '', function () {
                $(".contentDiv").show();
                $(".tableDiv").hide();
                resetbooststrapSelect();
                handleBootstrapSelect();
                closeModal();
                saveTeamMember();
                uploadAvatar();
                $(".page-content").LoadingOverlay("hide", true);
            });
        }, 100);
    }
    var getSaveTeamMemberModal = function () {
        var bsModal = $("#SaveModal");
        $(".btnSaveMember").off('click').click(function () {
            var id = $(this).attr("data-id");
            bsModal.html('');
            $(".page-content").LoadingOverlay("show");
            setTimeout(function () {
                bsModal.load('/ControlPanel/SaveTeamMemberModal?id=' + id , '', function () {
                    $(".contentDiv").show();
                    $(".tableDiv").hide();
                    resetbooststrapSelect();
                    handleBootstrapSelect();
                    closeModal();
                    saveTeamMember();
                    uploadAvatar();
                    $(".page-content").LoadingOverlay("hide", true);
                });
            }, 100);
        });
    };
    var saveTeamMember = function () {
        $('#SaveTeamMemberForm').on("submit", function (event) {
            var form = this;
            gsDisablSubmitButton(form);
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
                        teamMembersDataTableUpdate();
                        $("#teamMemberId").val(data.id);
                        $(".scroll-to-top").click();
                    } else if (data.cStatus === "notValid") {
                        notValidOperations(data.cMsg);
                        $(".scroll-to-top").click();
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
    };

    var deleteTeamMember = function () {
        $(".btnDelete").off('click').click(function () {
            var id = $(this).attr('data-Id');
            gsConfirm('' + Messages.deleteConfirm + '', function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        cache: false,
                        url: '/ControlPanel/DeleteTeamMember',
                        dataType: "JSON",
                        data: { 'id': id },
                        success: function (data) {
                            if (data.cStatus === "success") {
                                gsNotifyMsg(data.cMsg, data.cStatus);
                                teamMembersDataTableUpdate();

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
    var teamMembersSearch = function () {
        $("#btnSearch").off('click').click(function () {
            teamMembersDataTableUpdate();
        });
    };
    var resetTeamMembersDataTable = function () {
        $("#btnClearForm").off("click").click(function () {
            $("#txtNewsSearch").removeAttr("data-id");
            $("#txtNewsSearch").val("");
            gsResetInsertForm("SearchForm");
            teamMembersDataTableUpdate();
        });
    };


    /********NewsletterSubscribers**********/
    var newsletterSubscribersDataTable = function () {
        $('#tbl').dataTable({
            "language": {
                "url": "/Content/assets/global/plugins/DataTables-1.10.12/languages/ar.json"
            },
            "bServerSide": true,
            "sAjaxSource": "/ControlPanel/getNewsletterSubscribersDataTable",
            "bProcessing": true,
            "dom": '<"bottom"t<"col-sm-3 "l><"col-sm-4"i><"col-sm-5"p>><"clear">',
            "aaSorting": [[2, 'asc']],
            //"fnServerParams": function (aoData) {
            //    aoData.push({ "name": "Name", "value": $("#txtNewsSearch").val() },
            //        { "name": "NewsTypeId", "value": $("#ddlNewsType").val() });
            //},
            "bStateSave": true,
            "aoColumns": [
                { "sType": "html", "sWidth": '75%', "mDataProp": "Email", "bSortable": false, "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '75%', "mDataProp": "IsActive", "bSortable": false, "sClass": "tdCenter" },
                { "sType": "html", "sWidth": '10%', "mDataProp": "Id", "sClass": "tdCenter" }
            ],
            "fnRowCallback": function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                if (aData.IsActive === true) {
                    $('td:eq(1)', nRow).html("<span class='font-green-meadow fa fa-fw fa-check-circle-o fa-lg'></span>");
                } else {
                    $('td:eq(1)', nRow).html("<span class='font-red-thunderbird fa fa-fw fa-times-circle-o fa-lg'></span>");
                }
                var html = '<div class="btn-group">' +
                    '<a class="btn btnx  dark btn-outline btn-xs" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="false">' +
                    '<i class="fa fa-cog fa-fw fa-xs"></i>' +
                    '</a>' +
                    ' <ul class="dropdown-menu pull-right">';
                if (aData.IsActive === true) {
                    html += ' <li>' +
                        '<a href="javascript:;" class="lnk btnDisable" data-id ="' + aData.Id + '" ><i class="fa fa-times fa-fw"></i> ' + Messages.deactivate + '</a>' +
                        ' </li>';
                } else {
                    html += ' <li>' +
                        '<a href="javascript:;" class="lnk btnActivate" data-id ="' + aData.Id + '"><i class="fa fa-check fa-fw"></i> ' + Messages.activate + '</a>' +
                        ' </li>';
                }

                html +=
                    '</ul> </div>';
                $('td:eq(2)', nRow).html(html);
            },
            "fnDrawCallback": function (oSettings) {
                disableEmail();
                activateEmail();
            },
            "bFilter": false
            //"sPaginationType": "bootstrap"
        });
    };
    var newsletterSubscribersDataTableUpdate = function () {
        var oTable = $('#tbl').dataTable();
        oTable.fnDraw(false);
    };

    var disableEmail = function () {
        $(".btnDisable").off("click").click(function () {
                var id = parseInt($(this).attr('data-Id'));
                $.ajax({
                    type: "POST",
                    cache: false,
                    url: '/ControlPanel/NewsletterSubscriberChangeStatus',
                    dataType: "JSON",
                    async: false,
                    data: { 'id': id, 'status': false },
                    success: function (data) {
                        if (data.cStatus === "success") {
                            gsNotifyMsg(data.cMsg, data.cStatus);
                            newsletterSubscribersDataTableUpdate();
                        } else {
                            gsNotifyMsg(data.cMsg, data.cStatus);
                        }

                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        gsNotifyMsg('' + Messages.noResultFound + '', "error");
                        gsEnableSubmitButton(form);
                    }
                });
            });
    };
    var activateEmail = function () {
        $(".btnActivate").off("click").click(function () {
                var id = parseInt($(this).attr('data-Id'));
                $.ajax({
                    type: "POST",
                    cache: false,
                    url: '/ControlPanel/NewsletterSubscriberChangeStatus',
                    dataType: "JSON",
                    async: false,
                    data: { 'id': id, 'status': true },
                    success: function (data) {
                        if (data.cStatus === "success") {
                            gsNotifyMsg(data.cMsg, data.cStatus);
                            newsletterSubscribersDataTableUpdate();
                            location.reload(true);
                        } else {
                            gsNotifyMsg(data.cMsg, data.cStatus);
                        }

                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        gsNotifyMsg('' + Messages.noResultFound + '', "error");
                        gsEnableSubmitButton(form);
                    }
                });
            });
    };

    return {
        initNews: function() {
            newsDataTable(false);
            newsSearchAutoComplete();
            newsSearch();
            resetNewsDataTable();
            handleDatePickers();
            fnHideCol(2, "tblNews");
        },
        initArticles: function() {
            newsDataTable(true);
            newsSearchAutoComplete();
            newsSearch();
            resetNewsDataTable();
            handleDatePickers();
        },
        initCategory: function() {
            categoriesDataTable();
        },
        initMedia: function() {
            //albumsDataTable();
            //albumSearchAutoComplete();
            //albumSearch();
            //resetAlbumDataTable();
            mediaDataTable();
        },
        initAttachments: function() {
            attachmentsDataTable();
            attachmentSearchAutoComplete();
            attachmentSearch();
            resetAttachmentsDataTable();
        },
        initStaticPages: function() {
            staticPagesDataTable();
            handleSummernote();
            closeModal();
            uploadImg($("#imgUploadUser"), $("#hdImage"));
            uploadImg($("#imgUploadUser2"), $("#hdImage2"));
            uploadImg($("#imgUploadUser3"), $("#hdImage3"));

            saveStaticPages();
        },
        initTeamMembers: function () {
            teamMembersDataTable();
            teamMembersSearch();
            resetTeamMembersDataTable();
        },
        initAds: function() {
            adsDataTable();
        },
        initSocial: function() {
            saveSn();
        },
        initPagesHdr: function () {
            uploadImg($("#fileUpload_1"), $("#hdImage_1"), true);
            uploadImg($("#fileUpload_2"), $("#hdImage_2"), true);
            uploadImg($("#fileUpload_3"), $("#hdImage_3"), true);
            uploadImg($("#fileUpload_4"), $("#hdImage_4"), true);
            uploadImg($("#fileUpload_5"), $("#hdImage_5"), true);
            uploadImg($("#fileUpload_6"), $("#hdImage_6"), true);
            uploadImg($("#fileUpload_7"), $("#hdImage_7"), true);
            uploadImg($("#fileUpload_8"), $("#hdImage_8"), true);
            uploadImg($("#fileUpload_9"), $("#hdImage_9"), true);
            uploadImg($("#fileUpload_10"), $("#hdImage_10"), true);
            savePagesHdr();
        },
        initStaticData: function() {
            saveSd();
        },
        initContactUs() {
            contactUsDataTable();
        },
        initComments() {
            commentsDataTable();
            commentsSearch();
            resetCommentsDataTable();
            approveComment();
        },
        initAppSettings() {
            appSettingsDataTable();
        },
        initImportantLinks: function() {
            importantLinksDataTable();
        },
        initAgeCategory: function() {
            AgeCategoryDataTable();
        },
        initNewsletterSubscribers: function() {
            newsletterSubscribersDataTable();
        }

    };
}();