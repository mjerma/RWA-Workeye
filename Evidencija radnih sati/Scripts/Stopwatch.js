let startTime;
let timerInterval;
let elapsedTimes = new Array($('tbody tr').length);

function timeToString(time) {
    let diffInHrs = time / 3600000;
    let hh = Math.floor(diffInHrs);

    let diffInMin = (diffInHrs - hh) * 60;
    let mm = Math.floor(diffInMin);

    let diffInSec = (diffInMin - mm) * 60;
    let ss = Math.floor(diffInSec);

    let formattedHH = hh.toString().padStart(2, "0");
    let formattedMM = mm.toString().padStart(2, "0");
    let formattedSS = ss.toString().padStart(2, "0");

    return `${formattedHH}:${formattedMM}:${formattedSS}`;
}

function print(txt, htmlElement) {
    $(htmlElement).val(txt);
}

for (var i = 0; i < elapsedTimes.length; i++) {
    elapsedTimes[i] = 0;
}

function start() {
    let row = $(this).closest('tr');
    let htmlElement = $(row.find('.zabiljezeno'));
    let rowIndex = row.index();

    startTime = Date.now() - elapsedTimes[rowIndex];
    timerInterval = setInterval(function printTime() {
        elapsedTimes[rowIndex] = Date.now() - startTime;
        print(timeToString(elapsedTimes[rowIndex]), htmlElement);
    }, 10);
    $('.btn-start').prop('disabled', true);
    $(row.find('.btn-stop')).prop('disabled', false);
}

function stop() {
    clearInterval(timerInterval);
    $('.btn-start').prop('disabled', false);
    $(this).prop('disabled', true);
}

function calculateTotal() {
    let radniSatiUkupno = 0;
    let prekovremeniSatiUkupno = 0;

    if ($('#IDSatnica').length == 0) {
        let zabiljezenoUkupno = 0;
        elapsedTimes.forEach(el => zabiljezenoUkupno += el);

        $('#zabiljezenoUkupno').val(timeToString(zabiljezenoUkupno));
    }

    $('.radniSati').each(function (i, element) {
        radniSatiUkupno += parseInt($(element).val()) || 0;
    });

    $('#radniSatiUkupno').val(radniSatiUkupno);

    $('.prekovremeniSati').each(function (i, element) {
        prekovremeniSatiUkupno += parseInt($(element).val()) || 0;
    });

    $('#prekovremeniSatiUkupno').val(prekovremeniSatiUkupno);

}

$('#btnSpremi').click(calculateTotal);
$('.btn-start').click(start);
$('.btn-stop').click(stop);