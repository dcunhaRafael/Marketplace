
$.validator.addMethod('parentrequired', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        var parentSelector = $(element).attr('data-parent');
        var target = $(parentSelector).val();
        if (target == '' || target == '0') {
            $.validator.messages['parentrequired'] = 'Este campo é requerido.';
            return false;
        }
    }
    return true;
}, '');

$.validator.addMethod('parentequals', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        var parentSelector = $(element).attr('data-parent');
        var target = $(parentSelector).val();
        if (value != target) {
            var errorMessage = $(element).attr('data-parentequals');
            $.validator.messages['parentequals'] = errorMessage;
            return false;
        }
    }
    return true;
}, '');

$.validator.addMethod('elementrequired', function (value, element) {
    if (value == '' || value == '0') {
        var errorMessage = $(element).attr('data-elementrequired');
        $.validator.messages['elementrequired'] = errorMessage;
        return false;
    }
    return true;
}, '');

$.validator.addMethod('ckeckboxgrouprequired', function (value, element) {
    var checked = 0;
    var groupName = $(element).attr('data-checkboxgroupname');
    $('input[data-checkboxgroupname="' + groupName + '"]').each(function () {
        if ($(this).prop('checked') == true) {
            checked++;
        }
    });
    if (checked == 0) {
        $.validator.messages['ckeckboxgrouprequired'] = 'Selecione ao menos uma opção da lista.';
        return false;
    }
    return true;
}, '');

$.validator.addMethod('checkboxrequired', function (value, element) {
    var checked = $(element).prop('checked');
    if (!checked) {
        var errorMessage = $(element).attr('data-checkboxrequired-message');
        $.validator.messages['checkboxrequired'] = errorMessage;
        return false;
    }
    return true;
}, '');

$.validator.addMethod('elementequals', function (value, element) {
    var selector = $(element).attr('data-elementequals-selector');
    var equalsvalue = $(element).attr('data-elementequals-equalsvalue');
    alert($(selector).val());
    alert(equalsvalue);
    if ($(selector).val() != equalsvalue) {
        var errorMessage = $(element).attr('data-elementequals-message');
        $.validator.messages['elementequals'] = errorMessage;
        return false;
    }
    return true;
}, '');

$.validator.addMethod('hiddengroupvalue', function (value, element) {
    var checked = 0;
    var groupSelector = $(element).attr('data-hiddengroupvalue-selector');
    var expectedValue = $(element).attr('data-hiddengroupvalue-expectedvalue');
    $(groupSelector).each(function () {
        if ($(this).val() == expectedValue) {
            checked++;
        }
    });
    if (checked == 0) {
        var errorMessage = $(element).attr('data-hiddengroupvalue-message');
        $.validator.messages['hiddengroupvalue'] = errorMessage;
        return false;
    }
    return true;
}, '');

$.validator.addMethod('datevalid', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        if (!moment(value, "DD/MM/YYYY").isValid()) {
            $.validator.messages['datevalid'] = 'Data inválida (Utiize o formato DD/MM/AAAA).';
            return false;
        }
    }
    return true;
}, '');

$.validator.addMethod('datemin', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        var currentDate = convert.getDate(value, 'dd/mm/yyyy', '/');
        var minDate = convert.getDate($(element).attr('data-datemin'), 'yyyy-mm-dd', '-');
        if (currentDate < minDate) {
            $.validator.messages['datemin'] = 'Data não pode ser inferior a ' + moment(minDate).format('DD/MM/YYYY') + '.';
            return false;
        }
    }
    return true;
}, '');

$.validator.addMethod('datemax', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        var currentDate = convert.getDate(value, 'dd/mm/yyyy', '/');
        var maxDate = convert.getDate($(element).attr('data-datemax'), 'yyyy-mm-dd', '-');
        if (currentDate > maxDate) {
            $.validator.messages['datemax'] = 'Data não pode ser superior a ' + moment(maxDate).format('DD/MM/YYYY') + '.';
            return false;
        }
    }
    return true;
}, '');

$.validator.addMethod('numbermin', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        var minValue = parseInt($(element).attr('data-numbermin'));
        var thisValue = parseInt(value);
        if (parseInt(thisValue) < parseInt(minValue)) {
            $.validator.messages['numbermin'] = 'Valor não pode ser inferior a ' + minValue + '.';
            return false;
        }
    }
    return true;
}, '');

$.validator.addMethod('numbermax', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        var maxValue = parseInt($(element).attr('data-numbermax'));
        var thisValue = parseInt(value);
        if (parseInt(thisValue) > parseInt(maxValue)) {
            $.validator.messages['numbermax'] = 'Valor não pode ser superior a ' + maxValue + '.';
            return false;
        }
    }
    return true;
}, '');

