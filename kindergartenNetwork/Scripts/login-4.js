var Login = function () {

	var handleLogin = function() {
		$('#loginForm').validate({
	            errorElement: 'span', //default input error message container
	            errorClass: 'help-block', // default input error message class
	            focusInvalid: false, // do not focus the last invalid input
	            rules: {
	                Email: {
	                    required: true
	                },
	                Password: {
	                    required: true
	                },
	                Language: {
	                    required: true
	                },
	                Remember: {
	                    required: false
	                }
	            },

	            messages: {
	                Email: {
                        required: "البريد الإلكتروني مطلوب."

	                },
	                Language: {
	                    required: "Language is required."
	                },
	                Password: {
	                    required: "كلمة المرور مطلوبة."
	                }
	            },

	            invalidHandler: function (event, validator) { //display error alert on form submit   
	                $('.alert-danger', $('#loginForm')).show();
	            },

	            highlight: function (element) { // hightlight error inputs
	                $(element)
	                    .closest('.form-group').addClass('has-error'); // set error class to the control group
	            },

	            success: function (label) {
	                label.closest('.form-group').removeClass('has-error');
	                label.remove();
	            },

	            errorPlacement: function (error, element) {
	                error.insertAfter(element.closest('.input-icon'));
	            },

	            submitHandler: function (form) {
	                //form.submit();
	                login();
	            }
	        });

	        $('#loginForm input').keypress(function (e) {
	            if (e.which === 13) {
	                if ($('#loginForm').validate().form()) {
	                    $('#loginForm').submit();
	                }
	                return false;
	            }
	        });
	}
	var login = function () {
	    var postData = $("#loginForm").serializeArray();
	    var formUrl = $("#loginForm").attr("action");
	    $.ajax({
	        type: "POST",
	        cache: false,
	        url: formUrl,
	        data: postData,
	        dataType: "json",
	        success: function (data) {
	            if (data.cStatus === "success") {
	                if (data.isRedirect) {
	                    window.location.href = data.redirectUrl;
	                }
	                $(".alert-danger").css("display", "none");
	                $(".alert-success").html(" <button class='close' data-close='alert'></button>ÊãÊ Ç áÚãáíÉ ÈäÌÇÍ");
	                $(".alert-success").css("display", "block");
	                //gsNotifyMsg(data.cMsg, data.cStatus);
	            } else {
	                $(".alert-success").css("display", "none");
	                $(".alert-danger").html(" <button class='close' data-close='alert'></button>" + data.cMsg);
	                $(".alert-danger").css("display", "block");
	            }
	        },
	        error: function (xhr, ajaxOptions, thrownError) {
	        }
	    });
	}

	var handleForgetPassword = function () {
		$('.forget-form').validate({
	            errorElement: 'span', //default input error message container
	            errorClass: 'help-block', // default input error message class
	            focusInvalid: false, // do not focus the last invalid input
	            ignore: "",
	            rules: {
	                email: {
	                    required: true,
	                    email: true
	                }
	            },

	            messages: {
	                email: {
	                    required: "الإيميل مطلوب."
	                }
	            },

	            invalidHandler: function (event, validator) { //display error alert on form submit   

	            },

	            highlight: function (element) { // hightlight error inputs
	                $(element)
	                    .closest('.form-group').addClass('has-error'); // set error class to the control group
	            },

	            success: function (label) {
	                label.closest('.form-group').removeClass('has-error');
	                label.remove();
	            },

	            errorPlacement: function (error, element) {
	                error.insertAfter(element.closest('.input-icon'));
	            },

	            submitHandler: function (form) {
	                form.submit();
	            }
	        });

	        $('.forget-form input').keypress(function (e) {
	            if (e.which == 13) {
	                if ($('.forget-form').validate().form()) {
	                    $('.forget-form').submit();
	                }
	                return false;
	            }
	        });

	        jQuery('#forget-password').click(function () {
	            jQuery('#loginForm').hide();
	            jQuery('.forget-form').show();
	        });

	        jQuery('#back-btn').click(function () {
	            jQuery('#loginForm').show();
	            jQuery('.forget-form').hide();
	        });

	}

	var handleRegister = function () {

		        function format(state) {
            if (!state.id) { return state.text; }
            var $state = $(
             '<span><img src="../assets/global/img/flags/' + state.element.value.toLowerCase() + '.png" class="img-flag" /> ' + state.text + '</span>'
            );
            
            return $state;
        }

        if (jQuery().select2 && $('#country_list').size() > 0) {
            $("#country_list").select2({
	            placeholder: '<i class="fa fa-map-marker"></i>&nbsp;Select a Country',
	            templateResult: format,
                templateSelection: format,
                width: 'auto', 
	            escapeMarkup: function(m) {
	                return m;
	            }
	        });


	        $('#country_list').change(function() {
	            $('.register-form').validate().element($(this)); //revalidate the chosen dropdown value and show error or success message for the input
	        });
    	}


         $('.register-form').validate({
	            errorElement: 'span', //default input error message container
	            errorClass: 'help-block', // default input error message class
	            focusInvalid: false, // do not focus the last invalid input
	            ignore: "",
	            rules: {
	                
	                fullname: {
	                    required: true
	                },
	                email: {
	                    required: true,
	                    email: true
	                },
	                address: {
	                    required: true
	                },
	                city: {
	                    required: true
	                },
	                country: {
	                    required: true
	                },

	                username: {
	                    required: true
	                },
	                password: {
	                    required: true
	                },
	                rpassword: {
	                    equalTo: "#register_password"
	                },

	                tnc: {
	                    required: true
	                }
	            },

	            messages: { // custom messages for radio buttons and checkboxes
	                tnc: {
	                    required: "Please accept TNC first."
	                }
	            },

	            invalidHandler: function (event, validator) { //display error alert on form submit   

	            },

	            highlight: function (element) { // hightlight error inputs
	                $(element)
	                    .closest('.form-group').addClass('has-error'); // set error class to the control group
	            },

	            success: function (label) {
	                label.closest('.form-group').removeClass('has-error');
	                label.remove();
	            },

	            errorPlacement: function (error, element) {
	                if (element.attr("name") == "tnc") { // insert checkbox errors after the container                  
	                    error.insertAfter($('#register_tnc_error'));
	                } else if (element.closest('.input-icon').size() === 1) {
	                    error.insertAfter(element.closest('.input-icon'));
	                } else {
	                	error.insertAfter(element);
	                }
	            },

	            submitHandler: function (form) {
	                form.submit();
	            }
	        });

			$('.register-form input').keypress(function (e) {
	            if (e.which == 13) {
	                if ($('.register-form').validate().form()) {
	                    $('.register-form').submit();
	                }
	                return false;
	            }
	        });

	        jQuery('#register-btn').click(function () {
	            jQuery('#loginForm').hide();
	            jQuery('.register-form').show();
	        });

	        jQuery('#register-back-btn').click(function () {
	            jQuery('#loginForm').show();
	            jQuery('.register-form').hide();
	        });
	}
    


    return {
        //main function to initiate the module
        init: function () {
        	
            handleLogin();
            handleForgetPassword();
            handleRegister();    

            // init background slide images
		    $.backstretch([
		        "/Content/UploadedFile/loginImages/1.jpg",
		        "/Content/UploadedFile/loginImages/2.jpg",
		        "/Content/UploadedFile/loginImages/3.jpg",
		        "/Content/UploadedFile/loginImages/4.jpg",
                "/Content/UploadedFile/loginImages/5.jpg",
                "/Content/UploadedFile/loginImages/6.jpg"
		        ], {
		          fade: 1000,
		          duration: 8000
		    	}
        	);
        }
    };

}();

jQuery(document).ready(function() {
    Login.init();
});