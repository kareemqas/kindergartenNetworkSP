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

            invalidHandler: function(event, validator) { //display error alert on form submit   
                $('.alert-danger', $('#loginForm')).show();
            },

            highlight: function(element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function(label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function(error, element) {
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function(form) {
                //form.submit();
                login();
            }
        });

        $('#loginForm input').keypress(function(e) {
            if (e.which === 13) {
                if ($('#loginForm').validate().form()) {
                    $('#loginForm').submit();
                }
                return false;
            }
            return false;
        });
    }
    var login = function() {
        var postData = $("#loginForm").serializeArray();
        var formUrl = $("#loginForm").attr("action");
        $.ajax({
            type: "POST",
            cache: false,
            url: formUrl,
            data: postData,
            dataType: "json",
            success: function(data) {
                if (data.cStatus === "success") {
                    if (data.isRedirect) {
                        window.location.href = data.redirectUrl;
                    }
                    $(".alert-danger").css("display", "none");
                    $(".alert-success").html(" <button class='close' data-close='alert'></button>" + data.cMsg);
                    $(".alert-success").css("display", "block");
                    //gsNotifyMsg(data.cMsg, data.cStatus);
                } else {
                    $(".alert-success").css("display", "none");
                    $(".alert-danger").html(" <button class='close' data-close='alert'></button>" + data.cMsg);
                    $(".alert-danger").css("display", "block");
                }
            },
            error: function(xhr, ajaxOptions, thrownError) {
            }
        });
    }

    var handleForgetPassword = function() {
        $('#forgetPasswordForm').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",
            rules: {
                email: {
                    required: true
                }
            },

            messages: {
                email: {
                    required: "الإيميل مطلوب."
                }
            },

            invalidHandler: function(event, validator) { //display error alert on form submit   

            },

            highlight: function(element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function(label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function(error, element) {
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function(form) {
                forgetPassword();
            }
        });

        $('#forgetPasswordForm input').keypress(function(e) {
            if (e.which === 13) {
                if ($('#forgetPasswordForm').validate().form()) {
                    $('#forgetPasswordForm').submit();
                }
                return false;
            }
            return false;
        });
    }
        var forgetPassword = function () {
            $('#forgetPasswordForm').submit(function () {
                var formUrl = $("#forgetPasswordForm").attr("action");
                $.ajax({
                    type: "POST",
                    cache: false,
                    url: formUrl,
                    data: { 'email': $("#tbEmail").val() },
                    dataType: "json",
                    success: function (data) {
                        if (data.cStatus === "success") {
                            //window.location.href = "/Home/Index";
                            $(".alert-danger").css("display", "none");
                            $(".alert-success").html(" <button class='close' data-close='alert'></button>" + data.cMsg);
                            $(".alert-success").css("display", "block");
                            $("#tbEmail").val('');
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
            });
        }

	var handleResetPassword = function() {
        $('#ResetPasswordForm').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",
            rules: {
                password: {
                    required: true
                },
                password_confirm: {
                    required: true,
                    equalTo: "#password"
                }
            },

            messages: {
                password: {
                    required: "كلمة المرور مطلوبة."
                },
                password_confirm: {
                    required: "تأكيد كلمة المرور مطلوبة",
                    equalTo: "كلمة المرور غير متطابقة"
                }
            },

            invalidHandler: function(event, validator) { //display error alert on form submit   

            },

            highlight: function(element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function(label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function(error, element) {
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function(form) {
                resetPassword();
            }
        });

        $('#ResetPasswordForm input').keypress(function(e) {
            if (e.which === 13) {
                if ($('#ResetPasswordForm').validate().form()) {
                    $('#ResetPasswordForm').submit();
                }
                return false;
            }
            return false;
        });


        var resetPassword = function () {
            var formUrl = $("#ResetPasswordForm").attr("action");
            var id = $("#id").val();
            var token = $("#token").val();
            var pass = $("#password").val();
            $.ajax({
                type: "POST",
                cache: false,
                url: formUrl,
                data: { "id": id, "token": token, "newPass": pass },
                dataType: "json",
                success: function (data) {
                    if (data.cStatus === "success") {
                        $(".alert-danger").css("display", "none");
                        $(".alert-success").html(" <button class='close' data-close='alert'></button>" + data.cMsg);
                        $(".alert-success").css("display", "block");
                            window.location.href = "/Home/Login";
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

    }

    var handleRegister = function () {
		$('#RegistrationForm').validate({
			errorElement: 'span', //default input error message container
			errorClass: 'help-block', // default input error message class
			focusInvalid: false, // do not focus the last invalid input
			rules: {
				Email: {
					required: true
				},
				Pass: {
					required: true
				},
				Name: {
					required: true
				}
			},

			messages: {
				Email: {
					required: "البريد الإلكتروني مطلوب."

				},
				Name: {
					required: "الاسم مطلوي."
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
                register();
			}
		});

		$('#RegistrationForm input').keypress(function (e) {
			if (e.which === 13) {
				if ($('#RegistrationForm').validate().form()) {
					$('#RegistrationForm').submit();
				}
				return false;
			}
		});
    }
    var register = function() {
        var postData = $("#RegistrationForm").serializeArray();
        var formUrl = $("#RegistrationForm").attr("action");
        $.ajax({
            type: "POST",
            cache: false,
            url: formUrl,
            data: postData,
            dataType: "json",
            success: function(data) {
                if (data.cStatus === "success") {
                    window.location.href = "/Home/Index";
                    $(".alert-danger").css("display", "none");
                    $(".alert-success").html(" <button class='close' data-close='alert'></button>" + data.cMsg);
                    $(".alert-success").css("display", "block");
                    //gsNotifyMsg(data.cMsg, data.cStatus);
                } else {
                    $(".alert-success").css("display", "none");
                    $(".alert-danger").html(" <button class='close' data-close='alert'></button>" + data.cMsg);
                    $(".alert-danger").css("display", "block");
                }
            },
            error: function(xhr, ajaxOptions, thrownError) {
            }
        });
    }

	var subscribe = function () {
        $('#SubscribeInNewsletterForm').submit(function() {
            var formUrl = $("#SubscribeInNewsletterForm").attr("action");
            $.ajax({
                type: "POST",
                cache: false,
                url: formUrl,
                data: { 'email': $("#tbEmail").val() },
                dataType: "json",
                success: function(data) {
                    if (data.cStatus === "success") {
                        //window.location.href = "/Home/Index";
                        $(".alert-danger").css("display", "none");
                        $(".alert-success").html(" <button class='close' data-close='alert'></button>" + data.cMsg);
						$(".alert-success").css("display", "block");
                        $("#tbEmail").val('');
                        //gsNotifyMsg(data.cMsg, data.cStatus);
                    } else {
                        $(".alert-success").css("display", "none");
                        $(".alert-danger").html(" <button class='close' data-close='alert'></button>" + data.cMsg);
                        $(".alert-danger").css("display", "block");
                    }
                },
                error: function(xhr, ajaxOptions, thrownError) {
                }
            });
        });
    }


    return {
        //main function to initiate the module
        init: function () {
        	
            handleLogin();
            handleRegister();
		}, 
		initSubscribe: function () {
			subscribe();
			$('#SubscribeInNewsletterForm input').keypress(function (e) {
                if (e.which === 13) {
						$('#SubscribeInNewsletterForm').submit();
                        return false;
                }
                return false;
            });
        },
        initForgetPassword: function () {
            handleForgetPassword();
        },
		initResetPassword: function () {
   //         var password = document.getElementById("password")
   //             , confirmPassword = document.getElementById("confirm_password");

   //         function validatePassword() {
			//	if (password.value !== '' && confirmPassword.value !== '' && password.value !== confirmPassword.value) {
   //                 confirmPassword.setCustomValidity("Passwords Don't Match");
   //             } else {
   //                 confirmPassword.setCustomValidity('');
   //             }
			//}

   //         password.onchange = validatePassword;
			//confirmPassword.onkeyup = validatePassword;

            handleResetPassword();
        }
    };

}();