$.validator.addMethod("numbergreaterthan", function (value, element) {
    var otherElement = $(element).attr('data-numbergreaterthan-selector');
    if (!(parseInt(value) > parseInt($(otherElement).val()))) {
        var errorMessage = $(element).attr('data-numbergreaterthan-error');
        $.validator.messages["numbergreaterthan"] = errorMessage;
        return false;
    }
    return true;
}, '');

$.validator.addMethod('decimalmin', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        var minValue = convert.getDecimal($(element).attr('data-decimalmin'));
        var thisValue = convert.getDecimal(value);
        if (parseFloat(thisValue) < parseFloat(minValue)) {
            $.validator.messages['decimalmin'] = 'Valor não pode ser inferior a ' + $(element).attr('data-decimalmin') + '.';
            return false;
        }
    }
    return true;
}, '');

$.validator.addMethod('decimalmax', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        var maxValue = convert.getDecimal($(element).attr('data-decimalmax'));
        var thisValue = convert.getDecimal(value);
        if (parseFloat(thisValue) > parseFloat(maxValue)) {
            $.validator.messages['decimalmax'] = 'Valor não pode ser superior a ' + $(element).attr('data-decimalmax') + '.';
            return false;
        }
    }
    return true;
}, '');

$.validator.addMethod('decimalnotzero', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        var maxValue = 0;
        var thisValue = convert.getDecimal(value);
        if (parseFloat(thisValue) == parseFloat(maxValue)) {
            $.validator.messages['decimalnotzero'] = 'Valor não pode ser zero.';
            return false;
        }
    }
    return true;
}, '');


$.validator.addMethod('cpfvalid', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        if (value.length > 0) {
            var cpf = value.replace(/\D/g, '');
            if (!validations.isValidCpf(cpf)) {
                $.validator.messages['cpfvalid'] = 'CPF informado não é válido.';
                return false;
            }
        }
    }
    return true;
}, '');

$.validator.addMethod('cnpjvalid', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        if (value.length > 0) {
            var cnpj = value.replace(/\D/g, '');
            if (!validations.isValidCnpj(cnpj)) {
                $.validator.messages['cnpjvalid'] = 'CNPJ informado não é válido.';
                return false;
            }
        }
    }
    return true;
}, '');

$.validator.addMethod('cpfcnpjvalid', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        if (value.length > 0) {
            var cpfCnpj = value.replace(/\D/g, '');
            if (cpfCnpj.length > 11) {    // Maior que 11 sempre CNPJ
                if (!validations.isValidCnpj(cpfCnpj)) {
                    $.validator.messages['cpfcnpjvalid'] = 'CNPJ informado não é válido.';
                    return false;
                }
            } else {
                if (!validations.isValidCpf(cpfCnpj)) {    // Menor ou igual a 11 primeiro testa como CPF, depois como CNPJ
                    if (!validations.isValidCnpj(cpfCnpj)) {
                        $.validator.messages['cpfcnpjvalid'] = 'CPF/CNPJ informado não é válido.';
                        return false;
                    }
                }
            }
        }
    }
    return true;
}, '');

$.validator.addMethod('emailvalid', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        if (value.length > 0) {
            if (!validations.isValidEmail(value)) {
                $.validator.messages['emailvalid'] = 'E-mail informado não é válido.';
                return false;
            }
        }
    }
    return true;
}, '');

$.validator.addMethod('lawsuitnumbervalid', function (value, element) {
    var disabled = $(element).prop('disabled');
    var readonly = $(element).prop('readonly');
    if (!disabled && !readonly) {
        if (value.length > 0) {
            if (!validations.isValidLawsuitNumber(value)) {
                $.validator.messages['lawsuitnumbervalid'] = 'Número de processo informado não é válido.';
                return false;
            }
        }
    }
    return true;
}, '');

$.validator.addMethod("passwordvalid", function (value, element) {
    return /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,16}$/.test(value);
}, "Senhas devem possuir entre 8 e 16 caracteres, contendo letras maiúsculas, mínusculas, caracteres especiais e números."); 

$.fn.clearValidation = function () {
    //Internal $.validator is exposed through $(form).validate()
    var validator = $(this).validate();
    //Iterate through named elements inside of the form, and mark them as error free
    $('[name]', this).each(function () {
        validator.successList.push(this);//mark as error free
        validator.showErrors();//remove error messages if present
    });
    validator.resetForm();//remove error class on name elements and clear history
    validator.reset();//remove all error and success data
};

