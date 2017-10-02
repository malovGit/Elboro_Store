

//select product of the Cart Page
$(function () {
    $('.checked-box input:checkbox').change(function () {
        var checked = $(this).prop('checked');
        var productId = $(this).attr('id').slice(-1);
        $.ajax({
            type: 'POST',
            data: { checkin: checked, productId: productId },
            url: '/Cart/ChangeSelected/',
            success: function (data) {
                if (checked) $('a.check_out').removeClass('disabled');
                console.log = data;
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.responseText);
            }

        });
    });
});

//select All products checkboxes of the Cart Page********
$(function () {
    $('#selectedAll').change(function () {
        if ($(this).prop('checked') == false) {

            $('.checked-box input:checkbox').prop('checked', false);
            $('.checked-box input:checkbox').change();
            $('a.check_out').addClass('disabled');

            //$('a.check_out').css('background-color', '#a79393');
        }
        else {
            $('.checked-box input:checkbox').prop('checked', true);
            $('.checked-box input:checkbox').change();
            $('a.check_out').removeClass('disabled');
        }

    });
});

//Magnify Zoom***********
$(".magnify").jfMagnify({
    center: true,
    scale: "2.5",
    //containment: 'magnify',
    //magnifyGlass: '.magnify_glass',
    //magnifiedElement: '.magnified_element',
    //magnifiedZone: '.magnify_glass',
    //elementToMagnify: '.element_to_magnify',
});
//End Magnify Zoom***********


//Slider of Product Details***********
$(document).ready(function () {
    $('.slider-for').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        fade: true,
        asNavFor: '.slider-nav',
        speed: 800,
        cssEase: 'linear',
        
    });
    $('.slider-nav').slick({
        infinite: true,
        arrows: false,
        slidesToShow: 3,
        slidesToScroll: 1,
        asNavFor: '.slider-for',
        dots: true,
        centerMode: true,
        centerPadding: '-30px',
        focusOnSelect: true,
        //adaptiveHeight: true,
        lazyLoad: 'ondemand',
        //swipeToSlide: true,
        speed: 800,
        
        accessibility: true,
        cssEase: 'linear',
        responsive: [
            {
                breakpoint: 480,
                settings: {
                    arrows: false,
                    centerMode: false,
                    slidesToShow: 3,
                    slidesToScroll: 1,
                    аccessibility: false,
                    speed: 400,
                }
            }
        ]
    });
});
//End Slider of Product Details***********

//product details

//Load current Cart**************
$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/Cart/CurrentCartView/',
        success: function (data) {
            $('#cart_count').html(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.responseText);
        }
    });
});
//End Load current Cart**************

//Remove from cart**********************************
$(function () {
    $('.remove-item').click(function () {
        var count;
        var productId = $(this).attr('data-id');
        if ($(this).hasClass("cart_quantity_delete")) {
            count = $('#item_count-' + productId).val();
        }
        else {
            count = 1;
        }
        $.ajax({
            type: 'POST',
            data: { count: count, productId: productId },
            url: '/Cart/RemoveFromCart/',
            success: function (data) {
                if (data.CountLineItems == 0) {
                    $('#row-' + data.deleteId).fadeOut('slow');
                    $('#cart_count').html(data.TotalCountCart);
                    $('.cart-total-price span').html(accounting.formatMoney(data.TotalPriceCart));
                }
                else {
                    $('#item_count-' + data.deleteId).val(data.CountLineItems);
                    $('#cart_count').html(data.TotalCountCart);
                    $('#item_total_price-' + data.deleteId).html(accounting.formatMoney(data.TotalPriceLine));
                    $('.cart-total-price span').html(accounting.formatMoney(data.TotalPriceCart));
                }
                

            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.responseText);
            }
        });
    });
});
//End Remove from cart******************************

//Add to cart from cart*****************************
$(function () {
    $('.cart_quantity_up').click(function () {
        var count = 1;
        var productId = $(this).attr('data-id');
        $.ajax({
            type: 'POST',
            data: { count: count, productId: productId },
            url: '/Cart/AddToCart/',
            success: function (data) {
                $('#cart_count').html(data.TotalCountCart);
                $('#item_count-' + data.deleteId).val(data.CountLineItems);
                $('#item_total_price-' + data.deleteId).html(accounting.formatMoney(data.TotalPriceLine));
                $('.cart-total-price span').html(accounting.formatMoney(data.TotalPriceCart));
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.responseText);
            }
        });
    });
});
//End Add to cart from cart*****************************

