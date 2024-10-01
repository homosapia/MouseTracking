$(document).ready(function () {
    let mouseData = [];
    let lastPosition = null;
    let lastTime = null;

    $(document).on('mousemove', function (event) {
        let currentTime = new Date().getTime();
        let x = event.pageX;
        let y = event.pageY;

        if (lastPosition && lastTime) {
            let timeDiff = currentTime - lastTime;
            mouseData.push([x, y, timeDiff]);
        }

        lastPosition = [x, y];
        lastTime = currentTime;
    });

    $('#sendDataButton').on('click', function () {
        $.ajax({
            url: '/save',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({mouseMovementData: mouseData }),
            success: function (response) {
                console.log('Data sent successfully:', response);
            },
            error: function (error) {
                console.error('Error sending data:', error);
            }
        });
    });
});