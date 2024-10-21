window.getRtlFromLang = () => {
    const langData = localStorage.getItem('Lang');
    if (langData) {
        const langObject = JSON.parse(langData);
        return langObject.Rtl;
    }
    return null;
};

let isRTL = false;
const theme_color = "#c62f2f";

function updateLanguage(lang, rtl) {
    document.documentElement.lang = lang;
    if (rtl) {
        document.documentElement.dir = 'rtl';
        document.body.classList.add('rtl');
        isRTL = true;
    } else {
        document.documentElement.removeAttribute("dir");
        document.body.classList.remove('rtl');
        isRTL = false;
    }
}

function toggleShppingCart() {
    $('.cart-sidebar').toggleClass('active');
}

async function messageBox(message, title, icon) {
    await Swal.fire({
        html: message,
        title: title,
        icon: icon
    });
}

async function confirmationBox(message, title, buttons, colors, icon) {
    return await Swal.fire({
        title: title,
        html: message,
        icon: icon,
        focusCancel: true,
        showCancelButton: true,
        showDenyButton: buttons.length == 3,
        confirmButtonText: buttons[0],
        cancelButtonText: buttons[1],
        denyButtonText: buttons.length == 3 ? buttons[2] : null,
        confirmButtonColor: colors[0],
        cancelButtonColor: colors[1],
        denyButtonColor: buttons.length == 3 ? colors[2] : null,
    }).then((result) => {
        if (result.isConfirmed) {
            return buttons[0];
        } else if (result.isDismissed) {
            return buttons[1];
        } else if (result.isDenied) {
            return buttons[2];
        }
    });
}

function homeSlider() {
    $(".home-slider").owlCarousel({
        navText: ['<i class="bi bi-arrow-left"></i>', '<i class="bi bi-arrow-right"></i>'],
        rtl: false,
        loop: true,
        nav: true,
        dots: true,
        mouseDrag: true,
        autoplay: true,
        responsiveClass: true,
        smartSpeed: 1000,
        autoHeight: true,
        responsive: {
            0: {
                items: 1
            },
            400: {
                items: 1
            },
            600: {
                items: 1
            },
            900: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });
};
function bannerSlider() {
    $(".banner-slider").owlCarousel({
        navText: ['<i class="bi bi-arrow-left"></i>', '<i class="bi bi-arrow-right"></i>'],
        rtl: false,
        loop: true,
        nav: true,
        dots: false,
        margin: 25,
        mouseDrag: true,
        responsiveClass: true,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1
            },
            400: {
                items: 2
            },
            600: {
                items: 3
            },
            900: {
                items: 5
            },
            1000: {
                items: 6
            }
        }
    });
};

function productsSlider(className) {
    $(className).owlCarousel({
        navText: ['<i class="bi bi-arrow-left"></i>', '<i class="bi bi-arrow-right"></i>'],
        rtl: false,
        loop: true,
        nav: true,
        dots: false,
        margin: 25,
        mouseDrag: true,
        responsiveClass: true,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1
            },
            400: {
                items: 2
            },
            600: {
                items: 2
            },
            900: {
                items: 3
            },
            1000: {
                items: 4
            }
        }
    });
}

function productImageSlider(className) {
    $(className).owlCarousel({
        navText: ['<i class="bi bi-arrow-left"></i>', '<i class="bi bi-arrow-right"></i>'],
        rtl: false,
        loop: true,
        nav: true,
        dots: true,
        margin: 25,
        mouseDrag: true,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1
            }
        }
    });
}