//Add To Cart button click***************************
function AddToCart (value) {    
    var count;
    var productId;
    if ($('input.input-count').val() == undefined) { count = 1; productId = $(value).attr('data-id');}
    else {
        
        count = $('input.input-count').val();
        if (count == 0) {
            count = 1;
        }
        productId = $('#ProductID').val();
    }
        
        $.ajax({
            type: 'POST',
            data: { count: count, productId: productId },
            url: '/Cart/AddToCart/',
            success: function (data) {
                $('#cart_count').html(data.TotalCountCart);
                $(value).css("display", "none");
                $('#go_to_cart-' + productId).css("display", "inline-block");
            },
            error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.responseText);

        }
   });
}
//End Add To Cart button click***************************


//Modal AddToCart show
//$(document).ready(function () {
//    $('.add-to-cart').click(function () {
//        var prodId = $(this).attr('data-id');
//        $(this).css("display", "none");
//        $('#go_to_cart-'+prodId).css("display", "inline-block");
        
//    });
//});


//$(document).ready(function () {
//    $('a.check_out').click(function () {
//        $.ajax({
//            type: 'GET',
//            url: 'Cart/CheckIfUserAuth/',
//            success: function (data) {
//                if (data == true) {
//                    window.location.href = "/Order/CreateOrder/";
//                }
//                else {
//                    //$('div.login-action').html(data);
//                    $('#ModalLogin').modal('show');
//                    $('#logoutForm').submit();
//                }
//            },
//            error: function (xhr, ajaxOptions, thrownError) {
//                alert(xhr.responseText);
//            }
//        });
        
//   });
//});


//$(document).ready(function () {
//    $('.check_out').click(function () {
//        $('#authonticate_modal').modal('show');
//    });
//});

//$(function(){
//    $("#gallery").lightbox({
//    overlayBgColor: '#FFF',
//    overlayOpacity: 0.6,
//    imageLoading: 'Images/loading.gif',
//    imageBtnClose: 'Images/close.gif',
//    imageBtnPrev: 'Images/prev.gif',
//    imageBtnNext: 'Images/next.gif',
//    containerResizeSpeed: 350,
//    });
//});

//change count of products
function minus() {
    var result = $('input.input-count');
    var count = parseInt(result.val()) - 1;
    count = count < 1 ? 0 : count;
    result.val(count);
    result.change();
    return false;
}

function plus() {
    var result = $('input.input-count');//$(this).parent().find('input');//$('span.group-controls').find('input');
    result.val(parseInt(result.val()) + 1);
    result.change();
    return false;
}

 //LightBox settings use   
$(function () {
    lightbox.option({                                                      //Option	Default	Description
        'alwaysShowNavOnTouchDevices':true,	//If true, the left and right navigation arrows which appear on mouse hover when viewing image sets will always be visible on devices which support touch.
        'albumLabel':"Image %1 of %2",	        //The text displayed below the caption when viewing an image set. The default text shows the current image number and the total number of images in the set.
        'disableScrolling':	false,	            //If true, prevent the page from scrolling while Lightbox is open. This works by settings overflow hidden on the body.
        'fadeDuration':	300,	                //The time it takes for the Lightbox container and overlay to fade in and out, in milliseconds.
        'fitImagesInViewport':	false,	        //If true, resize images that would extend outside of the viewport so they fit neatly inside of it. This saves the user from having to scroll to see the entire image.
        'imageFadeDuration':600,	    //The time it takes for the image to fade in once loaded, in milliseconds.
        'maxWidth':	350 ,	            //If set, the image height will be limited to this number, in pixels.
        'maxHeight':350,	            //If set, the image width will be limited to this number, in pixels.
        'positionFromTop':	50,	        //The distance from top of viewport that the Lightbox container will appear, in pixels.
        'resizeDuration':	700,	    //The time it takes for the Lightbox container to animate its width and height when transition between different size images, in milliseconds.
        'showImageNumberLabel':	true,	//If false, the text indicating the current image number and the total number of images in set (Ex. "image 2 of 4") will be hidden.
        'wrapAround':false,	        //If true, when a user reaches the last image in a set, the right navigation arrow will appear and they will be to continue moving forward which will take them back to the first image in the set.
    });
});


(function($){
    $(document).ready(function () {
        $.ajax({
            type: 'GET',
            url: '/Store/GetSubCategories/',
            cache: false,
            success: function (data) {
                var items = [];
                $.each(data, function () {
                    items.push('<li role="presentation"><a role="menuitem" href="#"' + 'onclick="SelectCategory(this)">'+ this +'</a></li>');
                });
                $("#drop_category_search").append(items.join(' '));
            }
        });
    });
})(jQuery);


function SelectCategory(item) {
    jQuery('#chois_category').text(item.text);
    
}


