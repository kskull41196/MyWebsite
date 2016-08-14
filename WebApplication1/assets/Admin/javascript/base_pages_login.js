/*
 *  Document   : base_pages_login.js
 *  Author     : pixelcave
 */
var BasePagesLogin = function () {
        var e = function () {
                jQuery(".js-validation-login").validate({
                    errorClass: "help-block text-right animated fadeInDown",
                    errorElement: "div",
                    errorPlacement: function (e, r) {
                        jQuery(r).parents(".form-group .form-material").append(e)
                    },
                    highlight: function (e) {
                        jQuery(e).closest(".form-group").removeClass("has-error").addClass("has-error"), jQuery(e).closest(".help-block").remove()
                    },
                    success: function (e) {
                        jQuery(e).closest(".form-group").removeClass("has-error"), jQuery(e).closest(".help-block").remove()
                    },
                    rules: {
                        "login-username": {
                            required: !0,
                            minlength: 3
                        },
                        "login-password": {
                            required: !0,
                            minlength: 5
                        }
                    },
                    messages: {
                        "login-username": {
                            required: "Vui lòng nhập tên đăng nhập",
                            minlength: "Tên đăng nhập của bạn phải có ít nhất 3 ký tự"
                        },
                        "login-password": {
                            required: "Vui lòng cung cấp một mật khẩu",
                            minlength: "Mật khẩu của bạn phải có ít nhất 5 ký tự"
                        }
                    }
                })
            };
        return {
            init: function () {
                e()
            }
        }
    }();
jQuery(function () {
    BasePagesLogin.init()
});