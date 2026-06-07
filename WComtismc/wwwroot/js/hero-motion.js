(function () {
    var hero = document.getElementById('heroSection');
    if (!hero) return;

    var slides = Array.from(hero.querySelectorAll('[data-hero-slide]'));
    var dots = Array.from(hero.querySelectorAll('[data-hero-target]'));
    var current = 0;
    var timer = null;
    var intervalMs = 7000;

    function playSlideVideo(slide) {
        var video = slide.querySelector('.hero-video');
        if (!video) return;
        video.currentTime = 0;
        var playPromise = video.play();
        if (playPromise && playPromise.catch) {
            playPromise.catch(function () { });
        }
    }

    function pauseSlideVideo(slide) {
        var video = slide.querySelector('.hero-video');
        if (video) video.pause();
    }

    function setSlide(index) {
        if (!slides.length) return;
        current = (index + slides.length) % slides.length;

        slides.forEach(function (slide, i) {
            slide.classList.toggle('is-active', i === current);
            if (i === current) playSlideVideo(slide);
            else pauseSlideVideo(slide);
        });

        dots.forEach(function (dot) {
            var target = parseInt(dot.getAttribute('data-hero-target'), 10);
            dot.classList.toggle('is-active', target === current);
        });
    }

    function nextSlide() {
        setSlide(current + 1);
    }

    function startAuto() {
        stopAuto();
        timer = setInterval(nextSlide, intervalMs);
    }

    function stopAuto() {
        if (timer) clearInterval(timer);
    }

    dots.forEach(function (dot) {
        dot.addEventListener('click', function () {
            var target = parseInt(dot.getAttribute('data-hero-target'), 10);
            setSlide(target);
            startAuto();
        });
    });

    hero.querySelectorAll('.motion-thumb video').forEach(function (video) {
        video.play().catch(function () { });
    });

    setSlide(0);
    startAuto();

    hero.addEventListener('mouseenter', stopAuto);
    hero.addEventListener('mouseleave', startAuto);
})();
