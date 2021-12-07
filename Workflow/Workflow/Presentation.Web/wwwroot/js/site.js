// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var commons = {

    paddingLeft: function (inputNumber, outputLenght, paddingChar) {
        return (outputLenght > inputNumber.toString().length) ? ((Array(outputLenght).join(paddingChar) + inputNumber).slice(-outputLenght)) : inputNumber;
    },

    truncateText: function (str, length, ending) {
        if (length == null) {
            length = 100;
        }
        if (ending == null) {
            ending = '...';
        }
        if (str.length > length) {
            return str.substring(0, length - ending.length) + ending;
        } else {
            return str;
        }
    },

    onlyNumbers: function (input) {
        return ('' + input).replace(/\D/g, '');
    },

    isUndefined: function (pobj) {
        return (pobj == undefined);
    },

    scrollToElement: function (id) {
        if (commons.isUndefined($(id).offsetParent())) {
            $('html, body').animate({ scrollTop: $(id).offset().top - 60 }, 1000);
        } else {
            $('html, body').animate({ scrollTop: $(id).offsetParent().offset().top - 60 }, 1000);
        }
    },

    scrollToElementEx: function (parent, id) {
        $(parent).animate({ scrollTop: $(id).offset().top - $(id).offsetParent().offset().top }, 1000);
    },

    scrollToElementEx2: function (id, margin) {
        if (commons.isUndefined($(id).offsetParent())) {
            $('html, body').animate({ scrollTop: $(id).offset().top - margin }, 1000);
        } else {
            $('html, body').animate({ scrollTop: $(id).offsetParent().offset().top - margin }, 1000);
        }
    },

    getErrorMessage: function (xhr) {
        if (xhr) {
            if (xhr.responseJSON) {
                try {
                    return xhr.responseJSON.Message;
                } catch (err) {
                    return xhr.responseJSON;
                }
            } else {
                if (xhr.responseText) {
                    try {
                        var json = JSON.parse(xhr.responseText);
                        return json.Message;
                    } catch (err) {
                        return xhr.responseText;
                    }
                }
            }
            //}
        }
        return '';
    },

    postAndCallback: function (formSelector, validateFirst, callback) {
        $(formSelector).off('submit');
        $(formSelector).on('submit', function (event) {
            event.preventDefault();
            var $form = $(this);
            format.removeInputMasks(formSelector);
            var formValues = $form.serialize();
            format.initializeInputMasks(formSelector);
            $form.validate({
                debug: true,
                ignore: '',
                rules: {}
            });
            if (validateFirst) {
                if (!$form.valid()) {
                    var validator = $form.validate();
                    $.each(validator.errorMap, function (index, value) {
                        console.log('Erro em Id: ' + index + ' - Message: ' + value);
                    });
                    commons.scrollToElement('.error:visible');
                    return false;
                }
            }
            callback(formValues);
        });
        $(formSelector).submit();
    },

};

