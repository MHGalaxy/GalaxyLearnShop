function popUpNotification(title, message, position, duration, type) {
    var title = title;
    var message = message;
    var position = position;
    var duration = duration;
    var type = type;
    var callback = null;

    if (type == 'dialog') {
        callback = (result) => {
            console.log('result = ', result);
        };
    }

    const popup = Notification({
        position: position,
        duration: duration,
    });

    if (!popup[type]) {
        popup.error({
            title: 'Error',
            message: `Notification has no such method "${type}"`,
        });
        return;
    }

    popup[type]({
        title: title,
        message: message,
        callback: callback,
    });
}