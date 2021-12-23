$("img:not(.banner__item img)").each(function () {
  $(this).attr("loading", "lazy");
});

document.body.addEventListener(
  "load",
  (e) => {
    if (e.target.tagName != "IMG") {
      return;
    }
    // Remove the blurry placeholder.
    e.target.style.backgroundImage = "none";
  },
  /* capture */
  true
);

$(document).ready(function () {
  /* --- header - menu  ---- */
  // var h_header = $(".header").height();
  // console.log(h_header);
  // $(window).scroll(function () {
  //   if ($(window).scrollTop() > (h_header + 100)) {
  //     $(".header__main").addClass("fixed");

  //   } else {
  //     $(".header__main").removeClass("fixed");

  //   }
  // });

  $(".search-icon").click(function (e) {
    e.preventDefault();
    $(this).toggleClass("fa-times fa-search");
    $("#quick-search").toggleClass("active");
  });

  $(".menu-bar ").click(function (e) {
    e.preventDefault();
    $(".menu-bar ").toggleClass("open");
    $(".menu-mobile").toggleClass("active");
  });

  var $menu = $(".menu-mobile");
  $(document).mouseup((e) => {
    if (
      !$menu.is(e.target) && // if the target of the click isn't the container...
      $menu.has(e.target).length === 0
    ) {
      // ... nor a descendant of the container
      $menu.removeClass("active");
      $(".menu-bar ").removeClass("open");
    }
  });

  $(".drop-icon").click(function (e) {
    e.preventDefault();
    // $(".menu__child").slideUp();
    // $(this).toggleClass("fa-angle-up fa-angle-down");
    $(this).parent(".menu__item").find(".menu__child").slideToggle();
  });

  /* ---- date picker ---- */
  $("[name= 'checkIn']").daterangepicker({
    singleDatePicker: true,
    autoApply: true,
    minDate: moment(),
    locale: {
      format: 'DD/MM/YYYY'
    }
  });

  $("[name= 'checkOut']").daterangepicker({
    singleDatePicker: true,
    autoApply: true,
    // minDate: moment(),
    minDate: moment().add(1, "days"),
    locale: {
      format: 'DD/MM/YYYY'
    }
  });

  $("[name= 'checkIn']").on("apply.daterangepicker", function (e, picker) {
    var mindate = $(this).val();
    // console.log(typeof mindate);
    var date = moment(mindate, "DD/MM/YYYY");

    $("[name= 'checkOut']").daterangepicker({
      minDate: date,
      // minDate:mindate,
      startDate: date.add(1, "days"),
      singleDatePicker: true,
      autoApply: true,
      locale: {
        format: 'DD/MM/YYYY'
      }
    });
  });

  /* ----- video home ---- */

  $(".active-video").click(function (e) {
    e.preventDefault();
    $(".bg-video").addClass("active");
  });

  $(".close-video").click(function (e) {
    e.preventDefault();
    $(".bg-video").removeClass("active");
  });

  var $menu2 = $(".video-box");
  $(document).mouseup((e) => {
    if (
      !$menu2.is(e.target) && // if the target of the click isn't the container...
      $menu2.has(e.target).length === 0
    ) {
      // ... nor a descendant of the container
      $menu2.parent(".bg-video").removeClass("active");
    }
  });


  /* ---- recruitment page ---- */
  $(".apply-job").click(function (e) {
    $(".close-info").click();
  });


});