var format = {

    formatCpfCnpj: function (cpfCnpj) {
        cpfCnpj = commons.onlyNumbers(cpfCnpj);
        if (cpfCnpj.length > 0) {
            if (cpfCnpj.length <= 11) {
                cpfCnpj = commons.paddingLeft(cpfCnpj, 11, '0');
                cpfCnpj =
                    cpfCnpj.substr(0, 3) + '.' +
                    cpfCnpj.substr(3, 3) + '.' +
                    cpfCnpj.substr(6, 3) + '-' +
                    cpfCnpj.substr(9, 2);
            } else {
                cpfCnpj = commons.paddingLeft(cpfCnpj, 14, '0');
                cpfCnpj =
                    cpfCnpj.substr(0, 2) + '.' +
                    cpfCnpj.substr(2, 3) + '.' +
                    cpfCnpj.substr(5, 3) + '/' +
                    cpfCnpj.substr(8, 4) + '-' +
                    cpfCnpj.substr(12, 2);
            }
        }
        return cpfCnpj;
    },

    initializeInputMasks: function (parentId) {
        $(parentId + ' .mask-money-positive').each(function () {
            $(this).maskMoney({ prefix: '', allowNegative: false, allowZero: true, thousands: '.', decimal: ',', affixesStay: false });
            $(this).maskMoney('mask', $(this).val());
            $(this).attr("maxlength", 18);
        });

        $(parentId + ' .mask-percentage-5-2').each(function () {
            $(this).maskMoney({ prefix: '', allowNegative: false, allowZero: true, thousands: '.', decimal: ',', affixesStay: false });
            $(this).maskMoney('mask', $(this).val());
        })
        $(parentId + ' .mask-percentage-5-2').attr('maxlength', 6);

        $(parentId + ' .mask-percentage-4-2').each(function () {
            $(this).maskMoney({ prefix: '', allowNegative: false, allowZero: true, thousands: '.', decimal: ',', affixesStay: false });
            $(this).maskMoney('mask', $(this).val());
        })
        $(parentId + ' .mask-percentage-4-2').attr('maxlength', 5);

        $(parentId + ' .mask-percentage-8-6').each(function () {
            $(this).maskMoney({ prefix: '', allowNegative: false, allowZero: true, thousands: '.', decimal: ',', affixesStay: false, precision: 6 });
            $(this).maskMoney('mask', $(this).val());
        })
        $(parentId + ' .mask-percentage-8-6').attr('maxlength', 9);

        $(parentId + ' .mask-number').bind("keypress", function (e) {
            var keyCode = e.which ? e.which : e.keyCode
            if (!(keyCode >= 48 && keyCode <= 57)) {
                return false;
            }
        });
        $(parentId + ' .mask-number').on('paste', function (event) {
            if (event.originalEvent.clipboardData.getData('Text').match(/[^\d]/)) {
                event.preventDefault();
            }
        });

        var currentTime = new Date();
        var startDateFirtDay = new Date(currentTime.getFullYear(), currentTime.getMonth(), 1);
        var tomorrow = moment(currentTime, 'DD/MM/YYYYY').add(1, 'days').format('DD/MM/YYYY');

        $(parentId + ' .mask-date').each(function () {
            if (!($(this).prop('readonly') || $(this).is(':disabled'))) {
                $(this).mask('99/99/9999');
                $(this).datepicker({
                    dateFormat: 'dd/mm/yy',
                    dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    nextText: 'Próximo',
                    prevText: 'Anterior'
                });
            }
        })

        $(parentId + ' .mask-date-minmax').each(function () {
            if (!($(this).prop('readonly') || $(this).is(':disabled'))) {
                var minDays = parseInt($(this).attr('data-mindays')) * -1;
                var maxDays = parseInt($(this).attr('data-maxdays'));
                $(this).mask('99/99/9999');
                $(this).datepicker({
                    dateFormat: 'dd/mm/yy',
                    dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    nextText: 'Próximo',
                    prevText: 'Anterior',
                    minDate: minDays,
                    maxDate: maxDays
                });
            }
        })

        $(parentId + ' .mask-dateToday').each(function () {
            if (!($(this).prop('readonly') || $(this).is(':disabled'))) {
                $(this).mask('99/99/9999');
                $(this).datepicker({
                    minDate: 0,
                    dateFormat: 'dd/mm/yy',
                    dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    nextText: 'Próximo',
                    prevText: 'Anterior'
                });
            }
        })

        $(parentId + ' .mask-dateFirstDayMonth').each(function () {
            if (!($(this).prop('readonly') || $(this).is(':disabled'))) {
                $(this).mask('99/99/9999');
                $(this).datepicker({
                    minDate: startDateFirtDay,
                    dateFormat: 'dd/mm/yy',
                    dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    nextText: 'Próximo',
                    prevText: 'Anterior'
                });
            }
        })

        $(parentId + ' .mask-dateTomorrow').each(function () {
            if (!($(this).prop('readonly') || $(this).is(':disabled'))) {
                $(this).mask('99/99/9999');
                $(this).datepicker({
                    minDate: tomorrow,
                    dateFormat: 'dd/mm/yy',
                    dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    nextText: 'Próximo',
                    prevText: 'Anterior'
                });
            }
        })

        // Somente CPF
        var cpfMaskBehavior = function (val) {
            return '000.000.000-00';
        }, cpfOptions = {
            onKeyPress: function (val, e, field, options) {
                field.mask(cpfMaskBehavior.apply({}, arguments), options);
            }
        };
        $(parentId + ' .mask-cpf').mask(cpfMaskBehavior, cpfOptions);

        // Somente CNPJ
        var cnpjMaskBehavior = function (val) {
            return '00.000.000/0000-99';
        }, cnpjOptions = {
            onKeyPress: function (val, e, field, options) {
                field.mask(cnpjMaskBehavior.apply({}, arguments), options);
            }
        };
        $(parentId + ' .mask-cnpj').mask(cnpjMaskBehavior, cnpjOptions);

        // Misto de CPF ou CNPJ
        var cpfCnpjMaskBehavior = function (val) {
            return val.replace(/\D/g, '').length <= 11 ? '000.000.000-009999' : '00.000.000/0000-99';
        }, cpfCnpjpOptions = {
            onKeyPress: function (val, e, field, options) {
                field.mask(cpfCnpjMaskBehavior.apply({}, arguments), options);
            }
        };
        $(parentId + ' .mask-cpfcnpj').mask(cpfCnpjMaskBehavior, cpfCnpjpOptions);

        $(parentId + ' .mask-cep').mask("99999-999");

        $(parentId + ' .mask-process-number').mask('0000000-00.0000.0.00.0000');

        $(parentId + ' .mask-telephone').mask('(00) 0000-00009');
        $(parentId + ' .mask-telephone').blur(function (event) {
            if ($(this).val().length == 15) { // Celular com 9 dígitos + 2 dígitos DDD e 4 da máscara
                $(this).mask('(00) 00000-0009');
            } else {
                $(this).mask('(00) 0000-00009');
            }
        });

        $(parentId + ' .mask-year-month').mask("9999/99");

    },

    removeInputMasks: function (parentId) {
        $(parentId + ' .mask-money-positive').each(function () {
            $(this).val($(this).maskMoney('unmasked')[0]);
            $(this).val($(this).val().replace('.', ','));
        });
        $(parentId + ' .mask-percentage-5-2').each(function () {
            $(this).val($(this).maskMoney('unmasked')[0]);
            $(this).val($(this).val().replace('.', ','));
        });
        $(parentId + ' .mask-percentage-4-2').each(function () {
            $(this).val($(this).maskMoney('unmasked')[0]);
            $(this).val($(this).val().replace('.', ','));
        });
        $(parentId + ' .mask-percentage-8-6').each(function () {
            $(this).val($(this).maskMoney('unmasked')[0]);
            $(this).val($(this).val().replace('.', ','));
        });
        $(parentId + ' .mask-cep').unmask();
        $(parentId + ' .mask-cpf').unmask();
        $(parentId + ' .mask-cnpj').unmask();
        $(parentId + ' .mask-cpfcnpj').unmask();
    },

};

