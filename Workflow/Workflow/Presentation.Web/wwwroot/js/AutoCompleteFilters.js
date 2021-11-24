var AutoCompleteFilters = {

    brokerInit: function (selector, parentSelector, selectCallback, unselectCallback) {
        $(selector).autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: 'POST',
                    url: '/Common/ListBrokers',
                    data: {
                        'name': request.term
                    },
                    dataType: "json",
                    success: function (data) {
                        response($.map(data.brokers, function (item) {
                            return {
                                label: format.formatCpfCnpj(item.cpfCnpjNumber) + ' - ' + item.name,
                                val: JSON.stringify(item)
                            }
                        }))
                    },
                    error: function (response) {
                        messages.error(commons.getErrorMessage(response));
                    },
                    failure: function (response) {
                        messages.error(commons.getErrorMessage(response));
                    }
                });
            },
            select: function (e, i) {
                var item = JSON.parse(i.item.val);
                selectCallback(item);
            },
            change: function (e, i) {
                if (!i.item) {
                    unselectCallback();
                }
            },
            create: function (event, ui) {
                $(this).attr('autocomplete', 'nope');
            },
            minLength: 3,
            appendTo: parentSelector
        });
    },

    takerInit: function (selector, parentSelector, brokerUserIdSelector, selectCallback, unselectCallback) {
        $(selector).autocomplete({
            source: function (request, response) {
                var brokerUserId = $(brokerUserIdSelector).val();
                if (brokerUserId == '') {
                    messages.error('Selecione o produtor antes de prosseguir.');
                    $(selector).val('');
                    return false;
                }
                $.ajax({
                    type: 'POST',
                    url: '/Common/ListTakers',
                    data: {
                        'name': request.term,
                        'brokerId': brokerUserId
                    },
                    dataType: "json",
                    success: function (data) {
                        response($.map(data.takers, function (item) {
                            return {
                                label: format.formatCpfCnpj(item.cpfCnpjNumber) + ' - ' + item.name,
                                val: JSON.stringify(item)
                            }
                        }))
                    },
                    error: function (response) {
                        messages.error(commons.getErrorMessage(response));
                    },
                    failure: function (response) {
                        messages.error(commons.getErrorMessage(response));
                    }
                });
            },
            select: function (e, i) {
                var item = JSON.parse(i.item.val);
                selectCallback(item);
            },
            change: function (e, i) {
                if (!i.item) {
                    unselectCallback();
                }
            },
            create: function (event, ui) {
                $(this).attr('autocomplete', 'nope');
            },
            minLength: 3,
            appendTo: parentSelector
        });
    },

    insuredInit: function (selector, parentSelector, selectCallback, unselectCallback) {
        $(selector).autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: 'POST',
                    url: '/Common/ListInsureds',
                    data: {
                        'name': request.term
                    },
                    dataType: "json",
                    success: function (data) {
                        response($.map(data.insureds, function (item) {
                            return {
                                label: format.formatCpfCnpj(item.cpfCnpjNumber) + ' - ' + item.name,
                                val: JSON.stringify(item)
                            }
                        }))
                    },
                    error: function (response) {
                        messages.error(commons.getErrorMessage(response));
                    },
                    failure: function (response) {
                        messages.error(commons.getErrorMessage(response));
                    }
                });
            },
            select: function (e, i) {
                var item = JSON.parse(i.item.val);
                selectCallback(item);
            },
            change: function (e, i) {
                if (!i.item) {
                    unselectCallback();
                }
            },
            create: function (event, ui) {
                $(this).attr('autocomplete', 'nope');
            },
            minLength: 3,
            appendTo: parentSelector
        });
    },

}