$(document).ready(() => {
    function UpdateTable() {
        $.ajax({
            type: 'POST',
            url: '/Redactor/UpdateTopRequestsTable',
            async: false,
            success: function (result) {
                let table = "<tr class='table-head'>" + $('.table-head').html() + "</tr>";
                for (let i = 0; i < result.length; i++) {
                    let row = "<tr>"
                    row += "<th>" + result[i].number + "</th>";
                    row += "<th>" + result[i].userName + "</th>";
                    row += "<th>" + result[i].queriesCount + "</th>";
                    row += "<th>" + result[i].lastLoginTime + "</th>";
                    row += "<th>" + result[i].averageTimeBetweenQueriesInMinutes + "</th>";
                    row += "</tr>"
                    table += row;
                }
                $('#top-requests-table').html(table);
            }
        });
    }
    UpdateTable();
    $('#btn-suc').click(() => {
        var correct_words = [];
        let words = $('textarea').val().toString().replace(/[!"#$%&'()*+,-./:;<=>?[\]^_`{|}~0123456789]/g, " ").split(" ");
        for (let i = 0; i < words.length; i++) {
            if (words[i].length >= 4) {
                correct_words.push(words[i]);
            }
        }
        $.ajax({
            type: 'POST',
            data: { words: correct_words },
            url: '/Redactor/DetectLanguages',
            async: false,
            success: function (result) {
                if (result.status != "false") {
                    let correct_words_view = "";
                    for (let i = 0; i < result.length; i++) {
                        let languages_list = "<ul>";
                        for (let j = 0; j < result[i].languages.length; j++) {
                            languages_list += "<li>" + result[i].languages[j].language + " - " + result[i].languages[j].confidence + "%</li>";
                        }
                        languages_list += "</ul>"
                        correct_words_view += ("<span class='word'>" + result[i].text + " <span class='popup' style='display: none'>" + languages_list + "</span></span>");
                    }
                    $('#correct_text').html(correct_words_view);
                }
            }
        });
        UpdateTable();
    });
    $("#correct_text").delegate('.word', 'mouseenter', function () {
        $(this).children('.popup').toggle('fast');
    });
    $("#correct_text").delegate('.word', 'mouseleave', function () {
        $(this).children('.popup').toggle('fast');
    });
});