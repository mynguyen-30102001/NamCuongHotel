$(document).ready(function () {
  $(".banner-slide").owlCarousel({
    items: 1,
    loop: true,
    margin: 10,
    nav: true,
    dots: false,
    autoplay: true,
    autoplayTimeout: 5000,
    animateOut: "animate__fadeOut",
    // animateIn: "animate__fadeIn",
    responsiveClass: true,
    navText: [
      '<i class="fal fa-chevron-left"></i>',
      '<i class="fal fa-chevron-right"></i>',
    ],
  });

  $(".room-slide").owlCarousel({
    items: 1,
    center: true,
    loop: true,
    nav: true,
    dots: false,
    rewind: true,
    smartSpeed: 500,
    // slideTransition: "ease",
    autoplay: true,
    autoplayTimeout: 5000,
    autoplaySpeed: 800,
    responsiveClass: true,
    navText: [
      '<i class="fal fa-chevron-left"></i>',
      '<i class="fal fa-chevron-right"></i>',
    ],
    responsive: {
      0: {
        stagePadding: 0,
      },
      600: {
        stagePadding: 0,
        margin: 30,
      },
      1024: {
        stagePadding: 100,
        margin: 40,
      },
      1200: {
        stagePadding: 200,
        margin: 60,
      },
    },
  });

  $(".new-list").owlCarousel({
    margin: 30,
    loop: true,
    nav: true,
    dots: false,
    // autoplay: true,
    // autoplayTimeout: 5000,
    // autoplaySpeed: 800,
    responsiveClass: true,
    navText: [
      '<i class="fal fa-chevron-left"></i>',
      '<i class="fal fa-chevron-right"></i>',
    ],
    responsive: {
      0: {
        items: 1,
      },
      600: {
        items: 2,
      },
      1000: {
        items: 3,
      },
    },
  });

  /* --- slider room detail page- --- */
  $(".room-img__slide").owlCarousel({
    items: 1,
    loop: true,
    margin: 10,
    nav: true,
    dots: false,
    autoplay: true,
    autoplayTimeout: 5000,
    autoplaySpeed: 800,
    responsiveClass: true,
    navText: [
      '<i class="fal fa-chevron-left"></i>',
      '<i class="fal fa-chevron-right"></i>',
    ],
  });

  $(".room-related").owlCarousel({
    margin: 30,
    loop: true,
    nav: false,
    dots: false,
    rewind: true,
    smartSpeed: 800,
    autoplay: true,
    autoplayTimeout: 5000,
    autoplaySpeed: 800,
    responsiveClass: true,
    navText: [
      '<i class="fal fa-chevron-left"></i>',
      '<i class="fal fa-chevron-right"></i>',
    ],
    responsive: {
      0: {
        items: 1,
      },
      600: {
        items: 2,
      },
      1000: {
        items: 3,
      },
    },
  });


  $(".gallery-slide").owlCarousel({
    margin: 5,
    loop: true,
    nav: true,
    dots: false,
    rewind: true,
    smartSpeed: 600,
    // autoplay: true,
    // autoplayTimeout: 6000,
    // autoplaySpeed: 1000,
    responsiveClass: true,
    navText: [
      '<i class="fal fa-chevron-left"></i>',
      '<i class="fal fa-chevron-right"></i>',
    ],
    responsive: {
      0: {
        items: 2,
      },
      600: {
        items: 3,
      },
      1000: {
        items: 5,
      },
    },
  });
});