var loading = {

    show: function (message) {
        waitingDialog.show(message, {
            headerSize: 5
        });
    },

    hide: function () {
        waitingDialog.hide();
    },

};

var messages = {

    info: function (title, message, okCallback) {
        loading.hide();
        $('#MsgBoxInfoModal .title').text(title);
        $('#MsgBoxInfoModal .message').text(message);
        $("#MsgBoxInfoModal .ok-button").off('click');
        if (okCallback && typeof (okCallback) === "function") {
            $("#MsgBoxInfoModal .ok-button").click(function () {
                $('#MsgBoxInfoModal').modal('hide');
                okCallback();
            });
        }
        $('#MsgBoxInfoModal').modal();
    },

    confirm: function (title, message, okCallback, cancelCallback) {
        $('#MsgBoxConfirmModal .title').text(title);
        $('#MsgBoxConfirmModal .message').text(message);
        $("#MsgBoxConfirmModal .ok-button").off('click');
        if (okCallback && typeof (okCallback) === "function") {
            $("#MsgBoxConfirmModal .ok-button").click(function () {
                $('#MsgBoxConfirmModal').modal('hide');
                okCallback();
            });
        }
        $("#MsgBoxConfirmModal .cancel-button").off('click');
        if (cancelCallback && typeof (cancelCallback) === "function") {
            $("#MsgBoxConfirmModal .cancel-button").click(function () {
                $('#MsgBoxConfirmModal').modal('hide');
                cancelCallback();
            });
        }
        $('#MsgBoxConfirmModal').modal();
    },

    error: function (message, okCallback) {
        $('#MsgBoxErrorModal .title').text('Erro de execução');
        $('#MsgBoxErrorModal .message').text(message);
        $("#MsgBoxErrorModal .ok-button").off('click');
        if (okCallback && typeof (okCallback) === "function") {
            $("#MsgBoxErrorModal .ok-button").click(function () {
                $('#MsgBoxErrorModal').modal('hide');
                okCallback();
            });
        }
        $('#MsgBoxErrorModal').modal();
    },

    text: function (title, text, okCallback) {
        loading.hide();
        $('#MsgBoxTextAreaModal .title').text(title);
        $('#MsgBoxTextAreaModal .text').text(text);
        $("#MsgBoxTextAreaModal .ok-button").off('click');
        if (okCallback && typeof (okCallback) === "function") {
            $("#MsgBoxTextAreaModal .ok-button").click(function () {
                $('#MsgBoxTextAreaModal').modal('hide');
                okCallback();
            });
        }
        $('#MsgBoxTextAreaModal').modal();
    },

    ajaxError: function (response) {
        var message = '';
        if (response) {
            if (response.responseJSON) {
                try {
                    message = response.responseJSON.message;
                } catch (err) {
                    message = response.responseJSON;
                }
            } else {
                if (response.responseText) {
                    try {
                        var json = JSON.parse(response.responseText);
                        message = json.message;
                    } catch (err) {
                        message = response.responseText;
                    }
                }
            }
        }
        messages.error(message);
    },

};