function scrollToTop() {
    document.documentElement.scrollTop = 0;
}
(function ($) {
    'use strict';

    //Preloader
    let win = $(window);
    win.on('load', function () {
        $('.tw-loader').delay(100).fadeOut('slow');
    });

    $.ajaxSetup({
        headers: {
            'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
        }
    });

    //Top Menu sticky
    win.on('scroll', function () {
        if ($(this).scrollTop() > 500) {
            $('#sticky-header').addClass("sticky");
        } else {
            $('#sticky-header').removeClass("sticky");
        }
        top_categories_nicescroll();
    });


    //niceScroll for Top Categories
    let top_categories_nicescroll = function () {
        $(".nav_cat_content").getNiceScroll().resize();
        $(".nav_cat_content").niceScroll({
            cursorborder: "",
            cursorcolor: theme_color,
            boxzoom: false,
            scrollspeed: 60,
            cursorwidth: "3px",
            smoothscroll: true,
        });
    }

    top_categories_nicescroll();

     //Off Canvas Open close start
    $(".off-canvas-btn").on('click', function () {
        $(".mobile-menu-wrapper").addClass('open');
    });

    $(".offcanvas-btn-close, .off-canvas-overlay").on('click', function () {
        $(".mobile-menu-wrapper").removeClass('open');
    });

     //slide effect dropdown
    function dropdownAnimation() {
        $('.dropdown').on('show.bs.dropdown', function (e) {
            $(this).find('.dropdown-menu').first().stop(true, true).slideDown(500);
        });

        $('.dropdown').on('hide.bs.dropdown', function (e) {
            $(this).find('.dropdown-menu').first().stop(true, true).slideUp(500);
        });
    }

    dropdownAnimation();

    //offcanvas mobile menu start 
    let $offCanvasNav = $('.mobile-menu'),
        $offCanvasNavSubMenu = $offCanvasNav.find('.dropdown');

    //Add Toggle Button With Off Canvas Sub Menu
    $offCanvasNavSubMenu.parent().prepend('<span class="menu-expand"><i></i></span>');

    //Close Off Canvas Sub Menu
    $offCanvasNavSubMenu.slideUp();

    //Category Sub Menu Toggle
    $offCanvasNav.on('click', 'li a, li .menu-expand', function (e) {
        let $this = $(this);
        if (($this.parent().attr('class').match(/\b(has-children-menu|has-children|has-sub-menu)\b/)) && ($this.attr('href') === '#' || $this.hasClass('menu-expand'))) {
            e.preventDefault();
            if ($this.siblings('ul:visible').length) {
                $this.parent('li').removeClass('active');
                $this.siblings('ul').slideUp();
            } else {
                $this.parent('li').addClass('active');
                $this.closest('li').siblings('li').removeClass('active').find('li').removeClass('active');
                $this.closest('li').siblings('li').find('ul:visible').slideUp();
                $this.siblings('ul').slideDown();
            }
        }
    });

     //tooltip active js
    //$('[data-toggle="tooltip"]').tooltip();

    $('.home-slider').owlCarousel({
        navText: ['<i class="bi bi-arrow-left"></i>', '<i class="bi bi-arrow-right"></i>'],
        rtl: isRTL,
        loop: true,
        nav: true,
        dots: true,
        mouseDrag: true,
        responsiveClass: true,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1
            },
            400: {
                items: 1
            },
            600: {
                items: 1
            },
            900: {
                items: 1
            },
            1000: {
                items: 1
            }
        }
    });

    $('.brands-carousel').owlCarousel({
        navText: ['<i class="bi bi-arrow-left"></i>', '<i class="bi bi-arrow-right"></i>'],
        rtl: isRTL,
        loop: true,
        nav: true,
        dots: false,
        margin: 25,
        mouseDrag: true,
        responsiveClass: true,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1
            },
            400: {
                items: 2
            },
            600: {
                items: 3
            },
            900: {
                items: 5
            },
            1000: {
                items: 6
            }
        }
    });

    $('.category-carousel').owlCarousel({
        navText: ['<i class="bi bi-arrow-left"></i>', '<i class="bi bi-arrow-right"></i>'],
        rtl: isRTL,
        loop: true,
        nav: true,
        dots: false,
        margin: 25,
        mouseDrag: true,
        responsiveClass: true,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1
            },
            400: {
                items: 2
            },
            600: {
                items: 2
            },
            900: {
                items: 3
            },
            1000: {
                items: 4
            }
        }
    });

    //Sigle Product Slider
    let bigimage = $("#product_big");
    let thumbs = $("#product_thumbs");
    let syncedSecondary = true;

    bigimage.owlCarousel({
        navText: ['<i class="bi bi-arrow-left"></i>', '<i class="bi bi-arrow-right"></i>'],
        rtl: isRTL,
        items: 1,
        nav: true,
        autoplay: false,
        dots: false,
        loop: true,
        smartSpeed: 1000,
        slideSpeed: 2000,
        responsiveRefreshRate: 200
    }).on("changed.owl.carousel", syncPosition);

    thumbs.on("initialized.owl.carousel", function () {
        thumbs.find(".owl-item").eq(0).addClass("current");
    }).owlCarousel({
        navText: ['<i class="bi bi-arrow-left"></i>', '<i class="bi bi-arrow-right"></i>'],
        rtl: isRTL,
        dots: false,
        nav: false,
        margin: 10,
        smartSpeed: 1000,
        slideSpeed: 500,
        slideBy: 4,
        responsiveRefreshRate: 100,
        mouseDrag: true,
        responsiveClass: true,
        responsive: {
            0: {
                items: 3
            },
            400: {
                items: 5
            },
            600: {
                items: 6
            },
            900: {
                items: 6
            },
            1000: {
                items: 6
            }
        }
    }).on("changed.owl.carousel", syncPosition2);

    function syncPosition(el) {
        let count = el.item.count - 1;
        let current = Math.round(el.item.index - el.item.count / 2 - 0.5);

        if (current < 0) {
            current = count;
        }

        if (current > count) {
            current = 0;
        }

        thumbs.find(".owl-item").removeClass("current").eq(current).addClass("current");
        let onscreen = thumbs.find(".owl-item.active").length - 1;
        let start = thumbs.find(".owl-item.active").first().index();
        let end = thumbs.find(".owl-item.active").last().index();

        if (current > end) {
            thumbs.data("owl.carousel").to(current, 100, true);
        }

        if (current < start) {
            thumbs.data("owl.carousel").to(current - onscreen, 100, true);
        }
    }

    function syncPosition2(el) {
        if (syncedSecondary) {
            let number = el.item.index;
            bigimage.data("owl.carousel").to(number, 100, true);
        }
    }

    thumbs.on("click", ".owl-item", function (e) {
        e.preventDefault();
        let number = $(this).index();
        bigimage.data("owl.carousel").to(number, 300, true);
    });

    //Subscribe for footer
    $(document).on("click", ".subscribe_btn", function (event) {
        event.preventDefault();

        let sub_email = $("#subscribe_email").val();
        let status = 'subscribed';

        let sub_btn = $('.sub_btn').html();
        let sub_recordid = '';

        let subscribe_email = sub_email.trim();

        if (subscribe_email == '') {
            $('.subscribe_msg').html('<p class="text-danger">The email address field is required.</p>');
            return;
        }

        $.ajax({
            type: 'POST',
            url: base_url + '/frontend/saveSubscriber',
            data: 'RecordId=' + sub_recordid + '&email_address=' + subscribe_email + '&status=' + status,
            beforeSend: function () {
                $('.subscribe_msg').html('');
                $('.sub_btn').html('<span class="spinner-border spinner-border-sm"></span> Please Wait...');
            },
            success: function (response) {
                let msgType = response.msgType;
                let msg = response.msg;

                if (msgType == "success") {
                    $("#subscribe_email").val('');
                    $('.subscribe_msg').html('<p class="text-success">' + msg + '</p>');
                } else {
                    $('.subscribe_msg').html('<p class="text-danger">' + msg + '</p>');
                }

                $('.sub_btn').html(sub_btn);
            }
        });
    });

}(jQuery));
