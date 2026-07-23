/**
 * DR ASHRAF LAROUSSI MELLOULI - Interactive & Performance Scripts
 */

document.addEventListener("DOMContentLoaded", () => {
    const nav = document.querySelector('.nav-main');

    // --- Mobile Hamburger Menu ---
    const mobileMenuBtn = document.getElementById('mobileMenuToggle');
    if (mobileMenuBtn) {
        mobileMenuBtn.addEventListener('click', () => {
            nav.classList.toggle('mobile-active');
            document.body.style.overflowY = nav.classList.contains('mobile-active') ? 'hidden' : '';
        });
    }

    // --- 1. Scrolled Navigation Effect ---
    const handleScroll = () => {
        // Header visibility
        if (window.scrollY > 50) {
            nav.classList.add('scrolled');
        } else {
            nav.classList.remove('scrolled');
        }
    };

    window.addEventListener('scroll', handleScroll);
    handleScroll(); // Initial check

    // --- 2. Smooth Scroll for Anchor Links ---
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth'
                });
            }
        });
    });

    // --- 4. Radiant Fluidity Interaction (Light Inertia) ---
    const orbs = document.querySelectorAll('.light-orb');
    const lensFlare = document.querySelector('.lens-flare');

    let mouseX = 0, mouseY = 0;
    let targetX = 0, targetY = 0;

    document.addEventListener('mousemove', (e) => {
        targetX = (e.clientX / window.innerWidth - 0.5);
        targetY = (e.clientY / window.innerHeight - 0.5);

        // Lens flare specific (pixel coordinates)
        if (lensFlare) {
            lensFlare.style.left = `${e.clientX - window.innerWidth / 2}px`;
            lensFlare.style.top = `${e.clientY - window.innerHeight / 2}px`;
        }
    });

    const animateRadiance = () => {
        // High-end slow drift (lerp)
        mouseX += (targetX - mouseX) * 0.02;
        mouseY += (targetY - mouseY) * 0.02;

        orbs.forEach((orb, index) => {
            const speed = (index + 1) * 30;
            const x = mouseX * speed;
            const y = mouseY * speed;

            orb.style.transform = `translate(${x}px, ${y}px)`;
        });

        requestAnimationFrame(animateRadiance);
    };

    // --- 5. Atmospheric Journey Navigation ---
    const journeyNav = document.getElementById('journeyNav');
    const journeyLinks = document.querySelectorAll('.journey-link');
    const sections = document.querySelectorAll('section[id]');

    const updateJourney = () => {
        const scrollPos = window.scrollY;

        // Show/Hide Nav Pill
        if (journeyNav) {
            if (scrollPos > 300) {
                journeyNav.classList.add('visible');
            } else {
                journeyNav.classList.remove('visible');
            }
        }

        // Track Active Section
        sections.forEach(section => {
            const sectionTop = section.offsetTop - 300;
            const sectionHeight = section.offsetHeight;
            const id = section.getAttribute('id');

            if (scrollPos >= sectionTop && scrollPos < sectionTop + sectionHeight) {
                journeyLinks.forEach(link => {
                    link.classList.remove('active');
                    if (link.getAttribute('href').includes(id)) {
                        link.classList.add('active');
                    }
                });
            }
        });
    };

    window.addEventListener('scroll', updateJourney);
    updateJourney(); // Init on load

    animateRadiance();

    // --- 5. Kinetic Symmetry Section Interaction ---
    const symmetryWrapper = document.querySelector('.kinetic-symmetry-wrapper');
    const portrait = document.querySelector('.silhouette-portrait');

    const kineticObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
            }
        });
    }, { threshold: 0.3 });

    if (symmetryWrapper) kineticObserver.observe(symmetryWrapper);

    window.addEventListener('scroll', () => {
        if (!symmetryWrapper || !portrait) return;
        const rect = symmetryWrapper.getBoundingClientRect();
        if (rect.top < window.innerHeight && rect.bottom > 0) {
            const shift = (window.innerHeight - rect.top) * 0.05;
            portrait.style.transform = `scale(1.1) translateY(${shift}px)`;
        }
    });

    // --- 6. Intersection Observer for Reveals ---
    const revealObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
            }
        });
    }, { threshold: 0.1 });

    document.querySelectorAll('.reveal-on-scroll, .section-padding, .treatment-card').forEach(el => {
        revealObserver.observe(el);
    });

    // --- 7. TX Treatment Section — Cursor Glow Tracker ---
    const txSection = document.querySelector('.tx-section');
    const txGlow = document.getElementById('txCursorGlow');

    if (txSection && txGlow) {
        txSection.addEventListener('mousemove', (e) => {
            const rect = txSection.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;
            txGlow.style.left = x + 'px';
            txGlow.style.top = y + 'px';
        });
    }

    // --- 8. TX Cards — Staggered IntersectionObserver ---
    const txRevealObserver = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const el = entry.target;
                const delay = el.style.getPropertyValue('--delay') || '0s';
                setTimeout(() => {
                    el.classList.add('visible');
                }, parseFloat(delay) * 1000);
                txRevealObserver.unobserve(el);
            }
        });
    }, { threshold: 0.12 });

    document.querySelectorAll('.tx-card, .tx-strip-card, .tx-header').forEach(el => {
        txRevealObserver.observe(el);
    });

    // --- 9. LX Luxury Integrated Accordion Logic ---
    const lxList = document.getElementById('lxList');
    const lxItems = document.querySelectorAll('.lx-item');
    const lxCounter = document.getElementById('lxCounterCurrent');

    if (lxList && lxItems.length > 0) {
        const setActiveItem = (item, force = false) => {
            if (!item || (item.classList.contains('lx-item--active') && !force)) return;

            const index = item.getAttribute('data-index');
            if (lxCounter) lxCounter.textContent = index;

            lxItems.forEach(i => i.classList.remove('lx-item--active'));
            item.classList.add('lx-item--active');
        };

        lxItems.forEach(item => {
            item.addEventListener('mouseenter', () => setActiveItem(item));
            item.addEventListener('click', () => setActiveItem(item));
        });

        // Initialize First Item
        setTimeout(() => setActiveItem(lxItems[0], true), 200);

        // Scroll reveal for integrated items
        const lxRevealObs = new IntersectionObserver((entries) => {
            entries.forEach((entry, i) => {
                if (entry.isIntersecting) {
                    setTimeout(() => {
                        entry.target.style.opacity = '1';
                        entry.target.style.transform = 'translateY(0)';
                    }, i * 100);
                    lxRevealObs.unobserve(entry.target);
                }
            });
        }, { threshold: 0.1 });

        lxItems.forEach(item => {
            item.style.opacity = '0';
            item.style.transform = 'translateY(20px)';
            item.style.transition = 'opacity 0.8s ease, transform 0.8s var(--ease-aesthetic)';
            lxRevealObs.observe(item);
        });
    }

    // --- 10. Clinical FAQ Accordion Logic ---
    const faqItems = document.querySelectorAll('.faq-item');
    faqItems.forEach(item => {
        const question = item.querySelector('.faq-question');
        question.addEventListener('click', () => {
            const isActive = item.classList.contains('active');

            // Close other items
            faqItems.forEach(otherItem => {
                otherItem.classList.remove('active');
            });

            if (!isActive) {
                item.classList.add('active');
            }
        });
    });

    // --- 11. Custom Cursor Functionality ---
    const cursor = document.getElementById('proCursor');
    const cursorFollower = document.getElementById('proCursorFollower');

    if (cursor && cursorFollower && window.matchMedia("(pointer: fine)").matches) {
        let mouseX = 0, mouseY = 0;
        let followerX = 0, followerY = 0;

        document.addEventListener('mousemove', (e) => {
            mouseX = e.clientX;
            mouseY = e.clientY;

            // Instantly move the small dot
            cursor.style.left = mouseX + 'px';
            cursor.style.top = mouseY + 'px';
        });

        const animateCursor = () => {
            // Smoothly move the follower circle
            followerX += (mouseX - followerX) * 0.15;
            followerY += (mouseY - followerY) * 0.15;

            cursorFollower.style.left = followerX + 'px';
            cursorFollower.style.top = followerY + 'px';

            requestAnimationFrame(animateCursor);
        };
        animateCursor();

        // Add hover effect to links, buttons, and interactive elements
        const interactiveElements = document.querySelectorAll('a, button, input, textarea, .slider-dot, .lx-item, .matrix-item');
        interactiveElements.forEach(el => {
            el.addEventListener('mouseenter', () => {
                cursor.classList.add('hover');
                cursorFollower.classList.add('hover');
            });
            el.addEventListener('mouseleave', () => {
                cursor.classList.remove('hover');
                cursorFollower.classList.remove('hover');
            });
        });
    }

});

// --- 12. Page Transition / Preloader ---
window.addEventListener('load', () => {
    const preloader = document.getElementById('pagePreloader');
    if (preloader) {
        // Small delay to let the initial animation play out
        setTimeout(() => {
            preloader.classList.add('loaded');
            // Remove completely after transition finishes
            setTimeout(() => {
                preloader.style.display = 'none';
            }, 800);
        }, 800);
    }
});