var validationrules = {

    init: function (formSelector) {
        $(formSelector).validate({
            debug: true,
            ignore: '*:not([name])', //Fixes your name issue
            rules: {},
        });

        $(formSelector + ' .required').rules('add', { required: true });
        $(formSelector + ' .parentrequired').rules('add', { parentrequired: true });
        $(formSelector + ' .elementrequired').rules('add', { elementrequired: true });
        $(formSelector + ' .checkboxrequired').rules('add', { checkboxrequired: true });

        $(formSelector + ' .elementequals').rules('add', { elementequals: true });

        $(formSelector + ' .numeric').rules('add', { number: true });

        $(formSelector + ' .numbermin').rules('add', { numbermin: true });
        $(formSelector + ' .numbermax').rules('add', { numbermax: true });
        $(formSelector + ' .numbergreaterthan').rules('add', { numbergreaterthan: true });

        $(formSelector + ' .decimalmin').rules('add', { decimalmin: true });
        $(formSelector + ' .decimalmax').rules('add', { decimalmax: true });
        $(formSelector + ' .decimalnotzero').rules('add', { decimalnotzero: true });

        $(formSelector + ' .datevalid').rules('add', { datevalid: true });
        $(formSelector + ' .datemin').rules('add', { datemin: true });
        $(formSelector + ' .datemax').rules('add', { datemax: true });

        $(formSelector + ' .stringmin').rules('add', { stringmin: true });

        $(formSelector + ' .cpfvalid').rules('add', { cpfvalid: true });
        $(formSelector + ' .cnpjvalid').rules('add', { cnpjvalid: true });
        $(formSelector + ' .cpfcnpjvalid').rules('add', { cpfcnpjvalid: true });

        $(formSelector + ' .lawsuitnumbervalid').rules('add', { lawsuitnumbervalid: true });

        $(formSelector + ' .emailvalid').rules('add', { emailvalid: true });

    },

    remove: function (selector) {
        $(selector).each(function () {
            $(this).rules('remove');
            $(this).removeClass("error");
        });
    },

    removeRule: function (selector, rule) {
        $(selector).each(function () {
            $(this).rules('remove', rule);
            $(this).removeClass(rule);
            $(this).removeClass("error");
        });
    },

    addRule: function (selector, rule) {
        $(selector).each(function () {
            $(this).rules('add', rule);
            $(this).addClass(rule);
        });
    },

};

