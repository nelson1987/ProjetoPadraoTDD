﻿var left_side_width = 240; //Sidebar width in pixels

$(function () {
    "use strict";

    //Enable sidebar toggle
    $("[data-toggle='offcanvas']").click(function (e) {
        e.preventDefault();

        //If window is small enough, enable sidebar push menu
        if ($(window).width() <= 992) {
            $(".row-offcanvas").toggleClass("active");
            $(".left-side").removeClass("collapse-left");
            $(".right-side").removeClass("strech");
            $(".row-offcanvas").toggleClass("relative");
        } else {
            //Else, enable content streching
            $(".left-side").toggleClass("collapse-left");
            $(".right-side").toggleClass("strech");
        }
    });

    //Add hover support for touch devices
    $(".btn").bind("touchstart", function () {
        $(this).addClass("hover");
    }).bind("touchend", function () {
        $(this).removeClass("hover");
    });

    //Activate tooltips
    //$("[data-toggle='tooltip']").tooltip();

    /*     
     * Add collapse and remove events to boxes
     */
    //$("[data-widget='collapse']").click(function() {
    //    //Find the box parent        
    //    var box = $(this).parents(".box").first();
    //    //Find the body and the footer
    //    var bf = box.find(".box-body, .box-footer");
    //    if (!box.hasClass("collapsed-box")) {
    //        box.addClass("collapsed-box");
    //        //Convert minus into plus
    //        $(this).children(".fa-minus").removeClass("fa-minus").addClass("fa-plus");
    //        bf.slideUp();
    //    } else {
    //        box.removeClass("collapsed-box");
    //        //Convert plus into minus
    //        $(this).children(".fa-plus").removeClass("fa-plus").addClass("fa-minus");
    //        bf.slideDown();
    //    }
    //});

    /*
     * ADD SLIMSCROLL TO THE TOP NAV DROPDOWNS
     * ---------------------------------------
     */
    //$(".navbar .menu").slimscroll({
    //    height: "200px",
    //    alwaysVisible: false,
    //    size: "3px"
    //}).css("width", "100%");

    /*
     * INITIALIZE BUTTON TOGGLE
     * ------------------------
     */
    $(".btn-group[data-toggle=\"btn-toggle\"]").each(function () {
        var group = $(this);
        $(this).find(".btn").click(function (e) {
            group.find(".btn.active").removeClass("active");
            $(this).addClass("active");
            e.preventDefault();
        });

    });

    //$("[data-widget='remove']").click(function() {
    //    //Find the box parent        
    //    var box = $(this).parents(".box").first();
    //    box.slideUp();
    //});

    /* Sidebar tree view */
    //$(".sidebar .treeview").tree();

    /* 
     * Make sure that the sidebar is streched full height
     * ---------------------------------------------
     * We are gonna assign a min-height value every time the
     * wrapper gets resized and upon page load. We will use
     * Ben Alman's method for detecting the resize event.
     * 
     **/
    function _fix() {
        //Get window height and the wrapper height
        var height = $(window).height() - $("body > .header").height() - ($("body > .footer").outerHeight() || 0);
        $(".wrapper").css("min-height", height + "px");
        var content = $(".wrapper").height();
        var marginLeftSide = 0;
        if ($(".left-side").css("margin-top") != null) {
            //marginLeftSide = 0;
            marginLeftSide = parseInt($(".left-side").css("margin-top").replace("px", "").replace("-", ""));
        }

        content += marginLeftSide;
        height += marginLeftSide;
        //If the wrapper height is greater than the window
        if (content > height)
            //then set sidebar height to the wrapper
            $(".left-side, html, body").css("min-height", content + "px");
        else {
            //Otherwise, set the sidebar to the height of the window
            $(".left-side, html, body").css("min-height", height + "px");
        }
    }

    //Fire upon load
    _fix();
    //Fire when wrapper is resized
    $(".wrapper").resize(function () {
        _fix();
        fix_sidebar();
    });

    //Fix the fixed layout sidebar scroll bug
    fix_sidebar();

    /*
     * We are gonna initialize all checkbox and radio inputs to 
     * iCheck plugin in.
     * You can find the documentation at http://fronteed.com/iCheck/
     */
    //$("input[type='checkbox']:not(.simple), input[type='radio']:not(.simple)").iCheck({
    //    checkboxClass: 'icheckbox_minimal',
    //    radioClass: 'iradio_minimal'
    //});

});

function fix_sidebar() {
    //Make sure the body tag has the .fixed class
    if (!$("body").hasClass("fixed")) {
        return;
    }

    //Add slimscroll
    $(".sidebar").slimscroll({
        height: ($(window).height() - $(".header").height()) + "px",
        color: "rgba(0,0,0,0.2)"
    });
}

