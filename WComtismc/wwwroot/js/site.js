document.querySelectorAll('[data-scroll-row]').forEach(function (row) {
    var isDown = false;
    var startX;
    var scrollLeft;

    row.addEventListener('mousedown', function (e) {
        isDown = true;
        startX = e.pageX - row.offsetLeft;
        scrollLeft = row.scrollLeft;
        row.classList.add('is-dragging');
    });

    row.addEventListener('mouseleave', function () {
        isDown = false;
        row.classList.remove('is-dragging');
    });

    row.addEventListener('mouseup', function () {
        isDown = false;
        row.classList.remove('is-dragging');
    });

    row.addEventListener('mousemove', function (e) {
        if (!isDown) return;
        e.preventDefault();
        var x = e.pageX - row.offsetLeft;
        var walk = (x - startX) * 1.5;
        row.scrollLeft = scrollLeft - walk;
    });
});