var validations = {

    isValidEmail: function (email) {
        var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
        return pattern.test(email);
    },

    isValidFullName: function (fullName) {
        var pattern = new RegExp(/(^[^ ]+[ ]+[^ ].*$)/);
        return pattern.test(fullName);
    },

    isValidCnpj: function (cnpj) {
        cnpj = cnpj.replace(/[^\d]+/g, '');
        if (cnpj == '') return false;

        // Elimina CNPJs invalidos conhecidos
        if (cnpj.length != 14 ||
            cnpj == "00000000000000" || cnpj == "11111111111111" || cnpj == "22222222222222" ||
            cnpj == "33333333333333" || cnpj == "44444444444444" || cnpj == "55555555555555" ||
            cnpj == "66666666666666" || cnpj == "77777777777777" || cnpj == "88888888888888" || cnpj == "99999999999999") {
            return false;
        }

        // Valida DVs
        tamanho = cnpj.length - 2
        numeros = cnpj.substring(0, tamanho);
        digitos = cnpj.substring(tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0)) {
            return false;
        }

        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2) pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1)) {
            return false;
        }

        return true;
    },

    isValidCpf: function (cpf) {
        cpf = cpf.replace(/[^\d]+/g, '');
        if (cpf == '') return false;

        // Elimina CPFs invalidos conhecidos	
        if (cpf.length != 11 ||
            cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" ||
            cpf == "33333333333" || cpf == "44444444444" || cpf == "55555555555" ||
            cpf == "66666666666" || cpf == "77777777777" || cpf == "88888888888" || cpf == "99999999999") {
            return false;
        }

        // Valida 1o digito	
        add = 0;
        for (i = 0; i < 9; i++) {
            add += parseInt(cpf.charAt(i)) * (10 - i);
        }
        rev = 11 - (add % 11);
        if (rev == 10 || rev == 11) {
            rev = 0;
        }
        if (rev != parseInt(cpf.charAt(9))) {
            return false;
        }

        // Valida 2o digito	
        add = 0;
        for (i = 0; i < 10; i++) {
            add += parseInt(cpf.charAt(i)) * (11 - i);
        }
        rev = 11 - (add % 11);
        if (rev == 10 || rev == 11) {
            rev = 0;
        }
        if (rev != parseInt(cpf.charAt(10))) {
            return false;
        }

        return true;
    },

    validateInputField: function (fieldSelector, fieldTitle, validationCallback) {
        var required = $(fieldSelector).prop('required');
        var disabled = $(fieldSelector).prop('disabled');
        var fieldContents = $(fieldSelector).val();

        if (!disabled) {
            if (fieldContents.length == 0) {
                if (required) {
                    OpenInfoBox("Erro!", "Campo '" + fieldTitle + "' é de preenchimento obrigatório.", "$('" + fieldSelector + "').focus()");
                    return false;
                }
            } else {
                if (!validationCallback(fieldContents)) {
                    OpenInfoBox("Erro!", "Conteúdo do campo '" + fieldTitle + "' informado não é válido.", "$('" + fieldSelector + "').focus()");
                    return false;
                }
            }
        }

        return true;

    },

    isValidLawsuitNumber: function (lawsuitNumber) {
        // NNNNNNN-DD.AAAA.JTR.OOOO

        lawsuitNumber = lawsuitNumber.replace(/[^\d]+/g, '');
        if (lawsuitNumber == '') return false;
        if (lawsuitNumber.length > 20) return false;
        lawsuitNumber = '00000000000000000000' + lawsuitNumber;
        lawsuitNumber = lawsuitNumber.slice(lawsuitNumber.length - 20);

        var digitoEntrada = lawsuitNumber.substring(7, 9);

        var procJudNum = lawsuitNumber.substring(0, 7) + lawsuitNumber.substring(9);

        var procDig1 = procJudNum.slice(0, 1); b1 = eval(procDig1); b1 = b1 * 10000000000;
        var procDig2 = procJudNum.slice(1, 2); b2 = eval(procDig2); b2 = b2 * 1000000000;
        var procDig3 = procJudNum.slice(2, 3); b3 = eval(procDig3); b3 = b3 * 100000000;
        var procDig4 = procJudNum.slice(3, 4); b4 = eval(procDig4); b4 = b4 * 10000000;
        var procDig5 = procJudNum.slice(4, 5); b5 = eval(procDig5); b5 = b5 * 1000000;
        var procDig6 = procJudNum.slice(5, 6); b6 = eval(procDig6); b6 = b6 * 100000;
        var procDig7 = procJudNum.slice(6, 7); b7 = eval(procDig7); b7 = b7 * 10000;
        var procDig8 = procJudNum.slice(7, 8); b8 = eval(procDig8); b8 = b8 * 1000;
        var procDig9 = procJudNum.slice(8, 9); b9 = eval(procDig9); b9 = b9 * 100;
        var procDig10 = procJudNum.slice(9, 10); b10 = eval(procDig10); b10 = b10 * 10;
        var procDig11 = procJudNum.slice(10, 11); b11 = eval(procDig11);

        var proc1a11 = b1 + b2 + b3 + b4 + b5 + b6 + b7 + b8 + b9 + b10 + b11;
        var resto1 = proc1a11 % 97;

        var procDig12 = procJudNum.slice(11, 12); b12 = eval(procDig12); b12 = b12 * 1000000;
        var procDig13 = procJudNum.slice(12, 13); b13 = eval(procDig13); b13 = b13 * 100000;
        var procDig14 = procJudNum.slice(13, 14); b14 = eval(procDig14); b14 = b14 * 10000;
        var procDig15 = procJudNum.slice(14, 15); b15 = eval(procDig15); b15 = b15 * 1000;
        var procDig16 = procJudNum.slice(15, 16); b16 = eval(procDig16); b16 = b16 * 100;
        var procDig17 = procJudNum.slice(16, 17); b17 = eval(procDig17); b17 = b17 * 10;
        var procDig18 = procJudNum.slice(17); b18 = eval(procDig18);

        var proc12a18 = b12 + b13 + b14 + b15 + b16 + b17 + b18;
        var resto2 = (resto1 * 10000000 * 100 + proc12a18 * 100) % 97;

        var digitoCalculado = 98 - resto2;

        return (parseInt(digitoEntrada) == parseInt(digitoCalculado));
    },

};

