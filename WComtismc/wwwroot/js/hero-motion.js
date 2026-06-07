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

        video.muted = true;
        video.playsInline = true;

        var attemptPlay = function () {
            video.currentTime = 0;
            var playPromise = video.play();
            if (playPromise && playPromise.catch) {
                playPromise.catch(function () {
                    slide.classList.add('use-poster');
                });
            }
        };

        if (video.readyState >= 2) {
            attemptPlay();
        } else {
            video.addEventListener('loadeddata', attemptPlay, { once: true });
            video.load();
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
            var isActive = i === current;
            slide.classList.toggle('is-active', isActive);
            slide.classList.remove('use-poster');

            if (isActive) playSlideVideo(slide);
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
        video.muted = true;
        video.play().catch(function () { });
    });

    setSlide(0);
    startAuto();

    hero.addEventListener('mouseenter', stopAuto);
    hero.addEventListener('mouseleave', startAuto);
})();