////Change Page ProductList*************
//function ChangePage(currentPage){
//    var sortVal = $('#sortCategories option:selected').val();
//    var pagesize = $('#pageSize option:selected').val();
//    var currentCategoryId = $('#CurrentSubCategoryId').val();
//    $.ajax({
//        type:'GET',
//        cache: false,
//        data:{
//            subId: currentCategoryId,
//            sortBy: sortVal,
//            page: currentPage,
//            pageSize: pagesize
//        },
//        url: '@Url.Action("ProductList", "Product")',
//        success: function(data){
//            $('#prodMain').html(data);
//            //$(this).css('background-color', '#428bca');
//            //$('#pageLinks li a').text() == currentPage.css('background-color', '#428bca');
                    
//        }
//    });
//}
////End Change Page ProductList*************

////Change Category ProductList*************
//    $(document).ready(function () {
//        $('#sortCategories').on('change',ChangeEvent());
                
//    });
////End Change Category ProductList*****************


////Change Size ProductList*********************
//    $(document).ready(function () {
//        $('#pageSize').on('change',ChangeEvent());
            
//    });
////End Change Size ProductList*********************




//Autocomplite************
(function ($) {
    $(document).ready(function () {
        $('#inputSearch').autocomplete({
            minLength: 2,
            //delay: 800,
            source: function (request, response) {
                var currentCat = $('#chois_category').text();
                $.ajax({
                    dataType: 'json',
                    data: {
                        currentCat: currentCat,
                        term: request.term
                    },
                    url: '/Store/AutocompleteSearch/',
                    success: function (data) {
                        response($.map(data, function (val) {
                            return {
                                label: val.value,
                                value: val.value
                            };
                        }))
                    },

                });
            }
            //target.attr("data-autocomplete-source")
        });

    });
})(jQuery);
//End Autocomplite********




// slideupCat function for slide up the categories when hover

//$(document).ready(function () {
//    // slideupCat function for slide up the categories when hover
//    function slideupCat($cat, $label) {
//        $('#' + $cat).mouseover(function () {
//            $(this).css('background-position', '0 -70px')
//                .css('transition', 'all 0.5s ease-in')
//                .css('-webkit-transition', 'all 0.5s ease-in')
//                .css('-moz-transition', 'all 0.5s ease-in')
//                .css('-o-transition', 'all 0.5s ease-in')
//                .css('-ms-transition', 'all 0.5s ease-in');
//            $('#' + $label).css('margin-top', '180px')
//                .css('transition', 'all 0.5s ease-in')
//                .css('-webkit-transition', 'all 0.5s ease-in')
//                .css('-moz-transition', 'all 0.5s ease-in')
//                .css('-o-transition', 'all 0.5s ease-in')
//                .css('-ms-transition', 'all 0.5s ease-in');
//        });
//        $('#' + $cat).mouseout(function () {
//            $(this).css('background-position', '0 0').css('transition', 'all 0.5s ease-in').css('-webkit-transition', 'all 0.5s ease-in').css('-moz-transition', 'all 0.5s ease-in').css('-o-transition', 'all 0.5s ease-in').css('-ms-transition', 'all 0.5s ease-in');
//            $('#' + $label).css('margin-top', '250px').css('transition', 'all 0.5s ease-in').css('-webkit-transition', 'all 0.5s ease-in').css('-moz-transition', 'all 0.5s ease-in').css('-o-transition', 'all 0.5s ease-in').css('-ms-transition', 'all 0.5s ease-in');
//        });
//    }

//    slideupCat('cat1', 'lblcat1');
//    slideupCat('cat2', 'lblcat2');
//    slideupCat('cat3', 'lblcat3');
//    slideupCat('cat4', 'lblcat4');
//    slideupCat('cat5', 'lblcat5');
//});

//$(document).ready(function () {
//    var categ = ('div.catimage');
//    var lablal = ('label.catlablel');
//    for (var i = 1; i <= categ.length; i++) {
//        slideupCat($categ['id'].get(i), $lablal['id'].get(i));
//    }
//});
//menu category open with hover
//jQuery(document).ready(function () {
//    $(".dropdown").hover(
//        function () {
//            $('.dropdown-menu', this).stop().fadeIn("fast");
//        },
//        function () {
//            $('.dropdown-menu', this).stop().fadeOut("fast");
//        });
//});

//Thumbler of products
//$(document).ready(function () {
//    $('#list').click(function (event) {
//        event.preventDefault();
//        $('#products .item').addClass('list-group-item');
//    });
//    $('#grid').click(function (event) {
//        event.preventDefault();
//        $('#products .item').removeClass('list-group-item');
//        $('#products .item').addClass('grid-group-item');
//    });
//});

//dropdown sort

//$('.dropdown-menu a').on('click', function () {
//    $('.dropdown-toggle').html($(this).html() + '<span class="caret"></span>');
//})
               