var controls = {

    dataTableInit: function (selector) {
        if (!$.fn.DataTable.isDataTable(selector)) {
            $(selector).DataTable({
                "ordering": false,
                language: {
                    url: '/resources/datatables/pt_BR.json'
                }
            });
        }

    },

    dataTable2Init: function (selector) {
        commons.dataTableInit3(selector, 5);
    },

    dataTableInit3: function (selector, pageSize) {
        if (!$.fn.DataTable.isDataTable(selector)) {
            $(selector).DataTable({
                "ordering": false,
                "searching": false,
                "lengthChange": false,
                "pageLength": pageSize,
                language: {
                    url: '/resources/datatables/pt_BR.json'
                }
            });
        }
    },

};

var file = {

    download: function (postUrl, postData, waitingMessage, fileType) {
        $.ajax({
            url: '/Common/KeepAlive',
            type: 'GET',
            dataType: 'json',
            data: {}
        }).done(function (data) {
            file.get(postUrl, postData, waitingMessage, fileType, false, false);
        }).fail(function (jqXHR, textStatus) {
        });
    },

    open: function (postUrl, postData, waitingMessage, fileType) {
        $.ajax({
            url: '/Common/KeepAlive',
            type: 'GET',
            dataType: 'json',
            data: {}
        }).done(function (data) {
            file.get(postUrl, postData, waitingMessage, fileType, true, false);
        }).fail(function (jqXHR, textStatus) {
        });
    },

    print: function (postUrl, postData, waitingMessage, fileType) {
        $.ajax({
            url: '/Common/KeepAlive',
            type: 'GET',
            dataType: 'json',
            data: {}
        }).done(function (data) {
            file.get(postUrl, postData, waitingMessage, fileType, true, true);
        }).fail(function (jqXHR, textStatus) {
        });
    },

    get: function (postUrl, postData, waitingMessage, fileType, openInNewWindow, openPrintPreview) {
        $.ajax({
            type: "POST",
            url: postUrl,
            data: postData,
            beforeSend: function (hrx) {
                loading.show(waitingMessage);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                loading.hide();
                messages.error('Impressão não disponível no momento.');
            },
            xhr: function () {// Seems like the only way to get access to the xhr object
                var xhr = new XMLHttpRequest();
                xhr.responseType = 'blob'
                return xhr;
            },
            xhrFields: {
                responseType: 'blob'
            },
            success: function (response, status, xhr) {
                loading.hide();

                // check for a filename
                var filename = "";
                var disposition = xhr.getResponseHeader('Content-Disposition');
                if (disposition && disposition.indexOf('attachment') !== -1) {
                    var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                    var matches = filenameRegex.exec(disposition);
                    if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
                }

                var blob = new Blob([response], { type: fileType });

                if (typeof window.navigator.msSaveBlob !== 'undefined') {
                    // IE workaround for "HTML7007: One or more blob URLs were revoked by closing the blob for which they were created. These URLs will no longer resolve as the data backing the URL has been freed."
                    window.navigator.msSaveBlob(blob, filename);
                } else {
                    var URL = window.URL || window.webkitURL;
                    var downloadUrl = URL.createObjectURL(blob);
                    if (openInNewWindow) {
                        var parentOpener = window.opener;
                        window.opener = null;
                        var windowFeatures = "menubar=yes,location=yes,resizable=yes,scrollbars=yes,status=yes";
                        var newWindow = window.open(downloadUrl, filename, windowFeatures);
                        newWindow.opener = parentOpener;
                        if (newWindow) {
                            newWindow.focus()
                        }
                        if (openPrintPreview) {
                            newWindow.print();
                        }
                    } else {
                        if (filename) {
                            // use HTML5 a[download] attribute to specify filename
                            var a = document.createElement("a");
                            // safari doesn't support this yet
                            if (typeof a.download === 'undefined') {
                                window.location = downloadUrl;
                            } else {
                                a.href = downloadUrl;
                                a.download = filename;
                                document.body.appendChild(a);
                                a.click();
                            }
                        } else {
                            window.location = downloadUrl;
                        }
                    }
                    setTimeout(function () { URL.revokeObjectURL(downloadUrl); }, 100); // cleanup
                }
            }
        });
    }

};