/* CENTER ELEMENTS */
(function ($) {
    "use strict";
    jQuery.fn.center = function (parent) {
        if (parent) {
            parent = this.parent();
        } else {
            parent = window;
        }
        this.css({
            "position": "absolute",
            "top": ((($(parent).height() - this.outerHeight()) / 2) + $(parent).scrollTop() + "px"),
            "left": ((($(parent).width() - this.outerWidth()) / 2) + $(parent).scrollLeft() + "px")
        });
        return this;
    };
}(jQuery));

/*
 * jQuery resize event - v1.1 - 3/14/2010
 * http://benalman.com/projects/jquery-resize-plugin/
 * 
 * Copyright (c) 2010 "Cowboy" Ben Alman
 * Dual licensed under the MIT and GPL licenses.
 * http://benalman.com/about/license/
 */
(function ($, h, c) {
    var a = $([]), e = $.resize = $.extend($.resize, {}), i, k = "setTimeout", j = "resize", d = j + "-special-event", b = "delay", f = "throttleWindow";
    e[b] = 250;
    e[f] = true;
    $.event.special[j] = {
        setup: function () {
            if (!e[f] && this[k]) {
                return false;
            }
            var l = $(this);
            a = a.add(l);
            $.data(this, d, { w: l.width(), h: l.height() });
            if (a.length === 1) {
                g();
            }
        },
        teardown: function () {
            if (!e[f] && this[k]) {
                return false;
            }
            var l = $(this);
            a = a.not(l);
            l.removeData(d);
            if (!a.length) {
                clearTimeout(i);
            }
        },
        add: function (l) {
            if (!e[f] && this[k]) {
                return false;
            }
            var n;

            function m(s, o, p) {
                var q = $(this), r = $.data(this, d);
                r.w = o !== c ? o : q.width();
                r.h = p !== c ? p : q.height();
                n.apply(this, arguments);
            }

            if ($.isFunction(l)) {
                n = l;
                return m;
            } else {
                n = l.handler;
                l.handler = m;
            }
        }
    };

    function g() {
        i = h[k](function () {
            a.each(function () {
                var n = $(this), m = n.width(), l = n.height(), o = $.data(this, d);
                if (m !== o.w || l !== o.h) {
                    n.trigger(j, [o.w = m, o.h = l]);
                }
            });
            g();
        }, e[b]);
    }
}
)(jQuery, this);

/*!
 * SlimScroll https://github.com/rochal/jQuery-slimScroll
 * =======================================================
 * 
 * Copyright (c) 2011 Piotr Rochala (http://rocha.la) Dual licensed under the MIT 
 */
