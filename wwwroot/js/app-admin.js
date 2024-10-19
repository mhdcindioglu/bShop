var $ = jQuery.noConflict();

(function ($) {
	'use strict';

	//Menu active
	var href = location.href;
	var elem = '.left-navbar li a[href="' + href + '"]';

	$('ul.left-navbar li').parent().removeClass('active');
	$('ul.left-navbar li a').parent().removeClass('active');

	var parentClase = $(elem).parents('.dropdown');
	if (parentClase.length) {
		$(parentClase).addClass('active');
		$(elem).parent().addClass('active');
	} else {
		$(elem).addClass('active');
	}

	//menu-toggle
	$("#menu-toggle").on('click', function (e) {
		e.preventDefault();

		$("#wrapper").toggleClass("toggled");
		sidebar_nicescroll();
		return false;
	});

	//niceScroll
	var sidebar_nicescroll = function () {
		$(".sidebar-wrapper").getNiceScroll().resize();
		$(".sidebar-wrapper").niceScroll({
			cursorborder: "",
			cursorcolor: "#f1f5f9",
			boxzoom: false,
			scrollspeed: 60,
			cursorwidth: "3px",
			smoothscroll: true,
		});
	}

	//function for dropdown menu
	var sidebar_dropdown = function () {
		if ($(".sidebar-wrapper").length) {

			$(".left-navbar li.dropdown a.has-dropdown").off('click').on('click', function () {

				var sidebar = $(this);

				var active = false;
				if (sidebar.parent().hasClass("active")) {
					active = true;
				}

				$('.left-navbar li.active > .dropdown-menu').slideUp(500, function () {
					sidebar_nicescroll();
					return false;
				});

				$('.left-navbar li.dropdown.active').removeClass('active');

				if (active == true) {
					sidebar.parent().removeClass('active');
					sidebar.parent().find('> .dropdown-menu').slideUp(500, function () {
						sidebar_nicescroll();
						return false;
					});
				} else {

					sidebar.parent().addClass('active');
					sidebar.parent().find('> .dropdown-menu').slideDown(500, function () {
						sidebar_nicescroll();
						return false;
					});
				}

				return false;
			});

			$('.left-navbar li.active > .dropdown-menu').slideDown(500, function () {
				sidebar_nicescroll();
				return false;
			});
		}
	}

	sidebar_dropdown();
	sidebar_nicescroll();

	//Tabs Nav active
	var href = location.href;
	$('ul.tabs-nav li a').removeClass('active');
	$('ul.tabs-nav li a[href="' + href + '"]').addClass('active');

}(jQuery));

var config = {
	'.chosen-select': {},
	'.chosen-select-deselect': { allow_single_deselect: true },
	'.chosen-select-no-single': { disable_search_threshold: 10 },
	'.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
	'.chosen-select-rtl': { rtl: true },
	'.chosen-select-width': { width: '95%' },
	'.chosen-select': { search_contains: true }
}
for (var selector in config) {
	$(selector).chosen(config[selector]);
}