var rangeUtil = {

    initializeTermDate: function (startTermSelector, daysOfTermSelector, endTermSelector) {

        $(startTermSelector).change(function () {
            if (!($(daysOfTermSelector).val() == "" && $(endTermSelector).val() == "" || $(startTermSelector).val() == "")) {
                if ($(daysOfTermSelector).val() != "" && $(endTermSelector).val() == "") {
                    var startTerm = moment($(startTermSelector).val(), 'DD/MM/YYYY');
                    var daysOfTerm = Number($(daysOfTermSelector).val());
                    var newDate = startTerm.add(daysOfTerm, "days").format('DD/MM/YYYY');
                    $(endTermSelector).datepicker('setDate', newDate);
                } else {
                    var startTerm = moment($(startTermSelector).val(), 'DD/MM/YYYY');
                    var endTerm = moment($(endTermSelector).val(), 'DD/MM/YYYY');
                    var differenceDays = endTerm.diff(startTerm, 'days');
                    if (differenceDays < 0) {
                        $(endTermSelector).val($(startTermSelector).val());
                        differenceDays = 0;
                    }
                    $(daysOfTermSelector).val(differenceDays);
                }
            }
        });

        $(daysOfTermSelector).change(function () {
            var tomorrow = moment().add(1, "days").format('DD/MM/YYYY');
            if (!($(startTermSelector).val() == "" && $(endTermSelector).val() == "" || $(startTermSelector).val() == "")) {
                if ($(startTermSelector).val() == "" && $(endTermSelector).val() != "") {
                    var endTerm = moment($(endTermSelector).val(), 'DD/MM/YYYY');
                    var daysOfTerm = Number($(daysOfTermSelector).val());
                    var newDate = endTerm.subtract(daysOfTerm, "days").format('DD/MM/YYYY');

                    if (moment(newDate, 'DD/MM/YYYY').isSameOrAfter(moment(tomorrow, 'DD/MM/YYYY')))
                        $(startTermSelector).datepicker('setDate', newDate);
                    else {
                        var endData = moment($(endTermSelector).val(), 'DD/MM/YYYY');
                        var startDate = moment(tomorrow, 'DD/MM/YYYY');
                        var Diferenca = endData.diff(startDate, "days");
                        $(daysOfTermSelector).val(Diferenca);
                        $(startTermSelector).datepicker('setDate', tomorrow);
                    }
                } else {
                    var startTerm = moment($(startTermSelector).val(), 'DD/MM/YYYY');
                    var daysOfTerm = Number($(daysOfTermSelector).val());
                    var newDate = startTerm.add(daysOfTerm, "days").format('DD/MM/YYYY');
                    $(endTermSelector).datepicker('setDate', newDate);
                }
            }
        });

        $(endTermSelector).change(function () {
            var tomorrow = moment().add(1, "days").format('DD/MM/YYYY');
            if (!($(startTermSelector).val() == "" && $(daysOfTermSelector).val() == "" || $(endTermSelector).val() == "")) {
                if ($(startTermSelector).val() == "" && $(daysOfTermSelector).val() != "") {
                    var endTerm = moment($(endTermSelector).val(), 'DD/MM/YYYY');
                    var daysOfTerm = Number($(daysOfTermSelector).val());
                    var newDate = endTerm.subtract(daysOfTerm, "days").format('DD/MM/YYYY');

                    if (moment(newDate, 'DD/MM/YYYY').isSameOrAfter(moment(tomorrow, 'DD/MM/YYYY')))
                        $(startTermSelector).datepicker('setDate', newDate);
                    else {
                        var endData = moment($(endTermSelector).val(), 'DD/MM/YYYY');
                        var startDate = moment(tomorrow, 'DD/MM/YYYY');
                        var differenceDays = endData.diff(startDate, "days");
                        $(daysOfTermSelector).val(differenceDays);
                        $(startTermSelector).datepicker('setDate', tomorrow);
                    }
                }
                else {
                    var startTerm = moment($(startTermSelector).val(), 'DD/MM/YYYY');
                    var endTerm = moment($(endTermSelector).val(), 'DD/MM/YYYY');
                    var differenceDays = endTerm.diff(startTerm, 'days');
                    if (differenceDays < 0) {
                        $(startTermSelector).val($(endTermSelector).val());
                        differenceDays = 0;
                    }
                    $(daysOfTermSelector).val(differenceDays);
                }
            }
        });

    },

    initializeDate: function (beginSelector, endSelector) {

        $(beginSelector).change(function () {
            if ($(beginSelector).val() != '' && $(endSelector).val() != '') {
                var beginDate = moment($(beginSelector).val(), 'DD/MM/YYYY');
                var endDate = moment($(endSelector).val(), 'DD/MM/YYYY');
                var differenceDays = endDate.diff(beginDate, 'days');
                if (differenceDays < 0) {
                    $(endSelector).val($(beginSelector).val());
                }
            } else if ($(endSelector).val() == '') {
                $(endSelector).val($(beginSelector).val());
            }
        });

        $(endSelector).change(function () {
            if ($(beginSelector).val() != '' && $(endSelector).val() != '') {
                var beginDate = moment($(beginSelector).val(), 'DD/MM/YYYY');
                var endDate = moment($(endSelector).val(), 'DD/MM/YYYY');
                var differenceDays = endDate.diff(beginDate, 'days');
                if (differenceDays < 0) {
                    $(beginSelector).val($(endSelector).val());
                }
            } else if ($(beginSelector).val() == '') {
                $(beginSelector).val($(endSelector).val());
            }
        });

    },

    initializeDecimal: function (beginSelector, endSelector) {

        $(beginSelector).change(function () {
            if ($(beginSelector).val() != '' && $(endSelector).val() != '') {
                var beginValue = convert.getDecimal($(beginSelector).val(), 0);
                var endValue = convert.getDecimal($(endSelector).val(), 0);
                if (beginValue > endValue) {
                    $(endSelector).val($(beginSelector).val());
                }
            } else if ($(endSelector).val() == '') {
                $(endSelector).val($(beginSelector).val());
            }
        });

        $(endSelector).change(function () {
            if ($(beginSelector).val() != '' && $(endSelector).val() != '') {
                var beginValue = convert.getDecimal($(beginSelector).val(), 0);
                var endValue = convert.getDecimal($(endSelector).val(), 0);
                if (endValue < beginValue) {
                    $(beginSelector).val($(endSelector).val());
                }
            } else if ($(beginSelector).val() == '') {
                $(beginSelector).val($(endSelector).val());
            }
        });

    },

    terminateDecimal: function (beginSelector, endSelector) {
        $(beginSelector).off('change');
        $(endSelector).off('change');
    },

};
