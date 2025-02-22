﻿let RoleId 

$(document).ready(function () {
    
    var currentCategoryId = null;
    var currentCompanyName = null;
    var currentOriginal = null;
    //var currentPage = 1;
    var pageSize = 9;
    function filterProducts(categoryId, companyName, original, currentPage) {
        if (categoryId !== undefined) {
            currentCategoryId = categoryId;
        }
        if (companyName !== undefined) {
            currentCompanyName = companyName;
        }
        if (original !== undefined) {
            currentOriginal = original;
        }


        var sortCriteria = $('.sort-option.active').data('sort');
        var numOfStar = $('.form-check-input:checked').siblings('label').find('.fa-star').length || null;
        var searchFrom = $('#searchFrom').val();
        var searchTo = $('#searchTo').val();

       


        $("#searchFrom").val(searchFrom);
        $("#searchTo").val(searchTo);
        $('#current-sort').text(sortCriteria);

        $.ajax({
            url: '/api/v1/products/filter',
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            data: {
                categoryId: currentCategoryId, companyName: currentCompanyName, original: currentOriginal, startPrice: searchFrom, endPrice: searchTo, numOfStars: numOfStar, sortCriteria: sortCriteria,
                page: currentPage,
                pageSize: pageSize
            },
            success: function (data) {
                
                var productRow = $('#product-list');
                productRow.empty();

                // Check if data is a JSON string, if so parse it
                if (typeof data === 'string') {
                    data = JSON.parse(data);
                }


                const productsArray = JSON.parse(data.products);
                const filter = JSON.parse(data.filteredProductsCount);
                const total = JSON.parse(data.totalProductsCount);
                console.log('AAAAA')
                productsArray.forEach(function (product) {
                    
                    var fullStars = Math.floor(product.Statistic.AverageStar);
                    var hasHalfStar = (product.Statistic.AverageStar - fullStars) >= 0.5;
                    var emptyStars = 5 - fullStars - (hasHalfStar ? 1 : 0);

                    var starsHtml = '';
                    for (var i = 0; i < fullStars; i++) {
                        starsHtml += '<span class="fa fa-star" style="text-shadow: 0 0 1px black"></span>';
                    }
                    if (hasHalfStar) {
                        starsHtml += '<span class="fa fa-star-half-alt" style="text-shadow: 0 0 1px black"></span>';
                    }
                    for (var i = 0; i < emptyStars; i++) {
                        starsHtml += '<span class="fa fa-star" style="color: white; text-shadow: 0 0 1px black"></span>';
                    }
                    console.log('aaaaa', RoleId)
                    var productHtml = `
                                        <div class="col-sm-6 col-md-4">
                                            <!-- Start Product Item -->
                                            <div class="product-item">
                                                <div class="product-thumb">
                                                    <img src="${product.Image}" alt="Image">
                                                    <div class="product-action">
                                                    ${RoleId == 0 || RoleId == 3 || RoleId == undefined ? `<a href="?handler=AddToCart&productId=${product.Id}"><i class="ion-ios-cart"></i></a>` : ""}
                                                        <a href="/product_detail?productId=${product.Id}"><i class="ion-arrow-expand"></i></a>
                                                    </div>
                                                </div>
                                                <div class="product-info">
                                                    <div class="rating">
                                                        ${fullStars > 0 ?
                                                        `${starsHtml}<h4>${Math.round(product.Statistic.AverageStar * 10) / 10}</h4>` :
                                                        `<span class="fa fa-star" style="visibility: hidden;"></span><h4>No Review</h4>`
                    }
                                                    </div>
                                                    <h4 class="title"><a href="/product_detail?productId=${product.Id}">${product.Name}</a></h4>
                                                    <div class="prices">
                                                        <span class="price">${product.PurchasePrice} VND</span>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- End Product Item -->
                                        </div>`;
                    productRow.append(productHtml);

                });
                $('#filtered-products-count').text(filter);
                $('#total-products-count').text(total);
                updatePagination(total, pageSize, currentPage, filter);
            },
            error: function (xhr, status, error) {
                console.error('An error occurred:', error);
                alert('Failed to load products.');
            }
        });
    }

    // Function to update pagination controls
    function updatePagination(totalProducts, pageSize, currentPage, filter) {
        var totalPages = Math.ceil(totalProducts / pageSize);
        var paginationHtml = '';
        if (filter > 9) {
            for (var i = 1; i <= totalPages; i++) {
                paginationHtml += `<li><a class="page-number ${i === currentPage ? 'active' : ''}" data-page="${i}">${i}</a></li>`;
            }
            if (totalPages > 1 && currentPage < totalPages) {
                paginationHtml += `<li><a class="page-number next" data-page="${currentPage + 1}"><i class="fa fa-angle-right"></i></a></li>`;
            }
        } else {
            $('.page-numbers').empty();
        }
        
        
        $('.page-numbers').html(paginationHtml);
    }

    // Event handler for pagination click
    $(document).on('click', '.page-number', function () {
        var page = $(this).data('page');
        filterProducts(currentCategoryId, currentCompanyName, currentOriginal, page);
    });

    //Sort
    $(document).on('click', '.sort-option', function () {
        $('.sort-option').removeClass('active');
        $(this).addClass('active');
        $('#current-sort').text($(this).data('sort'));
        filterProducts(currentCategoryId, currentCompanyName, currentOriginal, 1);
    });


    //Star
    var lastSelected = null;

    $('.form-check-input').on('click', function () {

        if (lastSelected && lastSelected[0] === this) {
            $(this).prop('checked', false);
            lastSelected = null;
        } else {
            lastSelected = $(this);
        }

        filterProducts(currentCategoryId, currentCompanyName, currentOriginal, 1);
    });




    //Search Price
    $(".btn-src").click(function (e) {
        e.preventDefault();
        filterProducts(currentCategoryId, currentCompanyName, currentOriginal, 1);
    });

    //Category
    $('.widget-categories ul li .category-link').on('click', function () {

        var categoryId = $(this).data('category-id');
        if ($(this).hasClass('selected-category')) {
            $(this).removeClass('selected-category');
            categoryId = null;
        } else {
            $('.widget-categories ul li .category-link').removeClass('selected-category');
            $(this).addClass('selected-category');
        }
        filterProducts(categoryId, currentCompanyName, currentOriginal, 1);
    });

    //Company
    $('.widget-categories ul li .company-link').on('click', function () {

        var companyname = $(this).data('company-name');
        if ($(this).hasClass('selected-company')) {
            $(this).removeClass('selected-company');
            companyname = null;
        } else {
            $('.widget-categories ul li .company-link').removeClass('selected-company');
            $(this).addClass('selected-company');
        }
        filterProducts(currentCompanyName, companyname, currentOriginal, 1);
    });


    //Original
    $('.widget-categories ul li .original-link').on('click', function () {

        var original = $(this).data('original-name');
        if ($(this).hasClass('selected-original')) {
            $(this).removeClass('selected-original');
            original = null;
        } else {
            $('.widget-categories ul li .original-link').removeClass('selected-original');
            $(this).addClass('selected-original');
        }
        filterProducts(currentCompanyName, currentCompanyName, original, 1);
    });

   

    filterProducts(currentCompanyName, currentCompanyName, currentOriginal, 1);

    
});