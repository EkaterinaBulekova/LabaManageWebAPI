﻿function TagsAjax(Tagsurl, text) {
    var tx = (text == undefined) ? Tagsurl : Tagsurl + "?text="+text
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: tx,
        success: function (data) {
            $('#tokenfield').tokenfield({
                autocomplete: {
                    source: data,
                    delay: 100
                },
                showAutocompleteOnFocus: false
            })
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 500) {
                alert('Internal error: ' + jqXHR.responseText);
            } else {
                alert('Unexpected error.' + jqXHR.responseText);
            }
        }
    });
}

$('#tokenfield').on('tokenfield:createdtoken', function (event) {
    var existingTokens = $(this).tokenfield('getTokens');
    var count = 0;
    $.each(existingTokens, function (index, token) {
        if (token.value === event.attrs.value)
            count++;
    });
    if (count > 1) {
        existingTokens.pop();
        var newTokens = existingTokens;
    }
    $(this).tokenfield('setTokens', newTokens);
});