(function (f) {
    jQuery.fn.extend({
        slimScroll: function (h) {
            var a = f.extend({ width: "auto", height: "250px", size: "7px", color: "#000", position: "right", distance: "1px", start: "top", opacity: 0.4, alwaysVisible: !1, disableFadeOut: !1, railVisible: !1, railColor: "#333", railOpacity: 0.2, railDraggable: !0, railClass: "slimScrollRail", barClass: "slimScrollBar", wrapperClass: "slimScrollDiv", allowPageScroll: !1, wheelStep: 20, touchScrollStep: 200, borderRadius: "0px", railBorderRadius: "0px" }, h);
            this.each(function () {
                function r(d) {
                    if (s) {
                        d = d ||
                            window.event;
                        var c = 0;
                        d.wheelDelta && (c = -d.wheelDelta / 120);
                        d.detail && (c = d.detail / 3);
                        f(d.target || d.srcTarget || d.srcElement).closest("." + a.wrapperClass).is(b.parent()) && m(c, !0);
                        d.preventDefault && !k && d.preventDefault();
                        k || (d.returnValue = !1);
                    }
                }

                function m(d, f, h) {
                    k = !1;
                    var e = d, g = b.outerHeight() - c.outerHeight();
                    f && (e = parseInt(c.css("top")) + d * parseInt(a.wheelStep) / 100 * c.outerHeight(), e = Math.min(Math.max(e, 0), g), e = 0 < d ? Math.ceil(e) : Math.floor(e), c.css({ top: e + "px" }));
                    l = parseInt(c.css("top")) / (b.outerHeight() - c.outerHeight());
                    e = l * (b[0].scrollHeight - b.outerHeight());
                    h && (e = d, d = e / b[0].scrollHeight * b.outerHeight(), d = Math.min(Math.max(d, 0), g), c.css({ top: d + "px" }));
                    b.scrollTop(e);
                    b.trigger("slimscrolling", ~~e);
                    v();
                    p();
                }

                function C() {
                    window.addEventListener ? (this.addEventListener("DOMMouseScroll", r, !1), this.addEventListener("mousewheel", r, !1), this.addEventListener("MozMousePixelScroll", r, !1)) : document.attachEvent("onmousewheel", r);
                }

                function w() {
                    u = Math.max(b.outerHeight() / b[0].scrollHeight * b.outerHeight(), D);
                    c.css({ height: u + "px" });
                    var a = u == b.outerHeight() ? "none" : "block";
                    c.css({ display: a });
                }

                function v() {
                    w();
                    clearTimeout(A);
                    l == ~~l ? (k = a.allowPageScroll, B != l && b.trigger("slimscroll", 0 == ~~l ? "top" : "bottom")) : k = !1;
                    B = l;
                    u >= b.outerHeight() ? k = !0 : (c.stop(!0, !0).fadeIn("fast"), a.railVisible && g.stop(!0, !0).fadeIn("fast"));
                }

                function p() {
                    a.alwaysVisible || (A = setTimeout(function () {
                        a.disableFadeOut && s || (x || y) || (c.fadeOut("slow"), g.fadeOut("slow"));
                    }, 1E3));
                }

                var s, x, y, A, z, u, l, B, D = 30, k = !1, b = f(this);
                if (b.parent().hasClass(a.wrapperClass)) {
                    var n = b.scrollTop(),
                        c = b.parent().find("." + a.barClass),
                        g = b.parent().find("." + a.railClass);
                    w();
                    if (f.isPlainObject(h)) {
                        if ("height" in h && "auto" == h.height) {
                            b.parent().css("height", "auto");
                            b.css("height", "auto");
                            var q = b.parent().parent().height();
                            b.parent().css("height", q);
                            b.css("height", q);
                        }
                        if ("scrollTo" in h)
                            n = parseInt(a.scrollTo);
                        else if ("scrollBy" in h)
                            n += parseInt(a.scrollBy);
                        else if ("destroy" in h) {
                            c.remove();
                            g.remove();
                            b.unwrap();
                            return;
                        }
                        m(n, !1, !0);
                    }
                } else {
                    a.height = "auto" == a.height ? b.parent().height() : a.height;
                    n = f("<div></div>").addClass(a.wrapperClass).css({
                        position: "relative",
                        overflow: "hidden",
                        width: a.width,
                        height: a.height
                    });
                    b.css({ overflow: "hidden", width: a.width, height: a.height });
                    var g = f("<div></div>").addClass(a.railClass).css({ width: a.size, height: "100%", position: "absolute", top: 0, display: a.alwaysVisible && a.railVisible ? "block" : "none", "border-radius": a.railBorderRadius, background: a.railColor, opacity: a.railOpacity, zIndex: 90 }),
                        c = f("<div></div>").addClass(a.barClass).css({
                            background: a.color,
                            width: a.size,
                            position: "absolute",
                            top: 0,
                            opacity: a.opacity,
                            display: a.alwaysVisible ?
                                "block" : "none",
                            "border-radius": a.borderRadius,
                            BorderRadius: a.borderRadius,
                            MozBorderRadius: a.borderRadius,
                            WebkitBorderRadius: a.borderRadius,
                            zIndex: 99
                        }),
                        q = "right" == a.position ? { right: a.distance } : { left: a.distance };
                    g.css(q);
                    c.css(q);
                    b.wrap(n);
                    b.parent().append(c);
                    b.parent().append(g);
                    a.railDraggable && c.bind("mousedown", function (a) {
                        var b = f(document);
                        y = !0;
                        t = parseFloat(c.css("top"));
                        pageY = a.pageY;
                        b.bind("mousemove.slimscroll", function (a) {
                            currTop = t + a.pageY - pageY;
                            c.css("top", currTop);
                            m(0, c.position().top, !1);
                        });
                        b.bind("mouseup.slimscroll", function (a) {
                            y = !1;
                            p();
                            b.unbind(".slimscroll");
                        });
                        return !1;
                    }).bind("selectstart.slimscroll", function (a) {
                        a.stopPropagation();
                        a.preventDefault();
                        return !1;
                    });
                    g.hover(function () {
                        v();
                    }, function () {
                        p();
                    });
                    c.hover(function () {
                        x = !0;
                    }, function () {
                        x = !1;
                    });
                    b.hover(function () {
                        s = !0;
                        v();
                        p();
                    }, function () {
                        s = !1;
                        p();
                    });
                    b.bind("touchstart", function (a, b) {
                        a.originalEvent.touches.length && (z = a.originalEvent.touches[0].pageY);
                    });
                    b.bind("touchmove", function (b) {
                        k || b.originalEvent.preventDefault();
                        b.originalEvent.touches.length &&
                        (m((z - b.originalEvent.touches[0].pageY) / a.touchScrollStep, !0), z = b.originalEvent.touches[0].pageY);
                    });
                    w();
                    "bottom" === a.start ? (c.css({ top: b.outerHeight() - c.outerHeight() }), m(0, !0)) : "top" !== a.start && (m(f(a.start).position().top, null, !0), a.alwaysVisible || c.hide());
                    C();
                }
            });
            return this;
        }
    });
    jQuery.fn.extend({ slimscroll: jQuery.fn.slimScroll });
})(jQuery);


$(document).ready(function () {
    $("form").submit(function () {
        $(".aguarde").show();
        $(this).find('input, select, textarea').on('eavalid', function (e, type, valid, expr, cond) {
            if (!valid) { $(".aguarde").hide(); }
        });
        if (!$(this).valid()) {
            $(".aguarde").hide();
        }
    });
});